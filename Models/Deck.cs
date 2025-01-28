using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRogue.Models
{
    public class Deck
    {
        public Card[] CardDeck { get; set; }
        public Stack<Card> ShuffledCardDeck { get; set; }

        public Deck()
        {
            CardDeck = new Card[]{
                new Card() { Suit = "Clubs", Face = "2", CardValue = 2, Icon="twoofclubs.png" },
                new Card() { Suit = "Spades", Face = "2", CardValue = 2, Icon="twoofspades.png" },
                new Card() { Suit = "Hearts", Face = "2", CardValue = 2, Icon="twoofhearts.png" },
                new Card() { Suit = "Diamonds", Face = "2", CardValue = 2, Icon = "twoofdiamonds.png" },
                new Card() { Suit = "Clubs", Face = "3", CardValue = 3, Icon="threeofclubs.png" },
                new Card() { Suit = "Spades", Face = "3", CardValue = 3, Icon = "threeofspades.png" },
                new Card() { Suit = "Hearts", Face = "3", CardValue = 3, Icon = "threeofhearts.png" },
                new Card() { Suit = "Diamonds", Face = "3", CardValue = 3, Icon = "threeofdiamonds.png" },
                new Card() { Suit = "Clubs", Face = "4", CardValue = 4, Icon = "fourofclubs.png" },
                new Card() { Suit = "Spades", Face = "4", CardValue = 4, Icon = "fourofspades.png" },
                new Card() { Suit = "Hearts", Face = "4", CardValue = 4, Icon = "fourofhearts.png" },
                new Card() { Suit = "Diamonds", Face = "4", CardValue = 4, Icon = "fourofdiamonds.png" },
                new Card() { Suit = "Clubs", Face = "5", CardValue = 5, Icon = "fiveofclubs.png" },
                new Card() { Suit = "Spades", Face = "5", CardValue = 5, Icon = "fiveofspades.png" },
                new Card() { Suit = "Hearts", Face = "5", CardValue = 5, Icon = "fiveofhearts.png" },
                new Card() { Suit = "Diamonds", Face = "5", CardValue = 5, Icon = "fiveofdiamonds.png" },
                new Card() { Suit = "Clubs", Face = "6", CardValue = 6, Icon = "sixofclubs.png" },
                new Card() { Suit = "Spades", Face = "6", CardValue = 6, Icon = "sixofspades.png" },
                new Card() { Suit = "Hearts", Face = "6", CardValue = 6, Icon = "sixofhearts.png" },
                new Card() { Suit = "Diamonds", Face = "6", CardValue = 6, Icon = "sixofdiamonds.png" },
                new Card() { Suit = "Clubs", Face = "7", CardValue = 7, Icon = "sevenofclubs.png" },
                new Card() { Suit = "Spades", Face = "7", CardValue = 7, Icon = "sevenofspades.png" },
                new Card() { Suit = "Hearts", Face = "7", CardValue = 7, Icon = "sevenofhearts.png" },
                new Card() { Suit = "Diamonds", Face = "7", CardValue = 7, Icon = "sevenofdiamonds.png" },
                new Card() { Suit = "Clubs", Face = "8", CardValue = 8, Icon = "eightofclubs.png" },
                new Card() { Suit = "Spades", Face = "8", CardValue = 8, Icon="eightofspades.png" },
                new Card() { Suit = "Hearts", Face = "8", CardValue = 8, Icon = "eightofhearts.png" },
                new Card() { Suit = "Diamonds", Face = "8", CardValue = 8, Icon = "eightofdiamonds.png" },
                new Card() { Suit = "Clubs", Face = "9", CardValue = 9, Icon = "nineofclubs.png" },
                new Card() { Suit = "Spades", Face = "9", CardValue = 9, Icon = "nineofspades.png" },
                new Card() { Suit = "Hearts", Face = "9", CardValue = 9, Icon = "nineofhearts.png" },
                new Card() { Suit = "Diamonds", Face = "9", CardValue = 9, Icon = "nineofdiamonds.png" },
                new Card() { Suit = "Clubs", Face = "10", CardValue = 10, Icon = "tenofclubs.png" },
                new Card() { Suit = "Spades", Face = "10", CardValue = 10, Icon = "tenofspades.png" },
                new Card() { Suit = "Hearts", Face = "10", CardValue = 10, Icon = "tenofhearts.png" },
                new Card() { Suit = "Diamonds", Face = "10", CardValue = 10, Icon = "tenofdiamonds.png" },
                new Card() { Suit = "Clubs", Face = "Jack", CardValue = 10, Icon = "jackofclubs.png" },
                new Card() { Suit = "Spades", Face = "Jack", CardValue = 10, Icon = "jackofspades.png" },
                new Card() { Suit = "Hearts", Face = "Jack", CardValue = 10, Icon = "jackofhearts.png" },
                new Card() { Suit = "Diamonds", Face = "Jack", CardValue = 10, Icon = "jackofdiamonds.png" },
                new Card() { Suit = "Clubs", Face = "Queen", CardValue = 10, Icon = "queenofclubs.png" },
                new Card() { Suit = "Spades", Face = "Queen", CardValue = 10, Icon = "queenofspades.png" },
                new Card() { Suit = "Hearts", Face = "Queen", CardValue = 10, Icon = "queenofhearts.png" },
                new Card() { Suit = "Diamonds", Face = "Queen", CardValue = 10, Icon = "queenofdiamonds.png" },
                new Card() { Suit = "Clubs", Face = "King", CardValue = 10, Icon = "kingofclubs.png" },
                new Card() { Suit = "Spades", Face = "King", CardValue = 10, Icon = "kingofspades.png" },
                new Card() { Suit = "Hearts", Face = "King", CardValue = 10, Icon = "kingofhearts.png" },
                new Card() { Suit = "Diamonds", Face = "King", CardValue = 10, Icon = "kingofdiamonds.png" },
                new Card() { Suit = "Clubs", Face = "Ace", CardValue = 11, Icon="aceofclubs.png" },
                new Card() { Suit = "Spades", Face = "Ace", CardValue = 11, Icon="aceofspades.png" },
                new Card() { Suit = "Hearts", Face = "Ace", CardValue = 11, Icon="aceofhearts.png" },
                new Card() { Suit = "Diamonds", Face = "Ace", CardValue = 11, Icon="aceofdiamonds.png" }
            };

            ShuffledCardDeck = new Stack<Card>();
        }

        public void ShuffleDeck()
        {
            Random rng = new Random();
            Card[] deck = (Card[])CardDeck.Clone();
            int n = deck.Length;

            while (n > 1)
            {
                int k = rng.Next(n--);
                (deck[k], deck[n]) = (deck[n], deck[k]);
            }

            ShuffledCardDeck = new Stack<Card>(deck);
        }
    }
}
