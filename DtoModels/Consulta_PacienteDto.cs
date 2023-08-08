using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModels
{
    public class Consulta_PacienteDto
    {
        public int IdPaciente { get; set; }
        public int IdConsulta { get; set; }
        public string NomePaciente { get; set; } = string.Empty;
        public ConsultaDto._Especialidade Especialidade { get; set; }
        public DateTime DataConsulta { get; set; }
    }
}
