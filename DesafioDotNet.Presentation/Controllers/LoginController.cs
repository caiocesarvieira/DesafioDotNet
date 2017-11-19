using DesafioDotNet.Business;
using DesafioDotNet.Commom;
using DesafioDotNet.Entity;
using DesafioDotNet.Presentation.ControleLogin;
using DesafioDotNet.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DesafioDotNet.Presentation.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Logar(LoginViewModel model)
        {
            var retorno = new BusinessLogin().Logar(model.Login);

            if (retorno.IsValido)
                Session[UsuarioLogado.SESSION_USUARIO_LOGADO] = (retorno.Entity as Login);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logoff()
        {
            Session[UsuarioLogado.SESSION_USUARIO_LOGADO] = null;

            return View("Index");
        }

    }
}
