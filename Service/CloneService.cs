using System.Collections.Generic;
using System.Threading.Tasks;
using CadastroAPI.Models;
using CadastroAPI.Service.Interface;
using CadastroAPI.Models.Repository;

namespace CadastroAPI.Service
{
    public class CloneService : ICloneService
    {
        private readonly TbCloneMunicipioRepository _TbCloneMunicipioRepository;
        private readonly TbCloneCadastroRepository _TbCloneCadastroRepository;
        private readonly TbCloneSelecaoRepository _TbCloneSelecaoRepository;
        private readonly TbCloneClassificacaoXCloneRepository _TbCloneClassificacaoXCloneRepository;
        private readonly TbCloneClassificacaoRepository _TbCloneClassificacaoRepository;
        public CloneService(TbCloneMunicipioRepository tbCloneMunicipioRepository,TbCloneCadastroRepository tbCloneCadastroRepository
        ,TbCloneSelecaoRepository tbCloneSelecaoRepository,TbCloneClassificacaoXCloneRepository tbCloneClassificacaoXCloneRepository
        ,TbCloneClassificacaoRepository tbCloneClassificacaoRepository)
        {
            _TbCloneMunicipioRepository = tbCloneMunicipioRepository;
            _TbCloneCadastroRepository = tbCloneCadastroRepository;
            _TbCloneSelecaoRepository = tbCloneSelecaoRepository;
            _TbCloneClassificacaoXCloneRepository = tbCloneClassificacaoXCloneRepository;
            _TbCloneClassificacaoRepository = tbCloneClassificacaoRepository;
        }

        public async Task<IEnumerable<TbCloneCadastro>> GetClone()
        {
            IEnumerable<TbCloneCadastro> cloneList;

            cloneList = await _TbCloneCadastroRepository.GetClone();

            return cloneList;
        }

        public async Task<TbCloneCadastro> GetClone(long id)
        {
            TbCloneCadastro clone;

            clone = await _TbCloneCadastroRepository.GetCloneId(id);

            return clone;
        }

        public async Task<IEnumerable<TbCloneMunicipio>> GetMunicipio()
        {
            IEnumerable<TbCloneMunicipio> municipioList;

            municipioList = await _TbCloneMunicipioRepository.GetMunicipios();

            return municipioList;
        }

        public async Task<IEnumerable<TbCloneSelecao>> GetSelecaoPorClone(long cloneId)
        {
            IEnumerable<TbCloneSelecao> selecaoPorClone;

            selecaoPorClone = await _TbCloneSelecaoRepository.GetSelecaoPorClone(cloneId);

            return selecaoPorClone;
        }

        public async Task<TbCloneSelecao> PostSelecaoPorClone(long cloneId,TbCloneSelecao selecao)
        {
            var selecaoDb = await _TbCloneSelecaoRepository.GetSelecaoPorNome(selecao.selecao);

            if(selecaoDb == null)
                throw new System.Exception("Não foi possivel obter o id da seleção informada, nome da seleção: " + selecao.selecao);

            foreach(var classificacao in selecao.classificacao)
            {
                var classificacaoDb = await _TbCloneClassificacaoRepository.GetClassificacaoPorSelecaoIdENome(selecaoDb.id,classificacao.classificacao);

                if(selecaoDb == null)
                    throw new System.Exception("Não foi possivel obter o id da classificação informada, nome da classificação: " + classificacao.classificacao);
                
                 if(await _TbCloneClassificacaoXCloneRepository.GetPorCloneIdEClassificacaoID(cloneId,classificacaoDb.id) != null)
                    throw new System.Exception("Já existe classificação cadastrado para esse clone, classificação: " + classificacao.classificacao);

                var classificacaoPorClone = classificacao.classificacaoPorClone;
                classificacaoPorClone.classificacaoId = classificacaoDb.id;
                classificacaoPorClone.cloneId = cloneId; 
                classificacaoPorClone = await _TbCloneClassificacaoXCloneRepository.InsertClassificacaoPorClone(classificacaoPorClone);

                if(classificacaoPorClone == null)
                throw new System.Exception("Não foi possível salvar as informações da classificação do clone no banco de dados");
                
            }
            return selecao;
        }

        public async Task<TbCloneCadastro> PutClone(long id,TbCloneCadastro clone)
        {
             if(id != clone.id)
                throw new System.Exception("id para atualização é diferente do id do clone");

            var cloneDb = await GetClone(id);

            clone = await _TbCloneCadastroRepository.AtualizaClone(clone);

            if(clone == null)
                throw new System.Exception("Erro ao tentar atualiza as informações do Clone");

            return clone;
        }

        public async Task<TbCloneSelecao> PutSelecaoPorClone(long cloneId, TbCloneSelecao selecao)
        {
            
            var selecaoDb = await _TbCloneSelecaoRepository.GetSelecaoPorNome(selecao.selecao);

            if(selecaoDb == null)
                throw new System.Exception("Não foi possivel obter o id da seleção informada, nome da seleção: " + selecao.selecao);

            foreach(var classificacao in selecao.classificacao)
            {
                var classificacaoDb = await _TbCloneClassificacaoRepository.GetClassificacaoPorSelecaoIdENome(selecaoDb.id,classificacao.classificacao);

                if(selecaoDb == null)
                    throw new System.Exception("Não foi possivel obter o id da classificação informada, nome da classificação: " + classificacao.classificacao);
                
                var classificacaoPorCloneDb = await _TbCloneClassificacaoXCloneRepository.GetPorCloneIdEClassificacaoID(cloneId,classificacaoDb.id);

                // se não existir a classificação por clone cria o mesmo no BD
                if(classificacaoPorCloneDb == null)
                {
                    TbCloneSelecao cs = new TbCloneSelecao();
                    cs.id= selecaoDb.id;
                    cs.selecao = selecaoDb.selecao;
                    cs.classificacao = new List<TbCloneClassificacao>();
                    TbCloneClassificacao cc = new TbCloneClassificacao();
                    cc.id = classificacaoDb.id;
                    cc.classificacao = classificacaoDb.classificacao;
                    cc.classificacaoPorClone = classificacao.classificacaoPorClone;
                    cs.classificacao.Add(cc);
                    classificacao.classificacaoPorClone.cloneId = cloneId;
                    await PostSelecaoPorClone(cloneId,cs);
                }
                else
                {

                    var classificacaoPorClone = classificacao.classificacaoPorClone;
                    classificacaoPorClone.classificacaoId = classificacaoDb.id; 
                    
                    classificacaoPorClone = await _TbCloneClassificacaoXCloneRepository.UpdateClassificacaoPorClone(classificacaoPorClone);

                    if(classificacaoPorClone == null)
                        throw new System.Exception("Não foi possível salvar as informações da classificação do clone no banco de dados");

                } 
            }

            return selecao;
        }
    }
}