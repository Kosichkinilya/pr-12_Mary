namespace pr_12_Mary
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LockScreenPage());
        }
    }
}