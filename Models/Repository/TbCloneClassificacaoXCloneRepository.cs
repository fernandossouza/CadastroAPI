using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CadastroAPI.Models.Repository
{
    public class TbCloneClassificacaoXCloneRepository
    {
        private string stringConnection;
        private readonly IConfiguration _configuration;

        public TbCloneClassificacaoXCloneRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString("CadastroConnection");
        }

        public async Task<TbCloneClassificacaoXClone> GetPorCloneIdEClassificacaoID(long cloneId,long classificacaoId)
        {
            TbCloneClassificacaoXClone classificacao;
            string sSql = string.Empty;
            sSql = "SELECT * ";
            sSql = sSql + " FROM [SPI_TB_CLONE_CLASSIFICACAO_POR_CLONE] ";
            sSql = sSql + " WHERE [classificacaoId] = " + classificacaoId.ToString()+" AND [cloneId]="+cloneId;

            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                classificacao = await db.QueryFirstOrDefaultAsync<TbCloneClassificacaoXClone>(sSql);
            }
                return classificacao;
        }

        public async Task<TbCloneClassificacaoXClone> InsertClassificacaoPorClone(TbCloneClassificacaoXClone classificacaoClone)
        {
            IEnumerable<long> insertRow;
            string sSql = string.Empty;
            sSql = "INSERT INTO [SPI_TB_CLONE_CLASSIFICACAO_POR_CLONE] ([cloneId],[classificacaoId],[altura] ";
            sSql = sSql + " ,[diametroColo],[quantidadePares],[angulo],[quantidadeRamificacoes],[presencaBifurcacao],[coloracao] ";
            sSql = sSql + " ,[tamanhoRamificacao],[posicaoRamificacao],[danosAnatomicos],[manchasFoliares],[tamanhoAreaFoliar] ";
            sSql = sSql + " ,[espacamentoBandeja],[rotaId],[tempoGalpao],[tempoFase]) ";
            sSql = sSql + " VALUES (@cloneId,@classificacaoId,@altura,@diametroColo,@quantidadePares ";
            sSql = sSql + " ,@angulo,@quantidadeRamificacoes,@presencaBifurcacao,@coloracao,@tamanhoRamificacao,@posicaoRamificacao ";
            sSql = sSql + " ,@danosAnatomicos,@manchasFoliares,@tamanhoAreaFoliar,@espacamentoBandeja,@rotaId ";
            sSql = sSql + " ,@tempoGalpao,@tempoFase) ";
            sSql = sSql + " SELECT @@IDENTITY ";

            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                insertRow = await db.QueryAsync<long>(sSql,classificacaoClone);
            }

            if(insertRow == null || insertRow.Count() == 0)
                return null;

            classificacaoClone.id = insertRow.FirstOrDefault();

            return classificacaoClone;
        }

        public async Task<TbCloneClassificacaoXClone> UpdateClassificacaoPorClone(TbCloneClassificacaoXClone classificacaoClone)
        {
            int updateRow;
            string sSql = string.Empty;
            sSql = "UPDATE [dbo].[SPI_TB_CLONE_CLASSIFICACAO_POR_CLONE] ";
            sSql = sSql + " SET [cloneId] = @cloneId,[classificacaoId] = @classificacaoId,[altura] = @altura,[diametroColo] = @diametroColo ";
            sSql = sSql + " ,[quantidadePares] = @quantidadePares,[angulo] = @angulo,[quantidadeRamificacoes] = @quantidadeRamificacoes ";
            sSql = sSql + " ,[presencaBifurcacao] = @presencaBifurcacao,[coloracao] = @coloracao,[tamanhoRamificacao] = @tamanhoRamificacao ";
            sSql = sSql + " ,[posicaoRamificacao] = @posicaoRamificacao,[danosAnatomicos] = @danosAnatomicos,[manchasFoliares] = @manchasFoliares ";
            sSql = sSql + " ,[tamanhoAreaFoliar] = @tamanhoAreaFoliar,[espacamentoBandeja] = @espacamentoBandeja,[rotaId] = @rotaId ";
            sSql = sSql + " ,[tempoGalpao] = @tempoGalpao,[tempoFase] = @tempoFase ";
            sSql = sSql + " WHERE [id] = @id ";

            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                updateRow = await db.ExecuteAsync(sSql,classificacaoClone);
            }

            if(updateRow > 0)
                return classificacaoClone;
            else
                return null;
        }
    }
}