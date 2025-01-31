using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace BlackJackRogue.Models.ViewModels
{
    public partial class GameButtonStateViewModel : ObservableObject
    {
        public GameButtonStateViewModel()
        {
            // Initial Button States
            IsHitEnabled = false;
            IsStayEnabled = false;
            IsPlaceBetEnabled = true;
            IsResetGameEnabled = false;
            IsPerkOneEnabled = false;
            IsPerkTwoEnabled = false;
            IsPerkThreeEnabled = false;

            // Initial Button Indicators
            IsPlayerActionEnabledIndicator = "#99900000";
            IsPlaceBetEnabledIndicator = "Black";
            IsResetGameEnabledIndicator = "#99900000";
            IsPerkOneEnabledIndicator = "#99900000";
            IsPerkTwoEnabledIndicator = "#99900000";
            IsPerkThreeEnabledIndicator = "#99900000";

            // Initial Perk Usage
            FirstPerkUsage = true;
            SecondPerkUsage = true;
            ThirdPerkUsage = true;

        }

        [ObservableProperty]
        private bool isHitEnabled;
        [ObservableProperty]
        private string isPlayerActionEnabledIndicator;

        [ObservableProperty]
        private bool isStayEnabled;

        [ObservableProperty]
        private bool isPlaceBetEnabled;
        [ObservableProperty]
        private string isPlaceBetEnabledIndicator;

        [ObservableProperty]
        private bool isResetGameEnabled;
        [ObservableProperty]
        private string isResetGameEnabledIndicator;

        [ObservableProperty]
        private bool isPerkOneEnabled;
        [ObservableProperty]
        private string isPerkOneEnabledIndicator;

        [ObservableProperty]
        private bool isPerkTwoEnabled;
        [ObservableProperty]
        private string isPerkTwoEnabledIndicator;

        [ObservableProperty]
        private bool isPerkThreeEnabled;
        [ObservableProperty]
        private string isPerkThreeEnabledIndicator;

        [ObservableProperty]
        private bool firstPerkUsage;
        [ObservableProperty]
        private bool secondPerkUsage;
        [ObservableProperty]
        private bool thirdPerkUsage;

        public void UpdateStayButtonPressButtonStates()
        {
            //Disable Hit and Stay buttons, disable perk buttons, enable Reset Game button
            IsHitEnabled = false;
            IsStayEnabled = false;
            IsPlayerActionEnabledIndicator = "#99900000";
            IsPerkOneEnabled = false;
            IsPerkOneEnabledIndicator = "#99900000";
            IsPerkTwoEnabled = false;
            IsPerkTwoEnabledIndicator = "#99900000";
            IsPerkThreeEnabled = false;
            IsPerkThreeEnabledIndicator = "#99900000";
            IsResetGameEnabled = true;
            IsResetGameEnabledIndicator = "Black";
        }

        public void UpdateResetButtonPressStates()
        {
            //Enable Place Bet button, disable Reset Game button
            IsPlaceBetEnabled = true;
            IsPlaceBetEnabledIndicator = "Black";
            IsResetGameEnabled = false;
            IsResetGameEnabledIndicator = "#99900000";
        }

        public void UpdatePlaceBetButtonPressStates()
        {
            //Enable Hit and Stay buttons, enable perk buttons, disable Place Bet button
            IsHitEnabled = true;
            IsStayEnabled = true;
            IsPlayerActionEnabledIndicator = "Black";
            IsPlaceBetEnabled = false;
            IsPlaceBetEnabledIndicator = "#99900000";
            // Perks Should only be used once per game
            if (FirstPerkUsage)
            {
                IsPerkOneEnabled = true;
                IsPerkOneEnabledIndicator = "Black";
            }
            if (SecondPerkUsage)
            {
                IsPerkTwoEnabled = true;
                IsPerkTwoEnabledIndicator = "Black";
            }
            if (ThirdPerkUsage)
            {
                IsPerkThreeEnabled = true;
                IsPerkThreeEnabledIndicator = "Black";
            }

        }
    }
}
