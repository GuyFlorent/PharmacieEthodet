using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (productToAdd == null)
            {
                // Création objet produit
                productToAdd = new Produit();
                productToAdd.nom_produit = nom;
                productToAdd.prix_unite = prix;
                productToAdd.quantite = quantite;

                // Enregistrement dans la table produits et dans la table stock
                dbContext.Produits.Add(productToAdd);
                ajaouterStock(productToAdd);
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
        public void ajaouterStock(Produit produit)
        {
            Stock nouveauStock = new Stock();
            nouveauStock.quantite_produit = produit.quantite;
            dbContext.Stocks.Add(nouveauStock);
            dbContext.SaveChanges();
            // Je veux récupérer l'ID stock de la ligne créée
            var tempProd = dbContext.Stocks.FirstOrDefault(f => f.id_stock == nouveauStock.id_stock);
            produit.id_stock = tempProd.id_stock;
            dbContext.SaveChanges();
        }




    }
}
