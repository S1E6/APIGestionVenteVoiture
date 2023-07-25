using apiGestionVente.Model;
using apiGestionVente.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiGestionVente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly GestionVenteContext _context;

        public ClientsController(GestionVenteContext context)
        {
            _context = context;
        }

        private string GenerateNextIDCLIENT()
        {
            var lastIDCLIENT = _context.Clients
                .OrderByDescending(c => c.IDCLIENT)
                .Select(c => c.IDCLIENT)
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

        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetClients()
        {
            return _context.Clients.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Client> GetClient(string id)
        {
            var client = _context.Clients
                .Where(c => c.IDCLIENT.Contains(id) || c.NOM.Contains(id) || c.PRENOMS.Contains(id)).ToList();

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        public IActionResult CreateClient(Client client)
        {
            client.IDCLIENT = GenerateNextIDCLIENT();
            _context.Clients.Add(client);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetClient), new { id = client.IDCLIENT }, client);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateClient(string id, Client client)
        {
            if (id != client.IDCLIENT)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(string id)
        {
            var client = _context.Clients.Find(id);

            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
