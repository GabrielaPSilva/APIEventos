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
    public class GestorController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public GestorController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        #region Gestor
        // GET: api/Gestor/GetGestor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblGestor>>> GetGestor()
        {
            try
            {
                var listaGestores = await (
                                         from gestor in _context.TblGestor
                                         from tipoClassificacao in _context.TblTipoClassificacao.Where(c => c.Id == gestor.CodTipoClassificacao)
                                         where gestor.Ativo == true
                                         orderby gestor.NomeGestor
                                         select new
                                         {
                                             gestor.Id,
                                             gestor.NomeGestor,
                                             gestor.Cnpj,
                                             gestor.UsuarioModificacao,
                                             gestor.DataModificacao,
                                             gestor.Ativo,
                                             tipoClassificacao.Classificacao
                                         }).AsNoTracking().ToListAsync();

                if (listaGestores.Count == 0)
                {
                    return NotFound();
                }

                return Ok(listaGestores);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Gestor/GetGestorById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGestor>> GetGestorById(int id)
        {
            try
            {
                var tblGestor = await (
                                       from gestor in _context.TblGestor
                                       from tipoClassificacao in _context.TblTipoClassificacao.Where(c => c.Id == gestor.CodTipoClassificacao)
                                       where gestor.Id == id
                                       select new
                                       {
                                           gestor.Id,
                                           gestor.NomeGestor,
                                           gestor.Cnpj,
                                           gestor.UsuarioModificacao,
                                           gestor.DataModificacao,
                                           gestor.Ativo,
                                           tipoClassificacao.Classificacao
                                       }).AsNoTracking().ToListAsync();

                if (tblGestor != null)
                {
                    return NotFound();
                }
                return Ok(tblGestor);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Gestor/GetGestorExistsBase/cnpj
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<TblCustodiante>> GetGestorExistsBase(string cnpj)
        {
            try
            {
                var tblGestor = await (
                        from gestor in _context.TblGestor
                        from tipoClassificacao in _context.TblTipoClassificacao.Where(c => c.Id == gestor.CodTipoClassificacao)
                        where gestor.Ativo == false && gestor.Cnpj == cnpj
                        select new
                        {
                            gestor.Id,
                            gestor.NomeGestor,
                            gestor.Cnpj,
                            gestor.UsuarioModificacao,
                            gestor.DataModificacao,
                            gestor.Ativo,
                            tipoClassificacao.Classificacao
                        }).FirstOrDefaultAsync();

                if (tblGestor != null)
                {
                    return Ok(tblGestor);
                }
                else
                {
                    tblGestor = await (
                        from gestor in _context.TblGestor
                        from tipoClassificacao in _context.TblTipoClassificacao.Where(c => c.Id == gestor.CodTipoClassificacao)
                        where gestor.Cnpj == cnpj
                        select new
                        {
                            gestor.Id,
                            gestor.NomeGestor,
                            gestor.Cnpj,
                            gestor.UsuarioModificacao,
                            gestor.DataModificacao,
                            gestor.Ativo,
                            tipoClassificacao.Classificacao
                        }).FirstOrDefaultAsync();

                    if (tblGestor != null)
                    {
                        return Ok(tblGestor);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //POST: api/Gestor/AddGestor/GestorModel
        [HttpPost]
        public async Task<ActionResult<GestorModel>> AddGestor(GestorModel tblGestorModel)
        {
            TblGestor itensGestor = new TblGestor
            {
                NomeGestor = tblGestorModel.NomeGestor,
                CodTipoClassificacao = tblGestorModel.CodTipoClassificacao,
                Cnpj = tblGestorModel.Cnpj,
                UsuarioModificacao = tblGestorModel.UsuarioModificacao
            };

            try
            {
                _context.TblGestor.Add(itensGestor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetGestor),
                    new { id = itensGestor.Id }, itensGestor);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //PUT: api/Gestor/UpdateGestor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGestor(int id, GestorModel gestor)
        {
            try
            {
                TblGestor registroGestor = _context.TblGestor.Find(id);

                if (registroGestor != null)
                {
                    registroGestor.NomeGestor = gestor.NomeGestor == null ? registroGestor.NomeGestor : gestor.NomeGestor;
                    registroGestor.CodTipoClassificacao = gestor.CodTipoClassificacao == 0 ? registroGestor.CodTipoClassificacao : gestor.CodTipoClassificacao;
                    registroGestor.Cnpj = gestor.Cnpj == null ? registroGestor.Cnpj : gestor.Cnpj;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroGestor);
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
            catch (DbUpdateConcurrencyException e) when (!GestorExists(gestor.Id))
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DELETE: api/Gestor/DeleteGestor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGestor(int id)
        {
            var tblGestor = await _context.TblGestor.FindAsync(id);

            if (tblGestor == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblGestor.Remove(tblGestor);
                await _context.SaveChangesAsync();
                return Ok(tblGestor);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DESATIVA: api/Gestor/DisableGestor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DisableGestor(int id)
        {
            TblGestor registroGestor = _context.TblGestor.Find(id);

            if (registroGestor != null)
            {
                registroGestor.Ativo = false;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroGestor);
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

        // ATIVAR: api/Gestor/ActivateGestor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateGestor(int id)
        {
            TblGestor registroGestor = await _context.TblGestor.FindAsync(id);

            if (registroGestor != null)
            {
                registroGestor.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroGestor);
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

        private bool GestorExists(int id)
        {
            return _context.TblGestor.Any(e => e.Id == id);
        }
        #endregion
    }
}
