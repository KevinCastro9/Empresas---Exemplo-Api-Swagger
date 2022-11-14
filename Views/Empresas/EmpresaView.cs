using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views.Empresas
{
    public class EmpresaView
    {
        private string _id;
        [Required]
        public string Id { get { return _id; } set { _id = value != "" ? value : _id; } }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime Date_Ingestion { get; set; }
    }
}
