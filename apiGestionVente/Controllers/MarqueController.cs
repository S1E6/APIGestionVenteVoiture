using apiGestionVente.Model;
using apiGestionVente.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiGestionVente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarquesController : ControllerBase
    {
        private readonly GestionVenteContext _context;

        public MarquesController(GestionVenteContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Marque>> GetMarques()
        {
            return _context.Marques.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Marque> GetMarque(string id)
        {
            var marque = _context.Marques
                .Where(m => m.IDMARQUE.Contains(id) || m.DESIGNMARQUE.Contains(id)).ToList();
            if (marque == null)
            {
                return NotFound();
            }

            return Ok(marque);
        }
        private string GenerateNextIDMARQUE()
        {
            var lastIDMARQUE = _context.Marques
                .OrderByDescending(m => m.IDMARQUE)
                .Select(m => m.IDMARQUE)
                .FirstOrDefault();
            
            if (lastIDMARQUE == null)
            {
                return "MAR001";
            }
            int lastNumber = int.Parse(lastIDMARQUE.Substring(3));
            string nextNumber = (lastNumber + 1).ToString("D3");
            string nextIDMARQUE = "MAR" + nextNumber;

            return nextIDMARQUE;
        }

        [HttpPost]
        public IActionResult CreateMarque(Marque marque)
        {
            marque.IDMARQUE = GenerateNextIDMARQUE();
            _context.Marques.Add(marque);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMarque), new { id = marque.IDMARQUE }, marque);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateMarque(string id, Marque marque)
        {
            if (id != marque.IDMARQUE)
            {
                return BadRequest();
            }

            _context.Entry(marque).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMarque(string id)
        {
            var marque = _context.Marques.Find(id);

            if (marque == null)
            {
                return NotFound();
            }

            _context.Marques.Remove(marque);
            _context.SaveChanges();

            return NoContent();
        }
    }
}