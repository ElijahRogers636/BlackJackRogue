using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRogue.Models
{
    public class Card
    {
        public string Suit { get; set; } = String.Empty;
        public string Face { get; set; } = String.Empty;
        public int CardValue { get; set; }  // Ace's default value will be 11, unless a Hand total > 10 (Change Value during gameplay if applicable)
        public string Icon { get; set; } = String.Empty;

        private string cardName = String.Empty;
        public string CardName
        {
            get
            {
                if (CardValue < 10)
                {
                    cardName = $"{CardValue} of {Suit}";
                }
                else
                {
                    cardName = $"{Face} of {Suit}";
                }
                return cardName;
            }
        }

    }
}
