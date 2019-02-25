using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CadastroAPI.Models.Repository
{
    public class TbPessoasCadastroRepository
    {
        private string stringConnection;
        private readonly IConfiguration _configuration;

        public TbPessoasCadastroRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString("CadastroConnection");
        }

        public async Task<IEnumerable<TbPessoasCadastro>> GetPessoa()
        {
            IEnumerable<TbPessoasCadastro> pessoaList;
            string sSql = string.Empty;
            sSql = "SELECT id, matricula, nome, admissao, cargo,";
            sSql += "dataNascimento, estadoCivil, sexo, nivelEscolar,";
            sSql += "escalaTrabalho, municipioOrigem, situacao, ativo,";
            sSql += "tempoEmpresa, crachaRFID, login, password ";
            sSql += "FROM SPI_TB_PESSOA_CADASTRO";

            using (IDbConnection db = new SqlConnection(stringConnection))
            {
                pessoaList = await db.QueryAsync<TbPessoasCadastro>(sSql);
            }
            return pessoaList;
        }

        public async Task<TbPessoasCadastro> GetPessoa(int id)
        {
            IEnumerable<TbPessoasCadastro> pessoa;
            string sSql = string.Empty;

            sSql = "SELECT id, matricula, nome, admissao, cargo,";
            sSql += "dataNascimento, estadoCivil, sexo, nivelEscolar,";
            sSql += "escalaTrabalho, municipioOrigem, situacao, ativo,";
            sSql += "tempoEmpresa, crachaRFID, login, password ";
            sSql += "FROM SPI_TB_PESSOA_CADASTRO ";
            sSql += "WHERE id in(" + id.ToString() + ")";

            using (IDbConnection db = new SqlConnection(stringConnection))
            {
                pessoa = await db.QueryAsync<TbPessoasCadastro>(sSql);
            }
            return pessoa.FirstOrDefault();
        }

        public async Task<TbPessoasCadastro> UpdatePessoa(TbPessoasCadastro pessoa)
        {
            int updateRow;
            string sSql = string.Empty;
            sSql = "UPDATE [SPI_TB_PESSOA_CADASTRO]";
            sSql += "SET [ativo] = @ativo, [crachaRFID] = @crachaRFID, [login] = @login, [password] = @password ";
            sSql += "WHERE [id] in (@id)";

            using (IDbConnection db = new SqlConnection(stringConnection))
            {
                updateRow = await db.ExecuteAsync(sSql, pessoa);
            }

            if (updateRow > 0)
                return pessoa;
            else
                return null;
        }

        // public async Task<TbPessoasCadastro> InsertPessoa(TbPessoasCadastro pessoa)
        // {
        //     IEnumerable<int> insertRow;
        //     string sSql = string.Empty;
        //     sSql = "INSERT INTO SPI_TB_PESSOA_CADASTRO";
        //     sSql += "([id], [matricula], [nome],";
        //     sSql += "[admissao], [cargo],";
        //     sSql += "[estadoCivil], [sexo], [nivelEscolar],";
        //     sSql += "[escalaTrabalho], [municipioOrigem],";
        //     sSql += "[situacao], [ativo],";
        //     sSql += "[crachaRFID], [login], [password]) ";
        //     sSql += "VALUES(@id, @matricula, @nome, @admissao, @cargo,";
        //     sSql += "@estadoCivil, @sexo, @nivelEscolar,";
        //     sSql += "@escalaTrabalho, @municipioOrigem, @situacao, @ativo,";
        //     sSql += "@crachaRFID, @login, @password) ";
        //     sSql += "SET IDENTITY_INSERT SPI_TB_PESSOA_CADASTRO ON";

        //     using (IDbConnection db = new SqlConnection(stringConnection))
        //     {
        //         insertRow = await db.QueryAsync<int>(sSql, pessoa);
        //     }

        //     if (insertRow == null || insertRow.Count() == 0)
        //         return null;

        //     pessoa.Id = insertRow.FirstOrDefault();

        //     return pessoa;
        // }

    }
}