using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace pr_12_Mary
{
    public partial class MainPage : ContentPage
    {
        string mainDir = FileSystem.AppDataDirectory;
        public ObservableCollection<Country> Countries { get; set; }
        public Country SelectedCountry { get; set; }
        public ICommand RemoveCountryCommand { get; set; }
        public ICommand AddCountryCommand { get; set; }

        public MainPage()
        {
            InitializeComponent();

            // Заполнение списка стран рандомными данными
            Countries = new ObservableCollection<Country>
            {
                new Country { Name = "Россия", Population = 147185458, Capital = "Москва", Currency = "Российский рубль" },
                new Country { Name = "Украина", Population = 1110, Capital = "Киев", Currency = "Доллар США" },
                new Country { Name = "США", Population = 3654894, Capital = "Вашингтон", Currency = "Украинская гривна" },
                new Country { Name = "Германия", Population = 1158946, Capital = "Берлин", Currency = "Евро" },
                new Country { Name = "Китай", Population = 1235468965, Capital = "Пекин", Currency = "Китайский юань" }
            };

            // Установка источника данных для ListView
            countryListView.ItemsSource = Countries;
            RemoveCountryCommand = new Command(RemoveCountry);
            AddCountryCommand = new Command(AddCountry);
            BindingContext = this;
        }

        private void OnShowPopulationClicked(object sender, EventArgs e)
        {
            // Вывод общего населения в MessageBox
            int totalPopulation = Countries.Sum(country => country.Population);
            DisplayAlert("Общее население", $"Общее население всех стран {totalPopulation:N0}", "OK");
        }

        private void btnSaveItem(object sender, EventArgs e)
        {
            string jsonString = JsonSerializer.Serialize(Countries);
            File.WriteAllText(Path.Combine(mainDir, "Countries.txt"), jsonString);
        }

        private void btnOpenItem(object sender, EventArgs e)
        {
            string jsonString = File.ReadAllText(Path.Combine(mainDir, "Countries.txt"));
            Countries = JsonSerializer.Deserialize<ObservableCollection<Country>>(jsonString);
            countryListView.ItemsSource = Countries;
        }

        private void RemoveCountry()
        {
            // Удаление выбранной страны
            if (SelectedCountry != null)
            {
                Countries.Remove(SelectedCountry);
                countryListView.ItemsSource = null;
                countryListView.ItemsSource = Countries;
            }
        }

        private void workersList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Country selectedItem;
            if (e.SelectedItem != null)
            {
                selectedItem = (Country)e.SelectedItem;
                SelectedCountry = selectedItem;
                lbSelectedText.Text = selectedItem.Name + "\n" + selectedItem.Population + " " + selectedItem.Capital + " " + selectedItem.Currency;
            }
        }

        private void AddCountry()
        {
            // Добавление новой страны
            Country newCountry = new Country { Name = "Новая страна", Population = 0, Capital = "Столица", Currency = "Валюта" };
            Countries.Add(newCountry);
            SelectedCountry = newCountry;
            lbSelectedText.Text = newCountry.Name + "\n" + newCountry.Population + " " + newCountry.Capital + " " + newCountry.Currency;
        }

        private void OnEditCountryClicked(object sender, EventArgs e)
        {
            if (SelectedCountry != null)
            {
                EditCountryViewModel editViewModel = new EditCountryViewModel(Countries);
                EditCountryPage editPage = new EditCountryPage();
                editViewModel.index1 = Countries.IndexOf(SelectedCountry);
                editPage.ViewModel = editViewModel;
                Navigation.PushAsync(editPage);

            }
            else
            {
                DisplayAlert("Предупреждение", "Выберите страну для редактирования", "OK");
            }
        }
    }
}