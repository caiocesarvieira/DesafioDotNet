using DesafioDotNet.Commom;
using DesafioDotNet.Presentation.ControleLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DesafioDotNet.Presentation.Controllers
{
    public class ValidadorLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!UsuarioLogado.EstaLogado)
                RetornarLogin(filterContext);

            base.OnActionExecuting(filterContext);
        }

        private static void RetornarLogin(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var retorno = new { Mensagem = Mensagens.MSG_SESSION_EXPIRADA };
                filterContext.Result = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = retorno };
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary 
                    { 
                        { "controller", "Login" }, 
                        { "action", "Index" }
                    });
            }
        }

    }
}