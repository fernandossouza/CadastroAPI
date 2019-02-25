using System.Linq;
using System.Threading.Tasks;
using CadastroAPI.Models;
using CadastroAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CadastroAPI.Controllers
{
[Route("api/[controller]")]
    public class LoteController:Controller
    
    {
        private readonly ILoteService _loteService;
        public LoteController (ILoteService loteService)
    {
        _loteService = loteService;               
    }
       
        [HttpGet("{id}")]
        public async Task<IActionResult> Get( long id)
        {
            var lote = await _loteService.GetAsync(id);
            if(lote == null)
                return NotFound();
            return Ok(lote);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var loteList = await _loteService.GetListAsync();
            if(loteList == null || loteList.Count() == 0)
                return NotFound();
            return Ok(loteList);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TbLoteCadastro lote)
        {
            var newlote = await _loteService.AddAsync(lote);
            if(newlote == null)
                return NotFound();
            return Ok(newlote);
        }
        
    }
}