using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioDotNet.Commom
{
    public class ParametrosOrdenacaoPaginacao
    {
        public int Inicio { get; set; }

        public int Quantidade { get; set; }

        public string CampoOrdenacao { get; set; }

        public string DirecaoOrdenacao { get; set; }
    }
}
