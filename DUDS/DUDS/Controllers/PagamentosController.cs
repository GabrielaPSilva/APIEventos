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
    public class PagamentosController : ControllerBase
    {
        private readonly DataContext _context;

        public PagamentosController(DataContext context)
        {
            _context = context;
        }

        #region Pagamento Servico
        // GET: api/Pagamentos/PagamentoServico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPagamentoServico>>> PagamentoServico()
        {
            try
            {
                List<TblPagamentoServico> pgtosServico = await _context.TblPagamentoServico.AsNoTracking().ToListAsync();

                if (pgtosServico != null)
                {
                    return Ok(new { pgtosServico, Mensagem.SucessoListar });
                }
                else
                {
                    return NotFound(new { Erro = true, Mensagem.ErroTipoInvalido });
                }
            }
            catch (InvalidOperationException e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
            }
        }

        // GET: api/Pagamentos/GetPagamentoServico/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblPagamentoServico>> GetPagamentoServico(string id)
        {
            TblPagamentoServico tblPagamentoServico = await _context.TblPagamentoServico.FindAsync(id);

            try
            {
                if (tblPagamentoServico != null)
                {
                    return Ok(new { tblPagamentoServico, Mensagem.SucessoCadastrado });
                }
                else
                {
                    return NotFound(new { Erro = true, Mensagem.ErroTipoInvalido });
                }
            }
            catch (Exception e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
            }
        }

        //POST: api/Pagamentos/CadastrarPagamentoServico/PagamentoServicoModel
        [HttpPost]
        public async Task<ActionResult<PagamentoServicoModel>> CadastrarPagamentoServico(PagamentoServicoModel tblPagamentoServicoModel)
        {
            TblPagamentoServico itensPagamentoServico = new TblPagamentoServico
            {
                Competencia = tblPagamentoServicoModel.Competencia,
                CodFundo = tblPagamentoServicoModel.CodFundo,
                TaxaAdm = tblPagamentoServicoModel.TaxaAdm,
                AdmFiduciaria = tblPagamentoServicoModel.AdmFiduciaria,
                Servico = tblPagamentoServicoModel.Servico,
                SaldoParcial = tblPagamentoServicoModel.SaldoParcial,
                SaldoGestor = tblPagamentoServicoModel.SaldoGestor
            };

            try
            {
                _context.TblPagamentoServico.Add(itensPagamentoServico);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetPagamentoServico),
                    Ok(new { itensPagamentoServico, Mensagem.SucessoCadastrado }));
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = true, Mensagem.ErroCadastrar });
            }
        }

        #endregion

        #region Pagamento Admin Pfee
        // GET: api/Pagamentos/PgtoAdmPfee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPgtoAdmPfee>>> PgtoAdmPfee()
        {
            try
            {
                List<TblPgtoAdmPfee> pgtosAdmPfee = await _context.TblPgtoAdmPfee.AsNoTracking().ToListAsync();
                
                if (pgtosAdmPfee != null)
                {
                    return Ok(new { pgtosAdmPfee, Mensagem.SucessoListar });
                }
                else
                {
                    return NotFound(new { Erro = true, Mensagem.ErroTipoInvalido });
                }
            }
            catch (InvalidOperationException e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
            }
        }

        // GET: api/Pagamentos/GetPgtoAdmPfee/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblPgtoAdmPfee>> GetPgtoAdmPfee(string id)
        {
            TblPgtoAdmPfee tblPgtoAdmPfee = await _context.TblPgtoAdmPfee.FindAsync(id);
            
            try
            {
                if (tblPgtoAdmPfee != null)
                {
                    return Ok(new { tblPgtoAdmPfee, Mensagem.SucessoCadastrado });
                }
                else
                {
                    return NotFound(new { Erro = true, Mensagem.ErroTipoInvalido });
                }
            }
            catch (Exception e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
            }
        }

        //POST: api/Pagamentos/CadastrarPagamentoAdminPfee/PagamentoAdminPfeeModel
        [HttpPost]
        public async Task<ActionResult<PagamentoAdminPfeeModel>> CadastrarPagamentoAdminPfee(PagamentoAdminPfeeModel tblPagamentoAdminPfeeModel)
        {
            TblPgtoAdmPfee itensPagamentoAdminPfee = new TblPgtoAdmPfee
            {
                Competencia = tblPagamentoAdminPfeeModel.Competencia,
                CodCliente = tblPagamentoAdminPfeeModel.CodCliente,
                CodFundo = tblPagamentoAdminPfeeModel.CodFundo,
                TaxaPerformanceApropriada = tblPagamentoAdminPfeeModel.TaxaPerformanceApropriada,
                TaxaPerformanceResgate = tblPagamentoAdminPfeeModel.TaxaPerformanceResgate,
                TaxaAdministracao = tblPagamentoAdminPfeeModel.TaxaAdministracao,
                TaxaGestao = tblPagamentoAdminPfeeModel.TaxaGestao
            };

            try
            {
                _context.TblPgtoAdmPfee.Add(itensPagamentoAdminPfee);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetPgtoAdmPfee),
                   Ok(new { itensPagamentoAdminPfee, Mensagem.SucessoCadastrado }));
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = true, Mensagem.ErroCadastrar });
            }
        }

        #endregion
    }
}
