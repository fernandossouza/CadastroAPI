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
        private readonly IOrdemDeProducaoService _ordemProducaoService;
        public LoteService ( TbLoteCadastroRepository loteRepository, IOrdemDeProducaoService ordemProducaoService)
        {
            _loteRepository = loteRepository;
            _ordemProducaoService = ordemProducaoService;
        }
        public async Task<TbLoteCadastro> AddAsync(TbLoteCadastro lote)
        {
            var numeroSemana =CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            var oP = await _ordemProducaoService.GetAsync(lote.ordemProducaoId);

            if(oP == null)
                throw new Exception(" Ordem de produção não encontrada");

            lote.ordemProducao = oP.op;
            lote.clone = oP.clone;

            if(string.IsNullOrWhiteSpace(lote.lote))
            {
                // Criando nomeclatura do LOTE "S + Semana do Ano + Nome do Clone + Mês + Ano"
                lote.lote ="S";
               
                if(numeroSemana.ToString().Length == 1)
                    lote.lote += "0";
                lote.lote += numeroSemana.ToString();

                lote.lote += oP.clone;

                lote.lote += DateTime.Now.ToString("MMyy");

            }

            lote.lote += (lote.sufixo !=null)? lote.sufixo : ""; 
            lote.semana = numeroSemana;
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