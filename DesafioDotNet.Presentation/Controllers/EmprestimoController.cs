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
    public class EmprestimoController : BaseController
    {
        public ActionResult Index()
        {
            var jogos = new BusinessJogo().Listar().Entity as List<Jogo>;
            var amigos = new BusinessPessoa().Listar().Entity as List<Pessoa>;

            return View(new EmprestimoViewModel { Jogos = jogos, Amigos = amigos });
        }

        public JsonResult Salvar(EmprestimoViewModel model)
        {
            model.Emprestimo.Data = DateTime.Now;
            var retorno = new BusinessPessoaJogo().Salvar(model.Emprestimo);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Excluir(EmprestimoViewModel model)
        {
            var retorno = new BusinessPessoaJogo().Excluir(model.Emprestimo);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Listar(ParametrosDataTables parametros, string nomeAmigo, string descricaoJogo)
        {
            var retorno = new BusinessPessoaJogo().Pesquisar(new PessoaJogo()
            {
                Pessoa = new Pessoa { Nome = nomeAmigo },
                Jogo = new Jogo { Descricao = descricaoJogo }
            }, DefinirParametrosConsulta(parametros));

            if (retorno.IsValido)
                return Json(CriarDatatablesResponse(parametros, retorno.Entity as ResultadoDataTables<PessoaJogo>), JsonRequestBehavior.AllowGet);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}
