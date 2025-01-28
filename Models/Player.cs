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
        public ObservableCollection<Perks> Perks = new ObservableCollection<Perks>();
        
        public int CurrentBet { get; set; }
    }
}
