using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PharmacieEthodet
{
    class Donnees
    {
        PharmacieEthodetEntities dbContext = new PharmacieEthodetEntities();
       public List<Client> listeClients()
        {
           var liste = dbContext.Clients.ToList();
            return liste;
        }

        public void ajouterclients(string nom, string prenom, string email, string pass)
        {
            var client = new Client();
            client.nom = nom;
            client.prenom = prenom;
            client.email = email;
            client.password = pass;
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();
        }

        public bool verifierClients(string email, string pass)
        {
            
            //var verifie = dbContext.Clients.Where(f => f.email == email).Where(s => s.password == pass).FirstOrDefault();
            var verifie = dbContext.Clients.FirstOrDefault(f => f.email == email && f.password == pass);
            if (verifie != null)
            {
                return true;
            }
            else return false;

        }
        public void modifierClients(Client client)
        {
            var modif = dbContext.Clients.FirstOrDefault(f => f.id_client == client.id_client);
            modif.nom = client.nom;
            modif.prenom = client.prenom;
            modif.email = client.email;
            modif.password = client.password;
            dbContext.SaveChanges();
        }
        public void supprimerClients(Client client)
        {
            var suprime = dbContext.Clients.FirstOrDefault(f => f.id_client == client.id_client);
            dbContext.Clients.Remove(suprime);
            dbContext.SaveChanges();
        }
        public List<Produit> listeProduits()
        {
            var liste = dbContext.Produits.ToList();
            return liste;
        }
        public void ajouterProduits(string nom, decimal prix, int quantite)
        {
            // Recherche si le produit existe déjà
            Produit productToAdd = dbContext.Produits.FirstOrDefault(f => f.nom_produit == nom && f.prix_unite == prix);

            // Si le produit n'existe pas

            // Création objet produit
            if (productToAdd == null )
            {
                productToAdd = new Produit();
                productToAdd.nom_produit = nom;
                productToAdd.prix_unite = prix;
                productToAdd.quantite = quantite;
                productToAdd.date_heure_ajout_produit = DateTime.Now.ToString();

            // Enregistrement dans la table produits et dans la table stock
          
                dbContext.Produits.Add(productToAdd);
                ajaouterStock(productToAdd);
                dbContext.SaveChanges();
            } else //si le produit existe, on création du nouveau produit en gardant le meme ID stock pour faire ensuite la sommme dans stock
            {
                productToAdd = new Produit();
                productToAdd.nom_produit = nom;
                productToAdd.prix_unite = prix;
                productToAdd.quantite = quantite;
                productToAdd.date_heure_ajout_produit = DateTime.Now.ToString();
                var stoc = dbContext.Produits.FirstOrDefault(f=> f.nom_produit == nom && f.prix_unite == prix);
                productToAdd.id_stock = stoc.id_stock;
                dbContext.Produits.Add(productToAdd);
                dbContext.SaveChanges();
                //ajaouterStock(productToAdd);
                updateStock(productToAdd);
                dbContext.SaveChanges();
            }
           
            // Le produit existe déjà, on va mettre à jour la quantité ET mettre à jour le stock



            // Gestion ID Stock ?
        }

        private void updateStock(Produit productToAdd)
        {
            Stock stockToUpdate = dbContext.Stocks.FirstOrDefault(f => f.id_stock == productToAdd.id_stock);

            int nouveauStock = (int)dbContext.Produits.Where(f => f.id_stock == stockToUpdate.id_stock).Sum(f => f.quantite);

            stockToUpdate.quantite_produit = nouveauStock;
            dbContext.SaveChanges();
        }

        public void modifierProduit(Produit pro)
        {
            var modifPro = dbContext.Produits.FirstOrDefault(f => f.id_produit == pro.id_produit);
            modifPro.nom_produit = pro.nom_produit;
            modifPro.prix_unite = pro.prix_unite;
            modifPro.quantite = pro.quantite;
            dbContext.SaveChanges();
            updateStock(modifPro);
        }

        public void supprimerProduit(Produit pro)
        {
            var suppprime = dbContext.Produits.FirstOrDefault(f => f.id_produit == pro.id_produit);
            dbContext.Produits.Remove(suppprime);
            dbContext.SaveChanges();
        }
        
        // Méthode permettant de mettre à jour la quantité d'un produit ayant le même ID
        //cets bien
        public void ajaouterStock(Produit produit)
        {
            Stock nouveauStock = new Stock();
            nouveauStock.quantite_produit = produit.quantite;
            dbContext.Stocks.Add(nouveauStock);
            dbContext.SaveChanges();
            // Je veux récupérer l'ID stock de la ligne créée
            var tempProd = dbContext.Stocks.FirstOrDefault(f => f.id_stock == nouveauStock.id_stock);
            produit.id_stock = tempProd.id_stock;
            tempProd.nom_produit_stock = produit.nom_produit;
            dbContext.SaveChanges();
        }

        public void modifier_Image_Stock(Stock stock)//méthode permettant d'ajouter une photo au stock comme un update
        {
            var modif = dbContext.Stocks.FirstOrDefault(s => s.nom_produit_stock == stock.nom_produit_stock);
            modif.nom_produit_stock = stock.nom_produit_stock;
            modif.image_Produit = stock.image_Produit;
            modif.image_test = stock.image_test;
            dbContext.SaveChanges();
        }
        public List<Stock> listeProduits_Stock()
        {
            var liste = dbContext.Stocks.ToList();
            return liste;
        }
        // on passe à la commande avec cluent en parametre

        public void passerCommande(string nomclient, string nomproduit,int quantité)
        {

            //Dictionary<string, object> mesparms = new Dictionary<string, object>();
            //mesparms.Add("nomclient", nomclient);
            //mesparms.Add("nomproduit", nomproduit);
            //mesparms.Add("quantité", quantité);

            //execute(mesparms);

            //int qtey = (int)mesparms["quantité"];
            //string nomproduit2 = (string )mesparms["nomproduit"];

            var cli = dbContext.Clients.FirstOrDefault(f => f.nom == nomclient);
            var prod = dbContext.Stocks.FirstOrDefault(f => f.nom_produit_stock == nomproduit);
            var prixProd = dbContext.Produits.FirstOrDefault(f => f.nom_produit == nomproduit);
            if (prod.quantite_produit >= quantité) // si la quantité est inférieur ou égale a la quantité en stock
            {

                Commande nouvelCommande = new Commande(); // création nouvelle commande
                nouvelCommande.heure_commande = DateTime.Now.ToString();
                nouvelCommande.statut_commande = "Validé";
                nouvelCommande.statut_livraison = "Livré";
                nouvelCommande.id_client = cli.id_client;  // id_client dans commande sera egale au meme id_client dans client car le meme nom de client
                dbContext.Commandes.Add(nouvelCommande); //creation de la commande

                Achat nouvelAchat = new Achat();//reation nouvel achat
                nouvelAchat.quantité = quantité;
                nouvelAchat.id_stock = prod.id_stock; // id_stoc dans achat sera egale au meme id_stock dans produit car le meme nom de produit
                prod.quantite_produit -= quantité;
                nouvelAchat.prix_total = quantité * prixProd.prix_unite;
                dbContext.Achats.Add(nouvelAchat);


                dbContext.SaveChanges();

                var com = dbContext.Commandes.FirstOrDefault(f => f.id_commande == nouvelCommande.id_commande);
                var achat = dbContext.Achats.FirstOrDefault(f => f.id_achat == nouvelAchat.id_achat);
                achat.id_commande = com.id_commande; // recupération de id de la nouvelle commande

                dbContext.SaveChanges();

            }
            else
                MessageBox.Show("la quantité de produit est insuffissant  !!! \n verifier stock!");
        }

        // méthode pour retpuner la liste des commande
        public List<Commande> listeCommand()
        {
            var liste = dbContext.Commandes.ToList();
            return liste;
        }
        public List<Achat> listeAchats()
        {
            var liste = dbContext.Achats.ToList();
            return liste;
        }
    }
   
}
