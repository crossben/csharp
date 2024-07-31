using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetierSharedMemory.Model
{
    public class Encadreur:Personne
    {
        // L'attribut 'Specialite' représente la spécialité de l'encadreur
        // Affichage du nom de l'attribut dans l'interface utilisateur

        [Display(Name = "Spécialite de l'encadreur")]
        // Définition de la longueur maximale de l'attribut 'Specialite' à 80 caractères

        [MaxLength(80, ErrorMessage = "La taille maximale est de 80"), Required(ErrorMessage = "*")]
        // Définition de l'attribut 'Specialite' comme étant requis avec un message d'erreur personnalisé
        public string Specialite { get; set; }
    }
}