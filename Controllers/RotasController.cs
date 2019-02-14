using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CadastroAPI.Service;
using CadastroAPI.Service.Interface;
using CadastroAPI.Models;

namespace CadastroAPI.Controllers
{
    [Route("api/[controller]")]
    public class RotasController : Controller
    {
        private readonly IRotasTrechoService _rotasTrechoService;
        public RotasController(IRotasTrechoService rotasTrechoService)
        {
            _rotasTrechoService = rotasTrechoService;
        }

        // GET api/rotas/trechos
        [HttpGet("trechos/")]
        public async Task<IActionResult> GetTrecho()
        {
            try
            {
                var trechos = await _rotasTrechoService.GetTrechos(0);
                
                if(trechos.Count()>0)
                    return Ok(trechos);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/rotas/trechos/id
        [HttpGet("trechos/{id}")]
        public async Task<IActionResult> GetTrecho(long id)
        {
            try
            {
                var trechos = await _rotasTrechoService.GetTrechos(id);
                
                if(trechos.Count()>0)
                    return Ok(trechos);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/rotas
        [HttpGet()]
        public async Task<IActionResult> GetRotas()
        {
            try
            {
                var rotas = await _rotasTrechoService.GetRotas();
                
                if(rotas.Count()>0)
                    return Ok(rotas);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/rotas/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRotas(long id)
        {
            try
            {
                var rotas = await _rotasTrechoService.GetRotas(id);
                
                if(rotas != null)
                    return Ok(rotas);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/rotas
        [HttpPost()]
        public async Task<IActionResult> PostRota([FromBody]TbRotasCadastro rota)
        {
            try
            {
                var rotaDb = await _rotasTrechoService.PostRotas(rota);
                
                if(rotaDb != null)
                    return Ok(rotaDb);
                 else
                    return StatusCode(500, "Não foi possivel salvar as informações no banco de dados");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/rotas/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRota(long id,[FromBody]TbRotasCadastro rota)
        {
            try
            {
                var rotaDb = await _rotasTrechoService.PutRotas(id,rota);
                
                if(rotaDb != null)
                    return Ok(rotaDb);
                 else
                    return StatusCode(500, "Não foi possivel atualizar as informações no banco de dados");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}