/**
 * konieczny warunek na datę
 * stowrzyć foreacha na comboboxa
 * warunek czy zdjęcie zostało przesłane
 * przyciski usuń, import, export jako txt
 * stworzenie wyszukiwarki
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

        private void Dodaj_onClicik(object sender, RoutedEventArgs e)
        {
            /**
             * u - uczniowe
             * n - nauczyciele
             * p - personel
             * tablice zawierają id kontrolek WYMAGANYCH do wypełnienia
             */
            TextBox[] formtxtbox_u = {imie_uczen, nazwisko_uczen, imie_rodzic_1_uczen, imie_rodzic_2_uczen, pesel_uczen, klasa_uczen, grupa_uczen, miedzyklasa_uczen};
            ComboBox[] formcombobox_u = {plec_uczen};//po co mi tablica z jednym elementem? Jak wykorzystać warunek po foreachu?
            DatePicker[] formdatepicker_u = {data_urodzenia_uczen};

            TextBox[] formtxtbox_n = {imie_nauczyciel, nazwisko_nauczyciel, imie_rodzic_1_nauczyciel, imie_rodzic_2_nauczyciel, pesel_nauczyciel, przedmiot_nauczania_nauczyciel};
            ComboBox[] formcombobox_n = {plec_nauczyciel};
            DatePicker[] formdatepicker_n = {data_urodzenia_nauczyciel, data_zatrudnienia_nauczyciel};

            TextBox[] formtxtbox_p = {imie_personel, nazwisko_personel, imie_rodzic_1_personel, imie_rodzic_2_personel, pesel_personel, opis_stanowiska_personel};
            ComboBox[] formcombobox_p = {plec_personel, info_etat_personel};
            DatePicker[] formdatepicker_p = {data_urodzenia_personel, data_zatrudnienia_personel};



            TabItem ti = Tabs.SelectedItem as TabItem;

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
                                break;
                            }
                        }
                        foreach (ComboBox cmb in formcombobox_u)
                        {
                            if (cmb.SelectedIndex == 0)
                            {
                                MessageBox.Show("Nie wybrano wymaganych pól");
                                break;
                            }
                        }
                        //niepotrzebna pętla? - stworzyć onChange datepicker ustawiający wartość domyślną; jeśli datepicker posiada tą wartość nie przechodzi przez warunek
                        /*foreach (DatePicker dp in formdatepicker_u)
                        {
                            if (dp!=defaul)
                            {
                                MessageBox.Show("Nie wybrano wymaganych pól");
                                break;
                            }
                        }*/
                        break;
                    case "Nauczyciele":
                        break;
                    case "Personel":
                        break;
                }
            }
        }

        private void Data_zatrudnienia_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            var dzis = DateTime.Today;

            switch (ti.Header)
            {
                case "Nauczyciele":
                    if (dzis < data_zatrudnienia_nauczyciel.SelectedDate) 
                    {
                        MessageBox.Show("Data zatrudnienia nie może być wrpowadzona z wyprzedzeniem dzisiejszego dnia");
                        data_zatrudnienia_nauczyciel.DisplayDate = DateTime.Now.Date;
                    }
                        break;
                case "Personel":
                    if (dzis < data_zatrudnienia_personel.SelectedDate)
                    {
                        MessageBox.Show("Data zatrudnienia nie może być wrpowadzona z wyprzedzeniem dzisiejszego dnia");
                        data_zatrudnienia_personel.DisplayDate = DateTime.Now.Date;
                    }
                    break;
            }
        }

        private void data_urodzenia_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            DateTime dzis = DateTime.Today;

            TimeSpan ok = dzis - data_urodzenia_uczen.SelectedDate.Value;//rozwiązanie(chyba)

            /* switch (ti.Header)
             {
                 case "Uczniowie":
                     if (data_urodzenia_uczen.SelectedDate >=  dzis)
                     {
                         MessageBox.Show("Aby być uczniem w tej szkole, trzeba mieć skończone conajmniej 6 lat");
                     } 
                     if(u < 6)
                     {

                     }
                     break;

                 case "Nauczyciele":
                     break;
                 case "Personel":
                     break;
             }*/
        }
    }
}
