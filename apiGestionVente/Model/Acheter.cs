namespace apiGestionVente.Model;
using System.ComponentModel.DataAnnotations.Schema;
public class Acheter 
{
    public string NUMACHAT { get; set; }
    public string IDCLIENT { get; set; }
    public string NUMSERIE { get; set; }
    public int? QTE { get; set; }
    public long? RESTE { get; set; }
    public long? SOMME { get; set; }
    
    [ForeignKey("IDCLIENT")] 
    
    public Client Client { get; set; } 

    [ForeignKey("NUMSERIE")]
    
    public Voiture Voiture { get; set; } 
    
}
