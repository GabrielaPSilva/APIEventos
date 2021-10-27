using DUDS.Models;
using DUDS.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    public class RebateController : ControllerBase
    {
        private readonly IErrosPagamentoService _errosPagamento;
        private readonly IConfiguracaoService _configService;
        private readonly IPagamentoServicoService _pagamentoServicoService;
        private readonly IPagamentoTaxaAdministracaoPerformanceService _pagamentoTaxaAdministracaoPerformanceService;
        private readonly ICalculoRebateService _calculoRebateService;
        private readonly IGrupoRebateService _grupoRebateService;
        private readonly IEmailGrupoRebateService _emailGrupoRebateService;
        private readonly IControleRebateService _controleRebateService;

        public RebateController(IConfiguracaoService configService,
            IErrosPagamentoService errosPagamento,
            IPagamentoServicoService pagamentoServicoService,
            IPagamentoTaxaAdministracaoPerformanceService pagamentoTaxaAdministracaoPerformanceService,
            ICalculoRebateService calculoRebateService,
            IGrupoRebateService grupoRebateService,
            IControleRebateService controleRebateService,
            IEmailGrupoRebateService emailGrupoRebateService)
        {
            _errosPagamento = errosPagamento;
            _configService = configService;
            _pagamentoServicoService = pagamentoServicoService;
            _pagamentoTaxaAdministracaoPerformanceService = pagamentoTaxaAdministracaoPerformanceService;
            _calculoRebateService = calculoRebateService;
            _grupoRebateService = grupoRebateService;
            _controleRebateService = controleRebateService;
            _emailGrupoRebateService = emailGrupoRebateService;
        }

        #region Controle Rebate

        // GET: api/Rebate/GetControleRebate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ControleRebateModel>>> GetControleRebate()
        {
            try
            {
                var listaControleRebate = await _controleRebateService.GetAllAsync();

                if (listaControleRebate.Any())
                {
                    return Ok(listaControleRebate);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Rebate/GetControleRebateById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ControleRebateModel>> GetControleRebateById(int id)
        {
            try
            {
                var controleRebate = await _controleRebateService.GetByIdAsync(id);

                if (controleRebate == null)
                {
                    return NotFound();
                }
                return Ok(controleRebate);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Rebate/GetControleRebateExistsBase/codGrupoRebate/Competencia
        [HttpGet("{codGrupoRebate}, {Competencia}")]
        public async Task<ActionResult<ControleRebateModel>> GetControleRebateExistsBase(int codGrupoRebate, string competencia)
        {
            try
            {
                var controleRebate = await _controleRebateService.GetGrupoRebateExistsBase(codGrupoRebate, competencia);

                if (controleRebate == null)
                {
                    NotFound();
                }
                return Ok(controleRebate);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Rebate/AddGestor/ControleRebateModel
        [HttpPost]
        public async Task<ActionResult<ControleRebateModel>> AddControleRebate(ControleRebateModel controleRebate)
        {

            try
            {
                bool retorno = await _controleRebateService.AddAsync(controleRebate);

                return CreatedAtAction(nameof(GetControleRebateById), new { id = controleRebate.Id }, controleRebate);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Rebate/UpdateControleRebate/id
        [HttpPut("{id}")]
        public async Task<ActionResult<ControleRebateModel>> UpdateControleRebate(int id, ControleRebateModel controleRebate)
        {
            try
            {
                ControleRebateModel retornoControleRebate = await _controleRebateService.GetByIdAsync(id);

                if (controleRebate == null)
                {
                    return NotFound();
                }

                controleRebate.Id = id;
                bool retorno = await _controleRebateService.UpdateAsync(controleRebate);

                if (retorno)
                {
                    return Ok(controleRebate);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Rebate/DeleteControleRebate/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteControleRebate(int id)
        {
            try
            {
                bool retorno = await _controleRebateService.DisableAsync(id);

                if (retorno)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // ATIVAR: api/Rebate/ActivateControleRebate/id
        [HttpPut("{id}")]
        public async Task<ActionResult<ControleRebateModel>> ActivateControleRebate(int id)
        {
            try
            {
                bool retorno = await _controleRebateService.ActivateAsync(id);

                if (retorno)
                {
                    ControleRebateModel controleRebate = await _controleRebateService.GetByIdAsync(id);
                    return Ok(controleRebate);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion

        #region Erros Pagamento Rebate
        // GET: api/ErrosPagamento/ErrosPagamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrosPagamentoModel>>> GetErrosPagamento()
        {
            try
            {
                var errosPagamento = await _errosPagamento.GetAllAsync();

                if (errosPagamento.Any())
                {
                    return Ok(errosPagamento);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/ErrosPagamento/ErrosPagamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrosPagamentoModel>>> GetErrosPagamentoByCompetencia(string competencia)
        {
            try
            {
                var errosPagamentos = await _errosPagamento.GetErrosPagamentoByCompetencia(competencia);
                if (errosPagamentos.Any())
                {
                    return Ok(errosPagamentos);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        // GET: api/ErrosPagamento/GetErrosPagamento/cod_fundo/data_agendamento
        [HttpGet("{id}")]
        public async Task<ActionResult<ErrosPagamentoModel>> GetErrosPagamentoById(int id)
        {
            try
            {
                var errosPagamento = await _errosPagamento.GetByIdAsync(id);
                if (errosPagamento == null)
                {
                    return NotFound();
                }
                return Ok(errosPagamento);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        //POST: api/ErrosPagamento/CadastrarPagamentoServico/List<ErrosPagamentoModel>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ErrosPagamentoModel>>> AddErrosPagamento(List<ErrosPagamentoModel> errosPagamento)
        {
            try
            {
                bool retorno = await _errosPagamento.AddErrosPagamento(errosPagamento);
                if (retorno)
                {
                    return CreatedAtAction(nameof(GetErrosPagamentoByCompetencia),
                        new { competencia = errosPagamento.FirstOrDefault().Competencia, data_agendamento = errosPagamento.FirstOrDefault().DataAgendamento }, errosPagamento);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/ErrosPagamento/DeletarErrosPagamento/data_agendamento
        [HttpDelete("{data_agendamento}")]
        public async Task<IActionResult> DeletarErrosPagamento(DateTime data_agendamento)
        {
            try
            {
                bool retorno = await _errosPagamento.DeleteErrosPagamentoByDataAgendamento(data_agendamento);
                if (retorno)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/ErrosPagamento/DeletarErrosPagamento/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarErrosPagamentoById(int id)
        {
            try
            {
                bool retorno = await _errosPagamento.DeleteAsync(id);
                if (retorno)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        #endregion

        #region Pagamento Servico
        // GET: api/Pagamentos/GetPagamentoServico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagamentoServicoModel>>> GetPagamentoServico()
        {
            try
            {
                var pagamentoServicos = await _pagamentoServicoService.GetAllAsync();

                if (pagamentoServicos.Any())
                {
                    return Ok(pagamentoServicos);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PagamentoServicoModel>> GetPagamentoServicoById(int id)
        {
            try
            {
                var pagamentoServico = await _pagamentoServicoService.GetByIdAsync(id);
                if (pagamentoServico == null)
                {
                    return NotFound();
                }
                return Ok(pagamentoServico);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Pagamentos/GetPagamentoServicoByIds/competencia/cod_fundo
        [HttpGet("{competencia}/{cod_fundo}")]
        public async Task<ActionResult<PagamentoServicoModel>> GetPagamentoServicoByIds(string competencia, int cod_fundo)
        {
            try
            {
                var pagamentoServicos = await _pagamentoServicoService.GetByIdsAsync(competencia, cod_fundo);

                if (pagamentoServicos.Any())
                {
                    return Ok(pagamentoServicos);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Pagamentos/AddPagamentoServico/List<PagamentoServicoModel>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<PagamentoServicoModel>>> AddPagamentoServico(List<PagamentoServicoModel> pagamentoServico)
        {
            try
            {
                bool retorno = await _pagamentoServicoService.AddPagamentoServico(pagamentoServico);
                if (retorno)
                {
                    return CreatedAtAction(nameof(GetErrosPagamentoByCompetencia),
                        new { competencia = pagamentoServico.FirstOrDefault().Competencia }, pagamentoServico);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Pagamentos/DeletePagamentoServico/competencia
        [HttpDelete("{competencia}")]
        public async Task<IActionResult> DeletePagamentoServico(string competencia)
        {
            try
            {
                bool retorno = await _pagamentoServicoService.DeleteByCompetenciaAsync(competencia);
                if (retorno)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPagamentoServicoById(int id)
        {
            try
            {
                bool retorno = await _pagamentoServicoService.DeleteAsync(id);
                if (retorno)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        #endregion

        #region Pagamento Taxa Admin Pfee
        // GET: api/Pagamentos/GetPgtoTaxaAdmPfee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagamentoTaxaAdminPfeeModel>>> GetPgtoTaxaAdmPfee()
        {
            try
            {
                var pagamentoTaxaAdministracaoPerformance = await _pagamentoTaxaAdministracaoPerformanceService.GetAllAsync();

                if (pagamentoTaxaAdministracaoPerformance.Any())
                {
                    return Ok(pagamentoTaxaAdministracaoPerformance);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<PagamentoTaxaAdminPfeeModel>> GetPgtoTaxaAdmPfeeById(Guid id)
        {
            try
            {
                var pagamentoTaxaAdministracaoPerformance = await _pagamentoTaxaAdministracaoPerformanceService.GetByIdAsync(id);
                if (pagamentoTaxaAdministracaoPerformance == null)
                {
                    return NotFound();
                }
                return Ok(pagamentoTaxaAdministracaoPerformance);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Pagamentos/GetPgtoTaxaAdmPfeeByIds/competencia/cod_investidor_distribuidor/cod_administrador/cod_fundo
        [HttpGet("{competencia}/{cod_fundo}/{cod_administrador}/{cod_investidor_distribuidor}")]
        public async Task<ActionResult<IEnumerable<PagamentoTaxaAdminPfeeModel>>> GetPgtoTaxaAdmPfeeByIds(string competencia, int cod_fundo, int cod_administrador, int cod_investidor_distribuidor)
        {
            try
            {
                var pagamentoTaxaAdministracaoPerformance = await _pagamentoTaxaAdministracaoPerformanceService.GetByIdsAsync(competencia, cod_fundo, cod_administrador, cod_investidor_distribuidor);
                if (pagamentoTaxaAdministracaoPerformance.Any())
                {
                    return Ok(pagamentoTaxaAdministracaoPerformance);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Pagamentos/AddPgtoTaxaAdminPfee/List<PagamentoTaxaAdminPfeeModel>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<PagamentoTaxaAdminPfeeModel>>> AddPgtoTaxaAdminPfee(List<PagamentoTaxaAdminPfeeModel> pagamentoAdministracaoPerformance)
        {
            try
            {
                bool retorno = await _pagamentoTaxaAdministracaoPerformanceService.AddBulkAsync(pagamentoAdministracaoPerformance);
                if (retorno)
                {
                    return CreatedAtAction(nameof(GetErrosPagamentoByCompetencia),
                        new { competencia = pagamentoAdministracaoPerformance.FirstOrDefault().Competencia }, pagamentoAdministracaoPerformance);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Pagamentos/DeletePagamentoTaxaAdminPfee/competencia
        [HttpDelete("{competencia}")]
        public async Task<IActionResult> DeletePagamentoTaxaAdminPfee(string competencia)
        {
            try
            {
                bool retorno = await _pagamentoTaxaAdministracaoPerformanceService.DeleteByCompetenciaAsync(competencia);
                if (retorno)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagamentoTaxaAdminPfeeById(int id)
        {
            try
            {
                bool retorno = await _pagamentoTaxaAdministracaoPerformanceService.DeleteAsync(id);
                if (retorno)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion

        #region Pagamento Taxa Admin Pfee Com Dados do Investidor
        // GET: api/Pagamentos/GetPgtoTaxaAdmPfeeInvestidor
        [HttpGet("{competencia}")]
        public async Task<ActionResult<IEnumerable<PagamentoAdmPfeeInvestidorModel>>> GetPgtoTaxaAdmPfeeInvestidor(string competencia)
        {
            try
            {
                var pagamentoTaxaAdministracaoPerformance = await _pagamentoTaxaAdministracaoPerformanceService.GetPgtoAdmPfeeInvestByCompetenciaAsync(competencia);
                if (pagamentoTaxaAdministracaoPerformance.Any())
                {
                    return Ok(pagamentoTaxaAdministracaoPerformance);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        #endregion

        #region Calculo Rebate
        // GET: api/Pagamentos/GetCalculoPgtoTaxaAdminPfee
        [HttpGet("{competencia}")]
        public async Task<ActionResult<IEnumerable<CalculoRebateModel>>> GetCalculoRebate(string competencia)
        {
            try
            {
                var calculoRebate = await _calculoRebateService.GetByCompetenciaAsync(competencia);
                if (calculoRebate.Any())
                {
                    return Ok(calculoRebate);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<CalculoRebateModel>> GetCalculoRebateById(Guid id)
        {
            try
            {
                var calculoRebate = await _calculoRebateService.GetByIdAsync(id);
                if (calculoRebate == null)
                {
                    return NotFound();
                }
                return Ok(calculoRebate);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Pagamentos/AddCalculoPgtoTaxaAdminPfee/List<CalculoPgtoTaxaAdmPfeeModel>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CalculoRebateModel>>> AddCalculoRebate(List<CalculoRebateModel> calculoRebate)
        {
            try
            {
                bool retorno = await _calculoRebateService.AddBulkAsync(calculoRebate);
                if (retorno)
                {
                    return CreatedAtAction(nameof(GetCalculoRebate),
                        new { competencia = calculoRebate.FirstOrDefault().Competencia }, calculoRebate);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Pagamentos/DeleteCalculoPgtoTaxaAdminPfee/competencia
        [HttpDelete("{competencia}")]
        public async Task<IActionResult> DeleteCalculoRebate(string competencia)
        {
            try
            {
                bool retorno = await _calculoRebateService.DeleteByCompetenciaAsync(competencia);
                if (retorno)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Contrato/GetDescricaoCalculoPgtoTaxaAdmPfee
        [HttpGet("{cod_contrato}/{cod_sub_contrato}/{cod_contrato_fundo}/{cod_contrato_remuneracao}")]
        public async Task<ActionResult<IEnumerable<DescricaoCalculoRebateModel>>> GetDescricaoCalculoPgtoTaxaAdmPfee(int codContrato, int codSubContrato, int codContratoFundo, int codContratoRemuneracao, [FromQuery] string codCondicaoRemuneracao)
        {
            try
            {
                var calculoRebate = await _calculoRebateService.GetDescricaoRebateAsync(codContrato, codSubContrato, codContratoFundo, codContratoRemuneracao, codCondicaoRemuneracao);
                if (calculoRebate.Any())
                {
                    return Ok(calculoRebate);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion

        #region Grupo Rebate
        // GET: api/GrupoRebate/GetTblGrupoRebate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrupoRebateModel>>> GetGrupoRebate()
        {
            try
            {
                var grupoRebates = await _grupoRebateService.GetAllAsync();

                if (grupoRebates.Any())
                {
                    return Ok(grupoRebates);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/GrupoRebate/GetTblGrupoRebateById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<GrupoRebateModel>> GetGrupoRebateById(int id)
        {
            try
            {
                var grupoRebate = await _grupoRebateService.GetByIdAsync(id);
                if (grupoRebate == null)
                {
                    return NotFound();
                }
                return Ok(grupoRebate);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/GrupoRebate/AddGrupoRebate/GrupoRebateModel
        [HttpPost]
        public async Task<ActionResult<GrupoRebateModel>> AddGrupoRebate(GrupoRebateModel grupoRebate)
        {
            try
            {
                bool retorno = await _grupoRebateService.AddAsync(grupoRebate);
                if (retorno)
                {
                    return CreatedAtAction(nameof(GetGrupoRebateById),
                        new { id = grupoRebate.Id }, grupoRebate);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/GrupoRebate/UpdateGrupoRebate/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrupoRebate(int id, GrupoRebateModel grupoRebate)
        {
            try
            {
                var retornoGrupoRebate = await _grupoRebateService.GetByIdAsync(id);

                if (retornoGrupoRebate == null)
                {
                    return NotFound();
                }

                grupoRebate.Id = id;
                bool retorno = await _grupoRebateService.UpdateAsync(grupoRebate);

                if (retorno)
                {
                    return Ok(grupoRebate);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/GrupoRebate/DeleteGrupoRebate/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrupoRebate(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_grupo_rebate");

            if (!existeRegistro)
            {
                try
                {
                    GrupoRebateModel retornoGrupoRebate = await _grupoRebateService.GetByIdAsync(id);

                    if (retornoGrupoRebate != null)
                    {
                        bool retorno = await _grupoRebateService.DeleteAsync(id);
                        if (retorno)
                        {
                            return Ok();
                        }
                    }
                    return NotFound();
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

        // ATIVAR: api/GrupoRebate/ActivateGrupoRebate/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateGrupoRebate(int id)
        {
            try
            {
                GrupoRebateModel retornoGrupoRebate = await _grupoRebateService.GetByIdAsync(id);
                if (retornoGrupoRebate != null)
                {
                    bool retorno = await _grupoRebateService.ActivateAsync(id);
                    if (retorno)
                    {
                        return Ok();
                    }
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion

        #region Email Grupo Rebate
        // GET: api/GrupoRebate/GetEmailGrupoRebate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailGrupoRebateModel>>> GetEmailGrupoRebate()
        {
            try
            {
                var emailGrupoRebates = await _emailGrupoRebateService.GetAllAsync();

                if (emailGrupoRebates.Any())
                {
                    return Ok(emailGrupoRebates);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/GrupoRebate/GetTblGrupoRebateById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<EmailGrupoRebateModel>> GetEmailGrupoRebateById(int id)
        {
            try
            {
                var emailGrupoRebate = await _emailGrupoRebateService.GetByIdAsync(id);
                if (emailGrupoRebate == null)
                {
                    return NotFound();
                }
                return Ok(emailGrupoRebate);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/GrupoRebate/AddGrupoRebate/GrupoRebateModel
        [HttpPost]
        public async Task<ActionResult<EmailGrupoRebateModel>> AddEmailGrupoRebate(EmailGrupoRebateModel emailGrupoRebate)
        {
            try
            {
                bool retorno = await _emailGrupoRebateService.AddAsync(emailGrupoRebate);
                if (retorno)
                {
                    return CreatedAtAction(nameof(GetEmailGrupoRebateById),
                        new { id = emailGrupoRebate.Id }, emailGrupoRebate);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/GrupoRebate/UpdateGrupoRebate/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmailGrupoRebate(int id, EmailGrupoRebateModel emailGrupoRebate)
        {
            try
            {
                var retornoEmailGrupoRebate = await _emailGrupoRebateService.GetByIdAsync(id);

                if (retornoEmailGrupoRebate == null)
                {
                    return NotFound();
                }

                emailGrupoRebate.Id = id;
                bool retorno = await _emailGrupoRebateService.UpdateAsync(emailGrupoRebate);

                if (retorno)
                {
                    return Ok(emailGrupoRebate);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/GrupoRebate/DeleteGrupoRebate/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmailGrupoRebate(int id)
        {
            try
            {
                EmailGrupoRebateModel retornoEmailGrupoRebate = await _emailGrupoRebateService.GetByIdAsync(id);

                if (retornoEmailGrupoRebate != null)
                {
                    bool retorno = await _emailGrupoRebateService.DeleteAsync(id);
                    if (retorno)
                    {
                        return Ok();
                    }
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // ATIVAR: api/GrupoRebate/ActivateGrupoRebate/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateEmailGrupoRebate(int id)
        {
            try
            {
                EmailGrupoRebateModel retornoEmailGrupoRebate = await _emailGrupoRebateService.GetByIdAsync(id);
                if (retornoEmailGrupoRebate != null)
                {
                    bool retorno = await _emailGrupoRebateService.ActivateAsync(id);
                    if (retorno)
                    {
                        return Ok();
                    }
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion
    }

}
