using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using CadastroAPI.Models;
using CadastroAPI.Models.Repository;
using CadastroAPI.Service.Interface;

namespace CadastroAPI.Service
{
    public class LoteService : ILoteService
    {
        private readonly TbLoteCadastroRepository _loteRepository;
        private readonly TbOrdemDeProducaoCadastroRepository _ordemProducaoRepository;
        private readonly TbCloneCadastroRepository _cloneRepository;
        public LoteService ( TbLoteCadastroRepository loteRepository, TbOrdemDeProducaoCadastroRepository ordemProducaoRepository, TbCloneCadastroRepository cloneRepository)
        {
            _loteRepository = loteRepository;
            _ordemProducaoRepository = ordemProducaoRepository;
            _cloneRepository = cloneRepository;
        }
        public async Task<TbLoteCadastro> AddAsync(TbLoteCadastro lote)
        {
            var oP = await _ordemProducaoRepository.Get(lote.Id);

            if(oP == null)
                throw new Exception(" Ordem de produção não encontrada");

            lote.ordemProducao = oP.op;
            lote.clone = oP.clone;

            var insertedOrder = await _loteRepository.Insert(lote);
            return insertedOrder;
        }

        public async Task<TbLoteCadastro> GetAsync(long id)
        {
            var lote = await _loteRepository.Get(id);
            return lote;
        }

        public async Task<IEnumerable<TbLoteCadastro>> GetListAsync()
        {
            var lote = await _loteRepository.GetList();
            return lote;
        }

        public async Task<IEnumerable<TbLoteCadastro>> GetSemanaVigenteAsync()
        {

            int numeroSemana = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var loteList = await _loteRepository.GetSemanaVigente(numeroSemana);

            return loteList;
        }
    }
}