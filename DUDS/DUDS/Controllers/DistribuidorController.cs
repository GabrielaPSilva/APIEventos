using System;
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
            return await _context.TblDistribuidor.Where(c => c.Ativo == true).OrderBy(c => c.NomeDistribuidor).AsNoTracking().ToListAsync();
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

        [HttpPost]
        public async Task<ActionResult<DistribuidorModel>> CadastrarDistribuidor(DistribuidorModel tblDistribuidorModel)
        {
            var itensDistribuidor = new TblDistribuidor
            {
                NomeDistribuidor = tblDistribuidorModel.NomeDistribuidor,
                Cnpj = tblDistribuidorModel.Cnpj,
                ClassificacaoDistribuidor = tblDistribuidorModel.ClassificacaoDistribuidor,
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
                    registroDistribuidor.Cnpj = distribuidor.Cnpj == null ? registroDistribuidor.Cnpj : distribuidor.Cnpj;
                    registroDistribuidor.ClassificacaoDistribuidor = distribuidor.ClassificacaoDistribuidor == null ? registroDistribuidor.ClassificacaoDistribuidor : distribuidor.ClassificacaoDistribuidor;

                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException) when (!DistribuidorExists(distribuidor.Id))
            {
                return NotFound();
            }

            return NoContent();
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

        #region Distribuidor Administrador
        // GET: api/DistribuidorAdministrador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDistribuidorAdministrador>>> DistribuidorAdministrador()
        {
            return await _context.TblDistribuidorAdministrador.ToListAsync();
        }

        // GET: api/DistribuidorAdministrador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblDistribuidorAdministrador>> GetDistribuidorAdministrador(int id)
        {
            var tblDistribuidorAdministrador = await _context.TblDistribuidorAdministrador.FindAsync(id);

            if (tblDistribuidorAdministrador == null)
            {
                return NotFound();
            }

            return Ok(tblDistribuidorAdministrador);
        }

        [HttpPost]
        public async Task<ActionResult<DistribuidorAdministradorModel>> CadastrarDistribuidorAdmin(DistribuidorAdministradorModel tblDistribuidorAdminModel)
        {
            var itensDistribuidorAdmin = new TblDistribuidorAdministrador
            {
                CodAdministrador = tblDistribuidorAdminModel.CodAdministrador,
                CodDistrAdm = tblDistribuidorAdminModel.CodDistrAdm,
                CodDistribuidor = tblDistribuidorAdminModel.CodDistribuidor,
                UsuarioModificacao = tblDistribuidorAdminModel.UsuarioModificacao,
                DataModificacao = tblDistribuidorAdminModel.DataModificacao
            };

            _context.TblDistribuidorAdministrador.Add(itensDistribuidorAdmin);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDistribuidorAdministrador),
                new { id = itensDistribuidorAdmin.Id },
                Ok(itensDistribuidorAdmin));
        }
        #endregion
    }
}
