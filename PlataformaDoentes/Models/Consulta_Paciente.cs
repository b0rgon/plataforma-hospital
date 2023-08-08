using PlataformaDoentes.Models;
using System.ComponentModel.DataAnnotations;

namespace PlataformaAPI.Models
{
    public class Consulta_Paciente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdPaciente { get; set; }

        [Required]
        public int IdConsulta { get; set; }

        public Paciente? Paciente { get; set; }

        public Consulta? Consulta { get; set; }
    }
}
