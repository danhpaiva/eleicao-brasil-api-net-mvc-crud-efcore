using EleicaoBrasilApi.Data;
using EleicaoBrasilApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EleicaoBrasilApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CandidatosController(AppDbContext context) => _context = context;

        [HttpGet] // READ
        public IActionResult Get() => Ok(_context.Candidatos.ToList());

        [HttpPost] // CREATE
        public IActionResult Post(Candidato c)
        {
            _context.Candidatos.Add(c);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = c.Id }, c);
        }

        [HttpPut("{id}")] // UPDATE
        public IActionResult Put(int id, Candidato c)
        {
            var existing = _context.Candidatos.Find(id);
            if (existing == null) return NotFound();
            existing.Nome = c.Nome;
            _context.Candidatos.Update(existing);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")] // DELETE
        public IActionResult Delete(int id)
        {
            var c = _context.Candidatos.Find(id);
            if (c == null) return NotFound();
            _context.Candidatos.Remove(c);
            _context.SaveChanges();
            return NoContent();
        }
    }
}