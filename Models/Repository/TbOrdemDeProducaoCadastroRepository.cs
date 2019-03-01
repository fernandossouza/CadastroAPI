
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CadastroAPI.Models.Repository
{
    public class TbOrdemDeProducaoCadastroRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public TbOrdemDeProducaoCadastroRepository (IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("CadastroConnection");
        }

        public async Task<IEnumerable<TbOrdemDeProducaoCadastro>> GetList()
        {
            IEnumerable<TbOrdemDeProducaoCadastro> orderList;
            string sSql = "SELECT OP.*,C.[codigo] as clone ";
            sSql += " FROM [SPI_TB_ORDEMDEPRODUCAO_CADASTRO] AS OP ";
            sSql += " INNER JOIN [SPI_TB_CLONE_CADASTRADO] AS C ON OP.[idClone]  = C.[id]";

            using(IDbConnection db = new SqlConnection(_connectionString)){
                orderList = await db.QueryAsync<TbOrdemDeProducaoCadastro>(sSql);
            }

            return orderList;
        }

        public async Task<TbOrdemDeProducaoCadastro> Insert(TbOrdemDeProducaoCadastro ordem)
        {
            IEnumerable<long> insertRow;
            string sSql = "INSERT INTO [SPI_TB_ORDEMDEPRODUCAO_CADASTRO] ([OP],[qntMudas],[qntLotes],[qntProduzida], [qntPerdida], [idClone])";
            sSql +=  " VALUES (@op, @qntMudas, @qntLotes, @qntProduzida, @qntPerdida, @idClone)";
            sSql +=  " SELECT @@IDENTITY";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                insertRow = await db.QueryAsync<long>(sSql, ordem);
            }

            if(insertRow == null || insertRow.Count() == 0)
                return null;
            
            ordem.id = insertRow.FirstOrDefault();

            return ordem;
        }

        public async Task<TbOrdemDeProducaoCadastro> Get(long id)
        {
            IEnumerable<TbOrdemDeProducaoCadastro> ordem;
            string sSql = "SELECT [id],[OP],[qntMudas],[qntLotes],[qntProduzida],[qntPerdida],[idClone]";
            sSql += "FROM [SPI_DB_CADASTROS].[dbo].[SPI_TB_ORDEMDEPRODUCAO_CADASTRO]";
            sSql += "WHERE id = " + id.ToString();

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                ordem = await db.QueryAsync<TbOrdemDeProducaoCadastro>(sSql);
            }

            if(ordem == null || ordem.Count() == 0)
                return null;
            
            return ordem.FirstOrDefault();
        }

        public async Task<TbOrdemDeProducaoCadastro> Update(TbOrdemDeProducaoCadastro ordem)
        {
            long updatedRow;
            string sSql = "UPDATE [SPI_TB_ORDEMDEPRODUCAO_CADASTRO]";
            sSql += " SET [OP] = @op, [qntMudas] = @qntMudas, [qntProduzida] =  @qntProduzida, [qntPerdida] = @qntPerdida, [idClone] = @idClone";
            sSql += " WHERE [id] = @id";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                updatedRow = await db.ExecuteAsync(sSql, ordem);
            }

            if(updatedRow > 0)
                return ordem;

            return null;
        }
    }
}