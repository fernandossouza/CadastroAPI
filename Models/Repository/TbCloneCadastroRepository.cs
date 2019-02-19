using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CadastroAPI.Models.Repository
{
    public class TbCloneCadastroRepository
    {
        private string stringConnection;
        private readonly IConfiguration _configuration;

        public TbCloneCadastroRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString("CadastroConnection");
        }

        public async Task<IEnumerable<TbCloneCadastro>> GetClone()
        {
            IEnumerable<TbCloneCadastro> cloneList;
            string sSql = string.Empty;
            sSql = "SELECT * ";
            sSql = sSql + " FROM [SPI_TB_CLONE_CADASTRADO] ";

            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                cloneList = await db.QueryAsync<TbCloneCadastro>(sSql);
            }
                return cloneList;
        }

        public async Task<TbCloneCadastro> GetCloneId(long id)
        {
            IEnumerable<TbCloneCadastro> clone;
            string sSql = string.Empty;
            sSql = "SELECT C.*,MC.*,MT.*,R.* ";
            sSql = sSql + " FROM [SPI_TB_CLONE_CADASTRADO] as C ";
            sSql = sSql + " LEFT JOIN [SPI_TB_CLONE_MUNICIPIO] AS MC ON MC.id = C.municipioColetaId ";
            sSql = sSql + " LEFT JOIN [SPI_TB_CLONE_MUNICIPIO] AS MT ON MT.id = C.municipoTesteId ";
            sSql = sSql + " LEFT JOIN [SPI_TB_ROTAS_CADASTRADOS] AS R ON R.id = C.rotaIdCasaVegetacao ";
            sSql = sSql + " WHERE C.id = " + id.ToString();
            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                var lookup = new Dictionary<long,TbCloneCadastro>();
                db.Query<TbCloneCadastro,TbCloneMunicipio,TbCloneMunicipio,TbRotasCadastro,TbCloneCadastro>(sSql,(c,mc,mt,r) =>
                {
                TbCloneCadastro oClone;
                if (!lookup.TryGetValue(c.id, out oClone)) {
                         lookup.Add(c.id, oClone = c);
                     }
                     if (oClone.municipioColeta == null) 
                         oClone.municipioColeta = new TbCloneMunicipio();

                     oClone.municipioColeta = mc;

                     if (oClone.municipoTeste == null) 
                         oClone.municipoTeste = new TbCloneMunicipio();

                     oClone.municipoTeste = mt;

                     if(oClone.rotaCasaVegetacao == null)
                        oClone.rotaCasaVegetacao = new TbRotasCadastro();

                    oClone.rotaCasaVegetacao = r;

                     return oClone;

                 }).AsQueryable();
                 clone = lookup.Values;
            }

            if(clone == null || clone.Count()==0)
                return null;
            else
                return clone.FirstOrDefault();
        }

        public async Task<TbCloneCadastro> AtualizaClone(TbCloneCadastro clone)
        {
            int updateRow;
            string sSql = string.Empty;
            sSql = "UPDATE [SPI_TB_CLONE_CADASTRADO] ";
            sSql = sSql + " SET [cor] = @cor,[ativo] = @ativo,[percentualBifurcada] = @percentualBifurcada,[portaEnxerto] = @portaEnxerto ";
            sSql = sSql + " ,[nomeCientifico] = @nomeCientifico,[nomeComum] = @nomeComum,[categoria] = @categoria ";
            sSql = sSql + " ,[municipioColetaId] = @municipioColetaId,[criterioSelecao] = @criterioSelecao,[intensidadeSelecao] = @intensidadeSelecao ";
            sSql = sSql + " ,[municipoTesteId] = @municipoTesteId,[codigoCultivar] = @codigoCultivar,[numeroRegistroCultivar] = @numeroRegistroCultivar ";
            sSql = sSql + " ,[dataRegistroCultivar] = @dataRegistroCultivar,[mantenedoraCultivar] = @mantenedoraCultivar ";
            sSql = sSql + " ,[rotaIdCasaVegetacao] = @rotaIdCasaVegetacao,[dataMigracaoSap] = @dataMigracaoSap ";
            sSql = sSql + " WHERE [id] = @id ";

            
            using(IDbConnection db = new SqlConnection(stringConnection)){                
               
                updateRow = await db.ExecuteAsync(sSql,clone);
            }

           if(updateRow > 0)
                return clone;
            else
                return null;
        }
    }
}