using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Linq;

namespace CadastroAPI.Models.Repository
{
    public class TbRotasTrechoXRotasRepository
    {
        private string stringConnection;
        private readonly IConfiguration _configuration;

        public TbRotasTrechoXRotasRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString("CadastroConnection");
        }

        public async Task<TbRotasTrechoXRotas> InsertTrechoXRota(TbRotasTrechoXRotas trecho)
        {
            IEnumerable<long> insertRow;
            string sSql = string.Empty;
            sSql = "INSERT INTO [SPI_TB_ROTAS_TRECHOS_POR_ROTAS]([sentidoId],[ordem],[rotaId]) ";
            sSql = sSql + " VALUES (@sentidoId,@ordem,@rotaId) ";
            sSql = sSql + " SELECT @@IDENTITY ";

            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                insertRow = await db.QueryAsync<long>(sSql,trecho);
            }

            if(insertRow == null || insertRow.Count() == 0)
                return null;

            trecho.id = insertRow.FirstOrDefault();

            return trecho;
        }

        public async Task<TbRotasTrechoXRotas> UpdateTrechoXRota(TbRotasTrechoXRotas trecho)
        {
            int updateRow;
            string sSql = string.Empty;
            sSql = "UPDATE [SPI_TB_ROTAS_TRECHOS_POR_ROTAS] ";
            sSql = sSql + " SET [sentidoId] = @sentidoId,[ordem] = @ordem,[rotaId] = @rotaId ";
            sSql = sSql + " WHERE id = @id ";
            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                updateRow = await db.ExecuteAsync(sSql,trecho);
            }

            if(updateRow > 0)
                return trecho;
            else
                return null;
        }

        public async Task<bool> DeleteTrechoXRota(long id)
        {
            int updateRow;
            var o = new {id=id};
            string sSql = string.Empty;
            sSql = "DELETE FROM [SPI_TB_ROTAS_TRECHOS_POR_ROTAS] ";
            sSql = sSql + " WHERE id = @id ";
            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                updateRow = await db.ExecuteAsync(sSql,o);
            }

            if(updateRow > 0)
                return true;
            else
                return false;
        }
    }
}