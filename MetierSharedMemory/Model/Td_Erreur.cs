using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MetierSharedMemory.Model
{  //
    public class Td_Erreur
    { // Clé primaire pour la table Td_Erreur
        [Key]
        public int Id { get; set; }

        // Date de l'erreur, nullable
        public Nullable<System.DateTime> DateErreur { get; set; }

        // Description de l'erreur, longueur maximale de 3000 caractères et champ requis
        [MaxLength(3000), Required]
        public string DescriprionErreur { get; set; }

        // Titre de l'erreur, longueur maximale de 3000 caractères et champ requis
        [MaxLength(3000), Required]
        public string TitreErreur { get; set; }
    }
}