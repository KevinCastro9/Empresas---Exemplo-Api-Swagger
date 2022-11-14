using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views.Grupo
{
    public class TiposCustosView
    {
        private List<string> _id_Type = new List<string>() { "CUSTOS_1", "CUSTOS_2", "CUSTOS_3", "CUSTOS_4" };
        public List<string> Id_Type { get { return this._id_Type; } private set { } }

        private List<double> _valor = new List<double>() { 0, 0, 0, 0 };
        public List<double> Valor { get { return this._valor; } set { } }
    }
}
