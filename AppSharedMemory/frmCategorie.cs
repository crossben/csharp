using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppSharedMemory.Service;
using AppSharedMemory.Model;


namespace AppSharedMemory
{
    public partial class frmCategorie : Form
    {
        public frmCategorie()
        {
            InitializeComponent();
        }
        CategorieService service = new CategorieService();
        private void frmCategorie_Load(object sender, EventArgs e)
        {
            dgCategorie.DataSource = service.ServGetListCategorie();
            
        }

        /// <summary>
        /// Ajoute une nouvelle catégorie
        /// </summary>
        private void btnAjouter_Click(object sender, EventArgs e)
        {

             Categorie cat = new Categorie();
            cat.CodeCategorie = txtCode.Text;
            cat.LibelleCategorie = txtLibelle.Text;
            service.AddCategorie(cat);
            effacer();
        }

        /// <summary>
        /// Efface les champs de texte et recharge la liste des catégories
        /// </summary>
        private void effacer()
        {
            txtLibelle.Text = string.Empty;
            txtCode.Text = string.Empty;
            dgCategorie.DataSource = service.ServGetListCategorie();
            txtCode.Focus();
        }

        /// <summary>
        /// Supprime la catégorie sélectionnée
        /// </summary>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (dgCategorie.SelectedRows.Count > 0)
            {
                var selectedRow = dgCategorie.SelectedRows[0];
                int idCategorie = Convert.ToInt32(selectedRow.Cells["IdCategorie"].Value);

                bool result = service.DeleteCategorie(idCategorie);
                if (result)
                {
                    MessageBox.Show("Catégorie supprimée avec succès.");
                }
                else
                {
                    MessageBox.Show("Échec de la suppression de la catégorie.");
                }

                effacer();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une catégorie à supprimer.");
            }
        }


        /// <summary>
        /// Modifie la catégorie sélectionnée
        /// </summary>
        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (dgCategorie.SelectedRows.Count > 0)
            {
                var selectedRow = dgCategorie.SelectedRows[0];
                int idCategorie = Convert.ToInt32(dgCategorie.SelectedRows[0].Cells["IdCategorie"].Value);

                Categorie cat = new Categorie
                {
                    IdCategorie = idCategorie,
                    CodeCategorie = txtCode.Text,
                    LibelleCategorie = txtLibelle.Text
                };

                bool result = service.UpdateCategorie(cat);
                if (result)
                {
                    MessageBox.Show("Catégorie mise à jour avec succès.");
                }
                else
                {
                    MessageBox.Show("Échec de la mise à jour de la catégorie.");
                }

                effacer();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une catégorie à modifier.");
            }
        }

        /// <summary>
        /// Sélectionne une catégorie et remplit les champs de texte avec ses informations
        /// </summary>
        private void btnSelect_Click(object sender, EventArgs e)
        {
           
                if (dgCategorie.CurrentRow != null)
                {
                    int id = Convert.ToInt32(dgCategorie.CurrentRow.Cells["IdCategorie"].Value);
                    Categorie cat = service.GetCategorieById(id); // Assurez-vous que cette méthode est implémentée

                    if (cat != null)
                    {
                        txtCode.Text = cat.CodeCategorie;
                        txtLibelle.Text = cat.LibelleCategorie;
                    }
                }
            
        }
    }
 }

