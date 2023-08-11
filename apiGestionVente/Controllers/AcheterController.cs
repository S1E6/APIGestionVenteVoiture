using apiGestionVente.Model;
using apiGestionVente.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiGestionVente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchetersController : ControllerBase
    {
        private readonly GestionVenteContext _context;

        public AchetersController(GestionVenteContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Acheter>> GetAcheters()
        {
            var acheter = _context.Achats
                .Include(a => a.Client)
                .Include(a => a.Voiture)
                .ToList();
            
            return Ok(acheter);
            }

        [HttpGet("{id}")]
        public ActionResult<Acheter> GetAcheter(string id)
        {
            var acheter = _context.Achats
                .Include(a => a.Client)
                .Include(a => a.Voiture)
                .Where(a => a.NUMACHAT.Contains(id) || a.IDCLIENT.Contains(id) || a.NUMSERIE.Contains(id))
                .ToList();

            if (acheter == null)
            {
                return NotFound();
            }

            return Ok(acheter);
        }

        private string GenerateNextNUMACHAT()
        {
            var lastNUMACHAT = _context.Achats
                .OrderByDescending(a => a.NUMACHAT)
                .Select(a => a.NUMACHAT)
                .FirstOrDefault();

            if (lastNUMACHAT == null)
            {
                return "ACH001";
            }

            int lastNumber = int.Parse(lastNUMACHAT.Substring(3));
            string nextNumber = (lastNumber + 1).ToString("D3");
            string nextNUMACHAT = "ACH" + nextNumber;

            return nextNUMACHAT;
        }
        
        [HttpPost]
        public IActionResult CreateAchat(Acheter achat)
        {
            achat.NUMACHAT = GenerateNextNUMACHAT();
            if (achat.NUMSERIE != null && !string.IsNullOrEmpty(achat.Voiture.NUMSERIE))
            {
                _context.Attach(achat.Voiture);
            }

            if (achat.Client != null && !string.IsNullOrEmpty(achat.Client.IDCLIENT))
            {
                _context.Attach(achat.Client);
            }
            _context.Achats.Add(achat);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAcheter), new { id = achat.NUMACHAT }, achat);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateAcheter(string id, Acheter acheter)
        {
            if (id != acheter.NUMACHAT)
            {
                return BadRequest();
            }

            _context.Entry(acheter).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAcheter(string id)
        {
            var acheter = _context.Achats.Find(id);

            if (acheter == null)
            {
                return NotFound();
            }

            _context.Achats.Remove(acheter);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
