namespace BlackJackRogue.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<NewGamePage, string>(this, "UpdateNewGameButtonText", (sender, arg) =>
            {
                NewGameButton.Text = arg;
            });
        }

        //Button to navigate to new game page
        private async void OnNewGameButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {

                // Navigate to the NewGamePage
                await Navigation.PushAsync(new NewGamePage());
            }
        }

        //Button to navigate to how to play page
        private async void OnHowToPlayButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {

                // Navigate to the HowToPlayPage
                await Navigation.PushAsync(new HowToPlayPage());
            }
        }

        // Button to exit the application
        private void OnExitButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {

                // Exit the application
                Application.Current.Quit();
            }
        }
    }

}
