using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AppSharedMemory.Model;
using System.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace AppSharedMemory.Service
{
    public class CategorieService
    {
        /// <summary>
        /// Récupère une liste de catégories depuis le serveur.
        /// </summary>
        /// <returns>Liste de catégories</returns>
        public List<Categorie> ServGetListCategorie()
        {
            // Initialisation du HttpClient
            HttpClient client = new HttpClient();
            var services = new List<Categorie>();

            // Définir l'adresse de base de l'API depuis le fichier de configuration
            client.BaseAddress = new Uri(System.Configuration.ConfigurationSettings.AppSettings["linkServeur"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Envoyer une requête GET pour récupérer les catégories
            var response = client.GetAsync("groupe2/Categories/GetCategorie").Result;

            // Si la requête est réussie, analyser les données de la réponse
            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                services = JsonConvert.DeserializeObject<List<Categorie>>(responseData);
            }

            return services;
        }

        /// <summary>
        /// Ajoute ou met à jour une catégorie sur le serveur.
        /// </summary>
        /// <param name="cat">Objet Catégorie à ajouter ou mettre à jour</param>
        /// <returns>Vrai si l'opération est réussie, sinon faux</returns>
        public bool AddCategorie(Categorie cat)
        {
            bool rep = false;

            // Préparer les données de la catégorie à envoyer dans la requête POST
            string Id = cat.IdCategorie > 0 ? cat.IdCategorie.ToString() : "0";
            var values = new Dictionary<string, string>
            {
                { "IdCategorie", Id },
                { "CodeCategorie", cat.CodeCategorie },
                { "LibelleCategorie", cat.LibelleCategorie }
            };
            var content = new FormUrlEncodedContent(values);

            try
            {
                // Initialisation du HttpClient
                using (var client = new HttpClient())
                {
                    // Définir l'adresse de base de l'API depuis le fichier de configuration
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationSettings.AppSettings["linkServeur"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Envoyer une requête POST pour ajouter ou mettre à jour la catégorie
                    var response = client.PostAsync("groupe2/Categories/PostCategorie", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        rep = true; // L'opération a réussi
                    }
                }
            }
            catch (Exception ex)
            {
                // Enregistrer l'exception ou la gérer de manière appropriée
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
            }

            return rep;
        }

        /// <summary>
        /// Supprime une catégorie sur le serveur.
        /// </summary>
        /// <param name="id">Identifiant de la catégorie à supprimer</param>
        /// <returns>Vrai si l'opération est réussie, sinon faux</returns>
        public bool DeleteCategorie(int id)
        {
            bool rep = false;

            try
            {
                // Initialisation du HttpClient
                using (var client = new HttpClient())
                {
                    // Définir l'adresse de base de l'API depuis le fichier de configuration
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationSettings.AppSettings["linkServeur"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Envoyer une requête DELETE pour supprimer la catégorie
                    var response = client.DeleteAsync($"groupe2/Categories/DeleteCategorie/{id}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        rep = true; // L'opération a réussi
                    }
                }
            }
            catch (Exception ex)
            {
                // Enregistrer l'exception ou la gérer de manière appropriée
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
            }

            return rep;
        }

        /// <summary>
        /// Met à jour une catégorie sur le serveur.
        /// </summary>
        /// <param name="cat">Objet Catégorie à mettre à jour</param>
        /// <returns>Vrai si l'opération est réussie, sinon faux</returns>
        public bool UpdateCategorie(Categorie cat)
        {
            bool result = false;

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(cat);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationSettings.AppSettings["linkServeur"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PutAsync($"groupe2/Categories/PutCategorie/{cat.IdCategorie}", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
            }

            return result;
        }

        // Autres méthodes...

        /// <summary>
        /// Récupère une catégorie par son identifiant depuis le serveur.
        /// </summary>
        /// <param name="id">Identifiant de la catégorie</param>
        /// <returns>Objet Categorie</returns>
        public Categorie GetCategorieById(int id)
        {
            Categorie categorie = null;

            try
            {
                // Initialisation du HttpClient
                using (var client = new HttpClient())
                {
                    // Définir l'adresse de base de l'API depuis le fichier de configuration
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationSettings.AppSettings["linkServeur"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Envoyer une requête GET pour récupérer la catégorie par ID
                    var response = client.GetAsync($"groupe2/Categories/GetCategorie/{id}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = response.Content.ReadAsStringAsync().Result;
                        categorie = JsonConvert.DeserializeObject<Categorie>(responseData);
                    }
                }
            }
            catch (Exception ex)
            {
                // Enregistrer l'exception ou la gérer de manière appropriée
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
            }

            return categorie;
        }




    }
}
