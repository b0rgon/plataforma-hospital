using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaAPI.Models;
using PlataformaDoentes.Data;
using PlataformaDoentes.Models;

namespace PlataformaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Consultas_PacientesController : Controller
    {
        private readonly PlataformaDoentes_API_DbContext dbContext;

        public Consultas_PacientesController(PlataformaDoentes_API_DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsultasPacientes()
        {
            return Ok(await dbContext.Consultas_Pacientes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsultaPaciente(int id)
        {
            var consultaPaciente = await dbContext.Consultas_Pacientes.FindAsync(id);
            if (consultaPaciente == null)
                return NotFound("Não foi encontrada consulta.");

            return Ok(consultaPaciente);
        }

        [HttpPost]
        public async Task<IActionResult> AddConsultaPaciente(Consulta_Paciente consultaPaciente)
        {
            // verificar se já existe
            var existingConsultaPaciente = await dbContext.Consultas_Pacientes.FirstOrDefaultAsync(cp => cp.IdPaciente == consultaPaciente.IdPaciente
                                                                                                      && cp.IdConsulta == consultaPaciente.IdConsulta);
            if (existingConsultaPaciente != null)
                return BadRequest("Não existe associação entre paciente e consulta.");

            dbContext.Consultas_Pacientes.Add(consultaPaciente);
            await dbContext.SaveChangesAsync();

            return Ok(await dbContext.Consultas_Pacientes.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsultaPaciente(int id, Consulta_Paciente cp)
        {
            var consultaPaciente = await dbContext.Consultas_Pacientes.FindAsync(id);

            if (consultaPaciente != null)
            {
                consultaPaciente.IdPaciente = cp.IdPaciente;
                consultaPaciente.IdConsulta = cp.IdConsulta;

                await dbContext.SaveChangesAsync();

                return Ok(consultaPaciente);
            }

            return NotFound("Consulta não encontrado.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultaPaciente(int id)
        {
            var consultaPaciente = await dbContext.Consultas_Pacientes.FindAsync(id);

            if (consultaPaciente != null)
            {
                dbContext.Remove(consultaPaciente);
                await dbContext.SaveChangesAsync();
                return Ok(consultaPaciente);
            }

            return NotFound();
        }
    }
}
