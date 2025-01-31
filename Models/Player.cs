using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRogue.Models
{
    public class Player : Hand
    {
        public ObservableCollection<Perks> Perks { get; set; } = new ObservableCollection<Perks>();
        public int CurrentBet { get; set; } = 0;
        public string CurrentBetText => $"CURRENT BET: {CurrentBet}";
    }
}
