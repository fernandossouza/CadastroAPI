using System.Collections.Generic;
using System.Threading.Tasks;
using CadastroAPI.Models;

namespace CadastroAPI.Service.Interface
{
    public interface IRotasTrechoService
    {
         Task<IEnumerable<TbRotasTrecho>> GetTrechos(long id);
         Task<IEnumerable<TbRotasTrecho>> GetTrechos(List<long> idList);
         Task<IEnumerable<TbRotasCadastro>> GetRotas();
         Task<TbRotasCadastro> GetRotas(long id);
         Task<TbRotasCadastro> PostRotas(TbRotasCadastro rota);
         Task<TbRotasCadastro> PutRotas(long id,TbRotasCadastro rota);
         Task<IEnumerable<TbRotasTrechoInicio>> GetTrechoInicial();
         

         

    }
}