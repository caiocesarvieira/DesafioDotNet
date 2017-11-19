using DesafioDotNet.Entity;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioDotNet.Presentation.ControleLogin
{
    public class UsuarioLogado
    {
        public const string SESSION_USUARIO_LOGADO = "USUARIO_LOGADO";

        public static Login Usuario
        {
            get
            {
                if (EstaLogado)
                    return (HttpContext.Current.Session[SESSION_USUARIO_LOGADO] as Login);

                return null;
            }
        }

        public static bool EstaLogado
        {
            get
            {
                return HttpContext.Current.Session[SESSION_USUARIO_LOGADO] != null;
            }
        }
    }
}