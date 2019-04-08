using System.Collections.Generic;
using System.Threading.Tasks;
using CadastroAPI.Models;

namespace CadastroAPI.Service.Interface
{
    public interface IPessoasService
    {
        Task<IEnumerable<TbPessoasCadastro>> GetPessoa();
        Task<TbPessoasCadastro> GetPessoa(int id);
        Task<TbPessoasCadastro> PutPessoa(int id, TbPessoasCadastro pessoa);
        Task<string> GetRfidCracha(string mesa);
    }
}