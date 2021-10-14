using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DUDS.Models;
using DUDS.Service.Interface;

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    public class ContasController : ControllerBase
    {
        private readonly IConfiguracaoService _configService;
        private readonly IContaService _contaService;

        public ContasController(IConfiguracaoService configService, IContaService contaService)
        {
            _configService = configService;
            _contaService = contaService;
        }

        #region Conta
        // GET: api/Contas/GetContas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContaModel>>> GetContas()
        {
            try
            {
                var contas = await _contaService.GetAllAsync();

                if (contas.Any())
                {
                    return Ok(contas);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/Contas/GetContasById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ContaModel>> GetContaById(int id)
        {
            try
            {
                var tblConta = await _contaService.GetByIdAsync(id);

                if (tblConta == null)
                {
                    return NotFound();
                }

                return Ok(tblConta);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/Contas/GetContasExistsBase/cod_fundo/cod_investidor/cod_tipo_conta
        [HttpGet("{cod_tipo_conta}")]
        public async Task<ActionResult<ContaModel>> GetContasExistsBase(int codFundo, int codInvestidor, int codTipoConta)
        {
            try
            {
                var tblConta = await _contaService.GetContaExistsBase(codFundo, codInvestidor, codTipoConta);

                if (tblConta != null)
                {
                    return Ok(tblConta);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //POST: api/Contas/AddConta/ContaModel
        [HttpPost]
        public async Task<ActionResult<ContaModel>> AddConta(ContaModel contaModel)
        {
            try
            {
                var retorno = await _contaService.AddAsync(contaModel);

                return CreatedAtAction(
                    nameof(GetContaById),
                    new { id = contaModel.Id }, contaModel);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //PUT: api/Conta/UpdateConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConta(int id, ContaModel conta)
        {
            try
            {
                ContaModel retornoConta = await _contaService.GetByIdAsync(conta.Id);

                if (retornoConta == null)
                {
                    return NotFound();
                }

                conta.Id = id;
                bool retorno = await _contaService.UpdateAsync(conta);

                if (retorno)
                {
                    return Ok(conta);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Conta/DeleteConta/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConta(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_contas");

            if (!existeRegistro)
            {
                try
                {
                    bool retorno = await _contaService.DeleteAsync(id);

                    if (retorno)
                    {
                        return Ok();
                    }

                    return NotFound();
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // ATIVAR: api/Contas/ActivateContas/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateContas(int id)
        {
            var registroConta = await _contaService.GetByIdAsync(id);

            if (registroConta != null)
            {
                try
                {
                    await _contaService.ActivateAsync(id);
                    return Ok(registroConta);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }

            #endregion
        }
    }
}
