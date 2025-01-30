

namespace BlackJackRogue.Models
{
    class HowtoPlay
    {
        public string GamePlayLoop { get; } = " * Welcome to BlackJack Rogue! You will start the game by betting." +
            "\n * Betting is based off your remaining health pool" +
            "\n * The goal of the game is to get as close to 21 as possible without going over. " +
            "\n * You will be dealt two cards and the dealer will be dealt two cards. " +
            "\n * You will be able to see one of the dealer's cards." +
            "\n * You will then have the option to hit or stay. If you hit, you will be dealt another card. " +
            "\n * During your turn, you may choose to use perks, however, you can only use a perk once." +
            "\n * You will have three perks at the start of the game" +
            "\n * If you stay, you will keep your current hand. " +
            "\n * If you go over 21, you bust and the dealer wins. " +
            "\n * If you get 21, you win, unless the dealer also has 21. " +
            "\n * If you and the dealer have the same hand, it is a tie. " +
            "\n * If you win, you will receive 1.5x your bet. " +
            "\n * If you lose, you will lose your bet. " +
            "\n * If you tie, you will get your bet back. " +
            "\n * You will then start a new round and continue until either you or the dealer has no more health." +
            "\n * Good luck!\n";


        public string Betting { get; } = " * Betting in BlackJackRogue is based on health values." +
            "\n * You will start with 1000 health and the Dealer will start with a set amount of health. " +
            "\n * You will be able to bet between 100 and your current remaining health amount. " +
            "\n * If you win your bet you will gain 1.5x your bet, if you lose you will lose your full bet in health" +
            "\n * This will be the same for the dealer based on your bet each round" +
            "\n * Betting continues until either the dealer or player runs out of health.\n";

        public string PlayerTurn { get; } = " * During your turn you will have the option to hit or stay. " +
            "\n * If you hit, you will be dealt another card. " +
            "\n * If you stay, you will keep your current hand. " +
            "\n * You will also have the option to use perks during your turn. " +
            "\n * You can only use a perk once per game. " +
            "\n * You will start with three perks at the beginning of the game. " +
            "\n * Perks can be used to help you win the game. \n";

        public string DealerTurn { get; } = " * During the dealer's turn, the dealer will hit until they reach 17 or higher. \n";

        public string ReDraw { get; } = " * If you are unhappy with your last card draw, you can choose to redraw your that card. " +
            "\n * You can only redraw once per game. " +
            "\n * If you choose to redraw, you will lose your last card and be dealt a new card. " +
            "\n * You will not be able to redraw after you have chosen to stay. \n";

        public string Add100PlayerHp { get; } = " * This perk will add 100 health to your current health pool. " +
            "\n * You can only use this perk once per game. \n";

        public string Deal100DMGDealer { get; } = " * This perk will deal 100 damage to the dealer's health pool. " +
            "\n * You can only use this perk once per game. \n";
    }
}
