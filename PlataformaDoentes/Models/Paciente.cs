using PlataformaAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace PlataformaDoentes.Models
{
    public class Paciente
    {
        [Key]
        public int Num_Processo { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public DateTime DataNascimento { get; set; }

        public enum _Genero
        {
            Masculino, Feminino
        }

        [Required]
        public _Genero Genero { get; set; }

        public ICollection<Consulta_Paciente>? ConsultasPacientes { get; set; }
    }
}
