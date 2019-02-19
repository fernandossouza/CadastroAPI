using System.Collections.Generic;
using System.Threading.Tasks;
using CadastroAPI.Models;

namespace CadastroAPI.Service.Interface
{
    public interface ICloneService
    {
         Task<IEnumerable<TbCloneMunicipio>> GetMunicipio();
         Task<IEnumerable<TbCloneCadastro>> GetClone();
         Task<TbCloneCadastro> GetClone(long id);
         Task<TbCloneCadastro> PutClone(long id,TbCloneCadastro clone);
         Task<IEnumerable<TbCloneSelecao>> GetSelecaoPorClone(long cloneId);
         Task<TbCloneSelecao> PostSelecaoPorClone(long cloneId,TbCloneSelecao selecao);
         Task<TbCloneSelecao> PutSelecaoPorClone(long cloneId,TbCloneSelecao selecao);
    }
}