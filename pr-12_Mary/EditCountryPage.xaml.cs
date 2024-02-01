namespace pr_12_Mary;

public partial class EditCountryPage : ContentPage
{
    public EditCountryPage()
    {
        InitializeComponent();
    }

    public EditCountryViewModel ViewModel
    {
        get { return BindingContext as EditCountryViewModel; }
        set { BindingContext = value; }
    }
}