using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackRogue.Models
{
    class Deck
    {
        public Card[] CardDeck { get; set; } = [
            new Card() { Suit = "Clubs", Face = "2", CardValue = 2},
            new Card() { Suit = "Spades", Face = "2", CardValue = 2},
            new Card() { Suit = "Hearts", Face = "2", CardValue = 2},
            new Card() { Suit = "Diamonds", Face = "2", CardValue = 2},
            new Card() { Suit = "Clubs", Face = "3", CardValue = 3},
            new Card() { Suit = "Spades", Face = "3", CardValue = 3},
            new Card() { Suit = "Hearts", Face = "3", CardValue = 3},
            new Card() { Suit = "Diamonds", Face = "3", CardValue = 3},
            new Card() { Suit = "Clubs", Face = "4", CardValue = 4},
            new Card() { Suit = "Spades", Face = "4", CardValue = 4},
            new Card() { Suit = "Hearts", Face = "4", CardValue = 4},
            new Card() { Suit = "Diamonds", Face = "4", CardValue = 4},
            new Card() { Suit = "Clubs", Face = "5", CardValue = 5},
            new Card() { Suit = "Spades", Face = "5", CardValue = 5},
            new Card() { Suit = "Hearts", Face = "5", CardValue = 5},
            new Card() { Suit = "Diamonds", Face = "5", CardValue = 5},
            new Card() { Suit = "Clubs", Face = "6", CardValue = 6},
            new Card() { Suit = "Spades", Face = "6", CardValue = 6},
            new Card() { Suit = "Hearts", Face = "6", CardValue = 6},
            new Card() { Suit = "Diamonds", Face = "6", CardValue = 6},
            new Card() { Suit = "Clubs", Face = "7", CardValue = 7},
            new Card() { Suit = "Spades", Face = "7", CardValue = 7},
            new Card() { Suit = "Hearts", Face = "7", CardValue = 7},
            new Card() { Suit = "Diamonds", Face = "7", CardValue = 7},
            new Card() { Suit = "Clubs", Face = "8", CardValue = 8},
            new Card() { Suit = "Spades", Face = "8", CardValue = 8},
            new Card() { Suit = "Hearts", Face = "8", CardValue = 8},
            new Card() { Suit = "Diamonds", Face = "8", CardValue = 8},
            new Card() { Suit = "Clubs", Face = "9", CardValue = 9},
            new Card() { Suit = "Spades", Face = "9", CardValue = 9},
            new Card() { Suit = "Hearts", Face = "9", CardValue = 9},
            new Card() { Suit = "Diamonds", Face = "9", CardValue = 9},
            new Card() { Suit = "Clubs", Face = "10", CardValue = 10},
            new Card() { Suit = "Spades", Face = "10", CardValue = 10},
            new Card() { Suit = "Hearts", Face = "10", CardValue = 10},
            new Card() { Suit = "Diamonds", Face = "10", CardValue = 10},
            new Card() { Suit = "Clubs", Face = "Jack", CardValue = 10},
            new Card() { Suit = "Spades", Face = "Jack", CardValue = 10},
            new Card() { Suit = "Hearts", Face = "Jack", CardValue = 10},
            new Card() { Suit = "Diamonds", Face = "Jack", CardValue = 10},
            new Card() { Suit = "Clubs", Face = "Queen", CardValue = 10},
            new Card() { Suit = "Spades", Face = "Queen", CardValue = 10},
            new Card() { Suit = "Hearts", Face = "Queen", CardValue = 10},
            new Card() { Suit = "Diamonds", Face = "Queen", CardValue = 10},
            new Card() { Suit = "Clubs", Face = "King", CardValue = 10},
            new Card() { Suit = "Spades", Face = "King", CardValue = 10},
            new Card() { Suit = "Hearts", Face = "King", CardValue = 10},
            new Card() { Suit = "Diamonds", Face = "King", CardValue = 10},
            new Card() { Suit = "Clubs", Face = "Ace", CardValue = 11},
            new Card() { Suit = "Spades", Face = "Ace", CardValue = 11},
            new Card() { Suit = "Hearts", Face = "Ace", CardValue = 11},
            new Card() { Suit = "Diamonds", Face = "Ace", CardValue = 11}
        ];
        public Stack<Card> ShuffledCardDeck { get; set; }
    }
}
