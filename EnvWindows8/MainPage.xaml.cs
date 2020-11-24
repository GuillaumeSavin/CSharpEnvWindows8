using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using EnvWindows8.Données;
using Windows.UI.Popups;
using System;
using Windows.UI.Xaml.Navigation;
// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EnvWindows8
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Contacts donneesContacts = new Contacts();
        private Contact contactSelect;
        private Contact inputContact = new Contact("", "", "");

        public MainPage()
        {
            this.InitializeComponent();
            ResourceDictionary test = Application.Current.Resources;
            if (test.Count == 0)
            {
                donneesContacts.chargementContacts();
            }
            else
            {
                donneesContacts = (Contacts)Application.Current.Resources["Data"];
            }
            listeContacts.DataContext = donneesContacts;
        }

        private void listeContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listeContacts.SelectedIndex != -1)
            {
                contactSelect = (Contact)listeContacts.SelectedItem;
                Application.Current.Resources["ContactSelected"] = contactSelect;
                Application.Current.Resources["DataMain"] = donneesContacts;
                uiName.Text = contactSelect.Nom;
                uiPrenom.Text = contactSelect.Prénom;
                uiNumero.Text = contactSelect.Numéro;
            }
        }

        private async void BoutonCreer_Click(object sender, RoutedEventArgs e)
        {
            Contact empty = new Contact("","","");
            Application.Current.Resources["ContactSelected"] = empty;
            Application.Current.Resources["DataMain"] = donneesContacts;
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(ContactPage));
        }

        private async void BoutonModifier_Click(object sender, RoutedEventArgs e)
        {
            if(listeContacts.SelectedItem == null)
            {
                var dialog = new MessageDialog("Veuillez sélectionner un contact dans le liste avant de modifier");
                await dialog.ShowAsync();
            }
            else
            {
                contactSelect = (Contact)listeContacts.SelectedItem;
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(ContactPage));                
                
            }
        }

        private async void BoutonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (listeContacts.SelectedItem == null)
            {
                var dialog = new MessageDialog("Veuillez sélectionner un contact dans le liste avant de le supprimer");
                await dialog.ShowAsync();
            }
            else
            {
                contactSelect = (Contact)listeContacts.SelectedItem;
                listeContacts.SelectedIndex = -1;
                donneesContacts.Remove(contactSelect);
                var dialog = new MessageDialog("Suppression du Contact : \n\tNom : " + contactSelect.Nom + " \n\tPrénom : " + contactSelect.Prénom + " \n\tNuméro : " + contactSelect.Numéro);
                await dialog.ShowAsync();
            }
        }        
    }
}
