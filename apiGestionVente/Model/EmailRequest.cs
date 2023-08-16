namespace apiGestionVente.Model;

public class EmailRequest
{
        public string adresseDestinataire { get; set; }
        public string somme { get; set; }
        public DateTime dateAchat { get; set; }
        public string client { get; set; }
        public string voiture { get; set; }
}