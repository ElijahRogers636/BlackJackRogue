using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRogue.Models
{
    class Card
    {
        public string Suit { get; set; } = "";
        public string Face { get; set; } = "";
        public int CardValue { get; set; } = 0;  // Ace's default value will be 11, unless a Hand total > 10 (Change Value during gameplay if applicable)

        private string cardName = "";
        public string CardName
        {
            get
            {
                if (CardValue < 10)
                {
                    cardName = CardValue.ToString() + " of " + Suit;
                }
                else
                {
                    cardName = Face + " of " + Suit;
                }
                return cardName;
            }
        }

    }
}
