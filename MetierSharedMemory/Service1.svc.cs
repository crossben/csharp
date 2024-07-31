using MetierSharedMemory.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MetierSharedMemory.Utils;

namespace MetierSharedMemory
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {
        private readonly BdGp2SharedMemoryContext db;
        private readonly Logger logger;

        public Service1()
        {
            db = new BdGp2SharedMemoryContext();
            logger = new Logger();
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        /// <summary>
        /// </summary>
        /// permet lenregistrment d'un encadreur
        /// <param name="encadreur"></param>
        /// <returns></returns>
        public bool AddEncadreur(Encadreur encadreur)
        {
           try
            {
                db.encadreurs.Add(encadreur);
                db.SaveChanges();
                return true;
            }

            catch (Exception ex)
            {

                logger.WriteDataError("Service1-AddEncadreur", ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="encadreur"></param>
        /// <returns></returns>
        
            public bool UpdateEncadreur(Encadreur encadreur)
            {
            
               try
               {
                var leEncadreur = db.encadreurs.Find(encadreur.IdPersonne);
                   if (leEncadreur != null)
                   {
                    leEncadreur.Prenom = encadreur.Prenom;
                    leEncadreur.Nom = encadreur.Nom;
                    db.SaveChanges();
                    return true;
                   }
               }
                 catch (Exception ex)
                 {
                 logger.WriteDataError("Service1-UpdateEncadreur", ex.ToString());
                 return false;
                 }
                  return false;

            }

        /// <summary>
        /// </summary>
        /// <param name="IdEncadreur"></param>
        /// <returns></returns>
        public bool DeleteEncadreur(int? IdEncadreur)
        {

               try
               {
                var leEncadreur = db.encadreurs.Find(IdEncadreur);
                   if (leEncadreur != null)
                   {

                    db.encadreurs.Remove(leEncadreur);
                    db.SaveChanges();
                    return true;
                   }
                   
               }
               catch (Exception ex)
               {
                logger.WriteDataError("Service1-DeleteEncadreur", ex.ToString());
                return false;
               }
                  return false;

        }

        /// <summary> 
        /// </summary>
        /// <param name="IdEncadreur"></param>
        /// <returns></returns>
        public List<Encadreur> GetAllEncadreur()
        {
            return db.encadreurs.ToList();
        }
        /// <summary>
        /// </summary>
        /// <param name="IdEncadreur"></param>
        /// <returns></returns>
        public Encadreur GetEncadreur(int? IdEncadreur)
        {
            return db.encadreurs.Find(IdEncadreur);
        }
        /// <summary>
        /// </summary>
        /// <param name="Nom"></param>
        /// <param name="Prenom"></param>
        /// <param name="Specialite"></param>
        /// <returns></returns>
        public List<Encadreur> GetEncadreurs(string Nom, string Prenom, string Specialite)
        {
            var liste = db.encadreurs.ToList();
            if (!string.IsNullOrEmpty(Nom))
            {
                liste = liste.Where(a => a.Nom.ToUpper().Contains(Nom.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(Prenom))
            {
                liste = liste.Where(a => a.Prenom.ToUpper().Contains(Prenom.ToUpper())).ToList();
            }
           
            if (!string.IsNullOrEmpty(Specialite))
            {
                liste = liste.Where(a => a.Specialite.ToUpper().Contains(Specialite.ToUpper())).ToList();
            }
            return liste;
        }
    }

}
