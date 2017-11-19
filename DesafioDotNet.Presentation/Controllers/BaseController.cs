using DesafioDotNet.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DesafioDotNet.Presentation.Controllers
{
    public class BaseController : Controller
    {
        protected DatatablesResponse CriarDatatablesResponse<T>(ParametrosDataTables parametrosDataTable, ResultadoDataTables<T> retorno)
        {
            return new DatatablesResponse
            {
                aaData = retorno.Lista,
                iTotalDisplayRecords = retorno.QuantidadeRegistrosTotal,
                iTotalRecords = retorno.QuantidadeRegistros
            };
        }

        protected static ParametrosOrdenacaoPaginacao DefinirParametrosConsulta(ParametrosDataTables parametros)
        {
            return new ParametrosOrdenacaoPaginacao
            {
                Inicio = parametros.start,
                Quantidade = Constantes.QUANTIDADE_REGISTROS_PAGINA,
                CampoOrdenacao = parametros.order[0].column.ToString(),
                DirecaoOrdenacao = parametros.order == null ? String.Empty : parametros.order[0].dir
            };
        }

    }
}
