using DesafioDotNet.Commom;
using DesafioDotNet.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Text;

namespace DesafioDotNet.Data
{
    public class DataJogo : DataBase
    {
        #region AÇÕES

        public Retorno Incluir(Jogo Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("INSERT INTO TB_JOGO( ");
                CommandSQL.AppendLine("DESCRICAO) ");
                CommandSQL.AppendLine("VALUES (");
                CommandSQL.AppendLine("@DESCRICAO) ");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Command.Parameters.AddWithValue("@DESCRICAO", Entity.Descricao);
                Abrir();
                Command.ExecuteNonQuery();
                return new Retorno(true, String.Format(Mensagens.MSG_02, "Salvo"));
            }
            catch (Exception ex)
            {
                if (((SqlCeException)ex).NativeError == 25016)
                    return new Retorno(false, String.Format(Mensagens.MSG_04, "Jogo", "Descrição"));
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno Excluir(Jogo Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("DELETE FROM TB_JOGO WHERE CODIGO = @CODIGO");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Command.Parameters.AddWithValue("@CODIGO", Entity.Codigo);
                Abrir();
                Command.ExecuteNonQuery();
                return new Retorno(true, String.Format(Mensagens.MSG_02, "Excluido "));
            }
            catch (Exception ex)
            {
                if (((SqlCeException)ex).NativeError == 25025)
                    return new Retorno(false, Mensagens.MSG_06);
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno Alterar(Jogo Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("UPDATE TB_JOGO SET ");
                CommandSQL.AppendLine("DESCRICAO = @DESCRICAO ");
                CommandSQL.AppendLine("WHERE CODIGO = @CODIGO");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Command.Parameters.AddWithValue("@CODIGO", Entity.Codigo);
                Command.Parameters.AddWithValue("@DESCRICAO", Entity.Descricao);
                Abrir();
                Command.ExecuteNonQuery();
                return new Retorno(true, String.Format(Mensagens.MSG_02, "Alterado "));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        #endregion

        #region CONSULTAS

        public Retorno Listar(ParametrosOrdenacaoPaginacao parametros)
        {
            try
            {
                List<Jogo> Jogos = new List<Jogo>();

                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_JOGO.CODIGO, ");
                CommandSQL.AppendLine("TB_JOGO.DESCRICAO ");
                CommandSQL.AppendLine("FROM TB_JOGO ");

                CommandSQL.AppendLine("ORDER BY DESCRICAO ");
                CommandSQL.AppendLine("OFFSET @INICIO ROWS FETCH NEXT @QUANTIDADE ROWS ONLY ");

                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@QUANTIDADE", parametros.Quantidade);
                Command.Parameters.AddWithValue("@INICIO", parametros.Inicio);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Jogos.Add(FillEntity(Reader));
                }
                return new Retorno(Jogos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno Pesquisar(Jogo Entity, ParametrosOrdenacaoPaginacao parametros)
        {
            try
            {
                List<Jogo> Jogos = new List<Jogo>();

                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_JOGO.CODIGO, ");
                CommandSQL.AppendLine("TB_JOGO.DESCRICAO ");
                CommandSQL.AppendLine("FROM TB_JOGO ");

                CommandSQL.AppendLine("WHERE (TB_JOGO.DESCRICAO LIKE '%" + Entity.Descricao + "%' )");

                CommandSQL.AppendLine("ORDER BY DESCRICAO ");
                CommandSQL.AppendLine("OFFSET @INICIO ROWS FETCH NEXT @QUANTIDADE ROWS ONLY ");

                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@QUANTIDADE", parametros.Quantidade);
                Command.Parameters.AddWithValue("@INICIO", parametros.Inicio);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Jogos.Add(FillEntity(Reader));
                }
                return new Retorno(Jogos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno Listar()
        {
            try
            {
                List<Jogo> Jogos = new List<Jogo>();
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_JOGO.CODIGO, ");
                CommandSQL.AppendLine("TB_JOGO.DESCRICAO ");
                CommandSQL.AppendLine("FROM TB_JOGO ");

                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Jogos.Add(FillEntity(Reader));
                }
                return new Retorno(Jogos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno Consultar(Jogo Entity)
        {
            try
            {
                Jogo Jogo = new Jogo();
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_JOGO.CODIGO, ");
                CommandSQL.AppendLine("TB_JOGO.DESCRICAO ");
                CommandSQL.AppendLine("FROM TB_JOGO ");

                CommandSQL.AppendLine("WHERE TB_JOGO.CODIGO = @CODIGO ");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@CODIGO", Entity.Codigo);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Jogo = FillEntity(Reader);
                }
                return new Retorno(Jogo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        #endregion

        #region METODOS AUXILIARES

        public Retorno VerificarExistencia(Jogo Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT 1 FROM TB_JOGO ");
                CommandSQL.AppendLine("WHERE TB_JOGO.DESCRICAO = @DESCRICAO ");
                CommandSQL.AppendLine("AND TB_JOGO.CODIGO <> @CODIGO ");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@DESCRICAO", Entity.Descricao);
                Command.Parameters.AddWithValue("@CODIGO", Entity.Codigo);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    return new Retorno(false, String.Format(Mensagens.MSG_04, "Jogo", "Descricao"));
                }
                return new Retorno(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        private Jogo FillEntity(IDataReader reader)
        {
            Jogo Jogo = new Jogo();
            try
            {
                Jogo.Codigo = ConverterValorReader(reader, "CODIGO", 0);
                Jogo.Descricao = ConverterValorReader(reader, "DESCRICAO", String.Empty);
            }
            catch (Exception ex) { throw ex; }
            return Jogo;
        }

        #endregion

    }
}

