using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRogue.Models
{
    class Hand
    {
        public int CardValueSum { get; set; }
        public List<Card> CurrentCards { get; set; } = new List<Card>();
    }
}
