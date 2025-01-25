using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRogue.Models
{
    public class Player : Hand
    {
        public Perks[] Perks = new Perks[3];
    }
}
