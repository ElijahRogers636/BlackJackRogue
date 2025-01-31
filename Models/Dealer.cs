using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRogue.Models
{
    public class Dealer : Hand
    {
        public string Name { get; set; } = String.Empty;

        public Card BackOfCard { get; set; } = new() { Face = "Back", Suit = "Back", CardValue = 0, Icon = "backofcard.png" };

        public Card HiddenCard { get; set; } = new();
    }
}
