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

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    public class DistribuidorController : ControllerBase
    {
        private readonly IDistribuidorService _distribuidorService;
        private readonly IConfiguracaoService _configService;

        public DistribuidorController(IDistribuidorService distribuidorService, IConfiguracaoService configService)
        {
            _distribuidorService = distribuidorService;
            _configService = configService;
        }

        #region Distribuidor
        // GET: api/Distribuidor/GetDistribuidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistribuidorModel>>> GetDistribuidor()
        {
            try
            {
                var distribuidores = await _distribuidorService.GetAllAsync();

                if (distribuidores.Any())
                {
                    return Ok(distribuidores);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        // GET: api/Distribuidor/GetDistribuidorById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<DistribuidorModel>> GetDistribuidorById(int id)
        {
            try
            {
                var distribuidor = await _distribuidorService.GetByIdAsync(id);
                if (distribuidor == null)
                {
                    return NotFound();
                }
                return Ok(distribuidor);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        /*
        // GET: api/Distribuidor/GetDistribuidorExistsBase/cnpj
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<TblDistribuidor>> GetDistribuidorExistsBase(string cnpj)
        {
            var tblDistribuidor = await (
                                   from distribuidor in _context.TblDistribuidor
                                   from tipoClassificacao in _context.TblTipoClassificacao.Where(c => c.Id == distribuidor.CodTipoClassificacao)
                                   where distribuidor.Ativo == false && distribuidor.Cnpj == cnpj
                                   select new
                                   {
                                       distribuidor.Id,
                                       distribuidor.NomeDistribuidor,
                                       distribuidor.Cnpj,
                                       distribuidor.CodTipoClassificacao,
                                       distribuidor.UsuarioModificacao,
                                       distribuidor.DataModificacao,
                                       distribuidor.Ativo,
                                       tipoClassificacao.Classificacao
                                   }).FirstOrDefaultAsync();

            if (tblDistribuidor != null)
            {
                return Ok(tblDistribuidor);
            }
            else
            {
                tblDistribuidor = await (
                                   from distribuidor in _context.TblDistribuidor
                                   from tipoClassificacao in _context.TblTipoClassificacao.Where(c => c.Id == distribuidor.CodTipoClassificacao)
                                   where distribuidor.Cnpj == cnpj
                                   select new
                                   {
                                       distribuidor.Id,
                                       distribuidor.NomeDistribuidor,
                                       distribuidor.Cnpj,
                                       distribuidor.CodTipoClassificacao,
                                       distribuidor.UsuarioModificacao,
                                       distribuidor.DataModificacao,
                                       distribuidor.Ativo,
                                       tipoClassificacao.Classificacao
                                   }).FirstOrDefaultAsync();

                if (tblDistribuidor != null)
                {
                    return Ok(tblDistribuidor);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        /*
        //POST: api/Distribuidor/AddDistribuidor/DistribuidorModel
        [HttpPost]
        public async Task<ActionResult<DistribuidorModel>> AddDistribuidor(DistribuidorModel tblDistribuidorModel)
        {
            TblDistribuidor itensDistribuidor = new TblDistribuidor
            {
                NomeDistribuidor = tblDistribuidorModel.NomeDistribuidor,
                Cnpj = tblDistribuidorModel.Cnpj,
                CodTipoClassificacao = tblDistribuidorModel.CodTipoClassificacao,
                UsuarioModificacao = tblDistribuidorModel.UsuarioModificacao
            };

            try
            {
                _context.TblDistribuidor.Add(itensDistribuidor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetDistribuidor),
                    new { id = itensDistribuidor.Id }, itensDistribuidor);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //PUT: api/Distribuidor/UpdateDistribuidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDistribuidor(int id, DistribuidorModel distribuidor)
        {
            try
            {
                TblDistribuidor registroDistribuidor = _context.TblDistribuidor.Find(id);

                if (registroDistribuidor != null)
                {
                    registroDistribuidor.NomeDistribuidor = distribuidor.NomeDistribuidor == null ? registroDistribuidor.NomeDistribuidor : distribuidor.NomeDistribuidor;
                    registroDistribuidor.Cnpj = distribuidor.Cnpj == null ? registroDistribuidor.Cnpj : distribuidor.Cnpj;
                    registroDistribuidor.CodTipoClassificacao = distribuidor.CodTipoClassificacao == 0 ? registroDistribuidor.CodTipoClassificacao : distribuidor.CodTipoClassificacao;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroDistribuidor);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.InnerException.Message);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException e) when (!DistribuidorExists(distribuidor.Id))
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DELETE: api/Distribuidor/DeleteDistribuidor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistribuidor(int id)
        {
            TblDistribuidor tblDistribuidor = await _context.TblDistribuidor.FindAsync(id);

            if (tblDistribuidor == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblDistribuidor.Remove(tblDistribuidor);
                await _context.SaveChangesAsync();
                return Ok(tblDistribuidor);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DESATIVA: api/Distribuidor/DisableDistribuidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DisableDistribuidor(int id)
        {
            TblDistribuidor registroDistribuidor = _context.TblDistribuidor.Find(id);

            if (registroDistribuidor != null)
            {
                registroDistribuidor.Ativo = false;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroDistribuidor);
                }
                catch (Exception e)
                {
                    return BadRequest(e.InnerException.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }

        // ATIVAR: api/Distribuidor/ActivateDistribuidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateDistribuidor(int id)
        {
            TblDistribuidor registroDistribuidor = await _context.TblDistribuidor.FindAsync(id);

            if (registroDistribuidor != null)
            {
                registroDistribuidor.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroDistribuidor);
                }
                catch (Exception e)
                {
                    return BadRequest(e.InnerException.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }

        private bool DistribuidorExists(int id)
        {
            return _context.TblDistribuidor.Any(e => e.Id == id);
        }
        #endregion

        #region Distribuidor Administrador
        // GET: api/Distribuidor/GetDistribuidorAdministrador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDistribuidorAdministrador>>> GetDistribuidorAdministrador()
        {
            try
            {
                List<TblDistribuidorAdministrador> distribuidorAdministradores = await _context.TblDistribuidorAdministrador.AsNoTracking().ToListAsync();

                if (distribuidorAdministradores.Count == 0)
                {
                    return NotFound();
                }

                return Ok(distribuidorAdministradores);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Distribuidor/GetDistribuidorAdministradorByIds/cod_distribuidor/cod_administrador
        [HttpGet("{cod_distribuidor}/{cod_administrador}")]
        public async Task<ActionResult<TblDistribuidorAdministrador>> GetDistribuidorAdministradorByIds(int cod_distribuidor, int cod_administrador)
        {
            try
            {
                TblDistribuidorAdministrador tblDistribuidorAdministrador = await _context.TblDistribuidorAdministrador.FindAsync(cod_distribuidor, cod_administrador);
                if (tblDistribuidorAdministrador == null)
                {
                    return NotFound();
                }
                return Ok(tblDistribuidorAdministrador);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }
        /*
        //POST: api/Distribuidor/AddDistribuidorAdministrador/DistribuidorAdministradorModel
        [HttpPost]
        public async Task<ActionResult<DistribuidorAdministradorModel>> AddDistribuidorAdministrador(DistribuidorAdministradorModel tblDistribuidorAdminModel)
        {
            TblDistribuidorAdministrador itensDistribuidorAdmin = new TblDistribuidorAdministrador
            {
                CodAdministrador = tblDistribuidorAdminModel.CodAdministrador,
                CodDistrAdm = tblDistribuidorAdminModel.CodDistrAdm,
                CodDistribuidor = tblDistribuidorAdminModel.CodDistribuidor,
                UsuarioModificacao = tblDistribuidorAdminModel.UsuarioModificacao
            };

            try
            {
                _context.TblDistribuidorAdministrador.Add(itensDistribuidorAdmin);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetDistribuidorAdministrador),
                    new
                    {
                        cod_distribuidor = itensDistribuidorAdmin.CodDistribuidor,
                        cod_administrador = itensDistribuidorAdmin.CodAdministrador
                    }, itensDistribuidorAdmin);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //PUT: api/Distribuidor/UpdateDistribuidorAdministrador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDistribuidorAdministrador(int id, DistribuidorAdministradorModel distribuidorAdmin)
        {
            try
            {
                TblDistribuidorAdministrador registroDistribuidorAdministrador = _context.TblDistribuidorAdministrador.Where(c => c.Id == id).FirstOrDefault();

                if (registroDistribuidorAdministrador != null)
                {
                    registroDistribuidorAdministrador.CodAdministrador = distribuidorAdmin.CodAdministrador == 0 ? registroDistribuidorAdministrador.CodAdministrador : distribuidorAdmin.CodAdministrador;
                    registroDistribuidorAdministrador.CodDistribuidor = distribuidorAdmin.CodDistribuidor == 0 ? registroDistribuidorAdministrador.CodDistribuidor : distribuidorAdmin.CodDistribuidor;
                    registroDistribuidorAdministrador.CodDistrAdm = distribuidorAdmin.CodDistrAdm == null ? registroDistribuidorAdministrador.CodDistrAdm : distribuidorAdmin.CodDistrAdm;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroDistribuidorAdministrador);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.InnerException.Message);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException e) when (!DistribuidorAdministradorExists(distribuidorAdmin.Id))
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DELETE: api/Distribuidor/DeleteDistribuidorAdministrador/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistribuidorAdministrador(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_distribuidor_administrador");

            if (!existeRegistro)
            {
                TblDistribuidorAdministrador tblDistribuidorAdmin = _context.TblDistribuidorAdministrador.Where(c => c.Id == id).FirstOrDefault();

                if (tblDistribuidorAdmin == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblDistribuidorAdministrador.Remove(tblDistribuidorAdmin);
                    await _context.SaveChangesAsync();
                    return Ok(tblDistribuidorAdmin);
                }
                catch (Exception e)
                {
                    return BadRequest(e.InnerException.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }

        private bool DistribuidorAdministradorExists(int id)
        {
            return _context.TblDistribuidorAdministrador.Any(e => e.Id == id);
        }
        */
        #endregion
    }
}
