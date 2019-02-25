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
            string sSql = "SELECT [id],[ordemProducaoId],[qntMuda],[qntPerdida], [status], [semana], [lote]";
            sSql += "FROM [SPI_DB_CADASTROS].[dbo].[SPI_TB_LOTE_CADASTRO]";

            using(IDbConnection db = new SqlConnection(_connectionString)){
                orderList = await db.QueryAsync<TbLoteCadastro>(sSql);
            }

            return orderList;
        }

        public async Task<TbLoteCadastro> Insert(TbLoteCadastro lote)
        {
            IEnumerable<long> insertRow;
            string sSql = "INSERT INTO [SPI_TB_LOTE_CADASTRO] ([ordemProducaoId],[qntMuda],[qntPerdida], [status], [semana], [lote])";
            sSql +=  " VALUES (@ordemProducaoId, @qntMuda, @qntPerdida, @status, @semana, @lote)";
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
            string sSql = "SELECT [id],[ordemProducaoId],[qntMuda],[qntPerdida], [status], [semana], [lote]";
            sSql += "FROM [SPI_DB_CADASTROS].[dbo].[SPI_TB_Lote_CADASTRO]";
            sSql += "WHERE id = " + id.ToString();

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                ordem = await db.QueryAsync<TbLoteCadastro>(sSql);
            }

            if(ordem == null || ordem.Count() == 0)
                return null;
            
            return ordem.FirstOrDefault();
        }

        // public async Task<TbOrdemDeProducaoCadastro> Update(TbOrdemDeProducaoCadastro ordem)
        // {
        //     long updatedRow;
        //     string sSql = "UPDATE [SPI_TB_ORDEMDEPRODUCAO_CADASTRO]";
        //     sSql += " SET [OP] = @op, [qntMudas] = @qntMudas, [qntProduzida] =  @qntProduzida, [qntPerdida] = @qntPerdida, [idClone] = @idClone";
        //     sSql += " WHERE [id] = @id";

        //     using (IDbConnection db = new SqlConnection(_connectionString))
        //     {
        //         updatedRow = await db.ExecuteAsync(sSql, ordem);
        //     }

        //     if(updatedRow > 0)
        //         return ordem;

        //     return null;
        // }
    }
}