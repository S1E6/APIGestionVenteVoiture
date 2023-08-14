using apiGestionVente.Data;
using Microsoft.AspNetCore.Mvc;
using apiGestionVente.Model;
namespace apiGestionVente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphController : ControllerBase
    {
        private readonly GestionVenteContext _context;

        public GraphController(GestionVenteContext context)
        {
            _context = context;
        }

        [HttpGet("argent-par-mois")]
        public ActionResult<object> GetArgentParMois()
        {
            var result = new
            {
                months = new string[]
                {
                    "Janvier", "Février", "Mars", "Avril", "Mai", "Juin",
                    "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"
                },
                data = CalculateSommesAchatParMois()
            };

            return Ok(result);
        }

        private List<long> CalculateSommesAchatParMois()
        {
            var sommeParMois = new List<long>();
            
            for (int month = 1; month <= 12; month++)
            {
                var somme = _context.Achats
                    .Where(a => a.DATEACHAT.Month == month)
                    .Sum(a => a.SOMME) ?? 0;
                
                sommeParMois.Add(somme);
            }

            return sommeParMois;
        }
        [HttpGet("marques-sommes-voitures")]
        public IActionResult GetMarquesSommesVoitures()
        {
            var marques = _context.Marques.ToList();
            var marqueData = new MarqueData();

            marqueData.Labels = marques.Select(m => m.DESIGNMARQUE).ToList();
            marqueData.Data = new List<long>();

            foreach (var marque in marques)
            {
                var sommeVoitures = _context.Voitures
                    .Where(v => v.IDMARQUE == marque.IDMARQUE)
                    .Count();

                marqueData.Data.Add(sommeVoitures);
            }

            return Ok(marqueData);
        }
    }
}