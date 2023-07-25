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
            return _context.Achats.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Acheter> GetAcheter(string id)
        {
            var acheter = _context.Achats
                .Where(a => a.IDCLIENT.Contains(id) || a.NUMSERIE.Contains(id))
                .ToList();

            if (acheter == null)
            {
                return NotFound();
            }

            return Ok(acheter);
        }

        private string GenerateNextIDCLIENT()
        {
            var lastIDCLIENT = _context.Achats
                .OrderByDescending(a => a.IDCLIENT)
                .Select(a => a.IDCLIENT)
                .FirstOrDefault();

            if (lastIDCLIENT == null)
            {
                return "CLT001";
            }

            int lastNumber = int.Parse(lastIDCLIENT.Substring(3));
            string nextNumber = (lastNumber + 1).ToString("D3");
            string nextIDCLIENT = "CLT" + nextNumber;

            return nextIDCLIENT;
        }

        [HttpPost]
        public IActionResult CreateAcheter(Acheter acheter)
        {
            acheter.IDCLIENT = GenerateNextIDCLIENT();
            _context.Achats.Add(acheter);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAcheter), new { id = acheter.IDCLIENT }, acheter);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAcheter(string id, Acheter acheter)
        {
            if (id != acheter.IDCLIENT)
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
