using System.Collections.Generic;
using System.Threading.Tasks;
using CadastroAPI.Models.Repository;
using CadastroAPI.Models;
using CadastroAPI.Service.Interface;

namespace CadastroAPI.Service
{
    public class OrdemDeProducaoService : IOrdemDeProducaoService
    {
        private readonly TbOrdemDeProducaoCadastroRepository _ordemRepository;
        public OrdemDeProducaoService (TbOrdemDeProducaoCadastroRepository ordemRepository )
        {
            _ordemRepository = ordemRepository;
        }
        public async Task<TbOrdemDeProducaoCadastro> AddAsync(TbOrdemDeProducaoCadastro ordem)
        {
            var insertedOrder = await _ordemRepository.Insert(ordem);
            return insertedOrder;
        }

        public async Task<TbOrdemDeProducaoCadastro> GetAsync(long id)
        {
            var ordem = await _ordemRepository.Get(id);
            return ordem;
        }

        public async Task<IEnumerable<TbOrdemDeProducaoCadastro>> GetListAsync()
        {
            var ordemList = await _ordemRepository.GetList();
            return ordemList;
        }

        public async Task<IEnumerable<TbOrdemDeProducaoCadastro>> GetProcuraAsync(string opNome)
        {
            var ordemList = await _ordemRepository.GetProcuraPorNomeOP(opNome);
            return ordemList;
        }

        public async Task<TbOrdemDeProducaoCadastro> UpdateAsync(TbOrdemDeProducaoCadastro ordem)
        {
            var updatedOrdem = await _ordemRepository.Update(ordem);
            return updatedOrdem;
        }
    }
}