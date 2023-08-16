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
                return "WWW001";
            }

            int lastNumber = int.Parse(lastNUMSERIE.Substring(3));
            string nextNumber = (lastNumber + 1).ToString("D3");
            string nextNUMSERIE = "WWW" + nextNumber;

            return nextNUMSERIE;
        }
        [HttpGet]
        public ActionResult<IEnumerable<VoitureGroup>> GetVoitures()
        {
            var allVoitures = _context.Voitures
                .Include(v => v.Categorie)
                .Include(v => v.Marque)
                .Where(v => v.STATUS == 0)
                .ToList();

            var groupedVoitures = allVoitures
                .GroupBy(v => new { v.DESIGNVOITURE, v.Categorie, v.Marque, v.TYPE, v.BOITEVITESSE })
                .Select(group => new VoitureGroup
                {
                    Designation =  group.FirstOrDefault()?.DESIGNVOITURE ?? "Voiture inconnue",
                    Count = group.Count(),
                    Voitures = group.ToList()
                })
                .ToList();

            return groupedVoitures;
        }
        [HttpGet("vendue")]
        public ActionResult<IEnumerable<VoitureGroup>> GetVoituresVendue()
        {
            var allVoitures = _context.Voitures
                .Include(v => v.Categorie)
                .Include(v => v.Marque)
                .Where(v => v.STATUS == 1)
                .ToList();

            var groupedVoitures = allVoitures
                .GroupBy(v => new { v.DESIGNVOITURE, v.Categorie, v.Marque, v.TYPE, v.BOITEVITESSE })
                .Select(group => new VoitureGroup
                {
                    Designation =  group.FirstOrDefault()?.DESIGNVOITURE ?? "Voiture inconnue",
                    Count = group.Count(),
                    Voitures = group.ToList()
                })
                .ToList();

            return groupedVoitures;
        }

        [HttpGet("{id}")]
        public List<Voiture> GetVoiture(string id)
        {
            var voitures = _context.Voitures
                .Include(v => v.Categorie)
                .Include(v => v.Marque)
                .Where(v => v.NUMSERIE.Contains(id) ||
                            v.DESIGNVOITURE.Contains(id) ||
                            v.Categorie.DESIGNCAT.Contains(id) ||
                            v.Marque.DESIGNMARQUE.Contains(id) )
                .ToList();
            

            return voitures;
        }
        
        [HttpPost]
        public IActionResult CreateVoiture(Voiture voiture)
        {
            voiture.NUMSERIE = GenerateNextNUMSERIE();
            if (voiture.Categorie != null && !string.IsNullOrEmpty(voiture.Categorie.IDCATEGORIE))
            {
                _context.Attach(voiture.Categorie);
            }

            if (voiture.Marque != null && !string.IsNullOrEmpty(voiture.Marque.IDMARQUE))
            {
                _context.Attach(voiture.Marque);
            }
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
