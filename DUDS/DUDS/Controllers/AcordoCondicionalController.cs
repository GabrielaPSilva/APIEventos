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

namespace DUDS.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class AcordoCondicionalController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public AcordoCondicionalController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        // GET: api/AcordoCondicional/AcordoCondicional
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAcordoCondicional>>> AcordoCondicional()
        {
            try
            {
                List<TblAcordoCondicional> acordosCondicional = await _context.TblAcordoCondicional.Where(c => c.Ativo == true).OrderBy(c => c.CodAcordoRemuneracao).AsNoTracking().ToListAsync();

                if (acordosCondicional.Count() == 0)
                {
                    return NotFound();
                }

                if (acordosCondicional != null)
                {
                    return Ok(acordosCondicional);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e);
            }
        }

        // GET: api/AcordoCondicional/GetAcordoCondicional/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAcordoCondicional>> GetAcordoCondicional(int id)
        {
            TblAcordoCondicional tblAcordoCondicional = await _context.TblAcordoCondicional.FindAsync(id);

            try
            {
                if (tblAcordoCondicional != null)
                {
                    return Ok(tblAcordoCondicional);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }

        // POST: api/AcordoCondicional/CadastrarAcordoCondicional/AcordoCondicionalModel
        [HttpPost]
        public async Task<ActionResult<AcordoCondicionalModel>> CadastrarAcordoCondicional(AcordoCondicionalModel tblAcordoCondicionalModel)
        {
            TblAcordoCondicional itensAcordoCondicional = new TblAcordoCondicional
            {
                CodAcordoRemuneracao = tblAcordoCondicionalModel.CodAcordoRemuneracao,
                CodTipoCondicao = tblAcordoCondicionalModel.CodTipoCondicao,
                PercentualAdm = tblAcordoCondicionalModel.PercentualAdm,
                PercentualPfee = tblAcordoCondicionalModel.PercentualPfee,
                UsuarioModificacao = tblAcordoCondicionalModel.UsuarioModificacao
            };

            try
            {
                _context.TblAcordoCondicional.Add(itensAcordoCondicional);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                   nameof(GetAcordoCondicional),
                   new { id = itensAcordoCondicional.Id },
                   Ok(itensAcordoCondicional));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/AcordoCondicional/EditarAcordoCondicional/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarAcordoCondicional(int id, AcordoCondicionalModel acordoCondicional)
        {
            try
            {
                TblAcordoCondicional registroAcordoCondicional = _context.TblAcordoCondicional.Find(id);

                if (registroAcordoCondicional != null)
                {
                    registroAcordoCondicional.CodAcordoRemuneracao = acordoCondicional.CodAcordoRemuneracao == 0 ? registroAcordoCondicional.CodAcordoRemuneracao : acordoCondicional.CodAcordoRemuneracao;
                    registroAcordoCondicional.CodTipoCondicao = acordoCondicional.CodTipoCondicao == 0 ? registroAcordoCondicional.CodTipoCondicao : acordoCondicional.CodTipoCondicao;
                    registroAcordoCondicional.PercentualAdm = acordoCondicional.PercentualAdm == 0 ? registroAcordoCondicional.PercentualAdm : acordoCondicional.PercentualAdm;
                    registroAcordoCondicional.PercentualPfee = acordoCondicional.PercentualPfee == 0 ? registroAcordoCondicional.PercentualPfee : acordoCondicional.PercentualPfee;
                    registroAcordoCondicional.UsuarioModificacao = acordoCondicional.UsuarioModificacao == null ? registroAcordoCondicional.UsuarioModificacao : acordoCondicional.UsuarioModificacao;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroAcordoCondicional);
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
            catch (DbUpdateConcurrencyException e) when (!AcordoCondicionalExists(acordoCondicional.Id))
            {
                return NotFound(e);
            }
        }

        // DELETE: api/AcordoCondicional/DeletarAcordoCondicional/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAcordoCondicional(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_acordo_condicional");

            if (!existeRegistro)
            {
                TblAcordoCondicional tblAcordoCondicional = _context.TblAcordoCondicional.Find(id);

                if (tblAcordoCondicional == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblAcordoCondicional.Remove(tblAcordoCondicional);
                    await _context.SaveChangesAsync();
                    return Ok(tblAcordoCondicional);
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

        // DESATIVA: api/AcordoCondicional/DesativarAcordoCondicional/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarAcordoCondicional(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_acordo_condicional");

            if (!existeRegistro)
            {
                TblAcordoCondicional registroAcordoCondicional = _context.TblAcordoCondicional.Find(id);

                if (registroAcordoCondicional != null)
                {
                    registroAcordoCondicional.Ativo = false;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroAcordoCondicional);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private bool AcordoCondicionalExists(int id)
        {
            return _context.TblAcordoCondicional.Any(e => e.Id == id);
        }
    }
}
