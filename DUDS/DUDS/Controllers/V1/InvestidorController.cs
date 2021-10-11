using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;
using DUDS.Service.Interface;
using EFCore.BulkExtensions;

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    public class InvestidorController : ControllerBase
    {
        private readonly IConfiguracaoService _configService;
        private readonly IInvestidorService _investidorService;
        private readonly IInvestidorDistribuidorService _investidorDistribuidorService;

        public InvestidorController(IConfiguracaoService configService, IInvestidorService investidorService, IInvestidorDistribuidorService investidorDistribuidorService)
        {
            _configService = configService;
            _investidorService = investidorService;
            _investidorDistribuidorService = investidorDistribuidorService;
        }

        #region Investidor
        // GET: api/Investidor/GetInvestidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvestidorModel>>> GetInvestidor()
        {
            try
            {
                var investidores = await _investidorService.GetAllAsync();

                if (investidores.Any())
                {
                    return Ok(investidores);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/Investidor/GetInvestidorById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<InvestidorModel>> GetInvestidorById(int id)
        {
            try
            {
                var tblInvestidor = await _investidorService.GetByIdAsync(id);

                if (tblInvestidor == null)
                {
                    return NotFound();
                }

                return Ok(tblInvestidor);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/Investidor/GetInvestidorExistsBase/cnpj
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<InvestidorModel>> GetInvestidorExistsBase(string cnpj)
        {
            try
            {
                var tblInvestidor = await _investidorService.GetInvestidorExistsBase(cnpj);

                if (tblInvestidor != null)
                {
                    return Ok(tblInvestidor);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //POST: api/Investidor/AddInvestidor/InvestidorModel
        [HttpPost]
        public async Task<ActionResult<InvestidorModel>> AddInvestidor(InvestidorModel investidorModel)
        {
            try
            {
                var retorno = await _investidorService.AddAsync(investidorModel);

                return CreatedAtAction(
                    nameof(GetInvestidorById),
                    new { id = investidorModel.Id }, investidorModel);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //POST: api/Investidor/AddInvestidores/List<InvestidorModel>
        //[HttpPost]
        //public async Task<ActionResult<List<InvestidorModel>>> AddInvestidores(List<InvestidorModel> tblListInvestidorModel)
        //{
        //    try
        //    {
        //        List<TblInvestidor> listaInvestidores = new List<TblInvestidor>();
        //        TblInvestidor itensInvestidor = new TblInvestidor();

        //        foreach (var line in tblListInvestidorModel)
        //        {
        //            itensInvestidor = new TblInvestidor
        //            {
        //                NomeCliente = line.NomeCliente,
        //                Cnpj = line.Cnpj,
        //                TipoCliente = line.TipoCliente,
        //                CodAdministrador = line.CodAdministrador,
        //                CodGestor = line.CodGestor,
        //                CodGrupoRebate = line.CodGrupoRebate,
        //                CodTipoContrato = line.CodTipoContrato,
        //                UsuarioModificacao = line.UsuarioModificacao
        //            };

        //            listaInvestidores.Add(itensInvestidor);
        //        }

        //        await _context.BulkInsertAsync(listaInvestidores);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction(
        //             nameof(GetInvestidor), listaInvestidores);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.InnerException.Message);
        //    }
        //}

        //PUT: api/Investidor/UpdateInvestidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvestidor(InvestidorModel investidorModel)
        {
            try
            {
                var registroInvestidor = await _investidorService.GetByIdAsync(investidorModel.Id);

                if (registroInvestidor != null)
                {
                    try
                    {
                        await _investidorService.UpdateAsync(investidorModel);
                        return Ok(registroInvestidor);
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
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Investidor/DeleteInvestidor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestidor(int id)
        {
            try
            {
                var registroInvestidor = await _investidorService.DisableAsync(id);

                if (registroInvestidor)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // ATIVAR: api/Investidor/ActivateInvestidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateInvestidor(int id)
        {
            var registroInvestidor = await _investidorService.GetByIdAsync(id);

            if (registroInvestidor != null)
            {
                try
                {
                    await _investidorService.ActivateAsync(id);
                    return Ok(registroInvestidor);
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
        }

        #endregion

        #region Investidor Distribuidor
        // GET: api/Investidor/GetInvestidorDistribuidor
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TblInvestidorDistribuidor>>> GetInvestidorDistribuidor()
        //{
        //    try
        //    {
        //        List<TblInvestidorDistribuidor> investidorDistribuidores = await _context.TblInvestidorDistribuidor.AsNoTracking().ToListAsync();

        //        if (investidorDistribuidores.Count == 0)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(investidorDistribuidores);
        //    }
        //    catch (InvalidOperationException e)
        //    {
        //        return BadRequest(e.InnerException.Message);
        //    }
        //}

        // GET: api/Investidor/GetInvestidorDistribuidorByIds/codInvestidor/codDistribuidor/codAdministrador
        [HttpGet("{codInvestidor}/{codDistribuidor}/{codAdministrador}")]
        public async Task<ActionResult<InvestidorDistribuidorModel>> GetInvestidorDistribuidorByIds(int codInvestidor, int codDistribuidor, int codAdministrador)
        {
            try
            {
                var InvestidorDistribuidor = await _investidorDistribuidorService.GetByIdsAsync(codInvestidor, codDistribuidor, codAdministrador);
               
                if (InvestidorDistribuidor == null)
                {
                    return NotFound();
                }
                return Ok(InvestidorDistribuidor);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Investidor/AddInvestidorDistribuidor/InvestidorDistribuidorModel
        [HttpPost]
        public async Task<ActionResult<InvestidorDistribuidorModel>> AddInvestidorDistribuidor(InvestidorDistribuidorModel investidorDistribuidorModel)
        {
            try
            {
                bool retorno = await _investidorDistribuidorService.AddAsync(investidorDistribuidorModel);
                return CreatedAtAction(nameof(GetInvestidorDistribuidorByIds), new { 
                                                          codInvestidor = investidorDistribuidorModel.CodInvestidor,
                                                          codAdministrador = investidorDistribuidorModel.CodAdministrador,
                                                          codDistribuidor = investidorDistribuidorModel.CodDistribuidor}, investidorDistribuidorModel);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Investidor/AddInvestidorDistribuidores/List<InvestidorDistribuidorModel>
        //[HttpPost]
        //public async Task<ActionResult<List<InvestidorDistribuidorModel>>> AddInvestidorDistribuidores(List<InvestidorDistribuidorModel> tblListInvestidorDistribuidorModel)
        //{
        //    try
        //    {
        //        DistribuidorAdministradorModel retornoDistribuidorAdministrador = await _distribuidorAdministradorService.GetByIdAsync(distribuidorAdministrador.Id);
        //        if (retornoDistribuidorAdministrador == null)
        //        {
        //            return NotFound();
        //        }
        //        distribuidorAdministrador.Id = id;
        //        bool retorno = await _distribuidorAdministradorService.UpdateAsync(distribuidorAdministrador);
        //        if (retorno)
        //        {
        //            return Ok(distribuidorAdministrador);
        //        }
        //        return NotFound();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}

        //PUT: api/Investidor/UpdateInvestidorDistribuidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvestidorDistribuidor(int id, InvestidorDistribuidorModel investidorDistribuidor)
        {
            try
            {
                InvestidorDistribuidorModel retornoInvestidorDistribuidor = await _investidorDistribuidorService.GetByIdAsync(investidorDistribuidor.Id);
                
                if (retornoInvestidorDistribuidor == null)
                {
                    return NotFound();
                }
                
                investidorDistribuidor.Id = id;
                bool retorno = await _investidorDistribuidorService.UpdateAsync(investidorDistribuidor);
                
                if (retorno)
                {
                    return Ok(investidorDistribuidor);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Investidor/DeleteInvestidorDistribuidor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestidorDistribuidor(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_investidor_distribuidor");

            if (!existeRegistro)
            {
                try
                {
                    bool retorno = await _investidorDistribuidorService.DeleteAsync(id);
                    
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

        #endregion
    }
}
