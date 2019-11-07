using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

        private List<Stock> listeStock;
        private Stock AchatStock;

        private List<Commande> listeCommande;
        private Commande comd;

        private List<Achat> listeAchat;
        private Achat achat;

        private byte[] _imageBytes = null;
        private string img ;
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
            System.Windows.Data.CollectionViewSource commandeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("commandeViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // commandeViewSource.Source = [source de données générique]
            System.Windows.Data.CollectionViewSource achatViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("achatViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // achatViewSource.Source = [source de données générique]
            System.Windows.Data.CollectionViewSource stockViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stockViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // stockViewSource.Source = [source de données générique]

        }

        public void actualiser()
        {

            listePro = donnees.listeProduits();
            produitDataGrid.DataContext = listePro;// afficher la liste des produits dans la grid

            listeAchat = donnees.listeAchats();
            achatDataGrid.DataContext = listeAchat;

            listeCommande = donnees.listeCommand();
            commandeDataGrid.DataContext = listeCommande;

            listeStock = donnees.listeProduits_Stock();
            stockDataGrid.DataContext = listeStock;

            listeBoxProduit.ItemsSource = listeStock; // ajout de la liste des produit dans le combobox
            listeBoxProduit.DisplayMemberPath = "nom_produit_stock";//suite
            listeBoxProduit.SelectedIndex = 0;//suite

            cb_liste_produit.ItemsSource = listeStock; // ajout de la liste des produit coté stock dans le combobox
            cb_liste_produit.DisplayMemberPath = "nom_produit_stock";//suite
            cb_liste_produit.SelectedIndex = 0;//suite


            listclient = donnees.listeClients();
            clientDataGrid.DataContext = listclient;


            listeclientbox.ItemsSource = listclient; // ajout de la liste des clients dans le combobox
            listeclientbox.DisplayMemberPath = "nom";
            listeclientbox.SelectedIndex = 0;
            txtNom.Text = txtPrenom.Text = txtEmail.Text = txtPassword.Password = txtNomProduit.Text = "";
            txtPrixProduit.Text = txtQuantite.Text = "";
            txt_Local_ImagePath.Text = txt_Web_ImagePath.Text= "";
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


            }
            catch (Exception)
            {
            }
        }

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
            MessageBox.Show("stock mis à jour!!");
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nomClient = listeclientbox.Text;
            string nomProduit = listeBoxProduit.Text;
            string quantite = txtquantite_cmd.Text;
            donnees.passerCommande(nomClient, nomProduit, Convert.ToInt32(quantite));
            actualiser();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mon_tabcontrol.SelectedIndex = 4;

        }

        private void txt_chercher_image_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                Filter = "Images (*.jpg,*.png)|*.jpg;*.png|All Files(*.*)|*.*"
            };

            if (dialog.ShowDialog() != true) { return; }

            txt_Local_ImagePath.Text = dialog.FileName;
            MyImage.Source = new BitmapImage(new Uri(txt_Local_ImagePath.Text));
            img = txt_Local_ImagePath.Text;
           

          /*  using (var fs = new FileStream(txt_ImagePath.Text, FileMode.Open, FileAccess.Read))
            {
                _imageBytes = new byte[fs.Length];
                fs.Read(_imageBytes, 0, System.Convert.ToInt32(fs.Length));
            }*/
        }

        private void txt_ajouter_image_Click(object sender, RoutedEventArgs e)
        {
            AchatStock = new Stock();
            listeStock = donnees.listeProduits_Stock();
            var stock = listeStock[cb_liste_produit.SelectedIndex];
            AchatStock.nom_produit_stock = stock.nom_produit_stock;
            // AchatStock.image_Produit = _imageBytes;
            AchatStock.image_test = txt_Web_ImagePath.Text;
            donnees.modifier_Image_Stock(this.AchatStock);
            actualiser();

        }

        private void recuperer_image_Click(object sender, RoutedEventArgs e)
        {
            listeStock = donnees.listeProduits_Stock();
            try{
            var imga = listeStock[cb_liste_produit.SelectedIndex].image_test;

            MImage.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(imga);}catch(Exception){}
            /*  if (img != null)
              {
                  // Display the loaded image
                  MyImage.Source = new BitmapImage(new Uri(img.nom_produit_stock));
              }*/

            /* Stream StreamObj = new MemoryStream(img); //code permettant de recuperer l'image de la base de donnée

             BitmapImage BitObj = new BitmapImage();

             BitObj.BeginInit();

             BitObj.StreamSource = StreamObj;

             BitObj.EndInit();

             this.MImage.Source = BitObj;*/
        }

        private void cb_liste_produit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            
            listeStock = donnees.listeProduits_Stock();
            try{
            var imga = listeStock[cb_liste_produit.SelectedIndex].image_test;

            MImage.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(imga);}catch(Exception){}

            /*listeStock = donnees.listeProduits_Stock();
            try
            {
                var img = listeStock[cb_liste_produit.SelectedIndex].image_Produit;
                Stream StreamObj = new MemoryStream(img); //code permettant de recuperer l'image de la base de donnée

                BitmapImage BitObj = new BitmapImage();

                BitObj.BeginInit();

                BitObj.StreamSource = StreamObj;

                BitObj.EndInit();

                this.MImage.Source = BitObj;
            }
            catch (Exception) { }*/
        }
    }
}
