using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Linq;

namespace CadastroAPI.Models.Repository
{
    public class TbRotasTrechoRepository
    {
        private string stringConnection;
        private readonly IConfiguration _configuration;

        public TbRotasTrechoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString("CadastroConnection");
        }

        public async Task<IEnumerable<TbRotasTrecho>> GetTrechoAll(long id)
        {
            IEnumerable<TbRotasTrecho> rotasTrechoList;
            string sSql = string.Empty;
            sSql = "SELECT T.*,S.*,P.* ";
            sSql = sSql + " FROM [SPI_TB_ROTAS_TRECHOS] AS T ";
            sSql = sSql + " LEFT JOIN [SPI_TB_ROTAS_SENTIDOS] AS S ON T.id = S.trechoId ";
            sSql = sSql + " LEFT JOIN [SPI_TB_ROTAS_PROXIMO_TRECHOS] AS P ON S.id = P.sentidoId ";

            if(id>0)
                sSql = sSql + " WHERE T.[id] = " + id.ToString();


            using(IDbConnection db = new SqlConnection(stringConnection)){                
                var lookup = new Dictionary<long,TbRotasTrecho>();
                db.Query<TbRotasTrecho,TbRotasSentido,TbRotasProximoTrecho,TbRotasTrecho>(sSql,(t,s,p) =>
                {
                TbRotasTrecho trecho;
                if (!lookup.TryGetValue(t.id, out trecho)) {
                         lookup.Add(t.id, trecho = t);
                     }
                     if (trecho.direcao == null) 
                         trecho.direcao = new List<TbRotasSentido>();
                     trecho.direcao.Add(s);
                     if (s.proximoTrecho == null) 
                         s.proximoTrecho = new List<TbRotasProximoTrecho>();
                     s.proximoTrecho.Add(p);


                     return trecho;

                 }).AsQueryable();
                 rotasTrechoList = lookup.Values;
            }

            
                return rotasTrechoList;
        }

        public async Task<IEnumerable<TbRotasTrecho>> GetTrechoAll(List<long> idList)
        {
            IEnumerable<TbRotasTrecho> rotasTrechoList;
            string sSql = string.Empty;
            sSql = "SELECT [id],[trecho],[ativo] ";
            sSql = sSql + " FROM [SPI_TB_ROTAS_TRECHOS] ";

            if(idList.Count>0)
            {
                sSql = sSql + " WHERE In(0";

                foreach (var id in idList)
                {
                    sSql=sSql+","+id.ToString();
                }
                sSql = sSql + ")";

            }


            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                rotasTrechoList = await db.QueryAsync<TbRotasTrecho>(sSql);
            }
                return rotasTrechoList;
        }
        

    }
}