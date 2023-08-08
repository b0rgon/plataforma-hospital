using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModels
{
    public class ConsultaDto
    {
        public int Id { get; set; }
        public DateTime DataConsulta { get; set; }
        public enum _Especialidade
        {
            Oftalmologia, Ortopedia, Medicina
        }
        public _Especialidade Especialidade { get; set; }
    }
}
