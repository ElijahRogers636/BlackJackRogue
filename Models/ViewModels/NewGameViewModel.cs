using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace BlackJackRogue.Models.ViewModels
{
    public partial class NewGameViewModel : ObservableObject
    {
        // <---------------------------------------Models---------------------------------------------->
        [ObservableProperty]
        private Perks gamePerks;
        [ObservableProperty]
        private Deck gameDeck;
        [ObservableProperty]
        private Dealer gameDealer;
        [ObservableProperty]
        private Player gamePlayer;

        public NewGameViewModel()
        {
            gamePerks = new Perks();
            gameDeck = new Deck();
            gameDealer = new Dealer { Name = "FIRST DEALER", CardValueSum = 0, CurrHealthPoints = 1000, TotalHealthPoints = 1000, CurrentCards = new ObservableCollection<Card>() };
            gamePlayer = new Player { CardValueSum = 0, CurrHealthPoints = 1000, TotalHealthPoints = 1000, CurrentBet = 0, CurrentCards = new ObservableCollection<Card>() };

            // Property Initalization
            PlayerHealthPoints = gamePlayer.CurrHealthPoints;
            DealerHealthPoints = gameDealer.CurrHealthPoints;

            PlayerHealthBar = gamePlayer.HealthBar;
            DealerHealthBar = gameDealer.HealthBar;

            PlayerCurrentCards = gamePlayer.CurrentCards;
            DealerCurrentCards = gameDealer.CurrentCards;

            PlayerCurrentCardValueSum = gamePlayer.CardValueSum;
            DealerCurrentCardValueSum = gameDealer.CardValueSum;

            PlayerHealthBarText = $"{PlayerHealthPoints} / {gamePlayer.TotalHealthPoints}";
            DealerHealthBarText = $"{DealerHealthPoints} / {gameDealer.TotalHealthPoints}";

            CurrentBetText = $"CURRENT BET: {gamePlayer.CurrentBet}";
            HiddenCard = new();

            // Initial Deck Shuffle
            GameDeck.ShuffleDeck();

            // Subscribe to collection changed events to update cardvaluesum
            PlayerCurrentCards.CollectionChanged += (s, e) => UpdatePlayerCardValueSum();
            DealerCurrentCards.CollectionChanged += (s, e) => UpdateDealerCardValueSum();

            // Initial Button States
            IsHitEnabled = false;
            IsStayEnabled = false;
            IsPlaceBetEnabled = true;
            IsResetGameEnabled = false;
            IsPerkOneEnabled = false;
            IsPerkTwoEnabled = false;
            IsPerkThreeEnabled = false;
        }

        // <---------------------------------------Player Properties---------------------------------------------->

        [ObservableProperty]
        private int playerHealthPoints;

        [ObservableProperty]
        private double playerHealthBar;

        [ObservableProperty]
        private string playerHealthBarText;

        [ObservableProperty]
        private int playerCurrentBet;

        [ObservableProperty]
        private string currentBetText;

        [ObservableProperty]
        private ObservableCollection<Card> playerCurrentCards;

        [ObservableProperty]
        private int playerCurrentCardValueSum;

        // <---------------------------------------Dealer Properties---------------------------------------------->
        [ObservableProperty]
        private int dealerHealthPoints;

        [ObservableProperty]
        private double dealerHealthBar;

        [ObservableProperty]
        private string dealerHealthBarText;

        [ObservableProperty]
        private ObservableCollection<Card> dealerCurrentCards;

        [ObservableProperty]
        private int dealerCurrentCardValueSum;

        [ObservableProperty]
        private Card hiddenCard;

        // <---------------------------------------Button States---------------------------------------------->

        [ObservableProperty]
        private bool isHitEnabled;

        [ObservableProperty]
        private bool isStayEnabled;

        [ObservableProperty]
        private bool isPlaceBetEnabled;

        [ObservableProperty]
        private bool isResetGameEnabled;

        [ObservableProperty]
        private bool isPerkOneEnabled;

        [ObservableProperty]
        private bool isPerkTwoEnabled;

        [ObservableProperty]
        private bool isPerkThreeEnabled;

        // <---------------------------------------Commands---------------------------------------------->

        // Place Bet Command
        [RelayCommand]
        private void PlaceBet()
        {
            // Validate the bet amount
            if (PlayerCurrentBet <= 0)
            {
                CurrentBetText = "Bet must be greater than 0.";
                return;
            }

            if (PlayerCurrentBet > GamePlayer.CurrHealthPoints)
            {
                CurrentBetText = "Cannot exceed current HP.";
                return;
            }

            if (PlayerCurrentBet < 100)
            {
                if (GamePlayer.CurrHealthPoints > 100)
                {
                    CurrentBetText = "Minimum bet is 100.";
                    return;
                }
                else if (PlayerCurrentBet != GamePlayer.CurrHealthPoints)
                {
                    CurrentBetText = "Must enter remaining Health";
                    return;
                }
                    
            }

            // Logic to place a bet
            GamePlayer.CurrentBet = PlayerCurrentBet;
            CurrentBetText = $"CURRENT BET: {GamePlayer.CurrentBet}";

            // Deal initial cards
            DealInitialCards();

            //Enable Hit and Stay buttons, enable perk buttons, disable Place Bet button
            IsHitEnabled = true;
            IsStayEnabled = true;
            IsPerkOneEnabled = true;
            IsPerkTwoEnabled = true;
            IsPerkThreeEnabled = true;
            IsPlaceBetEnabled = false;
        }

        // Shuffle Deck Command
        [RelayCommand]
        private void ShuffleDeck()
        {
            GameDeck.ShuffleDeck();
        }

        // Hit Command
        [RelayCommand]
        private void Hit()
        {
            // Draw a card from the deck
            if (GameDeck.ShuffledCardDeck.Count > 0)
            {
                PlayerCurrentCards.Add(GameDeck.ShuffledCardDeck.Pop());
            }
            else
            {
                CurrentBetText = "Not enough cards to draw.";
            }

            // Check if player busts
            if (PlayerCurrentCardValueSum > 21)
            {
                //Disable Hit and Stay buttons, disable perk buttons, enable Reset Game button
                IsHitEnabled = false;
                IsStayEnabled = false;
                IsPerkOneEnabled = false;
                IsPerkTwoEnabled = false;
                IsPerkThreeEnabled = false;
                IsResetGameEnabled = true;
                PlayerBustUpdate();
            }
        }

        // Stay Command
        [RelayCommand]
        private void Stay()
        {
            //Disable Hit and Stay buttons, disable perk buttons, enable Reset Game button
            IsHitEnabled = false;
            IsStayEnabled = false;
            IsPerkOneEnabled = false;
            IsPerkTwoEnabled = false;
            IsPerkThreeEnabled = false;
            IsResetGameEnabled = true;

            DealerCurrentCards.RemoveAt(0); // Remove the back card
            DealerCurrentCards.Add(HiddenCard); // Add the hidden card

            // Dealer draws cards until they have a value of 17 or higher
            while (DealerCurrentCardValueSum < 17)
            {
                if (GameDeck.ShuffledCardDeck.Count > 0)
                {
                    DealerCurrentCards.Add(GameDeck.ShuffledCardDeck.Pop());
                }
                else
                {
                    CurrentBetText = "Not enough cards to draw.";
                    break;
                }
            }

            // Check if dealer busts
            if (DealerCurrentCardValueSum > 21)
            {
                DealerBustUpdate();
            }
            else
            {
                DecideEndOutcome();
            }
        }

        // Reset Game Command
        [RelayCommand]
        private void ResetGame()
        {
            // Clear the player's and dealer's hands
            PlayerCurrentCards.Clear();
            DealerCurrentCards.Clear();

            // Reset Current bet
            GamePlayer.CurrentBet = 0;
            CurrentBetText = $"CURRENT BET: {GamePlayer.CurrentBet}";

            // Shuffle the deck
            GameDeck.ShuffleDeck();

            // Enable Place Bet button and disable Reset Game buttons
            IsPlaceBetEnabled = true;
            IsResetGameEnabled = false;
        }

        // <---------------------------------------Methods---------------------------------------------->

        // Deal Initial Cards Method
        private void DealInitialCards()
        {
            if (GameDeck.ShuffledCardDeck.Count >= 4)
            {
                // Deal two cards to the dealer and player
                DealerCurrentCards.Add(new() {Face = "Back", Suit = "Back" , CardValue = 0, Icon = "backofcard.png"});
                // Store hidden card for later use
                HiddenCard = GameDeck.ShuffledCardDeck.Pop();
                PlayerCurrentCards.Add(GameDeck.ShuffledCardDeck.Pop());
                DealerCurrentCards.Add(GameDeck.ShuffledCardDeck.Pop());
                PlayerCurrentCards.Add(GameDeck.ShuffledCardDeck.Pop());

            }
            else
            {
                // Handle the case where there are not enough cards to deal
                CurrentBetText = "Not enough cards to deal.";
            }

        }

        // Update Card Value Sum, modify for Aces
        private void UpdatePlayerCardValueSum()
        {
            int tempSum = 0;
            int aceCount = 0;

            foreach (var card in PlayerCurrentCards)
            {
                if (card.CardValue == 11)
                {
                    aceCount++; // Count the number of Aces
                    tempSum += 11;
                }
                else
                {
                    tempSum += card.CardValue;
                }
            }

            // Adjust Aces if needed
            while (tempSum > 21 && aceCount > 0)
            {
                tempSum -= 10; // Convert an Ace (11) to (1)
                aceCount--;
            }

            PlayerCurrentCardValueSum = tempSum;
        }

        // Update Dealer Card Value Sum, modify for Aces
        private void UpdateDealerCardValueSum()
        {
            int tempSum = 0;
            int aceCount = 0;

            foreach (var card in DealerCurrentCards)
            {
                if (card.CardValue == 11)
                {
                    aceCount++; // Count the number of Aces
                    tempSum += 11;
                }
                else
                {
                    tempSum += card.CardValue;
                }
            }

            // Adjust Aces if needed
            while (tempSum > 21 && aceCount > 0)
            {
                tempSum -= 10; // Convert an Ace (11) to (1)
                aceCount--;
            }

            DealerCurrentCardValueSum = tempSum;
        }

        // Updates dealer and player health values incase of a dealer bust
        private void DealerBustUpdate()
        {
            CurrentBetText = "Dealer Bust! Player Wins!";
            // Player wins: increase health by 1.5x of bet up to total health value
            GamePlayer.CurrHealthPoints = Math.Min(GamePlayer.TotalHealthPoints, GamePlayer.CurrHealthPoints + (int)(GamePlayer.CurrentBet * 1.5));
            // Reduce dealer health by 1x bet value
            GameDealer.CurrHealthPoints -= GamePlayer.CurrentBet;

            // Update ViewModel properties
            PlayerHealthPoints = GamePlayer.CurrHealthPoints;
            DealerHealthPoints = GameDealer.CurrHealthPoints;
            PlayerHealthBar = GamePlayer.HealthBar;
            DealerHealthBar = GameDealer.HealthBar;
            PlayerHealthBarText = $"{PlayerHealthPoints} / {GamePlayer.TotalHealthPoints}";
            DealerHealthBarText = $"{DealerHealthPoints} / {GameDealer.TotalHealthPoints}";

        }

        // Updates dealer and player health values incase of a player bust
        private void PlayerBustUpdate()
        {
            CurrentBetText = "Player Bust! Player Wins!";
            // Player wins: increase health by 1.5x of bet up to total health value
            GameDealer.CurrHealthPoints = Math.Min(GameDealer.TotalHealthPoints, GameDealer.CurrHealthPoints + (int)(GamePlayer.CurrentBet * 1.5));
            // Reduce dealer health by 1x bet value
            GamePlayer.CurrHealthPoints -= GamePlayer.CurrentBet;

            // Update ViewModel properties
            PlayerHealthPoints = GamePlayer.CurrHealthPoints;
            DealerHealthPoints = GameDealer.CurrHealthPoints;
            PlayerHealthBar = GamePlayer.HealthBar;
            DealerHealthBar = GameDealer.HealthBar;
            PlayerHealthBarText = $"{PlayerHealthPoints} / {GamePlayer.TotalHealthPoints}";
            DealerHealthBarText = $"{DealerHealthPoints} / {GameDealer.TotalHealthPoints}";

        }
        // Decides the outcome of match and call bust methods to adjust health values
        private void DecideEndOutcome()
        {
            if(PlayerCurrentCardValueSum > DealerCurrentCardValueSum)
            {
                //player wins: inccrease health by 1.5x of bet up to total health value
                //reduce dealer health by 1x bet value
                // Message: Round Won
                DealerBustUpdate();
                CurrentBetText = "Round Won!";

            }
            else if(PlayerCurrentCardValueSum < DealerCurrentCardValueSum)
            {
                //dealer wins: inccrease health by 1.5x of bet up to total health value
                //reduce player health by 1x bet value
                // Message: Round Lost
                PlayerBustUpdate();
                CurrentBetText = "Round Lost!";
            }
            else
            {
                CurrentBetText = "Round Tie!";
            }
        }
    }
}