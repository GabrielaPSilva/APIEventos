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

        public InvestidorController(IConfiguracaoService configService, IInvestidorService investidorService)
        {
            _configService = configService;
            _investidorService = investidorService;
        }

        // GET: api/Investidor/GetInvestidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvestidorModel>>> GetInvestidor()
        {
            try
            {
                var investidores = await _investidorService.GetInvestidor();

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
                var tblInvestidor = await _investidorService.GetInvestidorById(id);

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
                var retorno = await _investidorService.AddInvestidor(investidorModel);

                return CreatedAtAction(
                    nameof(GetInvestidor),
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
                var registroInvestidor = await _investidorService.GetInvestidorById(investidorModel.Id);

                if (registroInvestidor != null)
                {
                    try
                    {
                        await _investidorService.UpdateInvestidor(investidorModel);
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
                var registroInvestidor = await _investidorService.DisableInvestidor(id);

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
            var registroInvestidor = await _investidorService.GetInvestidorById(id);

            if (registroInvestidor != null)
            {
                try
                {
                    await _investidorService.ActivateInvestidor(id);
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

        // GET: api/Investidor/GetInvestidorDistribuidorByIds/cod_investidor/cod_distribuidor/cod_administrador
        //[HttpGet("{cod_investidor}/{cod_distribuidor}/{cod_administrador}")]
        //public async Task<ActionResult<TblInvestidorDistribuidor>> GetInvestidorDistribuidorByIds(int cod_investidor, int cod_distribuidor, int cod_administrador)
        //{
        //    try
        //    {
        //        TblInvestidorDistribuidor tblInvestidorDistribuidor = await _context.TblInvestidorDistribuidor.FindAsync(cod_investidor, cod_distribuidor, cod_administrador);
        //        if (tblInvestidorDistribuidor == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(tblInvestidorDistribuidor);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.InnerException.Message);
        //    }
        //}

        //POST: api/Investidor/AddInvestidorDistribuidor/InvestidorDistribuidorModel
        //[HttpPost]
        //public async Task<ActionResult<InvestidorDistribuidorModel>> AddInvestidorDistribuidor(InvestidorDistribuidorModel tblInvestidorDistribuidorModel)
        //{
        //    TblInvestidorDistribuidor itensInvestidorDistribuidor = new TblInvestidorDistribuidor
        //    {
        //        CodAdministrador = tblInvestidorDistribuidorModel.CodAdministrador,
        //        CodDistribuidor = tblInvestidorDistribuidorModel.CodDistribuidor,
        //        CodInvestAdministrador = tblInvestidorDistribuidorModel.CodInvestAdministrador,
        //        CodInvestidor = tblInvestidorDistribuidorModel.CodInvestidor,
        //        UsuarioModificacao = tblInvestidorDistribuidorModel.UsuarioModificacao
        //    };

        //    try
        //    {
        //        _context.TblInvestidorDistribuidor.Add(itensInvestidorDistribuidor);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction(
        //            nameof(GetInvestidorDistribuidor),
        //            new
        //            {
        //                cod_investidor = itensInvestidorDistribuidor.CodInvestidor,
        //                cod_distribuidor = itensInvestidorDistribuidor.CodDistribuidor,
        //                cod_administrador = itensInvestidorDistribuidor.CodAdministrador
        //            }, itensInvestidorDistribuidor);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.InnerException.Message);
        //    }
        //}

        //POST: api/Investidor/AddInvestidorDistribuidores/List<InvestidorDistribuidorModel>
        //[HttpPost]
        //public async Task<ActionResult<List<InvestidorDistribuidorModel>>> AddInvestidorDistribuidores(List<InvestidorDistribuidorModel> tblListInvestidorDistribuidorModel)
        //{
        //    try
        //    {
        //        List<TblInvestidorDistribuidor> listaInvestidorDistribuidores = new List<TblInvestidorDistribuidor>();
        //        TblInvestidorDistribuidor itensInvestidorDistribuidor = new TblInvestidorDistribuidor();

        //        foreach (var line in tblListInvestidorDistribuidorModel)
        //        {
        //            itensInvestidorDistribuidor = new TblInvestidorDistribuidor
        //            {
        //                CodAdministrador = line.CodAdministrador,
        //                CodDistribuidor = line.CodDistribuidor,
        //                CodInvestAdministrador = line.CodInvestAdministrador,
        //                CodInvestidor = line.CodInvestidor,
        //                UsuarioModificacao = line.UsuarioModificacao
        //            };

        //            listaInvestidorDistribuidores.Add(itensInvestidorDistribuidor);
        //        }

        //        await _context.BulkInsertAsync(listaInvestidorDistribuidores);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction(
        //           nameof(GetInvestidorDistribuidor), listaInvestidorDistribuidores);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.InnerException.Message);
        //    }
        //}

        //PUT: api/Investidor/UpdateInvestidorDistribuidor/id
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateInvestidorDistribuidor(int id, InvestidorDistribuidorModel investidorDistribuidor)
        //{
        //    try
        //    {
        //        TblInvestidorDistribuidor registroInvestidorDistribuidor = _context.TblInvestidorDistribuidor.Where(c => c.Id == id).FirstOrDefault();

        //        if (registroInvestidorDistribuidor != null)
        //        {
        //            registroInvestidorDistribuidor.CodInvestAdministrador = investidorDistribuidor.CodInvestAdministrador == null ? registroInvestidorDistribuidor.CodInvestAdministrador : investidorDistribuidor.CodInvestAdministrador;
        //            registroInvestidorDistribuidor.CodInvestidor = investidorDistribuidor.CodInvestidor == 0 ? registroInvestidorDistribuidor.CodInvestidor : investidorDistribuidor.CodInvestidor;
        //            registroInvestidorDistribuidor.CodDistribuidor = investidorDistribuidor.CodDistribuidor == 0 ? registroInvestidorDistribuidor.CodDistribuidor : investidorDistribuidor.CodDistribuidor;
        //            registroInvestidorDistribuidor.CodAdministrador = investidorDistribuidor.CodAdministrador == 0 ? registroInvestidorDistribuidor.CodAdministrador : investidorDistribuidor.CodAdministrador;

        //            try
        //            {
        //                await _context.SaveChangesAsync();
        //                return Ok(registroInvestidorDistribuidor);
        //            }
        //            catch (Exception e)
        //            {
        //                return BadRequest(e.InnerException.Message);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (DbUpdateConcurrencyException e) when (!InvestidorDistribuidorExists(investidorDistribuidor.Id))
        //    {
        //        return NotFound(e.InnerException.Message);
        //    }
        //}

        // DELETE: api/Investidor/DeleteInvestidorDistribuidor/id
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteInvestidorDistribuidor(int id)
        //{
        //    bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_investidor_distribuidor");

        //    if (!existeRegistro)
        //    {
        //        TblInvestidorDistribuidor tblInvestidorDistribuidor = _context.TblInvestidorDistribuidor.Where(c => c.Id == id).FirstOrDefault();

        //        if (tblInvestidorDistribuidor == null)
        //        {
        //            return NotFound();
        //        }

        //        try
        //        {
        //            _context.TblInvestidorDistribuidor.Remove(tblInvestidorDistribuidor);
        //            await _context.SaveChangesAsync();
        //            return Ok(tblInvestidorDistribuidor);
        //        }
        //        catch (Exception e)
        //        {
        //            return BadRequest(e.InnerException.Message);
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //private bool InvestidorDistribuidorExists(int id)
        //{
        //    return _context.TblInvestidorDistribuidor.Any(e => e.Id == id);
        //}

        #endregion
    }
}
