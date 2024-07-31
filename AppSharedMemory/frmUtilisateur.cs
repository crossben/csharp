using AppSharedMemory.Model;
using AppSharedMemory.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppSharedMemory
{
    public partial class frmUtilisateur : Form
    {
        public frmUtilisateur()
        {
            InitializeComponent();
        }
        UtilisateurService service = new UtilisateurService();
        private void frmUtilisateur_Load(object sender, EventArgs e)
        {

            dgUtilisateur.DataSource = service.GetListUtilisateur();

        }

        /// <summary>
        /// Ajoute un nouvel utilisateur
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {

            Utilisateur utilisateur = new Utilisateur();
            utilisateur.nom = txtNom.Text;
            utilisateur.prenom = txtPrenom.Text;
            utilisateur.age = int.Parse(txtAge.Text);
            service.AddUtilisateur(utilisateur);
            effacer();

        }




        /// <summary>
        /// Efface les champs de texte et recharge la liste des utilisateurs
        /// </summary>
        private void effacer()
        {
            txtNom.Text = string.Empty;
            txtPrenom.Text = string.Empty;
            txtAge.Text = string.Empty;
            dgUtilisateur.DataSource = service.GetListUtilisateur();
            txtNom.Focus();
        }


        /// <summary>
        /// Supprime l'utilisateur sélectionné
        /// </summary>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (dgUtilisateur.SelectedRows.Count > 0)
            {
                var selectedRow = dgUtilisateur.SelectedRows[0];
                var idCellValue = selectedRow.Cells["Id"].Value.ToString();

                MessageBox.Show($"ID sélectionné : {idCellValue}");

                int id;
                if (!int.TryParse(idCellValue, out id))
                {
                    MessageBox.Show("L'ID sélectionné n'est pas valide.");
                    return;
                }

                bool result = service.DeleteUtilisateur(id);
                if (result)
                {
                    MessageBox.Show("Utilisateur supprimé avec succès.");
                }
                else
                {
                    MessageBox.Show("Échec de la suppression de l'utilisateur.");
                }

                effacer();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à supprimer.");
            }

        }

        /// <summary>
        /// Modifie l'utilisateur sélectionné
        /// </summary>
        private void btnModifier_Click(object sender, EventArgs e)
        {

            if (dgUtilisateur.SelectedRows.Count > 0)
            {
                int id;
                if (!int.TryParse(dgUtilisateur.SelectedRows[0].Cells["Id"].Value.ToString(), out id))
                {
                    MessageBox.Show("L'ID sélectionné n'est pas valide.");
                    return;
                }

                int age;
                if (!int.TryParse(txtAge.Text, out age))
                {
                    MessageBox.Show("Veuillez entrer un âge valide.");
                    return;
                }

                var utilisateur = new Utilisateur
                {
                    Id = id,
                    nom = txtNom.Text,
                    prenom = txtPrenom.Text,
                    age = age
                };

                bool result = service.UpdateUtilisateur(utilisateur);
                if (result)
                {
                    MessageBox.Show("Utilisateur mis à jour avec succès.");
                }
                else
                {
                    MessageBox.Show("Échec de la mise à jour de l'utilisateur.");
                }

                effacer();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à modifier.");
            }
        }
        /// <summary>
        /// Sélectionne un utilisateur et remplit les champs de texte avec ses informations
        /// </summary>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgUtilisateur.SelectedRows.Count > 0)
            {
                var selectedRow = dgUtilisateur.SelectedRows[0];
                txtNom.Text = selectedRow.Cells["Nom"].Value.ToString();
                txtPrenom.Text = selectedRow.Cells["Prenom"].Value.ToString();
                txtAge.Text = selectedRow.Cells["Age"].Value.ToString();
            }
        }
    }
}
