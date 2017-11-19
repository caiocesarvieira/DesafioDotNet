using DesafioDotNet.Commom;
using DesafioDotNet.Data;
using System;
using System.Collections.Generic;

namespace DesafioDotNet.Business
{
    public class BusinessBase
    {
        public ResultadoDataTables<T> ConverterParaResultadoDataTables<T>(Retorno retorno, int quantidadeTotalRegistros)
        {
            return new ResultadoDataTables<T>
            {
                Lista = (List<T>)retorno.Entity,
                QuantidadeRegistrosTotal = quantidadeTotalRegistros
            };
        }


        public static Retorno RecuperarTotalRegistros(string NomeTabela, string ChavePrimaria, string where)
        {
            return new DataBase().RecuperarTotalRegistros(NomeTabela, ChavePrimaria, where);
        }

        public static Retorno VerificarConexao()
        {
            try
            {
                return new DataBase().VerificarConexao();
            }
            catch (Exception ex)
            {
                return Retorno.CriarRetornoExcecao(ex);
            }

        }
    }
}
