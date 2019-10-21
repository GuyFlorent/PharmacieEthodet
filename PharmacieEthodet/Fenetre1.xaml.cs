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
using System.Windows.Shapes;

namespace PharmacieEthodet
{
    /// <summary>
    /// Logique d'interaction pour Fenetre1.xaml
    /// </summary>
    public partial class Fenetre1 : Window
    {
        Donnees donnees = new Donnees();
        private List<Client> listclient;
        private Client cli;
        private List<Produit> listePro;
        private Produit prod;
        public Fenetre1()
        {
            InitializeComponent();
            actualiser();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource clientViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // clientViewSource.Source = [source de données générique]
            System.Windows.Data.CollectionViewSource produitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("produitViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // produitViewSource.Source = [source de données générique]
        }

        public void actualiser()
        {
            listePro = donnees.listeProduits();
            produitDataGrid.DataContext = listePro;
            listclient = donnees.listeClients();
            clientDataGrid.DataContext = listclient;
            txtNom.Text = txtPrenom.Text = txtEmail.Text = txtPassword.Password = txtNomProduit.Text= "";
            txtPrixProduit.Text = "";
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            donnees.ajouterclients(txtNom.Text, txtPrenom.Text, txtEmail.Text, txtPassword.Password);
            actualiser();
            
        }

        private void BtnModier_Click(object sender, RoutedEventArgs e)
        {
            /*int i = clientDataGrid.SelectedIndex;
            this.cli = listclient[i];*/
            this.cli.nom = txtNom.Text;
            this.cli.prenom = txtPrenom.Text;
            this.cli.email = txtEmail.Text;
            this.cli.password = txtPassword.Password;
            donnees.modifierClients(this.cli);
            actualiser();
           
        }

        private void ClientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int i = clientDataGrid.SelectedIndex;
                this.cli = listclient[i];
                txtNom.Text = cli.nom;
                txtPrenom.Text = cli.prenom;
                txtEmail.Text = cli.email;
                txtPassword.Password = cli.password;
                donnees.modifierClients(this.cli);
                listclient = donnees.listeClients();
                clientDataGrid.DataContext = listclient;
               

            } catch (Exception) { 
            } }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(" etes vous sur de vouloir supprimer ce client?", "Allerte !!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                donnees.supprimerClients(this.cli);
                actualiser();
                MessageBox.Show("supprimer avec succes");
            }
        }

        private void BtnAjouterProduit_Click(object sender, RoutedEventArgs e)
        {
            donnees.ajouterProduits(txtNomProduit.Text, Convert.ToDecimal(txtPrixProduit.Text), Convert.ToInt32(txtQuantite.Text));
            actualiser();
        }

        private void BtnAModifierProduit_Click(object sender, RoutedEventArgs e)
        {
            this.prod.nom_produit = txtNomProduit.Text;
            this.prod.prix_unite = Convert.ToDecimal(txtPrixProduit.Text);
            this.prod.quantite = Convert.ToInt32(txtQuantite.Text);
            donnees.modifierProduit(this.prod);
            actualiser();

        }

        private void ProduitDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int i = produitDataGrid.SelectedIndex;
                this.prod = listePro[i];
                txtPrixProduit.Text = prod.prix_unite.ToString();
                txtNomProduit.Text = prod.nom_produit;
                txtQuantite.Text = prod.quantite.ToString();
               donnees.modifierProduit(this.prod);
                listePro = donnees.listeProduits();
                produitDataGrid.DataContext = listePro;
            }
            catch (Exception) { }
        }

        private void BtnSupprimerProduit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(" etes vous sur de vouloir supprimer ce produit?", "Allerte !!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                donnees.supprimerProduit(this.prod);
                actualiser();
                MessageBox.Show("supprimer avec succes");
            }
        }
    }
}
