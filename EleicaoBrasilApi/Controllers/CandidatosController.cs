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

        // Rota: GET api/candidatos/partido/PL
        [HttpGet("partido/{nomeDoPartido}")]
        public IActionResult GetByPartido(string nomeDoPartido)
        {
            // Filtra a lista baseada no parâmetro da URL
            var candidatos = _context.Candidatos
                                     .Where(c => c.Partido.ToLower() == nomeDoPartido.ToLower())
                                     .ToList();

            if (candidatos.Count == 0)
            {
                return NotFound($"Nenhum candidato encontrado para o partido {nomeDoPartido}");
            }

            return Ok(candidatos);
        }

        [HttpPost]
        public IActionResult Post(Candidato c)
        {
            // Validação: Verifica se já existe um candidato com o mesmo número
            var numeroJaExiste = _context.Candidatos.Any(x => x.Numero == c.Numero);

            if (numeroJaExiste)
            {
                return BadRequest("Erro: Já existe um candidato registrado com este número.");
            }

            _context.Candidatos.Add(c);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = c.Id }, c);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Candidato c)
        {
            var existing = _context.Candidatos.Find(id);
            if (existing == null) return NotFound();

            existing.Nome = c.Nome;
            existing.ViceNome = c.ViceNome; // Atualização obrigatória aqui
            existing.Partido = c.Partido;
            existing.Numero = c.Numero;

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