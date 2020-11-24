using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using EnvWindows8.Données;
using Windows.UI.Popups;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace EnvWindows8
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ContactPage : Page
    {
        private Contacts donneesContacts = new Contacts();
        private Contact contactReceived;
        private String status;
        public ContactPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            contactReceived = (Contact)Application.Current.Resources["ContactSelected"];
            donneesContacts = (Contacts)Application.Current.Resources["DataMain"];
            if(contactReceived.Nom == "" && contactReceived.Prénom == "" && contactReceived.Numéro == "")
            {
                status = "Créer";
            }
            else
            {
                status = "Modifier";
                uiName.Text = contactReceived.Nom;
                uiPrenom.Text = contactReceived.Prénom;
                uiNumero.Text = contactReceived.Numéro;
            }
            BoutonRetour.IsEnabled = this.Frame.CanGoBack;
        }

        private void BoutonRetour_Click(object sender, RoutedEventArgs e)
        {
            OnRetourRequested();
        }
        
        private async void BoutonValider_Click(object sender, RoutedEventArgs e)
        {
            if(status == "Créer")
            {
                var dialog = new MessageDialog("Création du Contact : \n\tNom : " + uiName.Text + " \n\tPrénom : " + uiPrenom.Text + " \n\tNuméro : " + uiNumero.Text);
                await dialog.ShowAsync();
                donneesContacts.Add(new Contact(uiName.Text, uiPrenom.Text, uiNumero.Text));
            }
            else
            {
                var dialog = new MessageDialog("Modification du Contact : \n\tNom : " + uiName.Text + " \n\tPrénom : " + uiPrenom.Text + " \n\tNuméro : " + uiNumero.Text);
                await dialog.ShowAsync();                
                
                donneesContacts[contactReceived].Nom = uiName.Text;
                donneesContacts[contactReceived].Prénom = uiPrenom.Text;
                donneesContacts[contactReceived].Numéro = uiNumero.Text;
                
            }
            Frame rootFrame = Window.Current.Content as Frame;
            Application.Current.Resources["Data"] = donneesContacts;
            rootFrame.Navigate(typeof(MainPage));
        }

        private bool OnRetourRequested()
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();

                return true;
            }

            return false;
        }

        
    }
}
