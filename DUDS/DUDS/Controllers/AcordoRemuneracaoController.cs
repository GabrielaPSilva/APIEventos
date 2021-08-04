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
    public class AcordoRemuneracaoController : ControllerBase
    {
        private readonly DataContext _context;

        public AcordoRemuneracaoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/AcordoRemuneracao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAcordoRemuneracao>>> AcordoRemuneracao()
        {
            return await _context.TblAcordoRemuneracao.Where(c => c.Ativo == true).AsNoTracking().ToListAsync();
        }

        // GET: api/AcordoRemuneracao/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAcordoRemuneracao>> GetAcordoRemuneracao(int id)
        {
            var tblAcordoRemuneracao = await _context.TblAcordoRemuneracao.FindAsync(id);

            if (tblAcordoRemuneracao == null)
            {
                return NotFound();
            }

            return Ok(tblAcordoRemuneracao);
        }

        [HttpPost]
        public async Task<ActionResult<AcordoRemuneracaoModel>> CadastrarAcordoRemuneracao(AcordoRemuneracaoModel tblAcordoRemuneracaoModel)
        {
            var itensAcordoRemuneracao = new TblAcordoRemuneracao
            {
                CodContratoDistribuicao = tblAcordoRemuneracaoModel.CodContratoDistribuicao,
                Inicio = tblAcordoRemuneracaoModel.Inicio,
                Fim = tblAcordoRemuneracaoModel.Fim,
                Percentual = tblAcordoRemuneracaoModel.Percentual,
                TipoTaxa = tblAcordoRemuneracaoModel.TipoTaxa,
                TipoRange = tblAcordoRemuneracaoModel.TipoRange,
                DataVigenciaInicio = tblAcordoRemuneracaoModel.DataVigenciaInicio,
                DataVigenciaFim = tblAcordoRemuneracaoModel.DataVigenciaFim,
                UsuarioModificacao = tblAcordoRemuneracaoModel.UsuarioModificacao,
                DataModificacao = tblAcordoRemuneracaoModel.DataModificacao
            };

            _context.TblAcordoRemuneracao.Add(itensAcordoRemuneracao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAcordoRemuneracao),
                new { id = itensAcordoRemuneracao.Id },
                Ok(itensAcordoRemuneracao));
        }

        //PUT: api/AcordoRemuneracao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarAcordoRemuneracao(int id, AcordoRemuneracaoModel acordoRemuneracao)
        {
            try
            {
                var registroAcordoRemuneracao = _context.TblAcordoRemuneracao.Find(id);

                if (registroAcordoRemuneracao != null)
                {
                    registroAcordoRemuneracao.CodContratoDistribuicao = acordoRemuneracao.CodContratoDistribuicao == 0 ? registroAcordoRemuneracao.CodContratoDistribuicao : acordoRemuneracao.CodContratoDistribuicao;
                    registroAcordoRemuneracao.Inicio = acordoRemuneracao.Inicio == 0 ? registroAcordoRemuneracao.Inicio : acordoRemuneracao.Inicio;
                    registroAcordoRemuneracao.Fim = acordoRemuneracao.Fim == 0 ? registroAcordoRemuneracao.Fim : acordoRemuneracao.Fim;
                    registroAcordoRemuneracao.Percentual = acordoRemuneracao.Percentual == 0 ? registroAcordoRemuneracao.Percentual : acordoRemuneracao.Percentual;
                    registroAcordoRemuneracao.TipoTaxa = acordoRemuneracao.TipoTaxa == null ? registroAcordoRemuneracao.TipoTaxa : acordoRemuneracao.TipoTaxa;
                    registroAcordoRemuneracao.TipoRange = acordoRemuneracao.TipoRange == null ? registroAcordoRemuneracao.TipoRange : acordoRemuneracao.TipoRange;
                    registroAcordoRemuneracao.DataVigenciaInicio = acordoRemuneracao.DataVigenciaInicio == null ? registroAcordoRemuneracao.DataVigenciaInicio : acordoRemuneracao.DataVigenciaInicio;
                    registroAcordoRemuneracao.DataVigenciaFim = acordoRemuneracao.DataVigenciaFim == null ? registroAcordoRemuneracao.DataVigenciaFim : acordoRemuneracao.DataVigenciaFim;

                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException) when (!AcordoRemuneracaoExists(acordoRemuneracao.Id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/AcordoRemuneracao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAcordoRemuneracao(int id)
        {
            var tblAcordoRemuneracao = await _context.TblAcordoRemuneracao.FindAsync(id);

            if (tblAcordoRemuneracao == null)
            {
                return NotFound();
            }

            _context.TblAcordoRemuneracao.Remove(tblAcordoRemuneracao);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DESATIVA: api/AcordoRemuneracao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarAcordoRemuneracao(int id)
        {
            var registroAcordoRemuneracao = _context.TblAcordoRemuneracao.Find(id);

            if (registroAcordoRemuneracao != null)
            {
                registroAcordoRemuneracao.Ativo = false;

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private bool AcordoRemuneracaoExists(int id)
        {
            return _context.TblAcordoRemuneracao.Any(e => e.Id == id);
        }
    }
}
