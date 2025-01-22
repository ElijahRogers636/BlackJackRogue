using BlackJackRogue.Views;

namespace BlackJackRogue
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
