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
        public LoteService ( TbLoteCadastroRepository loteRepository )
        {
            _loteRepository = loteRepository;
        }
        public async Task<TbLoteCadastro> AddAsync(TbLoteCadastro lote)
        {
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