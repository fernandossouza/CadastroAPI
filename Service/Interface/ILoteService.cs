using System.Collections.Generic;
using System.Threading.Tasks;
using CadastroAPI.Models;

namespace CadastroAPI.Service.Interface
{
    public interface ILoteService
    {
        Task<TbLoteCadastro> AddAsync(TbLoteCadastro lote);
        Task<TbLoteCadastro> GetAsync(long id);
        Task<IEnumerable<TbLoteCadastro>> GetSemanaVigenteAsync();
        Task<IEnumerable<TbLoteCadastro>> GetListAsync();
        Task<IEnumerable<TbLoteCadastro>> GetOrdemProducaoIdListAsync(long ordemProducaoId);
        Task<bool> DeleteAsync(long loteId);
    }
}