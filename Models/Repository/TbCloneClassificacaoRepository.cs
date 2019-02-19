using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CadastroAPI.Models.Repository
{
    public class TbCloneClassificacaoRepository
    {
        private string stringConnection;
        private readonly IConfiguration _configuration;

        public TbCloneClassificacaoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString("CadastroConnection");
        }

        public async Task<TbCloneClassificacao> GetClassificacaoPorSelecaoIdENome(long selecaoId, string nome)
        {
            TbCloneClassificacao classificacao;
            string sSql = string.Empty;
            sSql = "SELECT * ";
            sSql = sSql + " FROM [SPI_TB_CLONE_CLASSIFICACAO] ";
            sSql = sSql + " WHERE [selecaoId] = " + selecaoId.ToString() + " AND [classificacao] = '"+ nome +"'";

            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                classificacao = await db.QueryFirstOrDefaultAsync<TbCloneClassificacao>(sSql);
            }
                return classificacao;
        }
    }
}