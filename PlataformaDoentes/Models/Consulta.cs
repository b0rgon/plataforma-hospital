using PlataformaAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace PlataformaDoentes.Models
{
    public class Consulta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataConsulta { get;set; }

        public enum _Especialidade
        {
            Oftalmologia, Ortopedia, Medicina
        }

        [Required]
        public _Especialidade Especialidade { get; set; }

        public ICollection<Consulta_Paciente>? ConsultasPacientes { get; set; }
    }
}
