using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetierSharedMemory.Model
{
    // Classe représentant un mémoire
    public class Memoire
    {
        // Clé primaire pour la table des mémoires
        [Key]
        public int IdMemoire { get; set; }

        // Sujet du mémoire
        [Display(Name = "Sujet du mémoire")]
        // Longueur maximale de 80 caractères et champ requis
        [MaxLength(80, ErrorMessage = "La taille maximale est de 80"), Required(ErrorMessage = "*")]
        public string Sujet { get; set; }

        // Nom du document
        [Display(Name = "Nom du document")]
        // Longueur maximale de 80 caractères et champ requis
        [MaxLength(80, ErrorMessage = "La taille maximale est de 80"), Required(ErrorMessage = "*")]
        public string Filename { get; set; }

        // Année du mémoire, champ requis
        [Required(ErrorMessage = "*")]
        public int Annee { get; set; }

        // Clé étrangère facultative pour l'encadreur
        public int? IdEncadreur { get; set; }

        // Définition de la relation avec la classe Encadreur via la clé étrangère IdEncadreur
        [ForeignKey("IdEncadreur")]
        public Encadreur Encadreur { get; set; }
    }
}