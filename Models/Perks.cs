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
        public static void RedrawCard(PlayerViewModel player, Deck deck)
        {
            player.PlayerCurrentCards.RemoveAt(player.PlayerCurrentCards.Count - 1);
            player.PlayerCurrentCards.Add(deck.ShuffledCardDeck.Pop());
            player.UpdatePlayerProperties();
        }

        // Decreases dealer health by 100
        public static void RemoveDealerHealth(DealerViewModel dealer)
        {
            if(dealer.DealerCurrHealthPoints >= 100)
            {
                dealer.DealerCurrHealthPoints -= 100;
            }
            else
            {
                dealer.DealerCurrHealthPoints = 0;
            }
            dealer.UpdateDealerProperties();
        }

        // Increases player health by 100
        public static void AddPlayerHealth(PlayerViewModel player)
        {
            if (player.PlayerCurrHealthPoints <= 900)
            {
                player.PlayerCurrHealthPoints += 100;
            }
            else
            {
                player.PlayerCurrHealthPoints = 1000;
            }
            player.UpdatePlayerProperties();
        }

    }
}
