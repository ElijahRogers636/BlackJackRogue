using BlackJackRogue.Models.ViewModels;

namespace BlackJackRogue.Views
{
    public partial class NewGamePage : ContentPage
    {
        public NewGamePage()
        {
            InitializeComponent();
            BindingContext = new NewGameViewModel();
        }
        
    }
}

