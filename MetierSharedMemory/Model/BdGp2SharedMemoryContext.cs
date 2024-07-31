using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MySql.Data.EntityFramework;

namespace MetierSharedMemory.Model
{
    // Spécifie la configuration de l'EF pour utiliser MySQL
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class BdGp2SharedMemoryContext : DbContext
    {
        // Constructeur qui initialise la connexion à la base de données en utilisant la chaîne de connexion "connGp2SharedMemory"
        public BdGp2SharedMemoryContext() : base("connGp2SharedMemory") { }

        // Définition des DbSet pour les entités du modèle
        // DbSet pour l'entité Personne
        public DbSet<Personne> personnes { get; set; }

        // DbSet pour l'entité Encadreur
        public DbSet<Encadreur> encadreurs { get; set; }

        // DbSet pour l'entité Memoire
        public DbSet<Memoire> memoires { get; set; }

        // DbSet pour l'entité Td_Erreur
        public DbSet<Td_Erreur> td_Erreurs { get; set; }
    }
}