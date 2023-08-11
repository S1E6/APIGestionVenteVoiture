using System.ComponentModel.DataAnnotations.Schema;

namespace apiGestionVente.Model;

public class Voiture
{
    public string NUMSERIE { get; set; }
    public string IDCATEGORIE { get; set; }
    public string IDMARQUE { get; set; }
    public string DESIGNVOITURE { get; set; }
    public long? PRIX { get; set; }
    public string IMG { get; set; }
    public string TYPE { get; set; }
    public string BOITEVITESSE { get; set; }
    public int STATUS { get; set; }
    
    [ForeignKey("IDCATEGORIE")] 
    public Categorie Categorie { get; set; } 

    [ForeignKey("IDMARQUE")] 
    public Marque Marque { get; set; } 
}
public class VoitureGroup
{
    public string Designation { get; set; }
    public int Count { get; set; }
    public List<Voiture> Voitures { get; set; }
}