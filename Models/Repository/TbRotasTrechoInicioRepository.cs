using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CadastroAPI.Models.Repository
{
    public class TbRotasTrechoInicioRepository
    {
        private string stringConnection;
        private readonly IConfiguration _configuration;

        public TbRotasTrechoInicioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString("CadastroConnection");
        }

        public async Task<IEnumerable<TbRotasTrechoInicio>> GetTrechoAll()
        {
            IEnumerable<TbRotasTrechoInicio> rotasTrechoList;
            string sSql = string.Empty;
            sSql = "SELECT * ";
            sSql = sSql + " FROM [SPI_TB_ROTAS_TRECHOS_INICIO] ";

            using (IDbConnection db = new SqlConnection(stringConnection))
            {
                rotasTrechoList = await db.QueryAsync<TbRotasTrechoInicio>(sSql);
            }

            return rotasTrechoList;
        }

    }
}