using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CadastroAPI.Models;
using CadastroAPI.Models.Repository;
using CadastroAPI.Service.Interface;

namespace CadastroAPI.Service
{
    public class PessoasService : IPessoasService
    {
        private readonly TbPessoasCadastroRepository _TbPessoasCadastroRepository;

        public PessoasService(TbPessoasCadastroRepository tbPessoasCadastroRepository)
        {
            _TbPessoasCadastroRepository = tbPessoasCadastroRepository;
        }

        //Retorna todas as pessoas cadastradas
        public async Task<IEnumerable<TbPessoasCadastro>> GetPessoa()
        {
            IEnumerable<TbPessoasCadastro> pessoas;
            pessoas = await _TbPessoasCadastroRepository.GetPessoa();
            return pessoas;
        }

        //Retorna a pessoa dona do Id que foi submetido
        public async Task<TbPessoasCadastro> GetPessoa(int id)
        {
            TbPessoasCadastro pessoa = null;
            pessoa = await _TbPessoasCadastroRepository.GetPessoa(id);
            return pessoa;
        }

        public async Task<TbPessoasCadastro> PutPessoa(int id, TbPessoasCadastro mod)
        {
            if (id != mod.id)
                throw new System.Exception("Erro! Matricula não cadastrada!");

            var pessoa = await _TbPessoasCadastroRepository.GetPessoa(id);
            if (pessoa == null)
            {
                throw new Exception("Cadastro não encontrado");
                //await PostPessoa(pessoa);
            }
            else
            {
                pessoa = await _TbPessoasCadastroRepository.UpdatePessoa(mod);

                if (pessoa == null)
                    throw new Exception("Erro ao tentar atualizar as informações, por favor tente outra vez.");
            }

            return pessoa;
        }


        // public async Task<TbPessoasCadastro> PostPessoa(TbPessoasCadastro pessoa)
        // {
        //     pessoa = await _TbPessoasCadastroRepository.InsertPessoa(pessoa);

        //     if (pessoa == null)
        //     {
        //         throw new Exception("Erro! Não foi possível cadastrar essa pessoa, tente novamente.");
        //     }
        //     return pessoa;
        // }
    }
}