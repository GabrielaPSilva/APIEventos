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
    public class ContratoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public ContratoController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        #region Contrato
        // GET: api/Contrato/Contrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContrato>>> Contrato()
        {
            try
            {
                List<TblContrato> contratos = await _context.TblContrato.Where(c => c.Ativo == true).OrderBy(c => c.CodDistribuidor).AsNoTracking().ToListAsync();

                if (contratos.Count() == 0)
                {
                    return NotFound();
                }

                if (contratos != null)
                {
                    return Ok(contratos);
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

        // GET: api/Contrato/GetContrato/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContrato>> GetContrato(int id)
        {
            TblContrato tblContrato = await _context.TblContrato.FindAsync(id);

            try
            {
                if (tblContrato != null)
                {
                    return Ok(tblContrato);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/CadastrarContrato/ContratoModel
        [HttpPost]
        public async Task<ActionResult<ContratoModel>> CadastrarContrato(ContratoModel tblContratoModel)
        {
            TblContrato itensContrato = new TblContrato
            {
                CodDistribuidor = tblContratoModel.CodDistribuidor,
                Parceiro = tblContratoModel.Parceiro,
                TipoContrato = tblContratoModel.TipoContrato,
                UsuarioModificacao = tblContratoModel.UsuarioModificacao
            };

            try
            {
                _context.TblContrato.Add(itensContrato);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetContrato),
                    new { id = itensContrato.Id },
                      Ok(itensContrato));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/EditarContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarContrato(int id, ContratoModel contrato)
        {
            try
            {
                TblContrato registroContrato = _context.TblContrato.Find(id);

                if (registroContrato != null)
                {
                    registroContrato.CodDistribuidor = contrato.CodDistribuidor == 0 ? registroContrato.CodDistribuidor : contrato.CodDistribuidor;
                    registroContrato.TipoContrato = contrato.TipoContrato == null ? registroContrato.TipoContrato : contrato.TipoContrato;
                    registroContrato.Parceiro = contrato.Parceiro == null ? registroContrato.Parceiro : contrato.Parceiro;
                    registroContrato.UsuarioModificacao = contrato.UsuarioModificacao == null ? registroContrato.UsuarioModificacao : contrato.UsuarioModificacao;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroContrato);
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
            catch (DbUpdateConcurrencyException e) when (!ContratoExists(contrato.Id))
            {
                return NotFound(e);
            }
        }

        // DELETE: api/Contrato/DeletarContrato/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarContrato(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_contrato");

            if (!existeRegistro)
            {
                TblContrato tblContrato = await _context.TblContrato.FindAsync(id);

                if (tblContrato == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblContrato.Remove(tblContrato);
                    await _context.SaveChangesAsync();
                    return Ok(tblContrato);
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

        // DESATIVA: api/Contrato/DesativarContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarContrato(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_contrato");

            if (!existeRegistro)
            {
                TblContrato registroContrato = _context.TblContrato.Find(id);

                if (registroContrato != null)
                {
                    registroContrato.Ativo = false;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroContrato);
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

        private bool ContratoExists(int id)
        {
            return _context.TblContrato.Any(e => e.Id == id);
        }
        #endregion

        #region Sub Contrato

        // GET: api/Contrato/SubContrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblSubContrato>>> SubContrato()
        {
            try
            {
                List<TblSubContrato> subContratos = await _context.TblSubContrato.Where(c => c.Status == "Ativo").OrderBy(c => c.CodContrato).AsNoTracking().ToListAsync();

                if (subContratos.Count() == 0)
                {
                    return NotFound();
                }

                if (subContratos != null)
                {
                    return Ok(subContratos);
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

        // GET: api/Contrato/GetSubContrato/
        [HttpGet("{id}")]
        public async Task<ActionResult<TblSubContrato>> GetSubContrato(int id)
        {
            TblSubContrato tblSubContrato = await _context.TblSubContrato.FindAsync(id);

            try
            {
                if (tblSubContrato != null)
                {
                    return Ok(tblSubContrato);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/CadastrarSubContrato/SubContratoModel
        [HttpPost]
        public async Task<ActionResult<SubContratoModel>> CadastrarSubContrato(SubContratoModel tblSubContratoModel)
        {
            TblSubContrato itensSubContrato = new TblSubContrato
            {
                CodContrato = tblSubContratoModel.CodContrato,
                Versao = tblSubContratoModel.Versao,
                Status = tblSubContratoModel.Status,
                IdDocusign = tblSubContratoModel.IdDocusign,
                ClausulaRetroatividade = tblSubContratoModel.ClausulaRetroatividade,
                DataRetroatividade = tblSubContratoModel.DataRetroatividade,
                DataAssinatura = tblSubContratoModel.DataAssinatura,
                DataVigenciaInicio = tblSubContratoModel.DataVigenciaInicio,
                DataVigenciaFim = tblSubContratoModel.DataVigenciaFim,
                UsuarioModificacao = tblSubContratoModel.UsuarioModificacao
            };

            try
            {
                _context.TblSubContrato.Add(itensSubContrato);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetSubContrato),
                    new { id = itensSubContrato.Id },
                     Ok(itensSubContrato));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/EditarSubContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarSubContrato(int id, SubContratoModel subContrato)
        {
            try
            {
                TblSubContrato registroSubContrato = _context.TblSubContrato.Find(id);

                if (registroSubContrato != null)
                {
                    registroSubContrato.CodContrato = subContrato.CodContrato == 0 ? registroSubContrato.CodContrato : subContrato.CodContrato;
                    registroSubContrato.Versao = subContrato.Versao == null ? registroSubContrato.Versao : subContrato.Versao;
                    registroSubContrato.Status = subContrato.Status == null ? registroSubContrato.Status : subContrato.Status;
                    registroSubContrato.IdDocusign = subContrato.IdDocusign == null ? registroSubContrato.IdDocusign : subContrato.IdDocusign;
                    registroSubContrato.ClausulaRetroatividade = subContrato.ClausulaRetroatividade == false ? registroSubContrato.ClausulaRetroatividade : subContrato.ClausulaRetroatividade;
                    registroSubContrato.DataRetroatividade = subContrato.DataRetroatividade == null ? registroSubContrato.DataRetroatividade : subContrato.DataRetroatividade;
                    registroSubContrato.DataAssinatura = subContrato.DataAssinatura == null ? registroSubContrato.DataAssinatura : subContrato.DataAssinatura;
                    registroSubContrato.DataVigenciaInicio = subContrato.DataVigenciaInicio == null ? registroSubContrato.DataVigenciaInicio : subContrato.DataVigenciaInicio;
                    registroSubContrato.DataVigenciaFim = subContrato.DataVigenciaFim == null ? registroSubContrato.DataVigenciaFim : subContrato.DataVigenciaFim;
                    registroSubContrato.UsuarioModificacao = subContrato.UsuarioModificacao == null ? registroSubContrato.UsuarioModificacao : subContrato.UsuarioModificacao;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroSubContrato);
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
            catch (DbUpdateConcurrencyException e) when (!SubContratoExists(subContrato.Id))
            {
                return BadRequest(e);
            }
        }


        // DELETE: api/Contrato/DeletarSubContrato/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarSubContrato(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_sub_contrato");

            if (!existeRegistro)
            {
                TblSubContrato tblSubContrato = await _context.TblSubContrato.FindAsync(id);

                if (tblSubContrato == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblSubContrato.Remove(tblSubContrato);
                    await _context.SaveChangesAsync();
                    return Ok(tblSubContrato);
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

        // DESATIVA: api/Contrato/DesativarSubContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarSubContrato(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_sub_contrato");

            if (!existeRegistro)
            {
                var registroSubContrato = _context.TblSubContrato.Find(id);

                if (registroSubContrato != null)
                {
                    registroSubContrato.Status = "Inativo";

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroSubContrato);
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

        private bool SubContratoExists(int id)
        {
            return _context.TblSubContrato.Any(e => e.Id == id);
        }

        #endregion

        #region Contrato Alocador
        // GET: api/ContratoAlocador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContratoAlocador>>> GetTblContratoAlocador()
        {
            return await _context.TblContratoAlocador.ToListAsync();
        }

        // GET: api/ContratoAlocador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContratoAlocador>> GetTblContratoAlocador(int id)
        {
            var tblContratoAlocador = await _context.TblContratoAlocador.FindAsync(id);

            if (tblContratoAlocador == null)
            {
                return NotFound();
            }

            return tblContratoAlocador;
        }

        // PUT: api/ContratoAlocador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblContratoAlocador(int id, TblContratoAlocador tblContratoAlocador)
        {
            if (id != tblContratoAlocador.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblContratoAlocador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblContratoAlocadorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ContratoAlocador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblContratoAlocador>> PostTblContratoAlocador(TblContratoAlocador tblContratoAlocador)
        {
            _context.TblContratoAlocador.Add(tblContratoAlocador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblContratoAlocador", new { id = tblContratoAlocador.Id }, tblContratoAlocador);
        }

        // DELETE: api/ContratoAlocador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblContratoAlocador(int id)
        {
            var tblContratoAlocador = await _context.TblContratoAlocador.FindAsync(id);
            if (tblContratoAlocador == null)
            {
                return NotFound();
            }

            _context.TblContratoAlocador.Remove(tblContratoAlocador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblContratoAlocadorExists(int id)
        {
            return _context.TblContratoAlocador.Any(e => e.Id == id);
        }
        #endregion

        #region Contrato Fundo
        // GET: api/ContratoFundo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContratoFundo>>> GetTblContratoFundo()
        {
            return await _context.TblContratoFundo.ToListAsync();
        }

        // GET: api/ContratoFundo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContratoFundo>> GetTblContratoFundo(int id)
        {
            var tblContratoFundo = await _context.TblContratoFundo.FindAsync(id);

            if (tblContratoFundo == null)
            {
                return NotFound();
            }

            return tblContratoFundo;
        }

        // PUT: api/ContratoFundo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblContratoFundo(int id, TblContratoFundo tblContratoFundo)
        {
            if (id != tblContratoFundo.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblContratoFundo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblContratoFundoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ContratoFundo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblContratoFundo>> PostTblContratoFundo(TblContratoFundo tblContratoFundo)
        {
            _context.TblContratoFundo.Add(tblContratoFundo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblContratoFundo", new { id = tblContratoFundo.Id }, tblContratoFundo);
        }

        // DELETE: api/ContratoFundo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblContratoFundo(int id)
        {
            var tblContratoFundo = await _context.TblContratoFundo.FindAsync(id);
            if (tblContratoFundo == null)
            {
                return NotFound();
            }

            _context.TblContratoFundo.Remove(tblContratoFundo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblContratoFundoExists(int id)
        {
            return _context.TblContratoFundo.Any(e => e.Id == id);
        }
        #endregion

        #region Contrato Remuneração
        // GET: api/ContratoRemuneracao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContratoRemuneracao>>> GetTblContratoRemuneracao()
        {
            return await _context.TblContratoRemuneracao.ToListAsync();
        }

        // GET: api/ContratoRemuneracao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContratoRemuneracao>> GetTblContratoRemuneracao(int id)
        {
            var tblContratoRemuneracao = await _context.TblContratoRemuneracao.FindAsync(id);

            if (tblContratoRemuneracao == null)
            {
                return NotFound();
            }

            return tblContratoRemuneracao;
        }

        // PUT: api/ContratoRemuneracao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblContratoRemuneracao(int id, TblContratoRemuneracao tblContratoRemuneracao)
        {
            if (id != tblContratoRemuneracao.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblContratoRemuneracao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblContratoRemuneracaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ContratoRemuneracao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblContratoRemuneracao>> PostTblContratoRemuneracao(TblContratoRemuneracao tblContratoRemuneracao)
        {
            _context.TblContratoRemuneracao.Add(tblContratoRemuneracao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblContratoRemuneracao", new { id = tblContratoRemuneracao.Id }, tblContratoRemuneracao);
        }

        // DELETE: api/ContratoRemuneracao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblContratoRemuneracao(int id)
        {
            var tblContratoRemuneracao = await _context.TblContratoRemuneracao.FindAsync(id);
            if (tblContratoRemuneracao == null)
            {
                return NotFound();
            }

            _context.TblContratoRemuneracao.Remove(tblContratoRemuneracao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblContratoRemuneracaoExists(int id)
        {
            return _context.TblContratoRemuneracao.Any(e => e.Id == id);
        }
        #endregion
    }
}
