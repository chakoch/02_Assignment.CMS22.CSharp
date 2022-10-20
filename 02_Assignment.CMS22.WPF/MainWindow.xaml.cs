using _02_Assignment.CMS22.WPF.Models;
using _02_Assignment.CMS22.WPF.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace _02_Assignment.CMS22.WPF
{

    public partial class MainWindow : Window
    {
        private FileManager _fileManager = new FileManager();
        private ObservableCollection<ContactPerson> contacts; 
        private string _filePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\WPF.Json"; // berättar var filen ska sparas, skrivbodet i det här fallet.

        public MainWindow() // contructor
        {
            InitializeComponent();
            try { contacts = JsonConvert.DeserializeObject<ObservableCollection<ContactPerson>>(_fileManager.Read(_filePath)); } // försöka att läsa filerna 
            catch { contacts = new ObservableCollection<ContactPerson>(); } // om inte filen finns läs ny
            lv_Contacts.ItemsSource = contacts;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            
            var contact = contacts.FirstOrDefault(x => x.Email == tb_Email.Text); // nya kontakter får ej ha samma e-post som existerande kontakter 
            if (contact == null)
            {
                contacts.Add(new ContactPerson
                {
                    FirstName = tb_FirstName.Text,
                    LastName = tb_LastName.Text,
                    Email = tb_Email.Text,
                    Phone = tb_Phone.Text,
                    StreetName = tb_StreetName.Text,
                    PostalCode = tb_PostalCode.Text,
                    City = tb_City.Text,
                });
            }
            else
            {
                MessageBox.Show("Det finns en kontankt med samma e-postadress");
            }

            _fileManager.Save(_filePath, JsonConvert.SerializeObject(contacts)); //sparar listan till en Json-fil

            ClearFields();

        }

        private void ClearFields() // rensar gammala uppgifter om kontakten
        {
            tb_FirstName.Text = "";
            tb_LastName.Text = "";
            tb_Email.Text = "";
            tb_Phone.Text = "";
            tb_StreetName.Text = "";
            tb_PostalCode.Text = "";
            tb_City.Text = "";
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try // försker att ta bort en kontakt
            {
                var button = sender as Button;
                var contact = (ContactPerson)button!.DataContext;
                contacts.Remove(contact);
                tb_FirstName.Text = "";
                tb_LastName.Text = "";
                tb_Email.Text = "";
                tb_Phone.Text = "";
                tb_StreetName.Text = "";
                tb_PostalCode.Text = "";
                tb_City.Text = "";
            }
            catch { } // om det inte går spara filen
            _fileManager?.Save(_filePath, JsonConvert.SerializeObject(contacts));

            btn_Update.Visibility = Visibility.Collapsed; // visar och gömmer uppdatera knappen. Då kommer istället lägga till kanppen
            if (btn_Add.Visibility == Visibility.Collapsed)
            {
                btn_Add.Visibility = Visibility.Visible;
            }
        }

        private void lv_Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try // försker att visa listan på alla kontakter som är sparade i appen
            {
                var contact = (ContactPerson)lv_Contacts.SelectedItems[0]!;
                tb_FirstName.Text = contact.FirstName;
                tb_LastName.Text = contact.LastName;
                tb_Email.Text = contact.Email;
                tb_Phone.Text = contact.Phone;
                tb_StreetName.Text = contact.StreetName;
                tb_PostalCode.Text = contact.PostalCode;
                tb_City.Text = contact.City;
            }
            catch { } // om det inte går, gör inget

            btn_Add.Visibility = Visibility.Collapsed; // visar och gömmer lägga till-knappen. Då kommer istället uppdatera-kanppen
            if (btn_Update.Visibility == Visibility.Collapsed)
            {
                btn_Update.Visibility = Visibility.Visible;
            }

        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            

            var contact = (ContactPerson)lv_Contacts.SelectedItems[0]!;
            var index = contacts.IndexOf(contact); // hittar den valda kontakten för uppdatering

            contacts[index].FirstName = tb_FirstName.Text;
            contacts[index].LastName = tb_LastName.Text;
            contacts[index].Email = tb_Email.Text;
            contacts[index].Phone = tb_Phone.Text;
            contacts[index].StreetName = tb_StreetName.Text;
            contacts[index].PostalCode = tb_PostalCode.Text;
            contacts[index].City = tb_City.Text;

            lv_Contacts.Items.Refresh();
            ClearFields();
            _fileManager.Save(_filePath, JsonConvert.SerializeObject(contacts)); // sparar listan till Json-fil

            try { contacts = JsonConvert.DeserializeObject<ObservableCollection<ContactPerson>>(_fileManager.Read(_filePath)); } //försöker att läsa in listan från en Json-fil
            catch { } // om det går, gör inget

        }
    }
}
