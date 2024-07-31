using MetierSharedMemory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MetierSharedMemory
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
        [OperationContract]
        List<Encadreur> GetEncadreurs(string Nom, string Prenom, string Specialite);
        [OperationContract]
        Encadreur GetEncadreur(int? IdEncadreur);
        [OperationContract]
        bool DeleteEncadreur(int? IdEncadreur);
        [OperationContract]
        bool UpdateEncadreur(Encadreur encadreur);
        [OperationContract]
        bool AddEncadreur(Encadreur encadreur);
        [OperationContract]
        List<Encadreur> GetAllEncadreur();

        // TODO: ajoutez vos opérations de service ici
    }


    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
