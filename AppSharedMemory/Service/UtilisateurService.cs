using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AppSharedMemory.Model;
using System.Configuration;
using Newtonsoft.Json;
using System.Text;
using static System.Windows.Forms.LinkLabel;
using System.Collections;

namespace AppSharedMemory.Service
{
    public class UtilisateurService
    {
        /// <summary>
        /// Récupère une liste d'utilisateurs depuis le serveur.
        /// </summary>
        /// <returns>Liste d'utilisateurs</returns>
        public List<Utilisateur> GetListUtilisateur()
        {
            HttpClient client = new HttpClient();
            var utilisateurs = new List<Utilisateur>();
            

            client.BaseAddress = new Uri(System.Configuration.ConfigurationSettings.AppSettings["linkServeurphp"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync("api/list.php").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                utilisateurs = JsonConvert.DeserializeObject<List<Utilisateur>>(responseData);
            }
            return utilisateurs;
        }

        /// <summary>
        /// Ajoute ou met à jour un utilisateur sur le serveur.
        /// </summary>
        /// <param name="utilisateur">Objet Utilisateur à ajouter ou mettre à jour</param>
        /// <returns>Vrai si l'opération est réussie, sinon faux</returns>
        public bool AddUtilisateur(Utilisateur utilisateur)
        {
            bool rep = false;

            string Id = utilisateur.Id > 0 ? utilisateur.Id.ToString() : "0";
            var values = new Dictionary<string, string>
            {
                { "Id", Id },
                { "nom", utilisateur.nom },
                { "prenom", utilisateur.prenom },
                { "age", utilisateur.age.ToString() }
            };
            var content = new FormUrlEncodedContent(values);

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationSettings.AppSettings["linkServeurphp"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PostAsync("api/create.php", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        rep = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
            }

            return rep;
        }

        /// <summary>
        /// Supprime un utilisateur sur le serveur.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur à supprimer</param>
        /// <returns>Vrai si l'opération est réussie, sinon faux</returns>
        public bool DeleteUtilisateur(int id)
        {
            bool result = false;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationSettings.AppSettings["linkServeurphp"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Assurez-vous que l'URL est correctement formatée pour inclure l'ID
                    var response = client.DeleteAsync($"api/delete.php?id={id}").Result;
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

        /// <summary>
        /// Met à jour un utilisateur sur le serveur.
        /// </summary>
        /// <param name="utilisateur">Objet Utilisateur à mettre à jour</param>
        /// <returns>Vrai si l'opération est réussie, sinon faux</returns>
        public bool UpdateUtilisateur(Utilisateur utilisateur)
        {
            bool result = false;

            var json = JsonConvert.SerializeObject(utilisateur);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationSettings.AppSettings["linkServeurphp"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Assurez-vous que l'URL est correctement formatée pour inclure l'ID
                    var response = client.PutAsync($"api/update.php?id={utilisateur.Id}", content).Result;
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

        /// <summary>
        /// Récupère un utilisateur par son identifiant depuis le serveur.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur</param>
        /// <returns>Objet Utilisateur</returns>
        public Utilisateur GetUtilisateurById(int id)
        {
            Utilisateur utilisateur = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationSettings.AppSettings["linkServeurphp"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    var response = client.GetAsync($"api/details.php?id={id}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = response.Content.ReadAsStringAsync().Result;
                        utilisateur = JsonConvert.DeserializeObject<Utilisateur>(responseData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
            }

            return utilisateur;
        }
    }
}
