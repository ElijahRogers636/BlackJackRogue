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
        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            // Set the minimum window size
            const int minWidth = 1200;
            const int minHeight = 850;

            window.MinimumWidth = minWidth;
            window.MinimumHeight = minHeight;

            return window;
        }
    }
}
