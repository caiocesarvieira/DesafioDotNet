using DesafioDotNet.Business;
using DesafioDotNet.Commom;
using DesafioDotNet.Entity;
using DesafioDotNet.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DesafioDotNet.Presentation.Controllers
{
    [ValidadorLogin]
    public class AmigoController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Salvar(AmigoViewModel model)
        {
            var retorno = new BusinessPessoa().Salvar(model.Amigo);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Excluir(AmigoViewModel model)
        {
            var retorno = new BusinessPessoa().Excluir(model.Amigo);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Listar(ParametrosDataTables parametros)
        {
            var retorno = new BusinessPessoa().Listar(DefinirParametrosConsulta(parametros));

            if (retorno.IsValido)
                return Json(CriarDatatablesResponse(parametros, retorno.Entity as ResultadoDataTables<Pessoa>), JsonRequestBehavior.AllowGet);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
