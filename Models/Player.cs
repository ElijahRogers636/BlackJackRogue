using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRogue.Models
{
    class Player : Hand
    {
        public int PlayerHealth { get; set; }

        public Perks[] perks = new Perks[3];
    }
}
