using DesafioDotNet.Commom;
using DesafioDotNet.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Text;

namespace DesafioDotNet.Data
{
    public class DataPessoa : DataBase
    {
        #region AÇÕES

        public Retorno Incluir(Pessoa Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("INSERT INTO TB_PESSOA( ");
                CommandSQL.AppendLine("NOME, ");
                CommandSQL.AppendLine("APELIDO) ");
                CommandSQL.AppendLine("VALUES (");
                CommandSQL.AppendLine("@NOME, ");
                CommandSQL.AppendLine("@APELIDO) ");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Command.Parameters.AddWithValue("@NOME", Entity.Nome);
                Command.Parameters.AddWithValue("@APELIDO", Entity.Apelido.IsNull() ? DBNull.Value : (object)Entity.Apelido);
                Abrir();
                Command.ExecuteNonQuery();
                return new Retorno(true, String.Format(Mensagens.MSG_02, "Salvo"));
            }
            catch (Exception ex)
            {
                if (((SqlCeException)ex).NativeError == 25016)
                    return new Retorno(false, String.Format(Mensagens.MSG_04, "Amigo", "Nome"));
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno Excluir(Pessoa Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("DELETE FROM TB_PESSOA WHERE CODIGO = @CODIGO");
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

        public Retorno Alterar(Pessoa Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("UPDATE TB_PESSOA SET ");
                CommandSQL.AppendLine("NOME = @NOME, ");
                CommandSQL.AppendLine("APELIDO = @APELIDO ");
                CommandSQL.AppendLine("WHERE CODIGO = @CODIGO");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Command.Parameters.AddWithValue("@CODIGO", Entity.Codigo);
                Command.Parameters.AddWithValue("@NOME", Entity.Nome);
                Command.Parameters.AddWithValue("@APELIDO", Entity.Apelido.IsNull() ? DBNull.Value : (object)Entity.Apelido);
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
                List<Pessoa> Pessoas = new List<Pessoa>();
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_PESSOA.CODIGO, ");
                CommandSQL.AppendLine("TB_PESSOA.NOME, ");
                CommandSQL.AppendLine("TB_PESSOA.APELIDO ");
                CommandSQL.AppendLine("FROM TB_PESSOA ");

                CommandSQL.AppendLine("ORDER BY NOME ");
                CommandSQL.AppendLine("OFFSET @INICIO ROWS FETCH NEXT @QUANTIDADE ROWS ONLY ");

                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();

                Command.Parameters.AddWithValue("@INICIO", parametros.Inicio);
                Command.Parameters.AddWithValue("@QUANTIDADE", parametros.Quantidade);

                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Pessoas.Add(FillEntity(Reader));
                }
                return new Retorno(Pessoas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno Pesquisar(Pessoa Entity, ParametrosOrdenacaoPaginacao parametros)
        {
            try
            {
                List<Pessoa> Pessoas = new List<Pessoa>();

                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_PESSOA.CODIGO, ");
                CommandSQL.AppendLine("TB_PESSOA.NOME, ");
                CommandSQL.AppendLine("TB_PESSOA.APELIDO ");
                CommandSQL.AppendLine("FROM TB_PESSOA ");
                CommandSQL.AppendLine("WHERE (TB_PESSOA.NOME LIKE '%" + Entity.Nome + "%' )");

                CommandSQL.AppendLine("ORDER BY NOME ");
                CommandSQL.AppendLine("OFFSET @INICIO ROWS FETCH NEXT @QUANTIDADE ROWS ONLY ");

                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@QUANTIDADE", parametros.Quantidade);
                Command.Parameters.AddWithValue("@INICIO", parametros.Inicio);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Pessoas.Add(FillEntity(Reader));
                }
                return new Retorno(Pessoas);
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
                List<Pessoa> Pessoas = new List<Pessoa>();
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_PESSOA.CODIGO, ");
                CommandSQL.AppendLine("TB_PESSOA.NOME, ");
                CommandSQL.AppendLine("TB_PESSOA.APELIDO ");
                CommandSQL.AppendLine("FROM TB_PESSOA ");

                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Pessoas.Add(FillEntity(Reader));
                }
                return new Retorno(Pessoas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno Consultar(Pessoa Entity)
        {
            try
            {
                Pessoa Pessoa = new Pessoa();
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_PESSOA.CODIGO, ");
                CommandSQL.AppendLine("TB_PESSOA.NOME, ");
                CommandSQL.AppendLine("TB_PESSOA.APELIDO ");
                CommandSQL.AppendLine("FROM TB_PESSOA ");

                CommandSQL.AppendLine("WHERE TB_PESSOA.CODIGO = @CODIGO ");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@CODIGO", Entity.Codigo);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Pessoa = FillEntity(Reader);
                }
                return new Retorno(Pessoa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        #endregion

        #region METODOS AUXILIARES

        public Retorno VerificarExistencia(Pessoa Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT 1 FROM TB_PESSOA ");
                CommandSQL.AppendLine("WHERE TB_PESSOA.NOME = @NOME ");
                CommandSQL.AppendLine("AND TB_PESSOA.CODIGO <> @CODIGO ");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@NOME", Entity.Nome);
                Command.Parameters.AddWithValue("@CODIGO", Entity.Codigo);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    return new Retorno(false, String.Format(Mensagens.MSG_04, "Pessoa", "Nome"));
                }
                return new Retorno(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        private Pessoa FillEntity(IDataReader reader)
        {
            Pessoa Pessoa = new Pessoa();
            try
            {
                Pessoa.Codigo = ConverterValorReader(reader, "CODIGO", 0);
                Pessoa.Nome = ConverterValorReader(reader, "NOME", String.Empty);
                Pessoa.Apelido = ConverterValorReader(reader, "APELIDO", String.Empty);
            }
            catch (Exception ex) { throw ex; }
            return Pessoa;
        }

        #endregion

    }
}

