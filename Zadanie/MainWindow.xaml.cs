/**
 * poprawienie wyszukiwarki
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
using System.IO;

namespace Zadanie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Uczniowie> lista_u = new List<Uczniowie>();
        List<Nauczyciele> lista_n = new List<Nauczyciele>();
        List<Personel> lista_p = new List<Personel>();

        public MainWindow()
        {
            InitializeComponent();
        }



        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        bool czydodac = true;
        string wychowawstwo = "";
        /***********DODAWANIE DO REKORDÓW***********/
        private void Dodaj_Clicik(object sender, RoutedEventArgs e)
        {
            /**
             * u - uczniowe
             * n - nauczyciele
             * p - personel
             * tablice zawierają id kontrolek WYMAGANYCH do wypełnienia
             */

            TabItem ti = Tabs.SelectedItem as TabItem;

            TextBox[] formtxtbox_u = { imie_uczen, nazwisko_uczen, imie_rodzic_1_uczen, imie_rodzic_2_uczen, pesel_uczen, klasa_uczen, grupa_uczen, miedzyklasa_uczen };
            ComboBox[] formcombobox_u = { plec_uczen };//po co mi tablica z jednym elementem?
            DatePicker[] formdatepicker_u = { data_urodzenia_uczen };//i to też

            TextBox[] formtxtbox_n = { imie_nauczyciel, nazwisko_nauczyciel, imie_rodzic_1_nauczyciel, imie_rodzic_2_nauczyciel, pesel_nauczyciel, przedmiot_nauczania_nauczyciel };
            ComboBox[] formcombobox_n = { plec_nauczyciel };//co ja robie
            DatePicker[] formdatepicker_n = { data_urodzenia_nauczyciel, data_zatrudnienia_nauczyciel };

            TextBox[] formtxtbox_p = { imie_personel, nazwisko_personel, imie_rodzic_1_personel, imie_rodzic_2_personel, pesel_personel, opis_stanowiska_personel };
            ComboBox[] formcombobox_p = { plec_personel, info_etat_personel };
            DatePicker[] formdatepicker_p = { data_urodzenia_personel, data_zatrudnienia_personel };





            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Dodany zostanie rekord do tabeli " + ti.Header + ", kontynuować?", "Potwierdzenie dodania", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                /**
                 * w zależności jaka zakładka aktualnie jest wyświetlna, robi się pętla przez tablice, która sprawdzi czy wymagane pola są wypełnione
                 */

                switch (ti.Header)
                {

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
                        if (zdjecie_u.Source == null)
                        {
                            MessageBox.Show("Nie przesłano zdjęcia");
                            czydodac = false;
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
                                Data_Urodzenia = data_urodzenia_uczen.SelectedDate.Value.Date.ToShortDateString(),
                                Pesel = pesel_uczen.Text,
                                Zdjecie = new Uri(zdjecie_u.Source.ToString()),
                                Plec = plec_uczen.Text,
                                Klasa = klasa_uczen.Text,
                                Grupa = grupa_uczen.Text,
                                Miedzyklasa = miedzyklasa_uczen.Text

                            };
                            lista_u.Add(data);
                            datagrid_u.ItemsSource = null;
                            datagrid_u.ItemsSource = lista_u;

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
                        if (zdjecie_n.Source == null)
                        {
                            MessageBox.Show("Nie przesłano zdjęcia");
                            czydodac = false;
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

                        if (wychowawstwo_nauczyciel.IsChecked.Value)
                        {
                            wychowawstwo = "TAK";
                        }
                        else wychowawstwo = "NIE";

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
                                Data_Urodzenia = data_urodzenia_nauczyciel.SelectedDate.Value.Date.ToShortDateString(),
                                Pesel = pesel_nauczyciel.Text,
                                Zdjecie = new Uri(zdjecie_n.Source.ToString()),
                                Plec = plec_nauczyciel.Text,
                                Wychowawstwo = wychowawstwo,
                                Przedmioty = przedmiot_nauczania_nauczyciel.Text,
                                Data_Zatrudnienia = data_zatrudnienia_nauczyciel.SelectedDate.Value.Date.ToShortDateString()
                            };
                            lista_n.Add(data);
                            datagrid_n.ItemsSource = null;
                            datagrid_n.ItemsSource = lista_n;
                        }
                        czydodac = true;
                        wychowawstwo = "";
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
                        if (zdjecie_p.Source == null)
                        {
                            MessageBox.Show("Nie przesłano zdjęcia");
                            czydodac = false;
                        }
                        if (data_urodzenia_personel.SelectedDate == null)
                        {
                            MessageBox.Show("Wybierz datę urodzenia personelu");
                            czydodac = false;
                        }
                        if (data_urodzenia_personel.SelectedDate.Value.AddYears(18) > DateTime.Now)
                        {
                            MessageBox.Show("Osoba musi być pełnoletnia");
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
                                Data_Urodzenia = data_urodzenia_personel.SelectedDate.Value.Date.ToShortDateString(),
                                Pesel = pesel_personel.Text,
                                Zdjecie = new Uri(zdjecie_p.Source.ToString()),
                                Plec = plec_personel.Text,
                                Info_Etat = info_etat_personel.Text,
                                Opis = opis_stanowiska_personel.Text,
                                Data_Zatrudnienia = data_zatrudnienia_personel.SelectedDate.Value.Date.ToShortDateString(),
                            };
                            lista_p.Add(data);
                            datagrid_p.ItemsSource = null;
                            datagrid_p.ItemsSource = lista_p;
                        }
                        czydodac = true;
                        break;
                }
            }
        }
        /***********KLASY***********/
        public class Uczniowie
        {
            public string Imie { get; set; }
            public string Drugie_Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Nazwisko_Panienskie { get; set; }
            public string Imie_Rodzic_1 { get; set; }
            public string Imie_Rodzic_2 { get; set; }
            public string Data_Urodzenia { get; set; }
            public string Pesel { get; set; }
            public Uri Zdjecie { get; set; }
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
            public string Data_Urodzenia { get; set; }
            public string Pesel { get; set; }
            public Uri Zdjecie { get; set; }
            public string Plec { get; set; }
            public string Wychowawstwo { get; set; }
            public string Przedmioty { get; set; }
            public string Data_Zatrudnienia { get; set; }

        }

        public class Personel
        {
            public string Imie { get; set; }
            public string Drugie_Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Nazwisko_Panienskie { get; set; }
            public string Imie_Rodzic_1 { get; set; }
            public string Imie_Rodzic_2 { get; set; }
            public string Data_Urodzenia { get; set; }
            public string Pesel { get; set; }
            public Uri Zdjecie { get; set; }
            public string Plec { get; set; }
            public string Info_Etat { get; set; }
            public string Opis { get; set; }
            public string Data_Zatrudnienia { get; set; }

        }

        /***********USUWANIE***********/
        private void Usun_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            switch (ti.Header)
            {

                case "Uczniowie":
                    for (int i = datagrid_u.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        lista_u.RemoveAt(i);
                        datagrid_u.ItemsSource = null;
                        datagrid_u.ItemsSource = lista_u;
                        wyszukaj.Text = "";
                    }
                    break;
                case "Nauczyciele":
                    for (int i = datagrid_u.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        lista_u.RemoveAt(i);
                        datagrid_n.ItemsSource = null;
                        datagrid_n.ItemsSource = lista_n;
                        wyszukaj.Text = "";
                    }
                    break;
                case "Personel":
                    for (int i = datagrid_u.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        lista_u.RemoveAt(i);
                        datagrid_p.ItemsSource = null;
                        datagrid_p.ItemsSource = lista_p;
                        wyszukaj.Text = "";
                    }
                    break;
            }
        }

        /***********EKSPORTOWANIE***********/
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            string str = "";
            TabItem ti = Tabs.SelectedItem as TabItem;

            if (dlg.ShowDialog() == true)
            {
                switch (ti.Header)
                {
                    case "Uczniowie":

                        Uczniowie uczniowie = new Uczniowie();
                        foreach (var obj in datagrid_u.ItemsSource)
                        {

                            uczniowie = obj as Uczniowie;
                            str += uczniowie.Imie + " "
                                + uczniowie.Drugie_Imie + " "
                                + uczniowie.Nazwisko + " "
                                + uczniowie.Nazwisko_Panienskie + " "
                                + uczniowie.Imie_Rodzic_1 + " "
                                + uczniowie.Imie_Rodzic_2 + " "
                                + uczniowie.Data_Urodzenia + " "
                                + uczniowie.Pesel + " "
                                //+ uczniowie.Zdjecie + " "
                                + uczniowie.Plec + " "
                                + uczniowie.Klasa + " "
                                + uczniowie.Grupa + " "
                                + uczniowie.Miedzyklasa + " "
                                + "\n";
                        }
                        File.WriteAllText(dlg.FileName, str);
                        break;

                    case "Nauczyciele":
                        Nauczyciele nauczyciele = new Nauczyciele();
                        foreach (var obj in datagrid_n.ItemsSource)
                        {

                            nauczyciele = obj as Nauczyciele;
                            str += nauczyciele.Imie + " "
                                + nauczyciele.Drugie_Imie + " "
                                + nauczyciele.Nazwisko + " "
                                + nauczyciele.Nazwisko_Panienskie + " "
                                + nauczyciele.Imie_Rodzic_1 + " "
                                + nauczyciele.Imie_Rodzic_2 + " "
                                + nauczyciele.Data_Urodzenia + " "
                                + nauczyciele.Pesel + " "
                                //+ nauczyciele.Zdjecie + " "
                                + nauczyciele.Plec + " "
                                + nauczyciele.Wychowawstwo + " "
                                + nauczyciele.Przedmioty + " "
                                + nauczyciele.Data_Zatrudnienia + " "
                                + "\n";
                        }
                        File.WriteAllText(dlg.FileName, str);
                        break;

                    case "Personel":

                        foreach (var obj in datagrid_u.ItemsSource)
                        {
                            Personel personel = new Personel();
                            personel = obj as Personel;
                            str += personel.Imie + " "
                                + personel.Drugie_Imie + " "
                                + personel.Nazwisko + " "
                                + personel.Nazwisko_Panienskie + " "
                                + personel.Imie_Rodzic_1 + " "
                                + personel.Imie_Rodzic_2 + " "
                                + personel.Data_Urodzenia + " "
                                + personel.Pesel + " "
                                //+ personel.Zdjecie + " "
                                + personel.Plec + " "
                                + personel.Info_Etat + " "
                                + personel.Opis + " "
                                + personel.Data_Zatrudnienia + " "
                                + "\n";
                        }
                        File.WriteAllText(dlg.FileName, str);
                        break;
                }

            }



        }



        /***********IMPORTOWANIE***********/
        private void Import_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Pliki tekstowe (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                string[] lines = File.ReadAllLines(openFileDialog.FileName);
                string[] values;

                for (int i = 0; i < lines.Length; i++)
                {
                    values = lines[i].Split(' ');
                    string[] row = new string[values.Length];

                    for (int j = 0; j < values.Length; j++)
                    {
                        row[j] = values[j].Trim();

                    }
                    switch (ti.Header)
                    {

                        case "Uczniowie":
                            var data_u = new Uczniowie
                            {
                                Imie = row[0],
                                Drugie_Imie = row[1],
                                Nazwisko = row[2],
                                Nazwisko_Panienskie = row[3],
                                Imie_Rodzic_1 = row[4],
                                Imie_Rodzic_2 = row[5],
                                Data_Urodzenia = row[6],
                                Pesel = row[7],
                                Plec = row[8],
                                Klasa = row[9],
                                Grupa = row[10],
                                Miedzyklasa = row[11]
                            };
                            lista_u.Add(data_u);
                            datagrid_u.ItemsSource = null;
                            datagrid_u.ItemsSource = lista_u;
                            break;

                        case "Nauczyciele":
                            var data_n = new Nauczyciele
                            {
                                Imie = row[0],
                                Drugie_Imie = row[1],
                                Nazwisko = row[2],
                                Nazwisko_Panienskie = row[3],
                                Imie_Rodzic_1 = row[4],
                                Imie_Rodzic_2 = row[5],
                                Data_Urodzenia = row[6],
                                Pesel = row[7],
                                Plec = row[8],
                                Wychowawstwo = row[9],
                                Przedmioty = row[10],
                                Data_Zatrudnienia = row[11]
                            };
                            lista_n.Add(data_n);
                            datagrid_n.ItemsSource = null;
                            datagrid_n.ItemsSource = lista_n;
                            break;

                        case "Personel":
                            var data_p = new Personel
                            {
                                Imie = row[0],
                                Drugie_Imie = row[1],
                                Nazwisko = row[2],
                                Nazwisko_Panienskie = row[3],
                                Imie_Rodzic_1 = row[4],
                                Imie_Rodzic_2 = row[5],
                                Data_Urodzenia = row[6],
                                Pesel = row[7],
                                Plec = row[8],
                                Info_Etat = row[9],
                                Opis = row[10],
                                Data_Zatrudnienia = row[11]
                            };
                            lista_p.Add(data_p);
                            datagrid_p.ItemsSource = null;
                            datagrid_p.ItemsSource = lista_p;
                            break;
                    };
                }
            }
        }

        /***********PRZESYŁANIE ZDJĘCIA***********/
        private void Zdjecie_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                switch (ti.Header)
                {
                    case "Uczniowie":
                        Uri fileUriu = new Uri(openFileDialog.FileName);
                        zdjecie_u.Source = new BitmapImage(fileUriu);
                        break;
                    case "Nauczyciele":
                        Uri fileUrin = new Uri(openFileDialog.FileName);
                        zdjecie_n.Source = new BitmapImage(fileUrin);
                        break;
                    case "Personel":
                        Uri fileUrip = new Uri(openFileDialog.FileName);
                        zdjecie_p.Source = new BitmapImage(fileUrip);
                        break;
                }
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            //Jeśli moje zdrowie psychiczne się poprawi, warto byłoby to poprawić
            switch (ti.Header)
            {
                case "Uczniowie":
                    switch (cmbbx.Text)
                    {
                        case "Imie":
                            datagrid_u.ItemsSource = lista_u.Where(uczen => uczen.Imie.StartsWith(wyszukaj.Text));
                            break;
                        case "Drugie Imie":
                            datagrid_u.ItemsSource = lista_u.Where(uczen => uczen.Drugie_Imie.StartsWith(wyszukaj.Text));
                            break;
                        case "Nazwisko":
                            datagrid_u.ItemsSource = lista_u.Where(uczen => uczen.Nazwisko.StartsWith(wyszukaj.Text));
                            break;
                        case "Nazwisko Panieńskie":
                            datagrid_u.ItemsSource = lista_u.Where(uczen => uczen.Nazwisko_Panienskie.StartsWith(wyszukaj.Text));
                            break;
                        case "Imie Rodzic 1":
                            datagrid_u.ItemsSource = lista_u.Where(uczen => uczen.Imie_Rodzic_1.StartsWith(wyszukaj.Text));
                            break;
                        case "Imie Rodzic 2":
                            datagrid_u.ItemsSource = lista_u.Where(uczen => uczen.Imie_Rodzic_2.StartsWith(wyszukaj.Text));
                            break;
                        case "Data Urodzenia":
                            datagrid_u.ItemsSource = lista_u.Where(uczen => uczen.Data_Urodzenia.StartsWith(wyszukaj.Text));
                            break;
                        case "Pesel":
                            datagrid_u.ItemsSource = lista_u.Where(uczen => uczen.Pesel.StartsWith(wyszukaj.Text));
                            break;
                        case "Płeć":
                            datagrid_u.ItemsSource = lista_u.Where(uczen => uczen.Plec.StartsWith(wyszukaj.Text));
                            break;
                        case "Klasa":
                            datagrid_u.ItemsSource = lista_u.Where(uczen => uczen.Klasa.StartsWith(wyszukaj.Text));
                            break;
                        case "Grupa":
                            datagrid_u.ItemsSource = lista_u.Where(uczen => uczen.Grupa.StartsWith(wyszukaj.Text));
                            break;
                        case "Międzyklasa":
                            datagrid_u.ItemsSource = lista_u.Where(uczen => uczen.Miedzyklasa.StartsWith(wyszukaj.Text));
                            break;
                    }
                    break;
                case "Nauczyciele":
                    switch (cmbbx.Text) 
                    {
                        case "Imie":
                            datagrid_n.ItemsSource = lista_n.Where(uczen => uczen.Imie.StartsWith(wyszukaj.Text));
                            break;
                        case "Drugie Imie":
                            datagrid_n.ItemsSource = lista_n.Where(uczen => uczen.Drugie_Imie.StartsWith(wyszukaj.Text));
                            break;
                        case "Nazwisko":
                            datagrid_n.ItemsSource = lista_n.Where(uczen => uczen.Nazwisko.StartsWith(wyszukaj.Text));
                            break;
                        case "Nazwisko Panieńskie":
                            datagrid_n.ItemsSource = lista_n.Where(uczen => uczen.Nazwisko_Panienskie.StartsWith(wyszukaj.Text));
                            break;
                        case "Imie Rodzic 1":
                            datagrid_n.ItemsSource = lista_n.Where(uczen => uczen.Imie_Rodzic_1.StartsWith(wyszukaj.Text));
                            break;
                        case "Imie Rodzic 2":
                            datagrid_n.ItemsSource = lista_n.Where(uczen => uczen.Imie_Rodzic_2.StartsWith(wyszukaj.Text));
                            break;
                        case "Data Urodzenia":
                            datagrid_n.ItemsSource = lista_n.Where(uczen => uczen.Data_Urodzenia.StartsWith(wyszukaj.Text));
                            break;
                        case "Pesel":
                            datagrid_n.ItemsSource = lista_n.Where(uczen => uczen.Pesel.StartsWith(wyszukaj.Text));
                            break;
                        case "Płeć":
                            datagrid_n.ItemsSource = lista_n.Where(uczen => uczen.Plec.StartsWith(wyszukaj.Text));
                            break;
                    }
                    break;
                case "Personel":
                    switch (cmbbx.Text) 
                    {
                        case "Imie":
                            datagrid_p.ItemsSource = lista_p.Where(uczen => uczen.Imie.StartsWith(wyszukaj.Text));
                            break;
                        case "Drugie Imie":
                            datagrid_p.ItemsSource = lista_p.Where(uczen => uczen.Drugie_Imie.StartsWith(wyszukaj.Text));
                            break;
                        case "Nazwiskp":
                            datagrid_p.ItemsSource = lista_p.Where(uczen => uczen.Nazwisko.StartsWith(wyszukaj.Text));
                            break;
                        case "Nazwisko Panieńskie":
                            datagrid_p.ItemsSource = lista_p.Where(uczen => uczen.Nazwisko_Panienskie.StartsWith(wyszukaj.Text));
                            break;
                        case "Imie Rodzic 1":
                            datagrid_p.ItemsSource = lista_p.Where(uczen => uczen.Imie_Rodzic_1.StartsWith(wyszukaj.Text));
                            break;
                        case "Imie Rodzic 2":
                            datagrid_p.ItemsSource = lista_p.Where(uczen => uczen.Imie_Rodzic_2.StartsWith(wyszukaj.Text));
                            break;
                        case "Data Urodzenia":
                            datagrid_p.ItemsSource = lista_p.Where(uczen => uczen.Data_Urodzenia.StartsWith(wyszukaj.Text));
                            break;
                        case "Pesel":
                            datagrid_p.ItemsSource = lista_p.Where(uczen => uczen.Pesel.StartsWith(wyszukaj.Text));
                            break;
                        case "Płeć":
                            datagrid_p.ItemsSource = lista_p.Where(uczen => uczen.Plec.StartsWith(wyszukaj.Text));
                            break;
                    }
                    break;
            }

            if (wyszukaj.Text == "")
            {
                datagrid_u.ItemsSource = lista_u;
                zablokowane.Visibility = Visibility.Hidden;
            }
            else if (wyszukaj.Text != "")
            {
                zablokowane.Visibility = Visibility.Visible;
                datagrid_u.IsReadOnly = true;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 sw = new Window1();
            sw.Show();
        }

        private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                TabItem ti = Tabs.SelectedItem as TabItem;
            
            wyszukaj.Text = "";
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "pola oznaczone gwiazdką '*' nie są wymagane wo wypełnienia."+"\n"+
                "Importowanie tylko z plików tekstowych"+"\n"+
                "Eksportowanie - do pliku txt."+"\n"+
                "Usuwanie wierszy za pomocą przycisku usuń lub klawisza Delete. W przypadku użycia wyszukiwarki, działać będzie tylko przycisk usuń.");
        }
    }
}