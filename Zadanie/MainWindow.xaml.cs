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
             * tablice zawierają id kontrolek
             */
            TextBox[] formtxtbox_u = {imie_uczen};
            ComboBox[] formcombobox_u = { };
            DatePicker[] formdatepicker_u = { };

            TextBox[] formtxtbox_n = { };
            ComboBox[] formcombobox_n = { };
            DatePicker[] formdatepicker_n = { };

            TextBox[] formtxtbox_p = { };
            ComboBox[] formcombobox_p = { };
            DatePicker[] formdatepicker_p = { };



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
                            else continue;/* to jest źle - tu ma być wpisanie do datagrid*/
                        }
                        break;
                    case "Nauczyciele":
                        break;
                    case "Personel":
                        break;
                }
            }
        }
    }
}
