/**
 * warunek czy zdjęcie zostało przesłane
 * przyciski usuń, import, export jako txt
 * stworzenie wyszukiwarki
 * przycisk usuń robi się enabled true gdy wiersz tabeli jest podświetlony
 * usunąć godzinę w dodanych wierszach
 * wychowanie zamiast True wpisuje TAK
 * wybrany index płeć K,M lub I
 * wybrany index info etat w tekscie
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Zadanie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Zadanie_Loaded(object sender, RoutedEventArgs e) 
        {
  
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        bool czydodac = true;
        

        private void Dodaj_onClicik(object sender, RoutedEventArgs e)
        {
            /**
             * u - uczniowe
             * n - nauczyciele
             * p - personel
             * tablice zawierają id kontrolek WYMAGANYCH do wypełnienia
             */

            TabItem ti = Tabs.SelectedItem as TabItem;

            TextBox[] formtxtbox_u = {imie_uczen, nazwisko_uczen, imie_rodzic_1_uczen, imie_rodzic_2_uczen, pesel_uczen, klasa_uczen, grupa_uczen, miedzyklasa_uczen};
            ComboBox[] formcombobox_u = {plec_uczen};//po co mi tablica z jednym elementem?
            DatePicker[] formdatepicker_u = {data_urodzenia_uczen};//i to też

            TextBox[] formtxtbox_n = {imie_nauczyciel, nazwisko_nauczyciel, imie_rodzic_1_nauczyciel, imie_rodzic_2_nauczyciel, pesel_nauczyciel, przedmiot_nauczania_nauczyciel};
            ComboBox[] formcombobox_n = {plec_nauczyciel};//co ja robie
            DatePicker[] formdatepicker_n = {data_urodzenia_nauczyciel, data_zatrudnienia_nauczyciel};

            TextBox[] formtxtbox_p = {imie_personel, nazwisko_personel, imie_rodzic_1_personel, imie_rodzic_2_personel, pesel_personel, opis_stanowiska_personel};
            ComboBox[] formcombobox_p = {plec_personel, info_etat_personel};
            DatePicker[] formdatepicker_p = {data_urodzenia_personel, data_zatrudnienia_personel};



            

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Dodany zostanie rekord do tabeli " + ti.Header + ", kontynuować?", "Potwierdzenie dodania", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                /**
                 * w zależności jaka zakładka aktualnie jest wyświetlna, robi się pętla przez tablice, która sprawdzi czy wymagane pola są wypełnione
                 */

                switch (ti.Header) {

                    case "Uczniowie":
                        foreach (TextBox txt in formtxtbox_u)
                        {
                            if (string.IsNullOrEmpty(txt.Text))
                            {
                                MessageBox.Show("Nie wypełniono wymaganych pól");
                                czydodac = false;
                                break;
                            }
                        }
                        foreach (ComboBox cmb in formcombobox_u)
                        {
                            if (cmb.SelectedIndex == 0)
                            {
                                MessageBox.Show("Nie wybrano wymaganych pól");
                                czydodac = false;
                                break;
                            };
                        }
                        if (data_urodzenia_uczen.SelectedDate == null)
                        {
                            MessageBox.Show("Wybierz datę urodzenia ucznia");
                            czydodac = false;
                        }
                        else if (data_urodzenia_uczen.SelectedDate.Value.AddYears(6) > DateTime.Now)
                        {
                            MessageBox.Show("Aby być uczniem w tej szkole, trzeba mieć skończone conajmniej 6 lat");
                            czydodac = false;
                        }

                        if (czydodac)
                        {
                            /*dodawanie do datagrid rekordów*/
                            var data = new Uczniowie 
                            { 
                                Imie = imie_uczen.Text, 
                                Drugie_Imie = drugie_imie_uczen.Text,
                                Nazwisko = nazwisko_uczen.Text,
                                Nazwisko_Panienskie = nazwisko_panienskie_uczen.Text,
                                Imie_Rodzic_1 = imie_rodzic_1_uczen.Text,
                                Imie_Rodzic_2 = imie_rodzic_2_uczen.Text,
                                Data_Urodzenia = data_urodzenia_uczen.SelectedDate.Value,
                                Pesel = pesel_uczen.Text, 
                                /*Zdjecie = ??? ,*/
                                Plec = plec_uczen.SelectedIndex.ToString(),
                                Klasa = klasa_uczen.Text, 
                                Grupa = grupa_uczen.Text, 
                                Miedzyklasa = miedzyklasa_uczen.Text
                            };

                            datagrid_u.Items.Add(data);
                        }
                        czydodac = true;
                        break;
    
                    case "Nauczyciele":
                        foreach (TextBox txt in formtxtbox_n)
                        {
                            if (string.IsNullOrEmpty(txt.Text))
                            {
                                MessageBox.Show("Nie wypełniono wymaganych pól");
                                czydodac = false;
                                break;
                            }
                        }
                        foreach (ComboBox cmb in formcombobox_n)
                        {
                            if (cmb.SelectedIndex == 0)
                            {
                                MessageBox.Show("Nie wybrano wymaganych pól");
                                czydodac = false;
                                break;
                            }
                        }
                        if (data_urodzenia_nauczyciel.SelectedDate == null)
                        {
                            MessageBox.Show("Wybierz datę urodzenia nauczyciela");
                            czydodac = false;
                        }
                        else if (data_urodzenia_nauczyciel.SelectedDate.Value.AddYears(18) > DateTime.Now)
                        {
                            MessageBox.Show("Aby być nauczycielem tego ośrodka wychowawczego trzeba mieć skończone conajmniej 18 lat");
                            czydodac = false;
                        }

                        if (czydodac)
                        {
                            var data = new Nauczyciele
                            {
                                Imie = imie_nauczyciel.Text,
                                Drugie_Imie = drugie_imie_nauczyciel.Text,
                                Nazwisko = nazwisko_nauczyciel.Text,
                                Nazwisko_Panienskie = nazwisko_panienskie_nauczyciel.Text,
                                Imie_Rodzic_1 = imie_rodzic_1_nauczyciel.Text,
                                Imie_Rodzic_2 = imie_rodzic_2_nauczyciel.Text,
                                Data_Urodzenia = data_urodzenia_nauczyciel.SelectedDate.Value,
                                Pesel = pesel_nauczyciel.Text,
                                /*Zdjecie = ??? ,*/
                                Plec = plec_nauczyciel.SelectedIndex.ToString(),
                                Wychowawstwo = wychowawstwo_nauczyciel.IsChecked.Value,
                                Przedmioty = przedmiot_nauczania_nauczyciel.Text,
                                Data_Zatrudnienia = data_zatrudnienia_nauczyciel.SelectedDate.Value
                            };
                            datagrid_n.Items.Add(data);
                        }
                        czydodac = true;
                        break;

                    case "Personel":
                        foreach (TextBox txt in formtxtbox_p)
                        {
                            if (string.IsNullOrEmpty(txt.Text))
                            {
                                MessageBox.Show("Nie wypełniono wymaganych pól");
                                czydodac = false;
                                break;
                            }
                        }
                        foreach (ComboBox cmb in formcombobox_p)
                        {
                            if (cmb.SelectedIndex == 0)
                            {
                                MessageBox.Show("Nie wybrano wymaganych pól");
                                czydodac = false;
                                break;
                            }
                        }
                        if (data_urodzenia_personel.SelectedDate == null)
                        {
                            MessageBox.Show("Wybierz datę urodzenia personelu");
                            czydodac = false;
                        }
                        if (data_urodzenia_personel.SelectedDate.Value.AddYears(18) > DateTime.Now)
                        {
                            MessageBox.Show("Jako nieletni nikt by nie chiał pracować w tej budzie");
                            czydodac = false;
                        }

                        if (czydodac)
                        {
                            var data = new Personel
                            {
                                Imie = imie_personel.Text,
                                Drugie_Imie = drugie_imie_personel.Text,
                                Nazwisko = nazwisko_personel.Text,
                                Nazwisko_Panienskie = nazwisko_panienskie_personel.Text,
                                Imie_Rodzic_1 = imie_rodzic_1_personel.Text,
                                Imie_Rodzic_2 = imie_rodzic_2_personel.Text,
                                Data_Urodzenia = data_urodzenia_personel.SelectedDate.Value,
                                Pesel = pesel_personel.Text,
                                /*Zdjecie = ??? ,*/
                                Plec = plec_personel.SelectedItem.ToString(),
                                Info_Etat = info_etat_personel.SelectedItem.ToString(),
                                Opis = opis_stanowiska_personel.Text,
                                Data_Zatrudnienia = data_zatrudnienia_personel.SelectedDate.Value
                            };
                            datagrid_p.Items.Add(data);
                        }
                        czydodac = true;
                        break;
                }
            }
        }

        private void Data_zatrudnienia_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void data_urodzenia_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public class Uczniowie
        {
            public string Imie { get; set; }
            public string Drugie_Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Nazwisko_Panienskie { get; set; }
            public string Imie_Rodzic_1 { get; set; }
            public string Imie_Rodzic_2 { get; set; }
            public DateTime Data_Urodzenia{ get; set; }
            public string Pesel { get; set; }
            public string Zdjecie { get; set; }
            public string Plec { get; set; }
            public string Klasa { get; set; }
            public string Grupa { get; set; }
            public string Miedzyklasa { get; set; }

        }

        public class Nauczyciele
        {
            public string Imie { get; set; }
            public string Drugie_Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Nazwisko_Panienskie { get; set; }
            public string Imie_Rodzic_1 { get; set; }
            public string Imie_Rodzic_2 { get; set; }
            public DateTime Data_Urodzenia { get; set; }
            public string Pesel { get; set; }
            public string Zdjecie { get; set; }
            public string Plec { get; set; }
            public bool Wychowawstwo { get; set; }
            public string Przedmioty { get; set; }
            public DateTime Data_Zatrudnienia { get; set; }

        }

        public class Personel
        {
            public string Imie { get; set; }
            public string Drugie_Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Nazwisko_Panienskie { get; set; }
            public string Imie_Rodzic_1 { get; set; }
            public string Imie_Rodzic_2 { get; set; }
            public DateTime Data_Urodzenia { get; set; }
            public string Pesel { get; set; }
            public string Zdjecie { get; set; }
            public string Plec { get; set; }
            public string Info_Etat { get; set; }
            public string Opis { get; set; }
            public DateTime Data_Zatrudnienia { get; set; }

        }

        private void Usun_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Czy chcesz usunąć rekord z tabeli: "+ti.Header+"?","Potwierdzenie usunięcia", System.Windows.MessageBoxButton.YesNo);

            if (datagrid_u.SelectedItem != null)
            {
                switch(ti.Header)
                {
                    case "Uczniowie":
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            datagrid_u.Items.Remove(datagrid_u.SelectedItem);
                        }
                        break;
                    case "Nauczyciele":
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            datagrid_n.Items.Remove(datagrid_n.SelectedItem);
                        }
                        break;
                    case "Personel":
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            datagrid_p.Items.Remove(datagrid_p.SelectedItem);
                        }
                        break;
                }
                
            }
            else MessageBox.Show("Nie zaznaczono wiersza");
        }
    }
}
