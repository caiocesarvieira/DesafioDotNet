using DesafioDotNet.Business;
using DesafioDotNet.Commom;
using DesafioDotNet.Data;
using DesafioDotNet.Entity;
using System;

namespace DesafioDotNet.Business
{
    public class BusinessJogo : BusinessBase
    {
        #region AÇÕES

        public Retorno Salvar(Jogo Entity)
        {
            try
            {
                Retorno retorno = PreenchimentoObrigatorio(Entity);
                if (retorno.IsValido)
                {
                    if (Entity.Codigo == 0)
                        retorno = new DataJogo().Incluir(Entity);
                    else
                        retorno = new DataJogo().Alterar(Entity);
                }
                return retorno;
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        public Retorno Excluir(Jogo Entity)
        {
            try
            {
                return new DataJogo().Excluir(Entity);
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
                var retorno = new DataJogo().Listar(parametros);

                if (retorno.IsValido)
                    retorno.Entity = ConverterParaResultadoDataTables<Jogo>(retorno, RecuperarTotalRegistros("TB_JOGO", "CODIGO", String.Empty).Entity.ConverteValor(0));

                return retorno;
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        public Retorno Pesquisar(Jogo Entity, ParametrosOrdenacaoPaginacao parametros)
        {
            try
            {
                return new DataJogo().Pesquisar(Entity, parametros);
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
                return new DataJogo().Listar();
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        public Retorno Consultar(Jogo Entity)
        {
            try
            {
                return new DataJogo().Consultar(Entity);
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        #endregion

        #region METODOS AUXILIARES

        public Retorno PreenchimentoObrigatorio(Jogo Entity)
        {

            if (String.IsNullOrEmpty(Entity.Descricao))
                return new Retorno(false, String.Format(Mensagens.MSG_01, "Descricao"));

            return new Retorno(true);
        }

        private Retorno VerificarExistencia(Jogo Entity)
        {
            try
            {
                return new DataJogo().VerificarExistencia(Entity);
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        #endregion

    }
}

