using BlackJackRogue.Models;

namespace BlackJackRogue.Views;

public partial class HowToPlayPage : ContentPage
{
	public HowToPlayPage()
	{
		InitializeComponent();
        BindingContext = new HowtoPlay();
    }
}