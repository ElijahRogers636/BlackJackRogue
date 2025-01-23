
namespace BlackJackRogue.Views
{
    public partial class NewGamePage : ContentPage
    {
        public NewGamePage()
        {
            InitializeComponent();
        }

        private async void OnHowToPlayButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                // Navigate to the HowtoPlayPage
                await Navigation.PushAsync(new HowToPlayPage());
            }
        }

        private async void OnBacktoMainPageButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "UpdateNewGameButtonText", "Continue");

            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}

