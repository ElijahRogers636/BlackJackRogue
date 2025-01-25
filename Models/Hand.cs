using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRogue.Models
{
    public class Hand
    {
        public int CardValueSum { get; set; }
        public List<Card> CurrentCards { get; set; } = new List<Card>();
        public int CurrHealthPoints { get; set; }
        public int TotalHealthPoints { get; set; }
        public double HealthBar { get; set; } = 1.0;
    }
}
