using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Empresas
{
    public class Empresas
    {
        private string _id;
        [Required]
        public string Id { get { return this._id; } private set { this._id = value != "" ? value : this._id; } }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime Date_Ingestion { get; set; }
        public DateTime Last_Update { get; set; }
        public List<Custos> Custos { get; set; }

        public Empresas(string id, string status, DateTime date_ingestion)
        {
            Id = id;
            this.Status = status;
            this.Date_Ingestion = date_ingestion;
        }
    }
}
