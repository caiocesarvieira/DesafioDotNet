using DesafioDotNet.Commom;
using DesafioDotNet.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Text;

namespace DesafioDotNet.Data
{
    public class DataLogin : DataBase
    {
        #region AÇÕES

        public Retorno Incluir(Login Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("INSERT INTO TB_LOGIN( ");
                CommandSQL.AppendLine("USUARIO, ");
                CommandSQL.AppendLine("SENHA, ");
                CommandSQL.AppendLine("CODIGO_PESSOA) ");
                CommandSQL.AppendLine("VALUES (");
                CommandSQL.AppendLine("@USUARIO, ");
                CommandSQL.AppendLine("@SENHA, ");
                CommandSQL.AppendLine("@CODIGO_PESSOA) ");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Command.Parameters.AddWithValue("@USUARIO", Entity.Usuario);
                Command.Parameters.AddWithValue("@SENHA", Entity.Senha);
                Command.Parameters.AddWithValue("@CODIGO_PESSOA", Entity.Pessoa.Codigo);
                Abrir();
                Command.ExecuteNonQuery();
                return new Retorno(true, String.Format(Mensagens.MSG_02, "Salvo"));
            }
            catch (Exception ex)
            {
                if (((SqlCeException)ex).NativeError == 25016)
                    return new Retorno(false, String.Format(Mensagens.MSG_04, "Login", "Usuario"));
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno Excluir(Login Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("DELETE FROM TB_LOGIN WHERE CODIGO = @CODIGO");
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

        public Retorno Alterar(Login Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("UPDATE TB_LOGIN SET ");
                CommandSQL.AppendLine("USUARIO = @USUARIO, ");
                CommandSQL.AppendLine("SENHA = @SENHA ");
                CommandSQL.AppendLine("WHERE CODIGO = @CODIGO");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Command.Parameters.AddWithValue("@CODIGO", Entity.Codigo);
                Command.Parameters.AddWithValue("@USUARIO", Entity.Usuario);
                Command.Parameters.AddWithValue("@SENHA", Entity.Senha);
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
                List<Login> Logins = new List<Login>();

                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_LOGIN.CODIGO AS CODIGO_LOGIN, ");
                CommandSQL.AppendLine("TB_LOGIN.USUARIO, ");
                CommandSQL.AppendLine("TB_LOGIN.SENHA, ");
                CommandSQL.AppendLine("TB_PESSOA.CODIGO AS CODIGO_PESSOA, ");
                CommandSQL.AppendLine("TB_PESSOA.NOME, ");
                CommandSQL.AppendLine("TB_PESSOA.APELIDO ");
                CommandSQL.AppendLine("FROM TB_LOGIN ");
                CommandSQL.AppendLine("INNER JOIN TB_PESSOA ON ");
                CommandSQL.AppendLine("TB_LOGIN.CODIGO_PESSOA = TB_PESSOA.CODIGO ");

                CommandSQL.AppendLine("ORDER BY USUARIO ");
                CommandSQL.AppendLine("OFFSET @INICIO ROWS FETCH NEXT @QUANTIDADE ROWS ONLY ");

                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@QUANTIDADE", parametros.Quantidade);
                Command.Parameters.AddWithValue("@INICIO", parametros.Inicio);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Logins.Add(FillEntity(Reader));
                }
                return new Retorno(Logins);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno Pesquisar(Login Entity, ParametrosOrdenacaoPaginacao parametros)
        {
            try
            {
                List<Login> Logins = new List<Login>();

                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_LOGIN.CODIGO AS CODIGO_LOGIN, ");
                CommandSQL.AppendLine("TB_LOGIN.USUARIO, ");
                CommandSQL.AppendLine("TB_LOGIN.SENHA, ");
                CommandSQL.AppendLine("TB_PESSOA.CODIGO AS CODIGO_PESSOA, ");
                CommandSQL.AppendLine("TB_PESSOA.NOME, ");
                CommandSQL.AppendLine("TB_PESSOA.APELIDO ");
                CommandSQL.AppendLine("FROM TB_LOGIN ");
                CommandSQL.AppendLine("INNER JOIN TB_PESSOA ON ");
                CommandSQL.AppendLine("TB_LOGIN.CODIGO_PESSOA = TB_PESSOA.CODIGO ");
                CommandSQL.AppendLine("WHERE (TB_LOGIN.USUARIO LIKE '%" + Entity.Usuario + "%' )");

                CommandSQL.AppendLine("ORDER BY USUARIO ");
                CommandSQL.AppendLine("OFFSET @INICIO ROWS FETCH NEXT @QUANTIDADE ROWS ONLY ");

                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@QUANTIDADE", parametros.Quantidade);
                Command.Parameters.AddWithValue("@INICIO", parametros.Inicio);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Logins.Add(FillEntity(Reader));
                }
                return new Retorno(Logins);
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
                List<Login> Logins = new List<Login>();
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_LOGIN.CODIGO AS CODIGO_LOGIN, ");
                CommandSQL.AppendLine("TB_LOGIN.USUARIO, ");
                CommandSQL.AppendLine("TB_LOGIN.SENHA, ");
                CommandSQL.AppendLine("TB_PESSOA.CODIGO AS CODIGO_PESSOA, ");
                CommandSQL.AppendLine("TB_PESSOA.NOME, ");
                CommandSQL.AppendLine("TB_PESSOA.APELIDO ");
                CommandSQL.AppendLine("FROM TB_LOGIN ");
                CommandSQL.AppendLine("INNER JOIN TB_PESSOA ON ");
                CommandSQL.AppendLine("TB_LOGIN.CODIGO_PESSOA = TB_PESSOA.CODIGO ");

                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Logins.Add(FillEntity(Reader));
                }
                return new Retorno(Logins);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        public Retorno Consultar(Login Entity)
        {
            try
            {
                Login Login = new Login();
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT ");
                CommandSQL.AppendLine("TB_LOGIN.CODIGO AS CODIGO_LOGIN, ");
                CommandSQL.AppendLine("TB_LOGIN.USUARIO, ");
                CommandSQL.AppendLine("TB_LOGIN.SENHA, ");
                CommandSQL.AppendLine("TB_PESSOA.CODIGO AS CODIGO_PESSOA, ");
                CommandSQL.AppendLine("TB_PESSOA.NOME, ");
                CommandSQL.AppendLine("TB_PESSOA.APELIDO ");
                CommandSQL.AppendLine("FROM TB_LOGIN ");
                CommandSQL.AppendLine("INNER JOIN TB_PESSOA ON ");
                CommandSQL.AppendLine("TB_LOGIN.CODIGO_PESSOA = TB_PESSOA.CODIGO ");

                CommandSQL.AppendLine("WHERE TB_LOGIN.USUARIO = @USUARIO ");
                CommandSQL.AppendLine("AND TB_LOGIN.SENHA = @SENHA ");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@USUARIO", Entity.Usuario);
                Command.Parameters.AddWithValue("@SENHA", Entity.Senha);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Login = FillEntity(Reader);
                }
                return new Retorno(Login);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        #endregion

        #region METODOS AUXILIARES

        public Retorno VerificarExistencia(Login Entity)
        {
            try
            {
                CommandSQL = new StringBuilder();
                CommandSQL.AppendLine("SELECT 1 FROM TB_LOGIN ");
                CommandSQL.AppendLine("WHERE TB_LOGIN.USUARIO = @USUARIO ");
                CommandSQL.AppendLine("AND TB_LOGIN.CODIGO <> @CODIGO ");
                Command = CriaComandoSQL(CommandSQL.ToString());
                Abrir();
                Command.Parameters.AddWithValue("@USUARIO", Entity.Usuario);
                Command.Parameters.AddWithValue("@CODIGO", Entity.Codigo);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    return new Retorno(false, String.Format(Mensagens.MSG_04, "Login", "Usuario"));
                }
                return new Retorno(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Fechar(); }
        }

        private Login FillEntity(IDataReader reader)
        {
            Login Login = new Login();
            try
            {
                Login.Codigo = ConverterValorReader(reader, "CODIGO_LOGIN", 0);
                Login.Usuario = ConverterValorReader(reader, "USUARIO", String.Empty);
                Login.Senha = ConverterValorReader(reader, "SENHA", String.Empty);
                Login.Pessoa.Codigo = ConverterValorReader(reader, "CODIGO_PESSOA", 0);
                Login.Pessoa.Nome = ConverterValorReader(reader, "NOME", String.Empty);
                Login.Pessoa.Apelido = ConverterValorReader(reader, "APELIDO", String.Empty);
            }
            catch (Exception ex) { throw ex; }
            return Login;
        }

        #endregion

    }
}

