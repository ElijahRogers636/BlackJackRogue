using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace BlackJackRogue.Models.ViewModels
{
    public partial class NewGameViewModel : ObservableObject
    {
        private readonly Perks _perks;
        private readonly Deck _deck;
        private readonly Dealer _dealer;
        private readonly Player _player;

        public NewGameViewModel()
        {
            _perks = new Perks();
            _deck = new Deck();
            _dealer = new Dealer { Name = "First", CardValueSum = 0, CurrHealthPoints = 1000, TotalHealthPoints = 1000};
            _player = new Player { CardValueSum = 0, CurrHealthPoints = 1000, TotalHealthPoints = 1000};

            Perk1 = _perks.Perk1;
            Perk2 = _perks.Perk2;
            Perk3 = _perks.Perk3;

            VmShuffledCardDeck = _deck.ShuffledCardDeck;

            DealerCardValueSum = _dealer.CardValueSum;
            DealerCurrentCards = _dealer.CurrentCards ?? new List<Card>();
            CurrDealerHealthPoints = _dealer.CurrHealthPoints;
            TotalDealerHealthPoints = _dealer.TotalHealthPoints;
            DealerName = _dealer.Name ?? string.Empty;
            DealerHealthBar = _dealer.HealthBar;

            PlayerCardValueSum = _player.CardValueSum;
            PlayerCurrentCards = _player.CurrentCards ?? new List<Card>();
            CurrPlayerHealthPoints = _player.CurrHealthPoints;
            TotalPlayerHealthPoints = _player.TotalHealthPoints;
            PlayerPerks = _player.Perks ?? new Perks[3];
            PlayerHealthBar = _player.HealthBar;
        }

        // Perk names
        [ObservableProperty]
        private string _perk1;
        [ObservableProperty]
        private string _perk2;
        [ObservableProperty]
        private string _perk3;

        // Shuffled deck of cards for game
        [ObservableProperty]
        private Stack<Card> _vmShuffledCardDeck;

        // Dealer properties
        [ObservableProperty]
        private int _dealerCardValueSum;
        [ObservableProperty]
        private List<Card> _dealerCurrentCards;
        [ObservableProperty]
        private int _currDealerHealthPoints;
        [ObservableProperty]
        private int _totalDealerHealthPoints;
        [ObservableProperty]
        private string _dealerName;
        [ObservableProperty]
        private double _dealerHealthBar;

        // Player properties
        [ObservableProperty]
        private int _playerCardValueSum;
        [ObservableProperty]
        private List<Card> _playerCurrentCards;
        [ObservableProperty]
        private int _currPlayerHealthPoints;
        [ObservableProperty]
        private int _totalPlayerHealthPoints;
        [ObservableProperty]
        private Perks[] _playerPerks;
        [ObservableProperty]
        private double _playerHealthBar;

        // Concat string for player and dealer health points
        public string PlayerHealthText => $"{CurrPlayerHealthPoints} / {TotalPlayerHealthPoints}";
        public string DealerHealthText => $"{CurrDealerHealthPoints} / {TotalDealerHealthPoints}";

        // Shuffles the deck of cards
        public void ShuffleDeck()
        {
            VmShuffledCardDeck.Clear();
            Random rand = new Random();
            rand.Shuffle(_deck.CardDeck);

            foreach (Card card in _deck.CardDeck)
            {
                VmShuffledCardDeck.Push(card);
            }
        }
    }
}
