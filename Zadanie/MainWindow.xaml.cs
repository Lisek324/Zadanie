﻿/**
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
using System.IO;

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

        List<Uczniowie> lista = new List<Uczniowie>();

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
                                Plec = plec_nauczyciel.SelectedIndex.ToString(), //zrobić switcha
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
        public class Uczniowie
        {
            /*public Uczniowie(string imie, string drugie_Imie, string nazwisko, string nazwisko_Panienskie, string imie_Rodzic_1, string imie_Rodzic_2, DateTime data_Urodzenia, string pesel, string zdjecie, string plec, string klasa, string grupa, string miedzyklasa)
            {
                Imie = imie;
                Drugie_Imie = drugie_Imie;
                Nazwisko = nazwisko;
                Nazwisko_Panienskie = nazwisko_Panienskie;
                Imie_Rodzic_1 = imie_Rodzic_1;
                Imie_Rodzic_2 = imie_Rodzic_2;
                Data_Urodzenia = data_Urodzenia;
                Pesel = pesel;
                Zdjecie = zdjecie;
                Plec = plec;
                Klasa = klasa;
                Grupa = grupa;
                Miedzyklasa = miedzyklasa;
            }*/

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
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Czy chcesz usunąć rekord z tabeli: " + ti.Header + "?", "Potwierdzenie usunięcia", System.Windows.MessageBoxButton.YesNo);

            if (datagrid_u.SelectedItem != null)//zwróci fałsz w przypadku zaznaczonego wiersza w tabeli nauczycieli DO POPRAWY
            {
                switch (ti.Header)
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

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            /*
            StringBuilder strbldr = new StringBuilder();
            for (int i = 0; i < datagrid_u.Items.Count; i++)
            {
                Uczniowie u = (Uczniowie)datagrid_u.Items[i];
                strbldr.Append(u.ToString());
            }

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            if (dlg.ShowDialog() == true)
            {
                File.WriteAllText(dlg.FileName, strbldr.ToString());

            }
            */

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            string str = "";

            if (datagrid_u.SelectedItems.Count > 0)
            {
                Uczniowie uczniowie = new Uczniowie();
                if (dlg.ShowDialog() == true)
                {
                    foreach (var obj in datagrid_u.SelectedItems)
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
                            + uczniowie.Zdjecie + " "
                            + uczniowie.Plec + " "
                            + uczniowie.Klasa + " "
                            + uczniowie.Grupa + " "
                            + uczniowie.Miedzyklasa + " "
                            + "\n";
                    }
                    File.WriteAllText(dlg.FileName, str);
                }

            }

        }

        

          
        private void Import_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            

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
                    //lista.Add(new Uczniowie { Imie = row[0], Drugie_Imie = row[1], Nazwisko = row[2] });
                    var data = new Uczniowie
                    { 
                        Imie = row[0],
                        Drugie_Imie = row[1]
                    };
                    datagrid_u.Items.Add(data);
                    /*datagrid_u.ItemsSource = null;
                    datagrid_u.ItemsSource = lista;*/


                    /*foreach (var kom in datagrid_u.Items)
                    {
                        var data = new Uczniowie//klasa Uczniowie
                        {
                            Imie = row[i].ToString()
                            /*Imie = pierwszy string z pliku tekstowego
                        };
                    }*/
                }
            }
            
            
            /*lista.Add(new Uczniowie());
            datagrid_u.ItemsSource = null;
            datagrid_u.ItemsSource = lista;*/
            
        }
    }
}

