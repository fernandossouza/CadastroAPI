using System.Linq;
using System.Threading.Tasks;
using CadastroAPI.Models;
using CadastroAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CadastroAPI.Controllers
{
    [Route("api/ordemproducao")]
    public class OrdemDeProducaoController: Controller
    {
        private readonly IOrdemDeProducaoService _ordemService;

        public OrdemDeProducaoController (IOrdemDeProducaoService ordemService)
        {
            _ordemService = ordemService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get( long id)
        {
            var ordem = await _ordemService.GetAsync(id);
            if(ordem == null)
                return NotFound();
            return Ok(ordem);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var ordemList = await _ordemService.GetListAsync();
            if(ordemList == null || ordemList.Count() == 0)
                return NotFound();
            return Ok(ordemList);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TbOrdemDeProducaoCadastro ordem)
        {
            var newOrdem = await _ordemService.AddAsync(ordem);
            if(newOrdem == null)
                return NotFound();
            return Ok(newOrdem);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TbOrdemDeProducaoCadastro ordem)
        {
            var updatedOrdem = await _ordemService.UpdateAsync(ordem);
            if(updatedOrdem == null)
                return NotFound();
            
            return Ok(updatedOrdem);
        }
    }
}