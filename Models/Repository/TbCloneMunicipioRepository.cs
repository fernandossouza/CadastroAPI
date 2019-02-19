using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CadastroAPI.Models.Repository
{
    public class TbCloneMunicipioRepository
    {
        
        private string stringConnection;
        private readonly IConfiguration _configuration;

        public TbCloneMunicipioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString("CadastroConnection");
        }

        public async Task<IEnumerable<TbCloneMunicipio>> GetMunicipios()
        {
            IEnumerable<TbCloneMunicipio> municipioList;
            string sSql = string.Empty;
            sSql = "SELECT [id],[municipio] ";
            sSql = sSql + " FROM [SPI_TB_CLONE_MUNICIPIO] ";

            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                municipioList = await db.QueryAsync<TbCloneMunicipio>(sSql);
            }
                return municipioList;
        }
    }
}