using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSharedMemory.Model
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public int age { get; set; }
    }
}
