using System.Collections.Generic;
using System.Threading.Tasks;
using CadastroAPI.Models;
namespace CadastroAPI.Service.Interface
{
    public interface IOrdemDeProducaoService
    {
        Task<TbOrdemDeProducaoCadastro> AddAsync(TbOrdemDeProducaoCadastro ordem);
        Task<TbOrdemDeProducaoCadastro> GetAsync(long id);
        Task<IEnumerable<TbOrdemDeProducaoCadastro>> GetProcuraAsync(string opNome);
        Task<TbOrdemDeProducaoCadastro> UpdateAsync(TbOrdemDeProducaoCadastro ordem);
        Task<IEnumerable<TbOrdemDeProducaoCadastro>> GetListAsync();
    }
}