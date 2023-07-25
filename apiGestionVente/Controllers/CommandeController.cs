using apiGestionVente.Model;
using apiGestionVente.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiGestionVente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeController : ControllerBase
    {
        private readonly GestionVenteContext _context;

        public CommandeController(GestionVenteContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Commmande>> GetCommandes()
        {
            return _context.Commandes.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Commmande> GetCommande(string id)
        {
            var commande = _context.Commandes
                .Where(c => c.IDCLIENT.Contains(id) || c.NUMSERIE.Contains(id))
                .ToList();

            if (commande == null)
            {
                return NotFound();
            }

            return Ok(commande);
        }

        [HttpPost]
        public IActionResult CreateCommande(Commmande commande)
        {
            _context.Commandes.Add(commande);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCommande), new { id = commande.IDCLIENT }, commande);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCommande(string id, Commmande commande)
        {
            if (id != commande.IDCLIENT)
            {
                return BadRequest();
            }

            _context.Entry(commande).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCommande(string id)
        {
            var commande = _context.Commandes.Find(id);

            if (commande == null)
            {
                return NotFound();
            }

            _context.Commandes.Remove(commande);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
