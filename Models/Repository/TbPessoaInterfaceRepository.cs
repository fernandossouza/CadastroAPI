using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CadastroAPI.Models.Repository
{
    public class TbPessoaInterfaceRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public TbPessoaInterfaceRepository (IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("CadastroConnection");
        }

        public async Task<TbPessoaInterface> GetRfidPorMesa(long id)
        {
            IEnumerable<TbPessoaInterface> cracha;
            string sSql = "SELECT  * ";
            sSql += "FROM [SPI_DB_CADASTROS].[dbo].[SPI_TB_PESSOA_INTERFACE_CRACHA]";
            sSql += " WHERE rfid IS NOT NULL AND dtLido IS NULL AND id =" + id ;

            using(IDbConnection db = new SqlConnection(_connectionString)){
                cracha = await db.QueryAsync<TbPessoaInterface>(sSql);
            }

            if(cracha == null || cracha.Count() == 0)
                return null;

            return cracha.FirstOrDefault();
        }

        public async Task<TbPessoaInterface> Insert(TbPessoaInterface pessoaCracha)
        {
            IEnumerable<long> insertRow;
            string sSql = "INSERT INTO [SPI_TB_PESSOA_INTERFACE_CRACHA] ([mesa]) VALUES (@mesa) ";
            sSql +=  " SELECT @@IDENTITY";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                insertRow = await db.QueryAsync<long>(sSql, pessoaCracha);
            }

            if(insertRow == null || insertRow.Count() == 0)
                return null;
            
            pessoaCracha.id = insertRow.FirstOrDefault();

            return pessoaCracha;
        }


        public async Task<TbPessoaInterface> Update(TbPessoaInterface pessoaCracha)
        {
            int updateRow;
            string sSql = string.Empty;
            sSql = "UPDATE [SPI_TB_PESSOA_INTERFACE_CRACHA]";
            sSql += "SET [dtLido] = GETDATE() ";
            sSql += "WHERE [id] = @id";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                updateRow = await db.ExecuteAsync(sSql, pessoaCracha);
            }

            if (updateRow > 0)
                return pessoaCracha;
            else
                return null;
        }
    }
}