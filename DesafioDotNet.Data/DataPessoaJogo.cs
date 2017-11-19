using DesafioDotNet.Commom;
using DesafioDotNet.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Text;

namespace DesafioDotNet.Data
{
    public class DataPessoaJogo : DataBase
    {
        #region AÇÕES

        public Retorno Incluir(PessoaJogo Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("INSERT INTO TB_PESSOA_JOGO( ");
                CommandSQL.AppendLine("DATA, ");
                CommandSQL.AppendLine("CODIGO_PESSOA, ");
                CommandSQL.AppendLine("CODIGO_JOGO) ");
                CommandSQL.AppendLine("VALUES (");
                CommandSQL.AppendLine("@DATA, ");
                CommandSQL.AppendLine("@CODIGO_PESSOA, ");
                CommandSQL.AppendLine("@CODIGO_JOGO) ");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Command.Parameters.AddWithValue("@DATA", Entity.Data);
                Command.Parameters.AddWithValue("@CODIGO_PESSOA", Entity.Pessoa.Codigo);
                Command.Parameters.AddWithValue("@CODIGO_JOGO", Entity.Jogo.Codigo);
                Abrir();
                Command.ExecuteNonQuery();
                return new Retorno(true, String.Format(Mensagens.MSG_02, "Salvo"));
            }
            catch (Exception ex)
            {
                if (((SqlCeException)ex).NativeError == 25016)
                    return new Retorno(false, Mensagens.MSG_10);
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno Excluir(PessoaJogo Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("DELETE FROM TB_PESSOA_JOGO WHERE CODIGO = @CODIGO");
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

        public Retorno Alterar(PessoaJogo Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("UPDATE TB_PESSOA_JOGO SET ");
                CommandSQL.AppendLine("CODIGO_PESSOA = @CODIGO_PESSOA, ");
                CommandSQL.AppendLine("CODIGO_JOGO = @CODIGO_JOGO, ");
                CommandSQL.AppendLine("DATA = @DATA ");
                CommandSQL.AppendLine("WHERE CODIGO = @CODIGO");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Command.Parameters.AddWithValue("@CODIGO", Entity.Codigo);
                Command.Parameters.AddWithValue("@CODIGO_PESSOA", Entity.Pessoa.Codigo);
                Command.Parameters.AddWithValue("@CODIGO_JOGO", Entity.Jogo.Codigo);
                Command.Parameters.AddWithValue("@DATA", Entity.Data);
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
                List<PessoaJogo> PessoaJogos = new List<PessoaJogo>();

                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.CODIGO AS CODIGO_PESSOA_JOGO, ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.DATA, ");
                CommandSQL.AppendLine("TB_PESSOA.CODIGO AS CODIGO_PESSOA, ");
                CommandSQL.AppendLine("TB_PESSOA.NOME, ");
                CommandSQL.AppendLine("TB_PESSOA.APELIDO, ");
                CommandSQL.AppendLine("TB_JOGO.CODIGO AS CODIGO_JOGO, ");
                CommandSQL.AppendLine("TB_JOGO.DESCRICAO ");
                CommandSQL.AppendLine("FROM TB_PESSOA_JOGO ");
                CommandSQL.AppendLine("INNER JOIN TB_PESSOA ON ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.CODIGO_PESSOA = TB_PESSOA.CODIGO ");
                CommandSQL.AppendLine("INNER JOIN TB_JOGO ON ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.CODIGO_JOGO = TB_JOGO.CODIGO ");

                CommandSQL.AppendLine("ORDER BY DESCRICAO ");
                CommandSQL.AppendLine("OFFSET @INICIO ROWS FETCH NEXT @QUANTIDADE ROWS ONLY ");

                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@QUANTIDADE", parametros.Quantidade);
                Command.Parameters.AddWithValue("@INICIO", parametros.Inicio);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    PessoaJogos.Add(FillEntity(Reader));
                }
                return new Retorno(PessoaJogos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno Pesquisar(PessoaJogo Entity, ParametrosOrdenacaoPaginacao parametros)
        {
            try
            {
                List<PessoaJogo> PessoaJogos = new List<PessoaJogo>();



                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.CODIGO AS CODIGO_PESSOA_JOGO, ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.DATA, ");
                CommandSQL.AppendLine("TB_PESSOA.CODIGO AS CODIGO_PESSOA, ");
                CommandSQL.AppendLine("TB_PESSOA.NOME, ");
                CommandSQL.AppendLine("TB_PESSOA.APELIDO, ");
                CommandSQL.AppendLine("TB_JOGO.CODIGO AS CODIGO_JOGO, ");
                CommandSQL.AppendLine("TB_JOGO.DESCRICAO ");
                CommandSQL.AppendLine("FROM TB_PESSOA_JOGO ");
                CommandSQL.AppendLine("INNER JOIN TB_PESSOA ON ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.CODIGO_PESSOA = TB_PESSOA.CODIGO ");
                CommandSQL.AppendLine("INNER JOIN TB_JOGO ON ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.CODIGO_JOGO = TB_JOGO.CODIGO ");
                CommandSQL.AppendLine("WHERE TB_PESSOA.NOME LIKE @NOME_PESSOA ");
                CommandSQL.AppendLine("AND TB_JOGO.DESCRICAO LIKE @DESCRICAO_JOGO ");

                CommandSQL.AppendLine("ORDER BY DESCRICAO ");
                CommandSQL.AppendLine("OFFSET @INICIO ROWS FETCH NEXT @QUANTIDADE ROWS ONLY ");

                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@QUANTIDADE", parametros.Quantidade);
                Command.Parameters.AddWithValue("@INICIO", parametros.Inicio);
                Command.Parameters.AddWithValue("@NOME_PESSOA", "%" + Entity.Pessoa.Nome.Trim() + "%");
                Command.Parameters.AddWithValue("@DESCRICAO_JOGO", "%" + Entity.Jogo.Descricao.Trim() + "%");
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    PessoaJogos.Add(FillEntity(Reader));
                }
                return new Retorno(PessoaJogos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno PesquisarQuantidadeTotal(PessoaJogo Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT COUNT(1) ");
                CommandSQL.AppendLine("FROM TB_PESSOA_JOGO ");
                CommandSQL.AppendLine("INNER JOIN TB_PESSOA ON ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.CODIGO_PESSOA = TB_PESSOA.CODIGO ");
                CommandSQL.AppendLine("INNER JOIN TB_JOGO ON ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.CODIGO_JOGO = TB_JOGO.CODIGO ");
                CommandSQL.AppendLine("WHERE TB_PESSOA.NOME LIKE @NOME_PESSOA ");
                CommandSQL.AppendLine("AND TB_JOGO.DESCRICAO LIKE @DESCRICAO_JOGO ");

                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@NOME_PESSOA", "%" + Entity.Pessoa.Nome.Trim() + "%");
                Command.Parameters.AddWithValue("@DESCRICAO_JOGO", "%" + Entity.Jogo.Descricao.Trim() + "%");

                return new Retorno(Command.ExecuteScalar());
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
                List<PessoaJogo> PessoaJogos = new List<PessoaJogo>();
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.CODIGO AS CODIGO_PESSOA_JOGO, ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.DATA, ");
                CommandSQL.AppendLine("TB_PESSOA.CODIGO AS CODIGO_PESSOA, ");
                CommandSQL.AppendLine("TB_PESSOA.NOME, ");
                CommandSQL.AppendLine("TB_PESSOA.APELIDO, ");
                CommandSQL.AppendLine("TB_JOGO.CODIGO AS CODIGO_JOGO, ");
                CommandSQL.AppendLine("TB_JOGO.DESCRICAO ");
                CommandSQL.AppendLine("FROM TB_PESSOA_JOGO ");
                CommandSQL.AppendLine("INNER JOIN TB_PESSOA ON ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.CODIGO_PESSOA = TB_PESSOA.CODIGO ");
                CommandSQL.AppendLine("INNER JOIN TB_JOGO ON ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.CODIGO_JOGO = TB_JOGO.CODIGO ");

                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    PessoaJogos.Add(FillEntity(Reader));
                }
                return new Retorno(PessoaJogos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno Consultar(PessoaJogo Entity)
        {
            try
            {
                PessoaJogo PessoaJogo = new PessoaJogo();
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.CODIGO AS CODIGO_PESSOA_JOGO, ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.DATA, ");
                CommandSQL.AppendLine("TB_PESSOA.CODIGO AS CODIGO_PESSOA, ");
                CommandSQL.AppendLine("TB_PESSOA.NOME, ");
                CommandSQL.AppendLine("TB_PESSOA.APELIDO, ");
                CommandSQL.AppendLine("TB_JOGO.CODIGO AS CODIGO_JOGO, ");
                CommandSQL.AppendLine("TB_JOGO.DESCRICAO ");
                CommandSQL.AppendLine("FROM TB_PESSOA_JOGO ");
                CommandSQL.AppendLine("INNER JOIN TB_PESSOA ON ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.CODIGO_PESSOA = TB_PESSOA.CODIGO ");
                CommandSQL.AppendLine("INNER JOIN TB_JOGO ON ");
                CommandSQL.AppendLine("TB_PESSOA_JOGO.CODIGO_JOGO = TB_JOGO.CODIGO ");

                CommandSQL.AppendLine("WHERE TB_PESSOA_JOGO.CODIGO = @CODIGO ");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@CODIGO", Entity.Codigo);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    PessoaJogo = FillEntity(Reader);
                }
                return new Retorno(PessoaJogo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        #endregion

        #region METODOS AUXILIARES

        public Retorno VerificarExistencia(PessoaJogo Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT 1 FROM TB_PESSOA_JOGO ");
                CommandSQL.AppendLine("WHERE TB_PESSOA_JOGO.DATA = @DATA ");
                CommandSQL.AppendLine("AND TB_PESSOA_JOGO.CODIGO <> @CODIGO ");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@DATA", Entity.Data);
                Command.Parameters.AddWithValue("@CODIGO", Entity.Codigo);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    return new Retorno(false, String.Format(Mensagens.MSG_04, "PessoaJogo", "Data"));
                }
                return new Retorno(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        private PessoaJogo FillEntity(IDataReader reader)
        {
            PessoaJogo PessoaJogo = new PessoaJogo();
            try
            {
                PessoaJogo.Codigo = ConverterValorReader(reader, "CODIGO_PESSOA_JOGO", 0);
                PessoaJogo.Data = ConverterValorReader(reader, "DATA", DateTime.MinValue);
                PessoaJogo.Pessoa.Codigo = ConverterValorReader(reader, "CODIGO_PESSOA", 0);
                PessoaJogo.Pessoa.Nome = ConverterValorReader(reader, "NOME", String.Empty);
                PessoaJogo.Pessoa.Apelido = ConverterValorReader(reader, "APELIDO", String.Empty);
                PessoaJogo.Jogo.Codigo = ConverterValorReader(reader, "CODIGO_JOGO", 0);
                PessoaJogo.Jogo.Descricao = ConverterValorReader(reader, "DESCRICAO", String.Empty);
            }
            catch (Exception ex) { throw ex; }
            return PessoaJogo;
        }

        #endregion

    }
}

