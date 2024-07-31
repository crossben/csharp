using MetierSharedMemory.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MetierSharedMemory.Utils
{
    public class Logger
    {
        BdGp2SharedMemoryContext db = new BdGp2SharedMemoryContext ();

        /// <summary>
        /// Enregistre une erreur dans la base de données.
        /// </summary>
        /// <param name="TitreErreur">Le titre de l'erreur.</param>
        /// <param name="erreur">La description de l'erreur.</param>
        public void WriteDataError(string TitreErreur, string erreur)
        {
            try
            {
                Td_Erreur log = new Td_Erreur();
                log.DateErreur = DateTime.Now;
                log.DescriprionErreur = erreur.Length > 3000 ? erreur.Substring(0, 3000) : erreur; log.TitreErreur = TitreErreur;
                db.td_Erreurs.Add(log);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogSystem(ex.ToString(), "WriteDataEx");
            }
        }

        /// <summary>
        /// Cette méthode enregistre les logs au niveau du système.
        /// </summary>
        /// <param name="libelle">Le libellé de l'erreur.</param>
        /// <param name="erreur">La description de l'erreur.</param>
        public static void WriteLogSystem(string libelle, string erreur)
        {
            using(EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Shared Memory";
                eventLog.WriteEntry(string.Format("Libellé: {0}, Erreur: {1}, Date: {2}", libelle, erreur, DateTime.Now), EventLogEntryType.Error);
            }
        }
    }
}