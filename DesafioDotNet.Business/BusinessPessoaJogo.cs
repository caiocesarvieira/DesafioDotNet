using DesafioDotNet.Commom;
using DesafioDotNet.Data;
using DesafioDotNet.Entity;
using System;

namespace DesafioDotNet.Business
{
    public class BusinessPessoaJogo : BusinessBase
    {
        #region AÇÕES

        public Retorno Salvar(PessoaJogo Entity)
        {
            try
            {
                Retorno retorno = PreenchimentoObrigatorio(Entity);
                if (retorno.IsValido)
                {
                    if (Entity.Codigo == 0)
                        retorno = new DataPessoaJogo().Incluir(Entity);
                    else
                        retorno = new DataPessoaJogo().Alterar(Entity);
                }
                return retorno;
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        public Retorno Excluir(PessoaJogo Entity)
        {
            try
            {
                return new DataPessoaJogo().Excluir(Entity);
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
                return new DataPessoaJogo().Listar(parametros);
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        public Retorno Pesquisar(PessoaJogo Entity, ParametrosOrdenacaoPaginacao parametros)
        {
            try
            {
                var retorno = new DataPessoaJogo().Pesquisar(Entity, parametros);

                if (retorno.IsValido)
                    retorno.Entity = ConverterParaResultadoDataTables<PessoaJogo>(retorno, new DataPessoaJogo().PesquisarQuantidadeTotal(Entity).Entity.ConverteValor(0));

                return retorno;
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
                return new DataPessoaJogo().Listar();
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        public Retorno Consultar(PessoaJogo Entity)
        {
            try
            {
                return new DataPessoaJogo().Consultar(Entity);
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        #endregion

        #region METODOS AUXILIARES

        public Retorno PreenchimentoObrigatorio(PessoaJogo Entity)
        {
            if (Entity.Pessoa.Codigo == 0)
                return new Retorno(false, String.Format(Mensagens.MSG_01, "Amigo"));

            if (Entity.Jogo.Codigo == 0)
                return new Retorno(false, String.Format(Mensagens.MSG_01, "Jogo"));

            if (Entity.Data == DateTime.MinValue)
                return new Retorno(false, String.Format(Mensagens.MSG_01, "Data"));

            return new Retorno(true);
        }

        private Retorno VerificarExistencia(PessoaJogo Entity)
        {
            try
            {
                return new DataPessoaJogo().VerificarExistencia(Entity);
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }
        }

        #endregion

    }
}

