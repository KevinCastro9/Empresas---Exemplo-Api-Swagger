using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Grupo
{
    public class Grupo
    {
        [Required]
        private int _id;
        public int Id { get { return this._id; } private set { this._id = value > 0 ? value : this._id; } }

        [Required]
        public string Nome { get; set; }

        [Required]
        public char Category { get; set; }
        [Required]
        public DateTime date_ingestion { get; set; }
        public List<string> Companys { get; set; }

        public Grupo(int id, string nome, char category, DateTime dateIngestion)
        {
            Id = id;
            Nome = nome;
            Category = category;
            date_ingestion = dateIngestion;
        }
    }
}
