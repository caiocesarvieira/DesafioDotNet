using DesafioDotNet.Business;
using DesafioDotNet.Commom;
using DesafioDotNet.Data;
using DesafioDotNet.Entity;
using System;

namespace DesafioDotNet.Business
{
    public class BusinessLogin : BusinessBase
    {
        #region AÇÕES

        public Retorno Salvar(Login Entity)
        {
            try
            {
                Retorno retorno = PreenchimentoObrigatorio(Entity);
                if (retorno.IsValido)
                {
                    if (Entity.Codigo == 0)
                        retorno = new DataLogin().Incluir(Entity);
                    else
                        retorno = new DataLogin().Alterar(Entity);
                }
                return retorno;
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        public Retorno Excluir(Login Entity)
        {
            try
            {
                return new DataLogin().Excluir(Entity);
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        #endregion

        #region CONSULTAS

        public Retorno Listar(ParametrosOrdenacaoPaginacao parametros)
        {
            try
            {
                return new DataLogin().Listar(parametros);
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        public Retorno Pesquisar(Login Entity, ParametrosOrdenacaoPaginacao parametros)
        {
            try
            {
                return new DataLogin().Pesquisar(Entity, parametros);
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        public Retorno Listar()
        {
            try
            {
                return new DataLogin().Listar();
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        public Retorno Consultar(Login Entity)
        {
            try
            {
                return new DataLogin().Consultar(Entity);
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        #endregion

        #region METODOS AUXILIARES

        public Retorno PreenchimentoObrigatorio(Login Entity)
        {
            if (String.IsNullOrEmpty(Entity.Usuario))
                return new Retorno(false, String.Format(Mensagens.MSG_01, "Usuario"));

            if (String.IsNullOrEmpty(Entity.Senha))
                return new Retorno(false, String.Format(Mensagens.MSG_01, "Senha"));

            return new Retorno(true);
        }

        private Retorno VerificarExistencia(Login Entity)
        {
            try
            {
                return new DataLogin().VerificarExistencia(Entity);
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        #endregion


        public Retorno Logar(Login Entity)
        {
            try
            {
                var retorno = new DataLogin().Consultar(Entity);

                if (retorno.IsValido && (retorno.Entity as Login).Codigo == 0)
                    return new Retorno(false, Mensagens.MSG_07);

                return new Retorno(retorno.Entity);
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }
    }
}

