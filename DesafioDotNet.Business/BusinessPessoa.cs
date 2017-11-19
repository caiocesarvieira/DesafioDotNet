using DesafioDotNet.Business;
using DesafioDotNet.Commom;
using DesafioDotNet.Data;
using DesafioDotNet.Entity;
using System;

namespace DesafioDotNet.Business
{
    public class BusinessPessoa : BusinessBase
    {
        #region AÇÕES

        public Retorno Salvar(Pessoa Entity)
        {
            try
            {
                Retorno retorno = PreenchimentoObrigatorio(Entity);
                if (retorno.IsValido)
                {
                    if (Entity.Codigo == 0)
                        retorno = new DataPessoa().Incluir(Entity);
                    else
                        retorno = new DataPessoa().Alterar(Entity);
                }
                return retorno;
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        public Retorno Excluir(Pessoa Entity)
        {
            try
            {
                return new DataPessoa().Excluir(Entity);
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
                var retorno = new DataPessoa().Listar(parametros);

                if (retorno.IsValido)
                    retorno.Entity = ConverterParaResultadoDataTables<Pessoa>(retorno, RecuperarTotalRegistros("TB_PESSOA", "CODIGO", String.Empty).Entity.ConverteValor(0));

                return retorno;
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        public Retorno Pesquisar(Pessoa Entity, ParametrosOrdenacaoPaginacao parametros)
        {
            try
            {
                return new DataPessoa().Pesquisar(Entity, parametros);
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
                return new DataPessoa().Listar();
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        public Retorno Consultar(Pessoa Entity)
        {
            try
            {
                return new DataPessoa().Consultar(Entity);
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        #endregion

        #region METODOS AUXILIARES

        public Retorno PreenchimentoObrigatorio(Pessoa Entity)
        {
            if (String.IsNullOrEmpty(Entity.Nome))
                return new Retorno(false, String.Format(Mensagens.MSG_01, "Nome"));

            return new Retorno(true);
        }

        private Retorno VerificarExistencia(Pessoa Entity)
        {
            try
            {
                return new DataPessoa().VerificarExistencia(Entity);
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        #endregion

    }
}

