using BlackJackRogue.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRogue.Models
{
    public class Perks
    {
        public string Name { get; set; } = "Perk";

        // Perk Methods

        //Redraws the last card drawn by the player
        public static void RedrawCard(Player player, Deck deck)
        {
            player.CurrentCards.RemoveAt(player.CurrentCards.Count - 1);
            player.CurrentCards.Add(deck.ShuffledCardDeck.Pop());
        }

        // Decreases dealer health by 100
        public static void RemoveDealerHealth(Dealer dealer)
        {
            if(dealer.CurrHealthPoints >= 100)
            {
                dealer.CurrHealthPoints -= 100;
            }
            else
            {
                dealer.CurrHealthPoints = 0;
            }
             
        }

        // Increases player health by 100
        public static void AddPlayerHealth(Player player)
        {
            if (player.CurrHealthPoints <= 900)
            {
                player.CurrHealthPoints += 100;
            }
            else
            {
                player.CurrHealthPoints = 1000;
            }
        }

    }
}
