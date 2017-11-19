using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioDotNet.Commom
{
    public class ResultadoDataTables<T>
    {
        public List<T> Lista { get; set; }
        public int QuantidadeRegistros { get; set; }
        public int QuantidadeRegistrosTotal { get; set; }
    }
}
