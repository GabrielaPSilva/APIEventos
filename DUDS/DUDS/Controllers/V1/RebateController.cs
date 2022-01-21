using DUDS.Models.Filtros;
using DUDS.Models.PgtoServico;
using DUDS.Models.PgtoTaxaAdmPfee;
using DUDS.Models.Rebate;
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
        private readonly IPgtoServicoService _pagamentoServicoService;
        private readonly IPgtoTaxaAdmPfeeService _pagamentoTaxaAdministracaoPerformanceService;
        private readonly ICalculoRebateService _calculoRebateService;
        private readonly IGrupoRebateService _grupoRebateService;
        private readonly IEmailGrupoRebateService _emailGrupoRebateService;
        private readonly IControleRebateService _controleRebateService;

        public RebateController(IConfiguracaoService configService,
            IErrosPagamentoService errosPagamento,
            IPgtoServicoService pagamentoServicoService,
            IPgtoTaxaAdmPfeeService pagamentoTaxaAdministracaoPerformanceService,
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

        // GET: api/Rebate/GetControleRebateByCompetencia
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ControleRebateViewModel>>> GetControleRebateByCompetencia([FromQuery] FiltroModel filtro)
        {
            try
            {
                var listaControleRebate = await _controleRebateService.GetByCompetenciaAsync(filtro);

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
        public async Task<ActionResult<ControleRebateViewModel>> GetControleRebateById(int id)
        {
            try
            {
                var tblControleRebate = await _controleRebateService.GetByIdAsync(id);

                if (tblControleRebate == null)
                {
                    return NotFound();
                }

                return Ok(tblControleRebate);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Rebate/GetControleRebate
        [HttpGet("{grupoRebate}/{competencia}")]
        public async Task<ActionResult<IEnumerable<ControleRebateViewModel>>> GetFiltroControleRebate(int grupoRebate, string competencia, string codMellon = null, string investidor = null)
        {
            try
            {
                var listaControleRebate = await _controleRebateService.GetFiltroControleRebateAsync(grupoRebate, investidor, competencia, codMellon);

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

        // GET: api/Rebate/GetControleRebateExistsBase/codGrupoRebate/competencia
        [HttpGet("{codGrupoRebate}/{competencia}")]
        public async Task<ActionResult<ControleRebateViewModel>> GetControleRebateExistsBase(int codGrupoRebate, string competencia)
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

        // GET: api/Rebate/GetCountControleRebate/filtro
        [HttpGet()]
        public async Task<ActionResult<int>> GetCountControleRebate([FromQuery] FiltroModel filtro)
        {
            try
            {
                var controleRebate = await _controleRebateService.GetCountControleRebateAsync(filtro);

                if (controleRebate == 0)
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
        public async Task<ActionResult<ControleRebateViewModel>> AddControleRebate(ControleRebateModel controleRebate)
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

                if (ModelState.IsValid)
                {
                    bool retorno = await _controleRebateService.UpdateAsync(controleRebate);

                    if (retorno)
                    {
                        return Ok(controleRebate);
                    }
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
                var retorno = await _errosPagamento.AddErrosPagamento(errosPagamento);
                if (retorno.Any())
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
        public async Task<ActionResult<IEnumerable<PgtoServicoViewModel>>> GetPagamentoServico()
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
        public async Task<ActionResult<PgtoServicoViewModel>> GetPagamentoServicoById(int id)
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

        // GET: api/Pagamentos/GetPagamentoServicoByIds/competencia
        [HttpGet("{competencia}")]
        public async Task<ActionResult<IEnumerable<PgtoServicoViewModel>>> GetPagamentoServicoByCompetencia(string competencia)
        {
            try
            {
                var pagamentoServicos = await _pagamentoServicoService.GetPgtoServicoByCompetencia(competencia);

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

        // GET: api/Pagamentos/GetPagamentoServicoByIds/competencia/cod_fundo
        [HttpGet("{competencia}/{cod_fundo}")]
        public async Task<ActionResult<PgtoServicoViewModel>> GetPagamentoServicoByIds(string competencia, int cod_fundo)
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

        //POST: api/Pagamentos/AddPgtoServico/List<PagamentoServicoModel>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<PgtoServicoViewModel>>> AddPagamentoServico(List<PgtoServicoModel> pagamentoServico)
        {
            try
            {
                var retorno = await _pagamentoServicoService.AddPgtoServico(pagamentoServico);

                if (!retorno.Any())
                {
                    return CreatedAtAction(nameof(GetPagamentoServicoByCompetencia),
                        new { competencia = pagamentoServico.FirstOrDefault().Competencia }, pagamentoServico);
                }

                return BadRequest(retorno);
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
        public async Task<ActionResult<IEnumerable<PgtoTaxaAdmPfeeViewModel>>> GetPgtoTaxaAdmPfee()
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
        public async Task<ActionResult<PgtoTaxaAdmPfeeViewModel>> GetPgtoTaxaAdmPfeeById(Guid id)
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

        [HttpGet("{competencia}")]
        public async Task<ActionResult<IEnumerable<PgtoTaxaAdmPfeeViewModel>>> GetPgtoTaxaAdmPfeeByCompetencia(string competencia)
        {
            try
            {
                var pagamentoTaxaAdministracaoPerformance = await _pagamentoTaxaAdministracaoPerformanceService.GetByCompetenciaAsync(competencia);

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

        // GET: api/Pagamentos/GetPgtoTaxaAdmPfeeByIds/competencia/cod_investidor_distribuidor/cod_administrador/cod_fundo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PgtoTaxaAdmPfeeViewModel>>> GetPgtoTaxaAdmPfeeByIds([FromQuery] string competencia, [FromQuery] int? cod_fundo, [FromQuery] int? cod_administrador, [FromQuery] int? cod_investidor_distribuidor)
        {
            try
            {
                var pagamentoTaxaAdministracaoPerformance = await _pagamentoTaxaAdministracaoPerformanceService.GetByParametersAsync(competencia, cod_fundo, cod_administrador, cod_investidor_distribuidor);
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
        public async Task<ActionResult<IEnumerable<PgtoTaxaAdmPfeeViewModel>>> AddPgtoTaxaAdminPfee(List<PgtoTaxaAdmPfeeModel> pagamentoAdministracaoPerformance)
        {
            try
            {
                var retorno = await _pagamentoTaxaAdministracaoPerformanceService.AddBulkAsync(pagamentoAdministracaoPerformance);
                if (!retorno.Any())
                {
                    return CreatedAtAction(nameof(GetPgtoTaxaAdmPfeeByCompetencia),
                        new { competencia = pagamentoAdministracaoPerformance.FirstOrDefault().Competencia }, pagamentoAdministracaoPerformance);
                }
                return BadRequest(retorno);
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
        public async Task<ActionResult<IEnumerable<PgtoAdmPfeeInvestidorViewModel>>> GetPgtoTaxaAdmPfeeInvestidor(string competencia)
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
        // GET: api/Rebate/GetCalculoRebate/competencia/codGrupoRebate
        //[HttpGet("{competencia}/{codGrupoRebate}")]
        [HttpGet("{competencia}")]
        public async Task<ActionResult<IEnumerable<CalculoRebateViewModel>>> GetCalculoRebate(string competencia, int? codGrupoRebate)
        {
            try
            {
                var calculoRebate = await _calculoRebateService.GetCalculoRebate(competencia, codGrupoRebate);

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

        // GET: api/Rebate/GetCalculoRebateById/id
        [HttpGet("{guid}")]
        public async Task<ActionResult<CalculoRebateViewModel>> GetCalculoRebateById(Guid id)
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

        //POST: api/Rebate/AddCalculoRebate/List<CalculoPgtoTaxaAdmPfeeModel>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CalculoRebateViewModel>>> AddCalculoRebate(List<CalculoRebateModel> calculoRebate)
        {
            try
            {
                var retorno = await _calculoRebateService.AddBulkAsync(calculoRebate);

                if (!retorno.Any())
                {
                    // TODO - Arrumar esta coisa estranha
                    return CreatedAtAction(nameof(GetCalculoRebate), new { competencia = "2021-07" }, GetCalculoRebate("2021-07", null));
                }

                return BadRequest(retorno);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Rebate/DeleteCalculoRebate/competencia
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

        // GET: api/Rebate/GetDescricaoCalculoPgtoTaxaAdmPfee
        [HttpGet("{codContrato}/{codSubContrato}/{codContratoFundo}/{codContratoRemuneracao}")]
        public async Task<ActionResult<IEnumerable<DescricaoCalculoRebateViewModel>>> GetDescricaoCalculoPgtoTaxaAdmPfee(int codContrato, int codSubContrato, int codContratoFundo, int codContratoRemuneracao)
        {
            try
            {
                var calculoRebate = await _calculoRebateService.GetDescricaoRebateAsync(codContrato, codSubContrato, codContratoFundo, codContratoRemuneracao);

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

        // GET: api/Rebate/GetParametroControleRebate/filtro
        [HttpGet("{competencia}")]
        public async Task<ActionResult<IEnumerable<ControleRebateModel>>> GetParametroControleRebate(string competencia)
        {
            try
            {
                var controleRebate = await _calculoRebateService.GetParametroControleRebateAsync(competencia);

                if (!controleRebate.Any())
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

        #endregion

        #region Grupo Rebate
        // GET: api/Rebate/GetGrupoRebate
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

        // GET: api/Rebate/GetTblGrupoRebateById/id
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

        //POST: api/Rebate/AddGrupoRebate/GrupoRebateModel
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

        //PUT: api/Rebate/UpdateGrupoRebate/id
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

        // DELETE: api/Rebate/DeleteGrupoRebate/id
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

        // ATIVAR: api/Rebate/ActivateGrupoRebate/id
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
        public async Task<ActionResult<IEnumerable<EmailGrupoRebateViewModel>>> GetEmailGrupoRebate()
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

        // GET: api/GrupoRebate/GetEmailGrupoRebateById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<EmailGrupoRebateViewModel>> GetEmailGrupoRebateById(int id)
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

        // GET: api/GrupoRebate/GetEmailCodGrupoRebate
        [HttpGet("{codGrupoRebate}")]
        public async Task<ActionResult<IEnumerable<EmailGrupoRebateViewModel>>> GetEmailCodGrupoRebate(int codGrupoRebate)
        {
            try
            {
                var listEmailGrupoRebate = await _emailGrupoRebateService.GetByGrupoRebateAsync(codGrupoRebate);

                if (listEmailGrupoRebate.Any())
                {
                    return Ok(listEmailGrupoRebate);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/GrupoRebate/AddGrupoRebate/GrupoRebateModel
        [HttpPost]
        public async Task<ActionResult<EmailGrupoRebateViewModel>> AddEmailGrupoRebate(EmailGrupoRebateModel emailGrupoRebate)
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
                var retornoEmailGrupoRebate = await _emailGrupoRebateService.GetByIdAsync(id);

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
