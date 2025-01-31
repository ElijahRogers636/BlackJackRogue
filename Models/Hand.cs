using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRogue.Models
{
    public class Hand
    {
        public int CardValueSum { get; set; }
        public ObservableCollection<Card> CurrentCards { get; set; }
        public int CurrHealthPoints { get; set; }
        public int TotalHealthPoints { get; set; }
        public double HealthBar => (double)CurrHealthPoints / TotalHealthPoints;
        public string HealthBarText => $"{CurrHealthPoints} / {TotalHealthPoints}";
    }
}
