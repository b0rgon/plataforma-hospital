using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModels
{
    public class PacienteDto
    {
        public int Num_Processo { get; set; } // id

        public string Nome { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        public enum _Genero
        {
            Masculino, Feminino
        }

        public _Genero Genero { get; set; }
    }
}
