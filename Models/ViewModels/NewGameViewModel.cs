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
            gameDealer = new Dealer { Name = "First Dealer", CardValueSum = 0, CurrHealthPoints = 1000, TotalHealthPoints = 1000, CurrentCards = new ObservableCollection<Card>() };
            gamePlayer = new Player { CardValueSum = 0, CurrHealthPoints = 1000, TotalHealthPoints = 1000, CurrentBet = 0, CurrentCards = new ObservableCollection<Card>() };

            // Property Initalization
            PlayerHealthPoints = gamePlayer.CurrHealthPoints;
            DealerHealthPoints = gameDealer.CurrHealthPoints;
            PlayerHealthBar = gamePlayer.HealthBar;
            DealerHealthBar = gameDealer.HealthBar;
            PlayerCurrentCards = gamePlayer.CurrentCards;
            DealerCurrentCards = gameDealer.CurrentCards;
            PlayerHealthBarText = $"{PlayerHealthPoints} / {gamePlayer.TotalHealthPoints}";
            DealerHealthBarText = $"{DealerHealthPoints} / {gameDealer.TotalHealthPoints}";
            CurrentBetText = $"CURRENT BET: {gamePlayer.CurrentBet}";

            // Initial Deck Shuffle
            gameDeck.ShuffleDeck();
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

        // <---------------------------------------Dealer Properties---------------------------------------------->
        [ObservableProperty]
        private int dealerHealthPoints;

        [ObservableProperty]
        private double dealerHealthBar;

        [ObservableProperty]
        private string dealerHealthBarText;

        [ObservableProperty]
        private ObservableCollection<Card> dealerCurrentCards;

        // <---------------------------------------Commands---------------------------------------------->
        // Updates the player's health values
        [RelayCommand]
        private void UpdatePlayerHealth()
        {
            gamePlayer.CurrHealthPoints -= gamePlayer.CurrentBet;

            if (gamePlayer.CurrHealthPoints < 0)
            {
                gamePlayer.CurrHealthPoints = 0;
            }

            // Update ViewModel properties
            PlayerHealthPoints = gamePlayer.CurrHealthPoints;
            PlayerHealthBar = gamePlayer.HealthBar;

            // Update combined property text
            PlayerHealthBarText = $"{PlayerHealthPoints} / {gamePlayer.TotalHealthPoints}";
            CurrentBetText = $"CURRENT BET: {gamePlayer.CurrentBet}";
        }

        // Updates the dealer's health values
        [RelayCommand]
        private void UpdateDealerHealth()
        {
            gameDealer.CurrHealthPoints -= 50; // Example value

            if (gameDealer.CurrHealthPoints < 0)
            {
                gameDealer.CurrHealthPoints = 0;
            }

            // Update ViewModel properties
            DealerHealthPoints = gameDealer.CurrHealthPoints;
            DealerHealthBar = gameDealer.HealthBar;

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

            if (PlayerCurrentBet > gamePlayer.CurrHealthPoints)
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
            gamePlayer.CurrentBet = PlayerCurrentBet;
            CurrentBetText = $"CURRENT BET: {gamePlayer.CurrentBet}";

            // Deal initial cards
            DealInitialCards();
        }

        // Shuffle Deck Command
        [RelayCommand]
        private void ShuffleDeck()
        {
            gameDeck.ShuffleDeck();
        }

        // <---------------------------------------Methods---------------------------------------------->

        // Deal Initial Cards Method
        private void DealInitialCards()
        {
            if (gameDeck.ShuffledCardDeck.Count >= 4)
            {
                // Deal two cards to the dealer and player
                DealerCurrentCards.Add(gameDeck.ShuffledCardDeck.Pop());
                PlayerCurrentCards.Add(gameDeck.ShuffledCardDeck.Pop());
                DealerCurrentCards.Add(gameDeck.ShuffledCardDeck.Pop());
                PlayerCurrentCards.Add(gameDeck.ShuffledCardDeck.Pop());

            }
            else
            {
                // Handle the case where there are not enough cards to deal
                CurrentBetText = "Not enough cards to deal.";
            }

        }
    }
}