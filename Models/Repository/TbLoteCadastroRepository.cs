using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CadastroAPI.Models.Repository
{
    public class TbLoteCadastroRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public TbLoteCadastroRepository (IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("CadastroConnection");
        }

        public async Task<IEnumerable<TbLoteCadastro>> GetList()
        {
            IEnumerable<TbLoteCadastro> orderList;
            string sSql = "SELECT * ";
            sSql += "FROM [SPI_DB_CADASTROS].[dbo].[SPI_TB_LOTE_CADASTRO]";
            sSql += " WHERE status ='true' ";

            using(IDbConnection db = new SqlConnection(_connectionString)){
                orderList = await db.QueryAsync<TbLoteCadastro>(sSql);
            }

            return orderList;
        }

        public async Task<IEnumerable<TbLoteCadastro>> GetPorOrdemProducaoIdList(long ordemProducaoId)
        {
            IEnumerable<TbLoteCadastro> orderList;
            string sSql = "SELECT * ";
            sSql += "FROM [SPI_DB_CADASTROS].[dbo].[SPI_TB_LOTE_CADASTRO]";
            sSql += " WHERE [ordemProducaoId] ="+ ordemProducaoId;

            using(IDbConnection db = new SqlConnection(_connectionString)){
                orderList = await db.QueryAsync<TbLoteCadastro>(sSql);
            }

            return orderList;
        }

        public async Task<TbLoteCadastro> Insert(TbLoteCadastro lote)
        {
            IEnumerable<long> insertRow;
            string sSql = "INSERT INTO [SPI_TB_LOTE_CADASTRO] ([ordemProducaoId],[qntMuda],[qntPerdida], [status], [semana], [lote],[ordemProducao],[clone])";
            sSql +=  " VALUES (@ordemProducaoId, @qntMuda, @qntPerdida, @status, @semana, @lote,@ordemProducao,@clone)";
            sSql +=  " SELECT @@IDENTITY";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                insertRow = await db.QueryAsync<long>(sSql, lote);
            }

            if(insertRow == null || insertRow.Count() == 0)
                return null;
            
            lote.Id = insertRow.FirstOrDefault();

            return lote;
        }

        public async Task<TbLoteCadastro> Get(long id)
        {
            IEnumerable<TbLoteCadastro> ordem;
            string sSql = "SELECT * ";
            sSql += "FROM [SPI_DB_CADASTROS].[dbo].[SPI_TB_Lote_CADASTRO]";
            sSql += "WHERE status ='true' AND id = " + id.ToString();

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                ordem = await db.QueryAsync<TbLoteCadastro>(sSql);
            }

            if(ordem == null || ordem.Count() == 0)
                return null;
            
            return ordem.FirstOrDefault();
        }

        public async Task<IEnumerable<TbLoteCadastro>> GetSemanaVigente(int numeroSemana)
        {
            IEnumerable<TbLoteCadastro> ordem;
            string sSql = "SELECT * ";
            sSql += "FROM [SPI_TB_Lote_CADASTRO]";
            sSql += "WHERE status ='true' AND semana = " + numeroSemana.ToString();

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                ordem = await db.QueryAsync<TbLoteCadastro>(sSql);
            }

            if(ordem == null || ordem.Count() == 0)
                return null;
            
            return ordem;
        }

        public async Task<bool> delete(TbLoteCadastro ordem)
        {
            long updatedRow;
            string sSql = "UPDATE [SPI_TB_Lote_CADASTRO] ";
            sSql += " SET [status] = 'false' ";
            sSql += " WHERE [id] = @id";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                updatedRow = await db.ExecuteAsync(sSql, ordem);
            }

            if(updatedRow > 0)
                return true;

            return false;
        }
    }
}