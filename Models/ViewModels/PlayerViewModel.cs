using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace BlackJackRogue.Models.ViewModels
{
    public partial class PlayerViewModel : ObservableObject
    {
        [ObservableProperty]
        private Player player;
        public PlayerViewModel()
        {
            player = new Player()
            { 
                CurrentBet = 0, 
                CardValueSum = 0, 
                CurrHealthPoints = 1000, 
                TotalHealthPoints = 1000, 
                CurrentCards = new ObservableCollection<Card>(), 
                Perks = new ObservableCollection<Perks>() 
            };
            PlayerCurrentCardValueSum = Player.CardValueSum;
            PlayerCurrHealthPoints = Player.CurrHealthPoints;
            PlayerTotalHealthPoints = Player.TotalHealthPoints;
            PlayerCurrentCards = Player.CurrentCards;
            PlayerPerks = Player.Perks;
            PlayerHealthBar = Player.HealthBar;
            PlayerHealthBarText = Player.HealthBarText;
            CurrentBetText = Player.CurrentBetText;
        }
        // Player props that need instantiation
        [ObservableProperty]
        private int playerCurrHealthPoints;

        [ObservableProperty]
        private int playerTotalHealthPoints;

        [ObservableProperty]
        private ObservableCollection<Perks> playerPerks;

        [ObservableProperty]
        private int playerCurrentBet;

        [ObservableProperty]
        private ObservableCollection<Card> playerCurrentCards;

        [ObservableProperty]
        private int playerCurrentCardValueSum;

        //Player props that will instatiate based on above props
        [ObservableProperty]
        private double playerHealthBar;

        [ObservableProperty]
        private string playerHealthBarText;

        [ObservableProperty]
        private string currentBetText;

        // Update Player Card Value Sum, modify for Aces
        public void UpdatePlayerCardValueSum()
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

        //Update combinded properties
        public void UpdatePlayerProperties()
        {
            PlayerHealthBar = (double)PlayerCurrHealthPoints / PlayerTotalHealthPoints;
            PlayerHealthBarText = $"{PlayerCurrHealthPoints} / {PlayerTotalHealthPoints}";
        }
    }
}
