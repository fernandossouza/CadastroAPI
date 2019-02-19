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
    public class CloneController : Controller
    {
        private readonly ICloneService _CloneService;
        public CloneController(ICloneService cloneService)
        {
            _CloneService = cloneService;
        }

        // GET api/clone/municipio
        [HttpGet("municipio/")]
        public async Task<IActionResult> GetMunicipio()
        {
            try
            {
                var municipioList = await _CloneService.GetMunicipio();
                
                if(municipioList.Count()>0)
                    return Ok(municipioList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/clone
        [HttpGet()]
        public async Task<IActionResult> Getclone()
        {
            try
            {
                var cloneList = await _CloneService.GetClone();
                
                if(cloneList.Count()>0)
                    return Ok(cloneList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/clone/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Getclone(long id)
        {
            try
            {
                var clone = await _CloneService.GetClone(id);
                
                if(clone != null)
                    return Ok(clone);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/clone/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Putclone(long id,[FromBody] TbCloneCadastro clone)
        {
            try
            {
                if (ModelState.IsValid)
                {
                var rotaDb = await _CloneService.PutClone(id,clone);
                
                if(rotaDb != null)
                    return Ok(rotaDb);
                 else
                    return StatusCode(500, "Não foi possivel atualizar as informações no banco de dados");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/clone/selecao/{cloneId}
        [HttpGet("selecao/{cloneId}")]
        public async Task<IActionResult> GetSelecaoPorCloneId(long cloneId)
        {
            try
            {
                var selecaoList = await _CloneService.GetSelecaoPorClone(cloneId);
                
                if(selecaoList != null)
                    return Ok(selecaoList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/clone/selecao/{cloneId}
        [HttpPost("selecao/{cloneId}")]
        public async Task<IActionResult> PostSelecaoPorCloneId(long cloneId,[FromBody] TbCloneSelecao selecao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var selecaoReturn = await _CloneService.PostSelecaoPorClone(cloneId,selecao);
                
                    if(selecaoReturn != null)
                        return Ok(selecaoReturn);
                    else
                        return StatusCode(500, "Não foi possivel salvar as informações no banco de dados");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

         // PUT api/clone/selecao/{cloneId}
        [HttpPut("selecao/{cloneId}")]
        public async Task<IActionResult> PutSelecaoPorCloneId(long cloneId,[FromBody] TbCloneSelecao selecao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var selecaoReturn = await _CloneService.PutSelecaoPorClone(cloneId,selecao);
                
                    if(selecaoReturn != null)
                        return Ok(selecaoReturn);
                    else
                        return StatusCode(500, "Não foi possivel salvar as informações no banco de dados");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}