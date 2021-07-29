﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;

namespace DUDS.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class DistribuidorController : ControllerBase
    {
        private readonly DataContext _context;

        public DistribuidorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Distribuidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDistribuidor>>> Distribuidor()
        {
            return await _context.TblDistribuidor.Where(c => c.Ativo == true).OrderBy(c => c.NomeDistribuidorReduzido).AsNoTracking().ToListAsync();
        }

        // GET: api/Distribuidor/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblDistribuidor>> GetDistribuidor(int id)
        {
            var tblDistribuidor = await _context.TblDistribuidor.FindAsync(id);

            if (tblDistribuidor == null)
            {
                return NotFound();
            }

            return Ok(tblDistribuidor);
        }

        //PUT: api/Distribuidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarDistribuidor(int id, DistribuidorModel distribuidor)
        {
            try
            {
                var registroDistribuidor = _context.TblDistribuidor.Find(id);

                if (registroDistribuidor != null)
                {
                    registroDistribuidor.NomeDistribuidor = distribuidor.NomeDistribuidor == null ? registroDistribuidor.NomeDistribuidor : distribuidor.NomeDistribuidor;
                    registroDistribuidor.NomeDistribuidorReduzido = distribuidor.NomeDistribuidorReduzido == null ? registroDistribuidor.NomeDistribuidorReduzido : distribuidor.NomeDistribuidorReduzido;
                    registroDistribuidor.Cnpj = distribuidor.Cnpj == null ? registroDistribuidor.Cnpj : distribuidor.Cnpj;
                    registroDistribuidor.ClassificacaoDistribuidor = distribuidor.ClassificacaoDistribuidor == null ? registroDistribuidor.ClassificacaoDistribuidor : distribuidor.ClassificacaoDistribuidor;
                    registroDistribuidor.CodDistrAdm = (distribuidor.CodDistrAdm == null || distribuidor.CodDistrAdm == 0) ? registroDistribuidor.CodDistrAdm : distribuidor.CodDistrAdm;

                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException) when (!DistribuidorExists(distribuidor.Id))
            {
                return NotFound();
            }  

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<DistribuidorModel>> CadastrarDistribuidor(DistribuidorModel tblDistribuidorModel)
        {
            var itensDistribuidor = new TblDistribuidor
            {
                NomeDistribuidor = tblDistribuidorModel.NomeDistribuidor,
                NomeDistribuidorReduzido = tblDistribuidorModel.NomeDistribuidorReduzido,
                Cnpj = tblDistribuidorModel.Cnpj,
                ClassificacaoDistribuidor = tblDistribuidorModel.ClassificacaoDistribuidor,
                CodDistrAdm = tblDistribuidorModel.CodDistrAdm,
                DataModificacao = tblDistribuidorModel.DataModificacao,
                UsuarioModificacao = tblDistribuidorModel.UsuarioModificacao,
                Ativo = tblDistribuidorModel.Ativo,
            };

            _context.TblDistribuidor.Add(itensDistribuidor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDistribuidor),
                new { id = itensDistribuidor.Id },
                Ok(itensDistribuidor));
        }

        // DELETE: api/Distribuidor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarDistribuidor(int id)
        {
            var tblDistribuidor = await _context.TblDistribuidor.FindAsync(id);

            if (tblDistribuidor == null)
            {
                return NotFound();
            }

            _context.TblDistribuidor.Remove(tblDistribuidor);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DESATIVA: api/Distribuidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarDistribuidor(int id)
        {
            var registroDistribuidor = _context.TblDistribuidor.Find(id);

            if (registroDistribuidor != null)
            {
                registroDistribuidor.Ativo = false;

                await _context.SaveChangesAsync();

                return Ok();
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
    }
}
