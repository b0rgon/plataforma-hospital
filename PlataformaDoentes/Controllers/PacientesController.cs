using DtoModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaDoentes.Data;
using PlataformaDoentes.Models;

namespace PlataformaDoentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : Controller
    {
        private readonly PlataformaDoentes_API_DbContext dbContext;

        public PacientesController(PlataformaDoentes_API_DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetPacientes()
        {
            return Ok(await dbContext.Pacientes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<Paciente> GetPaciente(int id)
        {
            //var paciente = await dbContext.Pacientes.FirstOrDefaultAsync(p => p.Num_Processo == id);
            //if(paciente == null)
            //{
            //    return BadRequest("Paciente não encontrado.");
            //}
            //return Ok(paciente);

            return await dbContext.Pacientes.Where(p => p.Num_Processo == id).FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddPaciente(Paciente paciente)
        {
            // verifica se já existe
            var existingPaciente = await dbContext.Pacientes.FirstOrDefaultAsync(p => p.Num_Processo == paciente.Num_Processo);
            if (existingPaciente != null)
            {
                return BadRequest("Já existe um paciente com o mesmo número de processo.");
            }

            dbContext.Pacientes.Add(paciente);
            await dbContext.SaveChangesAsync();

            return Ok(await dbContext.Pacientes.ToListAsync());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePaciente([FromRoute] int id, Paciente p)
        {
            var paciente = await dbContext.Pacientes.FindAsync(id);

            if (paciente != null)
            {
                paciente.Nome = p.Nome;
                paciente.DataNascimento = p.DataNascimento;
                paciente.Genero = p.Genero;

                await dbContext.SaveChangesAsync();

                return Ok(paciente);
            }

            return BadRequest("Paciente não encontrado.");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePaciente([FromRoute] int id)
        {
            var paciente = await dbContext.Pacientes.FindAsync(id);

            if (paciente != null)
            {
                dbContext.Remove(paciente);
                await dbContext.SaveChangesAsync();
                return Ok(paciente);
            }

            return NotFound();
        }
    }
}
