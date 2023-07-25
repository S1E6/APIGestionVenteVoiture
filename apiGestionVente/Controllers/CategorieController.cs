using apiGestionVente.Model;
using apiGestionVente.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiGestionVente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        private readonly GestionVenteContext _context;

        public CategorieController(GestionVenteContext context)
        {
            _context = context;
        }

        private string GenerateNextIDCATEGORIE()
        {
            var lastIDCATEGORIE = _context.Categories
                .OrderByDescending(c => c.IDCATEGORIE)
                .Select(c => c.IDCATEGORIE)
                .FirstOrDefault();

            if (lastIDCATEGORIE == null)
            {
                return "CAT001";
            }

            int lastNumber = int.Parse(lastIDCATEGORIE.Substring(3));
            string nextNumber = (lastNumber + 1).ToString("D3");
            string nextIDCATEGORIE = "CAT" + nextNumber;

            return nextIDCATEGORIE;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categorie>> GetCategories()
        {
            return _context.Categories.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Categorie> GetCategorie(string id)
        {
            var categorie = _context.Categories
                .Where(c => c.IDCATEGORIE.Contains(id) || c.DESIGNCAT.Contains(id))
                .ToList();

            if (categorie == null)
            {
                return NotFound();
            }

            return Ok(categorie);
        }

        [HttpPost]
        public IActionResult CreateCategorie(Categorie categorie)
        {
            categorie.IDCATEGORIE = GenerateNextIDCATEGORIE();
            _context.Categories.Add(categorie);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCategorie), new { id = categorie.IDCATEGORIE }, categorie);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategorie(string id, Categorie categorie)
        {
            if (id != categorie.IDCATEGORIE)
            {
                return BadRequest();
            }

            _context.Entry(categorie).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategorie(string id)
        {
            var categorie = _context.Categories.Find(id);

            if (categorie == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categorie);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
