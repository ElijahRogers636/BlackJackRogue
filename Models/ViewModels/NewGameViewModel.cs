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

            // Initial Deck Shuffle
            GameDeck.ShuffleDeck();

            // Subscribe to collection changed events to update cardvaluesum
            PlayerCurrentCards.CollectionChanged += (s, e) => UpdatePlayerCardValueSum();
            DealerCurrentCards.CollectionChanged += (s, e) => UpdateDealerCardValueSum();
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

        // <---------------------------------------Commands---------------------------------------------->
        // Updates the player's health values
        [RelayCommand]
        private void UpdatePlayerHealth()
        {
            GamePlayer.CurrHealthPoints -= GamePlayer.CurrentBet;

            if (GamePlayer.CurrHealthPoints < 0)
            {
                GamePlayer.CurrHealthPoints = 0;
            }

            // Update ViewModel properties
            PlayerHealthPoints = GamePlayer.CurrHealthPoints;
            PlayerHealthBar = GamePlayer.HealthBar;

            // Update combined property text
            PlayerHealthBarText = $"{PlayerHealthPoints} / {GamePlayer.TotalHealthPoints}";
            CurrentBetText = $"CURRENT BET: {GamePlayer.CurrentBet}";
        }

        // Updates the dealer's health values
        [RelayCommand]
        private void UpdateDealerHealth()
        {
            GameDealer.CurrHealthPoints -= 50; // Example value

            if (GameDealer.CurrHealthPoints < 0)
            {
                GameDealer.CurrHealthPoints = 0;
            }

            // Update ViewModel properties
            DealerHealthPoints = GameDealer.CurrHealthPoints;
            DealerHealthBar = GameDealer.HealthBar;

            // Update combined property text
            DealerHealthBarText = $"{DealerHealthPoints} / {GameDealer.TotalHealthPoints}";
        }

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
                CurrentBetText = "Minimum bet is 100.";
                return;
            }

            // Logic to place a bet
            GamePlayer.CurrentBet = PlayerCurrentBet;
            CurrentBetText = $"CURRENT BET: {GamePlayer.CurrentBet}";

            // Deal initial cards
            DealInitialCards();
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
        }

        // Stay Command
        [RelayCommand]
        private void Stay()
        {
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
        }

        // <---------------------------------------Methods---------------------------------------------->

            // Deal Initial Cards Method
        private void DealInitialCards()
        {
            if (GameDeck.ShuffledCardDeck.Count >= 4)
            {
                // Deal two cards to the dealer and player
                DealerCurrentCards.Add(GameDeck.ShuffledCardDeck.Pop());
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

        // Update Player Card Value Sum
        private void UpdatePlayerCardValueSum()
        {
            PlayerCurrentCardValueSum = PlayerCurrentCards.Sum(card => card.CardValue);
        }

        // Update Dealer Card Value Sum
        private void UpdateDealerCardValueSum()
        {
            DealerCurrentCardValueSum = DealerCurrentCards.Sum(card => card.CardValue);
        }
    }
}