using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Linq;

namespace CadastroAPI.Models.Repository
{
    public class TbRotasCadastroRepository
    {
        private string stringConnection;
        private readonly IConfiguration _configuration;

        public TbRotasCadastroRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString("CadastroConnection");
        }

        public async Task<IEnumerable<TbRotasCadastro>> GetRotaAll()
        {
            IEnumerable<TbRotasCadastro> rotasList;
            string sSql = string.Empty;
            sSql = "SELECT [id],[nome],[prioridade],[ativo] ";
            sSql = sSql + " FROM [SPI_TB_ROTAS_CADASTRADOS] ";

            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                rotasList = await db.QueryAsync<TbRotasCadastro>(sSql);
            }
                return rotasList;
        }

        public async Task<TbRotasCadastro> GetRotaId(long id)
        {
            IEnumerable<TbRotasCadastro> rota;
            string sSql = string.Empty;
            sSql = "SELECT R.*,RT.*,T.*,S.* ";
            sSql = sSql + " FROM [SPI_TB_ROTAS_CADASTRADOS] AS R ";
            sSql = sSql + " LEFT JOIN [SPI_TB_ROTAS_TRECHOS_POR_ROTAS] AS RT ON R.id = RT.rotaId ";
            sSql = sSql + " LEFT JOIN [SPI_TB_ROTAS_SENTIDOS] AS S ON RT.sentidoId = S.id ";
            sSql = sSql + " LEFT JOIN [SPI_TB_ROTAS_TRECHOS] AS T ON S.trechoId = T.id ";
            sSql = sSql + " WHERE R.id = " + id.ToString();
            sSql = sSql + " ORDER BY RT.ordem ";

            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                var lookup = new Dictionary<long,TbRotasCadastro>();
                db.Query<TbRotasCadastro,TbRotasTrechoXRotas,TbRotasTrecho,TbRotasSentido,TbRotasCadastro>(sSql,(r,tr,t,s) =>
                {
                TbRotasCadastro oRota;
                if (!lookup.TryGetValue(r.id, out oRota)) {
                         lookup.Add(r.id, oRota = r);
                     }
                     if (oRota.trechoRota == null) 
                         oRota.trechoRota = new List<TbRotasTrecho>();

                     oRota.trechoRota.Add(t);

                     if (t.direcao == null) 
                         t.direcao = new List<TbRotasSentido>();

                     t.direcao.Add(s);

                     if(oRota.trechoId == null)
                        oRota.trechoId = new List<TbRotasTrechoXRotas>();

                    oRota.trechoId.Add(tr);

                     return oRota;

                 }).AsQueryable();
                 rota = lookup.Values;
            }

            if(rota == null || rota.Count()==0)
                return null;
            else
                return rota.FirstOrDefault();
        }

        public async Task<TbRotasCadastro> InsertRota(TbRotasCadastro rota)
        {
            IEnumerable<long> insertRow;
            string sSql = string.Empty;
            sSql = "INSERT INTO [SPI_TB_ROTAS_CADASTRADOS]([NOME],[PRIORIDADE],[ATIVO]) ";
            sSql = sSql + " VALUES (@nome,@prioridade,@ativo) ";
            sSql = sSql + " SELECT @@IDENTITY ";

            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                insertRow = await db.QueryAsync<long>(sSql,rota);
            }

            if(insertRow == null || insertRow.Count() == 0)
                return null;

            rota.id = insertRow.FirstOrDefault();

            return rota;
        }

        public async Task<TbRotasCadastro> UpdateRota(TbRotasCadastro rota)
        {
            int updateRow;
            string sSql = string.Empty;
            sSql = "UPDATE [SPI_TB_ROTAS_CADASTRADOS] ";
            sSql = sSql + " SET [nome] = @nome ,[prioridade] = @prioridade ,[ativo] = @ativo ";
            sSql = sSql + " WHERE [id] = @id ";

            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                updateRow = await db.ExecuteAsync(sSql,rota);
            }

            if(updateRow > 0)
                return rota;
            else
                return null;
        }


    }
}