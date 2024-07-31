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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            service = new ServiceMetier.Service1Client();
        
       
        }
        ServiceMetier.Service1Client service;
        private void Form1_Load(object sender, EventArgs e)
        {
            dgEncadreur.DataSource = service.GetAllEncadreur();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ServiceMetier.Encadreur encadreur=new ServiceMetier.Encadreur();
            encadreur.Nom=txtNom.Text;
            encadreur.Prenom=txtPrenom.Text;
            encadreur.Specialite=txtSpecialite.Text;    
            service.AddEncadreur(encadreur);
            Effacer();

        }

        private void Effacer()
        {
            txtNom.Text = string.Empty;
            txtPrenom.Text = string.Empty;
            txtSpecialite.Text = string.Empty;
            dgEncadreur.DataSource=service.GetAllEncadreur();
            txtNom.Focus();
        }

       
    }
}
