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

namespace PharmacieEthodet
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Donnees donnees = new Donnees();

        public MainWindow()
        {
            InitializeComponent();

        }

        private void BtnInscription_Click(object sender, RoutedEventArgs e)
        {
            donnees.ajouterclients(txtNom.Text, txtPrenom.Text, txtEmail.Text, txtPassword.Password);
            vider(); MessageBox.Show("Ajouter avec succes!");

        }
        public void vider()
        {
            txtNom.Text = txtPrenom.Text = txtEmail.Text = txtPassword.Password = "";
            conEmail.Text = conPassword.Password = "";
        }

        private void BtnConnexion_Click(object sender, RoutedEventArgs e)

        {
          /*  if (string.IsNullOrEmpty(conEmail.Text) || string.IsNullOrEmpty(conPassword.Password)){
                error.Content = "Champ vide !!!";
                vider();
            }
            else {
                bool trouve = donnees.verifierClients(conEmail.Text, conPassword.Password);
                if (trouve)
                {*/
                    Fenetre1 fenetre1 = new Fenetre1();
                    fenetre1.Show();
                    this.Close();
               /* } else
                    error.Content = "Erreur !!!";
                vider();
            }*/
        } }
}
