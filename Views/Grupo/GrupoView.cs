using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views.Grupo
{
    public class GrupoView
    {

        private int _id;
        [Required]
        public int Id { get { return this._id; } set { this._id = value > 0 ? value : this._id; } }
        [Required]
        public string Nome { get; set; }
        [Required]
        public char Category { get; set; }
        [Required]
        public DateTime date_ingestion { get; set; }
    }
}
