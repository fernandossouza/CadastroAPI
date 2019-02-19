using System;
using System.Linq;
using System.Threading.Tasks;
using CadastroAPI.Models;
using CadastroAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CadastroAPI.Controllers
{
    [Route("api/[controller]")]
    public class PessoasController: Controller
    {
        private readonly IPessoasService _PessoasService;
        public PessoasController(IPessoasService pessoasService)
        {
            _PessoasService = pessoasService;
        }

        //GET: api/Pessoas
        [HttpGet()]
        public async Task<IActionResult> GetPessoa()
        {
            try
            {
                var pessoa = await _PessoasService.GetPessoa();


                if (pessoa.Count() > 0)
                    return Ok(pessoa);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //GET: api/Pessoas/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPessoa(int id)
        {
            try
            {
                var pessoa = await _PessoasService.GetPessoa(id);

                if (pessoa != null)
                    return Ok(pessoa);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //PUT: api/Pessoas/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa(int id, [FromBody]TbPessoasCadastro pessoa)
        {
            try
            {
                var pessoaDb = await _PessoasService.PutPessoa(id, pessoa);

                if (pessoaDb != null)
                {
                    return Ok(pessoaDb);
                }
                else
                {
                    return StatusCode(500, "Erro ao tentar atualizar as informações no banco");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}