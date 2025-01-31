using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace BlackJackRogue.Models.ViewModels
{
    public partial class DealerViewModel : ObservableObject
    {
        [ObservableProperty]
        private Dealer dealer;
        public DealerViewModel()
        {
            dealer = new Dealer()
            {
                Name = "FIRST DEALER",
                CurrHealthPoints = 1000,
                TotalHealthPoints = 1000,
                CurrentCards = new ObservableCollection<Card>(),
                CardValueSum = 0
            };
            DealerName = dealer.Name;
            DealerCurrentCardValueSum = dealer.CardValueSum;
            DealerCurrHealthPoints = dealer.CurrHealthPoints;
            DealerTotalHealthPoints = dealer.TotalHealthPoints;
            DealerCurrentCards = dealer.CurrentCards;
            DealerHealthBar = dealer.HealthBar;
            DealerHealthBarText = dealer.HealthBarText;
            BackOfCard = dealer.BackOfCard;
            HiddenCard = dealer.HiddenCard;
        }

        // Dealer props that need instantiation
        [ObservableProperty]
        private string dealerName;

        [ObservableProperty]
        private int dealerCurrHealthPoints;

        [ObservableProperty]
        private int dealerTotalHealthPoints;

        [ObservableProperty]
        private ObservableCollection<Card> dealerCurrentCards;

        [ObservableProperty]
        private int dealerCurrentCardValueSum;

        //Dealer props that will instatiate based on above props

        [ObservableProperty]
        private double dealerHealthBar;

        [ObservableProperty]
        private string dealerHealthBarText;

        [ObservableProperty]
        private Card backOfCard;

        [ObservableProperty]
        private Card hiddenCard;

        // <====================================Methods for Dealer====================================>

        // Update Dealer Card Value Sum, modify for Aces
        public void UpdateDealerCardValueSum()
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

        //Update combinded properties
        public void UpdateDealerProperties()
        {
            DealerHealthBar = (double)DealerCurrHealthPoints / DealerTotalHealthPoints;
            DealerHealthBarText = $"{DealerCurrHealthPoints} / {DealerTotalHealthPoints}";
        }
    }
}
