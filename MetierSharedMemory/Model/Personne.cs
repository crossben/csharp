using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetierSharedMemory.Model
{
    // Classe représentant une personne
    public class Personne
    {
        // Clé primaire pour la table des personnes
        [Key]
        public int IdPersonne { get; set; }

        // Nom de la personne
        [Display(Name = "Nom")]
        // Longueur maximale de 80 caractères et champ requis
        [MaxLength(80, ErrorMessage = "La taille maximale est de 80"), Required(ErrorMessage = "*")]
        public string Nom { get; set; }

        // Prénom de la personne
        [Display(Name = "Prénom")]
        // Longueur maximale de 80 caractères et champ requis
        [MaxLength(80, ErrorMessage = "La taille maximale est de 80"), Required(ErrorMessage = "*")]
        public string Prenom { get; set; }
    }
}