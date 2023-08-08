using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaDoentes.Data;
using PlataformaDoentes.Models;

namespace PlataformaDoentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultasController : Controller
    {
        private readonly PlataformaDoentes_API_DbContext dbContext;

        public ConsultasController(PlataformaDoentes_API_DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsultas()
        {
            return Ok(await dbContext.Consultas.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsulta(int id)
        {
            var consulta = await dbContext.Consultas.FirstOrDefaultAsync(c => c.Id == id);
            if (consulta == null)
            {
                return BadRequest("Consulta não encontrada.");
            }
            return Ok(consulta);
        }

        [HttpPost]
        public async Task<IActionResult> AddConsulta(Consulta consulta)
        {
            // verifica se já existe
            var existingConsulta = await dbContext.Consultas.FirstOrDefaultAsync(c => c.Id == consulta.Id);
            if (existingConsulta != null)
            {
                return BadRequest("Já existe uma consulta com o mesmo id.");
            }

            dbContext.Consultas.Add(consulta);
            await dbContext.SaveChangesAsync();

            return Ok(await dbContext.Consultas.ToListAsync());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateConsulta([FromRoute] int id, Consulta c)
        {
            var consulta = await dbContext.Consultas.FindAsync(id);

            if (consulta != null)
            {
                consulta.DataConsulta = c.DataConsulta;
                consulta.Especialidade = c.Especialidade;

                await dbContext.SaveChangesAsync();

                return Ok(consulta);
            }

            return BadRequest("Consulta não encontrado.");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteConsulta([FromRoute] int id)
        {
            var consulta = await dbContext.Consultas.FindAsync(id);

            if(consulta != null)
            {
                dbContext.Remove(consulta);
                await dbContext.SaveChangesAsync();
                return Ok(consulta);
            }

            return NotFound();
        }
    }
}
