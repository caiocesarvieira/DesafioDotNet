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
    public class JogoController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Salvar(JogoViewModel model)
        {
            var retorno = new BusinessJogo().Salvar(model.Jogo);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Excluir(JogoViewModel model)
        {
            var retorno = new BusinessJogo().Excluir(model.Jogo);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Listar(ParametrosDataTables parametros)
        {
            var retorno = new BusinessJogo().Listar(DefinirParametrosConsulta(parametros));

            if (retorno.IsValido)
                return Json(CriarDatatablesResponse(parametros, retorno.Entity as ResultadoDataTables<Jogo>), JsonRequestBehavior.AllowGet);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
