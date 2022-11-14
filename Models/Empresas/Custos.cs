using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Empresas
{
    public class Custos
    {
        [Required]
        public int Ano { get; set; }
        [Required]
        public string Id_Type { get; set; }
        [Required]
        public DateTime Last_Update { get; set; }
        [Required]
        public double Valor { get; set; }

        public Custos(int ano, string id_Type, DateTime last_Update, double valor)
        {
            Ano = ano;
            Id_Type = id_Type;
            Last_Update = last_Update;
            Valor = valor;
        }
    }
}
