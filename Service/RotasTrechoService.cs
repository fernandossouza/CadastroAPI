using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroAPI.Models;
using CadastroAPI.Models.Repository;
using CadastroAPI.Service.Interface;
namespace CadastroAPI.Service
{
    public class RotasTrechoService : IRotasTrechoService
    {
        private readonly TbRotasTrechoRepository _TbRotasTrechoRepository;
        private readonly TbRotasCadastroRepository _TbRotasCadastroRepository;
        private readonly TbRotasTrechoXRotasRepository _TbRotasTrechoXRotasRepository;
        private readonly TbRotasTrechoInicioRepository _TbRotasTrechoInicioRepository;
        public RotasTrechoService(TbRotasTrechoRepository tbRotasTrechoRepository, TbRotasCadastroRepository tbRotasCadastroRepository,
        TbRotasTrechoXRotasRepository tbRotasTrechoXRotasRepository, TbRotasTrechoInicioRepository tbRotasTrechoInicioRepository)
        {
            _TbRotasTrechoRepository = tbRotasTrechoRepository;
            _TbRotasCadastroRepository = tbRotasCadastroRepository;
            _TbRotasTrechoXRotasRepository = tbRotasTrechoXRotasRepository;
            _TbRotasTrechoInicioRepository = tbRotasTrechoInicioRepository;
        }


        public async Task<IEnumerable<TbRotasCadastro>> GetRotas()
        {
            IEnumerable<TbRotasCadastro> rotas;

            rotas = await _TbRotasCadastroRepository.GetRotaAll();

            return rotas;
        }

        public async Task<TbRotasCadastro> GetRotas(long id)
        {
            TbRotasCadastro rotas = null;

            rotas = await _TbRotasCadastroRepository.GetRotaId(id);

            return rotas;
        }

        public async Task<IEnumerable<TbRotasTrechoInicio>> GetTrechoInicial()
        {
            IEnumerable<TbRotasTrechoInicio> rotasTrechoList;

            rotasTrechoList = await _TbRotasTrechoInicioRepository.GetTrechoAll();

            foreach(var trecho in rotasTrechoList)
            {
                var trechoDb = await GetTrechos(trecho.trechoId);

                if(trechoDb !=null || trechoDb.Count() >0)
                    trecho.trecho = trechoDb.FirstOrDefault();
            }

            return rotasTrechoList;
        }

        public async Task<IEnumerable<TbRotasTrecho>> GetTrechos(long id)
        {
            IEnumerable<TbRotasTrecho> rotasTrechoList;

            rotasTrechoList = await _TbRotasTrechoRepository.GetTrechoAll(id);

            return rotasTrechoList;
        }

        public Task<IEnumerable<TbRotasTrecho>> GetTrechos(List<long> idList)
        {
            throw new System.NotImplementedException();
        }

        public async Task<TbRotasCadastro> PostRotas(TbRotasCadastro rota)
        {
            rota = await _TbRotasCadastroRepository.InsertRota(rota);

            if(rota == null)
            throw new System.Exception("Erro ao tentar as informação da rota");

            foreach (var trechoId in rota.trechoId)
            {
                trechoId.rotaId = rota.id;
                var retorno = await _TbRotasTrechoXRotasRepository.InsertTrechoXRota(trechoId);

                if(retorno == null)
                    throw new System.Exception("Erro ao tentar adicionar as informações dos trechos da rota");
            }


            return rota;
        }

        public async Task<TbRotasCadastro> PutRotas(long id, TbRotasCadastro rota)
        {
            if(id != rota.id)
                throw new System.Exception("Id da rota de atualização é diferente do id de atualização");

            var rotaDb = await GetRotas(id);

            if(rotaDb == null)
            {
                await PostRotas(rota);
            }
            else
            {
                rota = await _TbRotasCadastroRepository.UpdateRota(rota);

                if(rota == null)
                    throw new System.Exception("Erro ao tentar atualiza as informações da Rota");

                foreach(var trechoId in rota.trechoId)
                {
                    TbRotasTrechoXRotas retorno=null;
                    trechoId.rotaId = rota.id;

                    // Verifica se os trecho das rotas já estão cadastrados ou deve cadastrar
                    if(rotaDb.trechoId.Where(x=>x.id == trechoId.id).Count()>0)
                        retorno = await _TbRotasTrechoXRotasRepository.UpdateTrechoXRota(trechoId);
                    else
                        retorno = await _TbRotasTrechoXRotasRepository.InsertTrechoXRota(trechoId);

                    if(retorno == null)
                    throw new System.Exception("Erro ao tentar atualizar as informações dos trechos da rota");
                }

                var trechosDeleteList = rotaDb.trechoId.Select(x=>x.id).Except(rota.trechoId.Select(x=>x.id));

                foreach(var idTrecho in trechosDeleteList)
                {
                   await _TbRotasTrechoXRotasRepository.DeleteTrechoXRota(idTrecho);
                }
            }

            return rota;

        }

    }
}