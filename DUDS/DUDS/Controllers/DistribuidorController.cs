﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;
using DUDS.Service.Interface;

namespace DUDS.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class DistribuidorController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public DistribuidorController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        // GET: api/Distribuidor/Distribuidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDistribuidor>>> Distribuidor()
        {
            try
            {
                List<TblDistribuidor> distribuidores = await _context.TblDistribuidor.Where(c => c.Ativo == true).OrderBy(c => c.NomeDistribuidor).AsNoTracking().ToListAsync();

                if (distribuidores.Count() == 0)
                {
                    return BadRequest(Mensagem.ErroListar);
                }

                if (distribuidores != null)
                {
                    return Ok(new { distribuidores, Mensagem.SucessoListar });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (InvalidOperationException e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return NotFound(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        // GET: api/Distribuidor/GetDistribuidor/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblDistribuidor>> GetDistribuidor(int id)
        {
            TblDistribuidor tblDistribuidor = await _context.TblDistribuidor.FindAsync(id);

            try
            {
                if (tblDistribuidor != null)
                {
                    return Ok(new { tblDistribuidor, Mensagem.SucessoCadastrado });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (Exception e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        //POST: api/Distribuidor/CadastrarDistribuidor/DistribuidorModel
        [HttpPost]
        public async Task<ActionResult<DistribuidorModel>> CadastrarDistribuidor(DistribuidorModel tblDistribuidorModel)
        {
            TblDistribuidor itensDistribuidor = new TblDistribuidor
            {
                NomeDistribuidor = tblDistribuidorModel.NomeDistribuidor,
                Cnpj = tblDistribuidorModel.Cnpj,
                ClassificacaoDistribuidor = tblDistribuidorModel.ClassificacaoDistribuidor,
                DataModificacao = tblDistribuidorModel.DataModificacao,
                UsuarioModificacao = tblDistribuidorModel.UsuarioModificacao,
                Ativo = tblDistribuidorModel.Ativo,
            };

            try
            {
                _context.TblDistribuidor.Add(itensDistribuidor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetDistribuidor),
                    new { id = itensDistribuidor.Id },
                    Ok(new { itensDistribuidor, Mensagem.SucessoCadastrado }));
            }
            catch (Exception e)
            {
                return BadRequest(new { Erro = e, Mensagem.ErroCadastrar });
            }
        }

        //PUT: api/Distribuidor/EditarDistribuidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarDistribuidor(int id, DistribuidorModel distribuidor)
        {
            try
            {
                TblDistribuidor registroDistribuidor = _context.TblDistribuidor.Find(id);

                if (registroDistribuidor != null)
                {
                    registroDistribuidor.NomeDistribuidor = distribuidor.NomeDistribuidor == null ? registroDistribuidor.NomeDistribuidor : distribuidor.NomeDistribuidor;
                    registroDistribuidor.Cnpj = distribuidor.Cnpj == null ? registroDistribuidor.Cnpj : distribuidor.Cnpj;
                    registroDistribuidor.ClassificacaoDistribuidor = distribuidor.ClassificacaoDistribuidor == null ? registroDistribuidor.ClassificacaoDistribuidor : distribuidor.ClassificacaoDistribuidor;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(new { registroDistribuidor, Mensagem.SucessoAtualizado });
                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { Erro = e, Mensagem.ErroAtualizar });
                    }
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (DbUpdateConcurrencyException e) when (!DistribuidorExists(distribuidor.Id))
            {
                return NotFound(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        // DELETE: api/Distribuidor/DeletarDistribuidor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarDistribuidor(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_distribuidor");

            if (!existeRegistro)
            {
                TblDistribuidor tblDistribuidor = await _context.TblDistribuidor.FindAsync(id);

                if (tblDistribuidor == null)
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }

                try
                {
                    _context.TblDistribuidor.Remove(tblDistribuidor);
                    await _context.SaveChangesAsync();
                    return Ok(new { Mensagem.SucessoExcluido });
                }
                catch (Exception e)
                {
                    return BadRequest(new { Erro = e, Mensagem.ErroExcluir });
                }
            }
            else
            {
                return BadRequest(Mensagem.ExisteRegistroDesativar);
            }
        }

        // DESATIVA: api/Distribuidor/DesativarDistribuidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarDistribuidor(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_distribuidor");

            if (!existeRegistro)
            {
                var registroDistribuidor = _context.TblDistribuidor.Find(id);

                if (registroDistribuidor != null)
                {
                    registroDistribuidor.Ativo = false;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(new { Mensagem.SucessoDesativado });
                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { Erro = e, Mensagem.ErroDesativar });
                    }
                }
                else
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }
            }
            else
            {
                return BadRequest(Mensagem.ExisteRegistroDesativar);
            }
        }

        private bool DistribuidorExists(int id)
        {
            return _context.TblDistribuidor.Any(e => e.Id == id);
        }

        #region Distribuidor Administrador
        // GET: api/Distribuidor/DistribuidorAdministrador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDistribuidorAdministrador>>> DistribuidorAdministrador()
        {
            try
            {
                List<TblDistribuidorAdministrador> distribuidorAdministradores = await _context.TblDistribuidorAdministrador.AsNoTracking().ToListAsync();

                if (distribuidorAdministradores.Count() == 0)
                {
                    return BadRequest(Mensagem.ErroListar);
                }

                if (distribuidorAdministradores != null)
                {
                    return Ok(new { distribuidorAdministradores, Mensagem.SucessoListar });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (InvalidOperationException e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return NotFound(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        // GET: api/Distribuidor/GetDistribuidorAdministrador/cod_distribuidor/cod_administrador
        [HttpGet("{cod_distribuidor}/{cod_administrador}")]
        public async Task<ActionResult<TblDistribuidorAdministrador>> GetDistribuidorAdministrador(int cod_distribuidor, int cod_administrador)
        {
            TblDistribuidorAdministrador tblDistribuidorAdministrador = await _context.TblDistribuidorAdministrador.FindAsync(cod_distribuidor, cod_administrador);

            try
            {
                if (tblDistribuidorAdministrador != null)
                {
                    return Ok(new { tblDistribuidorAdministrador, Mensagem.SucessoCadastrado });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (Exception e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        //POST: api/Distribuidor/CadastrarDistribuidorAdmin/DistribuidorAdministradorModel
        [HttpPost]
        public async Task<ActionResult<DistribuidorAdministradorModel>> CadastrarDistribuidorAdministrador(DistribuidorAdministradorModel tblDistribuidorAdminModel)
        {
            TblDistribuidorAdministrador itensDistribuidorAdmin = new TblDistribuidorAdministrador
            {
                CodAdministrador = tblDistribuidorAdminModel.CodAdministrador,
                CodDistrAdm = tblDistribuidorAdminModel.CodDistrAdm,
                CodDistribuidor = tblDistribuidorAdminModel.CodDistribuidor,
                UsuarioModificacao = tblDistribuidorAdminModel.UsuarioModificacao,
                DataModificacao = tblDistribuidorAdminModel.DataModificacao
            };

            try
            {
                _context.TblDistribuidorAdministrador.Add(itensDistribuidorAdmin);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetDistribuidorAdministrador),
                    new { id = itensDistribuidorAdmin.Id },
                    Ok(new { itensDistribuidorAdmin, Mensagem.SucessoCadastrado }));
            }
            catch (Exception e)
            {
                return BadRequest(new { Erro = e, Mensagem.ErroCadastrar });
            }
        }

        // DELETE: api/Distribuidor/DeletarDistribuidorAdministrador/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarDistribuidorAdministrador(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_distribuidor_administrador");

            if (!existeRegistro)
            {
                TblDistribuidorAdministrador tblDistribuidorAdmin = _context.TblDistribuidorAdministrador.Where(c => c.Id == id).FirstOrDefault();

                if (tblDistribuidorAdmin == null)
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }

                try
                {
                    _context.TblDistribuidorAdministrador.Remove(tblDistribuidorAdmin);
                    await _context.SaveChangesAsync();
                    return Ok(new { Mensagem.SucessoExcluido });
                }
                catch (Exception e)
                {
                    return BadRequest(new { Erro = e, Mensagem.ErroExcluir });
                }
            }
            else
            {
                return BadRequest(Mensagem.ExisteRegistroDesativar);
            }
        }

        #endregion
    }
}
