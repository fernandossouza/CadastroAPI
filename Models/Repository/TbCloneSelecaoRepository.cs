using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CadastroAPI.Models.Repository
{
    public class TbCloneSelecaoRepository
    {
        private string stringConnection;
        private readonly IConfiguration _configuration;

        public TbCloneSelecaoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString("CadastroConnection");
        }

        public async Task<TbCloneSelecao> GetSelecaoPorNome(string nome)
        {
            TbCloneSelecao selecao;
            string sSql = string.Empty;
            sSql = "SELECT * ";
            sSql = sSql + " FROM [SPI_TB_CLONE_SELECAO] ";
            sSql = sSql + " WHERE [selecao] = '" + nome +"'";

            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                selecao = await db.QueryFirstOrDefaultAsync<TbCloneSelecao>(sSql);
            }
                return selecao;
        }

        public async Task<IEnumerable<TbCloneSelecao>> GetSelecaoPorClone(long cloneId)
        {
            IEnumerable<TbCloneSelecao> selecao;
            string sSql = string.Empty;
            sSql = "SELECT S.*,C.*,CC.* ";
            sSql = sSql + " FROM [SPI_TB_CLONE_SELECAO] AS S ";
            sSql = sSql + " INNER JOIN [SPI_TB_CLONE_CLASSIFICACAO] AS C ON C.selecaoId = S.id ";
            sSql = sSql + " INNER JOIN [SPI_TB_CLONE_CLASSIFICACAO_POR_CLONE] AS CC ON CC.classificacaoId = C.id ";
            sSql = sSql + " WHERE CC.cloneId = " + cloneId.ToString();
            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                var lookup = new Dictionary<long,TbCloneSelecao>();
                db.Query<TbCloneSelecao,TbCloneClassificacao,TbCloneClassificacaoXClone,TbCloneSelecao>(sSql,(s,c,cc) =>
                {
                TbCloneSelecao oSelecao;
                if (!lookup.TryGetValue(s.id, out oSelecao)) {
                         lookup.Add(s.id, oSelecao = s);
                     }
                     if (oSelecao.classificacao == null) 
                         oSelecao.classificacao = new List<TbCloneClassificacao>();

                     oSelecao.classificacao.Add(c);

                     if (c.classificacaoPorClone == null) 
                         c.classificacaoPorClone = new TbCloneClassificacaoXClone();

                     c.classificacaoPorClone =cc;

                     return oSelecao;

                 }).AsQueryable();
                 selecao = lookup.Values;
            }

            if(selecao == null || selecao.Count()==0)
                return null;
            else
                return selecao;
        }
    }
}