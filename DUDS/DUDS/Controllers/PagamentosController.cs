using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;
using EFCore.BulkExtensions;

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

        // GET: api/Pagamentos/GetPagamentoServico/cod_fundo
        [HttpGet("{cod_fundo}")]
        public async Task<ActionResult<TblPagamentoServico>> GetPagamentoServico(int cod_fundo)
        {
            TblPagamentoServico tblPagamentoServico = await _context.TblPagamentoServico.FindAsync(cod_fundo);

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

        //POST: api/Pagamentos/CadastrarPagamentoServico/List<PagamentoServicoModel>
        [HttpPost]
        public async Task<ActionResult<PagamentoServicoModel>> CadastrarPagamentoServico(List<PagamentoServicoModel> tblPagamentoServicoModel)
        {
            List<TblPagamentoServico> listaPagamentosServico = new List<TblPagamentoServico>();
            TblPagamentoServico itensPagamentoServico = new TblPagamentoServico();

            try
            {
                foreach (var line in tblPagamentoServicoModel)
                {
                    itensPagamentoServico = new TblPagamentoServico
                    {
                        Competencia = line.Competencia,
                        CodFundo = line.CodFundo,
                        TaxaAdm = line.TaxaAdm,
                        AdmFiduciaria = line.AdmFiduciaria,
                        Servico = line.Servico,
                        SaldoParcial = line.SaldoParcial,
                        SaldoGestor = line.SaldoGestor
                    };

                    listaPagamentosServico.Add(itensPagamentoServico);
                }

                await _context.BulkInsertAsync(listaPagamentosServico);
                await _context.SaveChangesAsync();

                return Ok(new { itensPagamentoServico, Mensagem.SucessoCadastrado });
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = true, Mensagem.ErroCadastrar });
            }
        }

        // DELETE: api/Pagamentos/DeletarPagamentoServico/competencia
        [HttpDelete("{competencia}")]
        public async Task<ActionResult<IEnumerable<TblPagamentoServico>>> DeletarPagamentoServico(string competencia)
        {
            IList<TblPagamentoServico> tblPagamentoServico = await _context.TblPagamentoServico.Where(c => c.Competencia == competencia).ToListAsync();

            if (tblPagamentoServico == null)
            {
                return NotFound(Mensagem.ErroTipoInvalido);
            }

            try
            {
                _context.TblPagamentoServico.RemoveRange(tblPagamentoServico);
                await _context.SaveChangesAsync();
                return Ok(new { Mensagem.SucessoExcluido });
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = true, Mensagem.ErroExcluir });
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

        // GET: api/Pagamentos/GetPgtoAdmPfee/cod_fundo
        [HttpGet("{cod_fundo}")]
        public async Task<ActionResult<TblPgtoAdmPfee>> GetPgtoAdmPfee(int cod_fundo)
        {
            TblPgtoAdmPfee tblPgtoAdmPfee = await _context.TblPgtoAdmPfee.FindAsync(cod_fundo);
            
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

        //POST: api/Pagamentos/CadastrarPagamentoAdminPfee/List<PagamentoAdminPfeeModel>
        [HttpPost]
        public async Task<ActionResult<PagamentoAdminPfeeModel>> CadastrarPagamentoAdminPfee(List<PagamentoAdminPfeeModel> tblPagamentoAdminPfeeModel)
        {
            List<TblPgtoAdmPfee> listaPagamentosAdminPfee = new List<TblPgtoAdmPfee>();
            TblPgtoAdmPfee itensPagamentoAdminPfee = new TblPgtoAdmPfee();

            try
            {
                foreach (var line in tblPagamentoAdminPfeeModel)
                {
                    itensPagamentoAdminPfee = new TblPgtoAdmPfee
                    {
                        Competencia = line.Competencia,
                        CodCliente = line.CodCliente,
                        CodFundo = line.CodFundo,
                        TaxaPerformanceApropriada = line.TaxaPerformanceApropriada,
                        TaxaPerformanceResgate = line.TaxaPerformanceResgate,
                        TaxaAdministracao = line.TaxaAdministracao,
                        TaxaGestao = line.TaxaGestao
                    };

                    listaPagamentosAdminPfee.Add(itensPagamentoAdminPfee);
                }

                await _context.BulkInsertAsync(listaPagamentosAdminPfee);
                await _context.SaveChangesAsync();

                return Ok(new { itensPagamentoAdminPfee, Mensagem.SucessoCadastrado });
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = true, Mensagem.ErroCadastrar });
            }
        }

        // DELETE: api/Pagamentos/DeletarPagamentoAdminPfee/competencia
        [HttpDelete("{competencia}")]
        public async Task<ActionResult<IEnumerable<TblPgtoAdmPfee>>> DeletarPagamentoAdminPfee(string competencia)
        {
            IList<TblPgtoAdmPfee> tblPagamentoAdminPfee = await _context.TblPgtoAdmPfee.Where(c => c.Competencia == competencia).ToListAsync();

            if (tblPagamentoAdminPfee == null)
            {
                return NotFound(Mensagem.ErroTipoInvalido);
            }

            try
            {
                _context.TblPgtoAdmPfee.RemoveRange(tblPagamentoAdminPfee);
                await _context.SaveChangesAsync();
                return Ok(new { Mensagem.SucessoExcluido });
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = true, Mensagem.ErroExcluir });
            }
        }
        #endregion
    }
}
