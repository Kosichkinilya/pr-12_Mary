namespace pr_12_Mary;

public partial class LockScreenPage : ContentPage
{
    private const string CorrectPassword = "Mary";

    public LockScreenPage()
    {
        InitializeComponent();
    }

    private void OnUnlockClicked(object sender, EventArgs e)
    {
        string enteredPassword = passwordEntry.Text;

        if (enteredPassword == CorrectPassword)
        {
            // Пароль верный, переходите на основную страницу вашего приложения
            // Например, Navigation.PushAsync(new MainPage());
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
        else
        {
            DisplayAlert("Ошибка", "Введен неверный пароль. Повторите попытку.", "OK");
        }
    }
}