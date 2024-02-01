using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace pr_12_Mary
{
    public class EditCountryViewModel : BindableObject
    {
        private Country _selectedCountry;
        private ObservableCollection<Country> _countries;
        public int index1 { get; set; }
        public Country SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Country> Countries
        {
            get { return _countries; }
            set
            {
                _countries = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveChangesCommand { get; }

        public EditCountryViewModel(ObservableCollection<Country> countries)
        {
            Countries = countries; // Передача коллекции в EditCountryViewModel
            SelectedCountry = new Country(); // Инициализация выбранной страны

            SaveChangesCommand = new Command(OnSaveChanges);
        }

        private void OnSaveChanges()
        {
            if (SelectedCountry != null)
            {
                // Ваш код сохранения изменений
                // Например, найдите выбранную страну в коллекции Countries и обновите ее данные

                Countries[index1] = SelectedCountry;

                // Закройте страницу редактирования
            }
            Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}