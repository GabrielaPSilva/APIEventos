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
    public class AcordoRemuneracaoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public AcordoRemuneracaoController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        // GET: api/AcordoRemuneracao/AcordoRemuneracao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAcordoRemuneracao>>> AcordoRemuneracao()
        {
            try
            {
                List<TblAcordoRemuneracao> acordosRemuneracao = await _context.TblAcordoRemuneracao.Where(c => c.Ativo == true).AsNoTracking().ToListAsync();

                if (acordosRemuneracao.Count() == 0)
                {
                    return NotFound();
                }

                if (acordosRemuneracao != null)
                {
                    return Ok(acordosRemuneracao);
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

        // GET: api/AcordoRemuneracao/GetAcordoRemuneracao/cod_contrato_distribuicao/percentual/tipo_taxa/tipo_range
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAcordoRemuneracao>> GetAcordoRemuneracao(int id)
        {
            TblAcordoRemuneracao tblAcordoRemuneracao = await _context.TblAcordoRemuneracao.FindAsync(id);

            try
            {
                if (tblAcordoRemuneracao != null)
                {
                    return Ok(tblAcordoRemuneracao);
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

        // POST: api/AcordoRemuneracao/CadastrarAcordoRemuneracao/AcordoRemuneracaoModel
        [HttpPost]
        public async Task<ActionResult<AcordoRemuneracaoModel>> CadastrarAcordoRemuneracao(AcordoRemuneracaoModel tblAcordoRemuneracaoModel)
        {
            TblAcordoRemuneracao itensAcordoRemuneracao = new TblAcordoRemuneracao
            {
                CodContratoDistribuicao = tblAcordoRemuneracaoModel.CodContratoDistribuicao,
                TipoRange = tblAcordoRemuneracaoModel.TipoRange,
                DataVigenciaInicio = tblAcordoRemuneracaoModel.DataVigenciaInicio,
                DataVigenciaFim = tblAcordoRemuneracaoModel.DataVigenciaFim,
                UsuarioModificacao = tblAcordoRemuneracaoModel.UsuarioModificacao
            };

            try
            {
                _context.TblAcordoRemuneracao.Add(itensAcordoRemuneracao);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                   nameof(GetAcordoRemuneracao),
                   new { id = itensAcordoRemuneracao.Id },
                   Ok(itensAcordoRemuneracao));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/AcordoRemuneracao/DeletarAcordoRemuneracao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAcordoRemuneracao(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_acordo_remuneracao");

            if (!existeRegistro)
            {
                TblAcordoRemuneracao tblAcordoRemuneracao = _context.TblAcordoRemuneracao.Where(c => c.Id == id).FirstOrDefault();

                if (tblAcordoRemuneracao == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblAcordoRemuneracao.Remove(tblAcordoRemuneracao);
                    await _context.SaveChangesAsync();
                    return Ok(tblAcordoRemuneracao);
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

        // DESATIVA: api/AcordoRemuneracao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarAcordoRemuneracao(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_acordo_remuneracao");

            if (!existeRegistro)
            {
                TblAcordoRemuneracao registroAcordoRemuneracao = _context.TblAcordoRemuneracao.Where(c => c.Id == id).FirstOrDefault();

                if (registroAcordoRemuneracao != null)
                {
                    registroAcordoRemuneracao.Ativo = false;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroAcordoRemuneracao);
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

        private bool AcordoRemuneracaoExists(int id)
        {
            return _context.TblAcordoRemuneracao.Any(e => e.Id == id);
        }
    }
}
