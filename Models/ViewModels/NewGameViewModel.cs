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
        private Deck gameDeck;

        [ObservableProperty]
        private DealerViewModel gameDealer;

        [ObservableProperty]
        private PlayerViewModel gamePlayer;

        [ObservableProperty]
        private GameButtonStateViewModel gameButtonState;

        public NewGameViewModel()
        {
            gameDeck = new Deck();
            gameDealer = new DealerViewModel();
            gamePlayer = new PlayerViewModel(); 
            gameButtonState = new GameButtonStateViewModel();

            // Initial Deck Shuffle
            GameDeck.ShuffleDeck();

            // Subscribe to collection changed events to update cardvaluesum
            GamePlayer.PlayerCurrentCards.CollectionChanged += (s, e) => GamePlayer.UpdatePlayerCardValueSum();
            GameDealer.DealerCurrentCards.CollectionChanged += (s, e) => GameDealer.UpdateDealerCardValueSum();

        }



        // Place Bet Command
        [RelayCommand]
        private void PlaceBet()
        {
            // Validate the bet amount
            if (GamePlayer.PlayerCurrentBet <= 0)
            {
                GamePlayer.CurrentBetText = "Bet must be greater than 0.";
                return;
            }

            if (GamePlayer.PlayerCurrentBet > GamePlayer.PlayerCurrHealthPoints)
            {
                GamePlayer.CurrentBetText = "Cannot exceed current HP.";
                return;
            }

            if (GamePlayer.PlayerCurrentBet < 100)
            {
                if (GamePlayer.PlayerCurrHealthPoints > 100)
                {
                    GamePlayer.CurrentBetText = "Minimum bet is 100.";
                    return;
                }
                else if (GamePlayer.PlayerCurrentBet != GamePlayer.PlayerCurrHealthPoints)
                {
                    GamePlayer.CurrentBetText = "Must enter remaining Health";
                    return;
                }

            }

            // Logic to place a bet
            GamePlayer.CurrentBetText = $"CURRENT BET: {GamePlayer.PlayerCurrentBet}";

            // Deal initial cards
            DealInitialCards();

            // Disable Place Bet button and enable Hit and Stay buttons
            GameButtonState.UpdatePlaceBetButtonPressStates();

        }

        // Hit Command
        [RelayCommand]
        private void Hit()
        {
            // Draw a card from the deck
            if (GameDeck.ShuffledCardDeck.Count > 0)
            {
                GamePlayer.PlayerCurrentCards.Add(GameDeck.ShuffledCardDeck.Pop());
            }
            else
            {
                GamePlayer.CurrentBetText = "Not enough cards to draw.";
            }
            //Check if player busts
            PlayerBustCheck();
        }

        // Stay Command
        [RelayCommand]
        private void Stay()
        {
            // Updates color and useable state of buttons after player stays
            GameButtonState.UpdateStayButtonPressButtonStates();

            GameDealer.DealerCurrentCards.RemoveAt(0); // Remove the back card
            GameDealer.DealerCurrentCards.Add(GameDealer.HiddenCard); // Add the hidden card

            // Dealer draws cards until they have a value of 17 or higher
            while (GameDealer.DealerCurrentCardValueSum < 17)
            {
                if (GameDeck.ShuffledCardDeck.Count > 0)
                {
                    GameDealer.DealerCurrentCards.Add(GameDeck.ShuffledCardDeck.Pop());
                }
                else
                {
                    GamePlayer.CurrentBetText = "Not enough cards to draw.";
                    break;
                }
            }
            //Check if dealer busts
            DealerBustCheck();
        }

        // Reset Game Command
        [RelayCommand]
        private void ResetGame()
        {
            // Clear the player's and dealer's hands
            GamePlayer.PlayerCurrentCards.Clear();
            GameDealer.DealerCurrentCards.Clear();

            // Reset Current bet
            GamePlayer.PlayerCurrentBet = 0;

            // Shuffle the deck
            GameDeck.ShuffleDeck();

            // Enable Place Bet button and disable Reset Game buttons
            GameButtonState.UpdateResetButtonPressStates();
        }

        // Perk Commands
        [RelayCommand]
        private void PerkOne()
        {
            Perks.RedrawCard(GamePlayer, GameDeck);
            PlayerBustCheck();
            GameButtonState.UpdatePerkOneButtonPressStates();

        }
        [RelayCommand]
        private void PerkTwo()
        {
            Perks.RemoveDealerHealth(GameDealer);
            DealerBustCheck();
            GameButtonState.UpdatePerkTwoButtonPressStates();
            CheckGameResult();

        }
        [RelayCommand]
        private void PerkThree()
        {
            Perks.AddPlayerHealth(GamePlayer);
            GameButtonState.UpdatePerkThreeButtonPressStates();
        }

        // <---------------------------------------Methods---------------------------------------------->

        // Deal Initial Cards Method
        private void DealInitialCards()
        {
            if (GameDeck.ShuffledCardDeck.Count >= 4)
            {
                // Deal two cards to the dealer and player
                GameDealer.DealerCurrentCards.Add(GameDealer.BackOfCard);
                // Store hidden card for later use
                GameDealer.HiddenCard = GameDeck.ShuffledCardDeck.Pop();
                GamePlayer.PlayerCurrentCards.Add(GameDeck.ShuffledCardDeck.Pop());
                GameDealer.DealerCurrentCards.Add(GameDeck.ShuffledCardDeck.Pop());
                GamePlayer.PlayerCurrentCards.Add(GameDeck.ShuffledCardDeck.Pop());

            }
            else
            {
                // Handle the case where there are not enough cards to deal
                GamePlayer.CurrentBetText = "Not enough cards to deal.";
            }

        }

        // Updates dealer and player health values incase of a dealer bust
        private void DealerBustUpdate()
        {
            GamePlayer.CurrentBetText = "Dealer Bust! Player Wins!";
            // Player wins: increase health by 1.5x of bet up to total health value
            GamePlayer.PlayerCurrHealthPoints = Math.Min(GamePlayer.PlayerTotalHealthPoints, GamePlayer.PlayerCurrHealthPoints + (int)(GamePlayer.PlayerCurrentBet * 1.5));
            // Reduce dealer health by 1x bet value
            GameDealer.DealerCurrHealthPoints -= GamePlayer.PlayerCurrentBet;
            GamePlayer.UpdatePlayerProperties();
            GameDealer.UpdateDealerProperties();
            // Update ViewModel properties
            CheckGameResult();

        }

        // Updates dealer and player health values incase of a player bust
        private void PlayerBustUpdate()
        {
            GamePlayer.CurrentBetText = "Player Bust! Dealer Wins!";
            // Player wins: increase health by 1.5x of bet up to total health value
            GameDealer.DealerCurrHealthPoints = Math.Min(GameDealer.DealerTotalHealthPoints, GameDealer.DealerCurrHealthPoints + (int)(GamePlayer.PlayerCurrentBet * 1.5));
            // Reduce dealer health by 1x bet value
            GamePlayer.PlayerCurrHealthPoints -= GamePlayer.PlayerCurrentBet;
            GamePlayer.UpdatePlayerProperties();
            GameDealer.UpdateDealerProperties();
            // Update ViewModel properties
            CheckGameResult();

        }

        // Checks for dealer and palyer busts over 21
        private void DealerBustCheck()
        {
            // Check if dealer busts
            if (GameDealer.DealerCurrentCardValueSum > 21)
            {
                DealerBustUpdate();
            }
            else if (GameDealer.DealerCurrentCardValueSum > 16 && GameDealer.DealerCurrentCardValueSum < 22)
            {
                DecideEndOutcome();
            }
            
        }

        private void PlayerBustCheck()
        {
            // Check if player busts
            if (GamePlayer.PlayerCurrentCardValueSum > 21)
            {
                // Updates color and useable state of buttons as if the player stayed
                GameButtonState.UpdateStayButtonPressButtonStates();
                PlayerBustUpdate();
            }
        }

        // Decides the outcome of match and call bust methods to adjust health values
        private void DecideEndOutcome()
        {
            if(GamePlayer.PlayerCurrentCardValueSum > GameDealer.DealerCurrentCardValueSum)
            {
                //player wins: inccrease health by 1.5x of bet up to total health value
                //reduce dealer health by 1x bet value
                // Message: Round Won
                DealerBustUpdate();
                GamePlayer.CurrentBetText = "Round Won!";

            }
            else if(GamePlayer.PlayerCurrentCardValueSum < GameDealer.DealerCurrentCardValueSum)
            {
                //dealer wins: inccrease health by 1.5x of bet up to total health value
                //reduce player health by 1x bet value
                // Message: Round Lost
                PlayerBustUpdate();
                GamePlayer.CurrentBetText = "Round Lost!";
            }
            else
            {
                GamePlayer.CurrentBetText = "Round Tie!";
            }
            GamePlayer.UpdatePlayerProperties();
            GameDealer.UpdateDealerProperties();
            CheckGameResult();
        }

        // Check Game Result message
        private async void CheckGameResult()
        {
            if (GamePlayer.PlayerCurrHealthPoints <= 0)
            {
                await Shell.Current.DisplayAlert("Game Over", "You have lost the game!", "OK");
                await Shell.Current.GoToAsync("//MainPage");
            }
            else if (GameDealer.DealerCurrHealthPoints <= 0)
            {
                await Shell.Current.DisplayAlert("Congratulations", "You have won the game!", "OK");
                await Shell.Current.GoToAsync("//MainPage");
            }
        }
    }
}