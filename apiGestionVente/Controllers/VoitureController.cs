using System.Collections.Generic;
using System.Linq;
using apiGestionVente.Model;
using apiGestionVente.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiGestionVente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoitureController : ControllerBase
    {
        private readonly GestionVenteContext _context;

        public VoitureController(GestionVenteContext context)
        {
            _context = context;
        }

        private string GenerateNextNUMSERIE()
        {
            var lastNUMSERIE = _context.Voitures
                .OrderByDescending(v => v.NUMSERIE)
                .Select(v => v.NUMSERIE)
                .FirstOrDefault();

            if (lastNUMSERIE == null)
            {
                return "NUM001";
            }

            int lastNumber = int.Parse(lastNUMSERIE.Substring(3));
            string nextNumber = (lastNumber + 1).ToString("D3");
            string nextNUMSERIE = "NUM" + nextNumber;

            return nextNUMSERIE;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Voiture>> GetVoitures()
        {
            return _context.Voitures.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Voiture> GetVoiture(string id)
        {
            var voiture = _context.Voitures
                .Where(v => v.NUMSERIE.Contains(id) || v.DESIGNVOITURE.Contains(id) || v.IDMARQUE.Contains(id) || 
                            v.IDCATEGORIE.Contains(id) || v.TYPE.Contains(id) || v.BOITEVITESSE.Contains(id)
                            )
                .ToList();

            if (voiture == null)
            {
                return NotFound();
            }

            return Ok(voiture);
        }

        [HttpPost]
        public IActionResult CreateVoiture(Voiture voiture)
        {
            voiture.NUMSERIE = GenerateNextNUMSERIE();
            _context.Voitures.Add(voiture);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetVoiture), new { id = voiture.NUMSERIE }, voiture);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVoiture(string id, Voiture voiture)
        {
            if (id != voiture.NUMSERIE)
            {
                return BadRequest();
            }

            _context.Entry(voiture).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVoiture(string id)
        {
            var voiture = _context.Voitures.Find(id);

            if (voiture == null)
            {
                return NotFound();
            }

            _context.Voitures.Remove(voiture);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
