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
    public class CustodianteController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public CustodianteController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        // GET: api/Custodiante/Custodiante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCustodiante>>> Custodiante()
        {
            try
            {
                List<TblCustodiante> custodiantes = await _context.TblCustodiante.Where(c => c.Ativo == true).OrderBy(c => c.NomeCustodiante).AsNoTracking().ToListAsync();

                if (custodiantes.Count == 0)
                {
                    return NotFound();
                }

                return Ok(custodiantes);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Custodiante/GetCustodiante/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCustodiante>> GetCustodiante(int id)
        {
            try
            {
                TblCustodiante tblCustodiante = await _context.TblCustodiante.FindAsync(id);
                if (tblCustodiante == null)
                {
                    return NotFound();
                }
                return Ok(tblCustodiante);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Custodiante/GetCustodianteExiste/cnpj
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<TblCustodiante>> GetCustodianteExiste(string cnpj)
        {
            TblCustodiante tblCustodiante = new TblCustodiante();

            try
            {
                tblCustodiante = await _context.TblCustodiante.Where(c => c.Ativo == false && c.Cnpj == cnpj).FirstOrDefaultAsync();

                if (tblCustodiante != null)
                {
                    return Ok(tblCustodiante);
                }
                else
                {
                    tblCustodiante = await _context.TblCustodiante.Where(c => c.Cnpj == cnpj).FirstOrDefaultAsync();

                    if (tblCustodiante != null)
                    {
                        return Ok(tblCustodiante);
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

        //POST: api/Custodiante/CadastrarCustodiante/CustodianteModel
        [HttpPost]
        public async Task<ActionResult<CustodianteModel>> CadastrarCustodiante(CustodianteModel tblCustodianteModel)
        {
            TblCustodiante itensCustodiante = new TblCustodiante
            {
                NomeCustodiante = tblCustodianteModel.NomeCustodiante,
                Cnpj = tblCustodianteModel.Cnpj,
                UsuarioModificacao = tblCustodianteModel.UsuarioModificacao
            };

            try
            {
                _context.TblCustodiante.Add(itensCustodiante);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetCustodiante),
                    new { id = itensCustodiante.Id },
                   Ok(itensCustodiante));
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //PUT: api/Custodiante/EditarCustodiante/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCustodiante(int id, CustodianteModel custodiante)
        {
            try
            {
                TblCustodiante registroCustodiante = _context.TblCustodiante.Find(id);

                if (registroCustodiante != null)
                {
                    registroCustodiante.NomeCustodiante = custodiante.NomeCustodiante == null ? registroCustodiante.NomeCustodiante : custodiante.NomeCustodiante;
                    registroCustodiante.Cnpj = custodiante.Cnpj == null ? registroCustodiante.Cnpj : custodiante.Cnpj;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroCustodiante);
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
            catch (DbUpdateConcurrencyException e) when (!CustodianteExists(custodiante.Id))
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DELETE: api/Custodiante/DeletarCustodiante/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarCustodiante(int id)
        {
            TblCustodiante tblCustodiante = await _context.TblCustodiante.FindAsync(id);

            if (tblCustodiante == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblCustodiante.Remove(tblCustodiante);
                await _context.SaveChangesAsync();
                return Ok(tblCustodiante);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DESATIVA: api/Custodiante/DesativarCustodiante/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarCustodiante(int id)
        {
            TblCustodiante registroCustodiante = _context.TblCustodiante.Find(id);

            if (registroCustodiante != null)
            {
                registroCustodiante.Ativo = false;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroCustodiante);
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

        // ATIVAR: api/Custodiante/AtivarCustodiante/id
        [HttpPut("{id}")]
        public async Task<IActionResult> AtivarCustodiante(int id)
        {
            TblCustodiante registroCustodiante = await _context.TblCustodiante.FindAsync(id);

            if (registroCustodiante != null)
            {
                registroCustodiante.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroCustodiante);
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

        private bool CustodianteExists(int id)
        {
            return _context.TblCustodiante.Any(e => e.Id == id);
        }
    }
}
