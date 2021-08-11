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
using EFCore.BulkExtensions;

namespace DUDS.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class InvestidorController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public InvestidorController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        // GET: api/Investidor/Investidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblInvestidor>>> Investidor()
        {
            try
            {
                List<TblInvestidor> investidores = await _context.TblInvestidor.Where(c => c.Ativo == true).OrderBy(c => c.NomeCliente).AsNoTracking().ToListAsync();

                if (investidores.Count() == 0)
                {
                    return BadRequest(Mensagem.ErroListar);
                }

                if (investidores != null)
                {
                    return Ok(new { investidores, Mensagem.SucessoListar });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (InvalidOperationException e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return NotFound(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        // GET: api/Investidor/GetInvestidor/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGestor>> GetInvestidor(int id)
        {
            TblInvestidor tblInvestidor = await _context.TblInvestidor.FindAsync(id);

            try
            {
                if (tblInvestidor != null)
                {
                    return Ok(new { tblInvestidor, Mensagem.SucessoCadastrado });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (Exception e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        //POST: api/Investidor/CadastrarInvestidor/InvestidorModel
        [HttpPost]
        public async Task<ActionResult<InvestidorModel>> CadastrarInvestidor(InvestidorModel tblInvestidorModel)
        {
            TblInvestidor itensInvestidor = new TblInvestidor
            {
                NomeCliente = tblInvestidorModel.NomeCliente,
                Cnpj = tblInvestidorModel.Cnpj,
                TipoCliente = tblInvestidorModel.TipoCliente,
                CodAdministrador = tblInvestidorModel.CodAdministrador,
                CodGestor = tblInvestidorModel.CodGestor
            };

            try
            {
                _context.TblInvestidor.Add(itensInvestidor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetInvestidor),
                    new { id = itensInvestidor.Id },
                     Ok(new { itensInvestidor, Mensagem.SucessoCadastrado }));
            }
            catch (Exception e)
            {
                return BadRequest(new { Erro = e, Mensagem.ErroCadastrar });
            }
        }

        //POST: api/Investidor/CadastrarInvestidor/List<InvestidorModel>
        [HttpPost]
        public async Task<ActionResult<List<InvestidorModel>>> CadastrarInvestidores(List<InvestidorModel> tblListInvestidorModel)
        {
            try
            {
                List<TblInvestidor> listaInvestidores = new List<TblInvestidor>();
                TblInvestidor itensInvestidor = new TblInvestidor();

                foreach (var line in tblListInvestidorModel)
                {
                    itensInvestidor = new TblInvestidor
                    {
                        NomeCliente = line.NomeCliente,
                        Cnpj = line.Cnpj,
                        TipoCliente = line.TipoCliente,
                        CodAdministrador = line.CodAdministrador,
                        CodGestor = line.CodGestor
                    };

                    listaInvestidores.Add(itensInvestidor);
                }

                await _context.BulkInsertAsync(listaInvestidores);
                await _context.SaveChangesAsync();

                return Ok(new { itensInvestidor, Mensagem.SucessoCadastrado });
            }
            catch (Exception e)
            {
                return BadRequest(new { Erro = e, Mensagem.ErroCadastrar, e });
            }
        }

        //PUT: api/Investidor/EditarInvestidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarInvestidor(int id, InvestidorModel investidor)
        {
            try
            {
                TblInvestidor registroInvestidor = _context.TblInvestidor.Find(id);

                if (registroInvestidor != null)
                {
                    registroInvestidor.NomeCliente = investidor.NomeCliente == null ? registroInvestidor.NomeCliente : investidor.NomeCliente;
                    registroInvestidor.Cnpj = investidor.Cnpj == null ? registroInvestidor.Cnpj : investidor.Cnpj;
                    registroInvestidor.TipoCliente = investidor.TipoCliente == null ? registroInvestidor.TipoCliente : investidor.TipoCliente;
                    registroInvestidor.CodAdministrador = investidor.CodAdministrador == 0 ? registroInvestidor.CodAdministrador : investidor.CodAdministrador;
                    registroInvestidor.CodGestor = investidor.CodGestor == 0 ? registroInvestidor.CodGestor : investidor.CodGestor;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(new { registroInvestidor, Mensagem.SucessoAtualizado });
                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { Erro = e, Mensagem.ErroAtualizar });
                    }
                }
                else
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }
            }
            catch (DbUpdateConcurrencyException e) when (!InvestidorExists(investidor.Id))
            {
                return BadRequest(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        // DELETE: api/Investidor/DeletarInvestidor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarInvestidor(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_investidor");

            if (!existeRegistro)
            {
                TblInvestidor tblInvestidor = await _context.TblInvestidor.FindAsync(id);

                if (tblInvestidor == null)
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }

                try
                {
                    _context.TblInvestidor.Remove(tblInvestidor);
                    await _context.SaveChangesAsync();
                    return Ok(new { Mensagem.SucessoExcluido });
                }
                catch (Exception e)
                {
                    return BadRequest(new { Erro = e, Mensagem.ErroExcluir });
                }
            }
            else
            {
                return BadRequest(new { Erro = true, Mensagem.ExisteRegistroDesativar });
            }
        }

        // DESATIVA: api/Investidor/DesativarInvestidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarInvestidor(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_investidor");

            if (!existeRegistro)
            {
                var registroInvestidor = _context.TblInvestidor.Find(id);

                if (registroInvestidor != null)
                {
                    registroInvestidor.Ativo = false;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(new { Mensagem.SucessoDesativado });
                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { Erro = e, Mensagem.ErroDesativar });
                    }
                }
                else
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }
            }
            else
            {
                return BadRequest(Mensagem.ExisteRegistroDesativar);
            }
        }

        private bool InvestidorExists(int id)
        {
            return _context.TblInvestidor.Any(e => e.Id == id);
        }

        #region Investidor Distribuidor
        // GET: api/Investidor/InvestidorDistribuidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblInvestidorDistribuidor>>> InvestidorDistribuidor()
        {
            try
            {
                List<TblInvestidorDistribuidor> investidorDistribuidores = await _context.TblInvestidorDistribuidor.AsNoTracking().ToListAsync();

                if (investidorDistribuidores.Count() == 0)
                {
                    return BadRequest(Mensagem.ErroListar);
                }

                if (investidorDistribuidores != null)
                {
                    return Ok(new { investidorDistribuidores, Mensagem.SucessoListar });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (InvalidOperationException e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return NotFound(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        // GET: api/Investidor/GetInvestidorDistribuidor/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblInvestidorDistribuidor>> GetInvestidorDistribuidor(int id)
        {
            TblInvestidorDistribuidor tblInvestidorDistribuidor = await _context.TblInvestidorDistribuidor.FindAsync(id);

            try
            {
                if (tblInvestidorDistribuidor != null)
                {
                    return Ok(new { tblInvestidorDistribuidor, Mensagem.SucessoCadastrado });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (Exception e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        //POST: api/Investidor/CadastrarInvestidorDistribuidor/InvestidorDistribuidorModel
        [HttpPost]
        public async Task<ActionResult<InvestidorDistribuidorModel>> CadastrarInvestidorDistribuidor(InvestidorDistribuidorModel tblInvestidorDistribuidorModel)
        {
            TblInvestidorDistribuidor itensInvestidorDistribuidor = new TblInvestidorDistribuidor
            {
                CodAdministrador = tblInvestidorDistribuidorModel.CodAdministrador,
                CodDistribuidor = tblInvestidorDistribuidorModel.CodDistribuidor,
                CodInvestCustodia = tblInvestidorDistribuidorModel.CodInvestCustodia,
                CodInvestidor = tblInvestidorDistribuidorModel.CodInvestidor,
                UsuarioModificacao = tblInvestidorDistribuidorModel.UsuarioModificacao,
                DataModificacao = tblInvestidorDistribuidorModel.DataModificacao
            };

            try
            {
                _context.TblInvestidorDistribuidor.Add(itensInvestidorDistribuidor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetInvestidor),
                    new { id = itensInvestidorDistribuidor.Id },
                    Ok(new { itensInvestidorDistribuidor, Mensagem.SucessoCadastrado }));
            }
            catch (Exception e)
            {
                return BadRequest(new { Erro = e, Mensagem.ErroCadastrar });
            }
        }

        //POST: api/Investidor/CadastrarInvestidorDistribuidor/List<InvestidorDistribuidorModel>
        [HttpPost]
        public async Task<ActionResult<List<InvestidorDistribuidorModel>>> CadastrarInvestidorDistribuidores(List<InvestidorDistribuidorModel> tblListInvestidorDistribuidorModel)
        {
            try
            {
                List<TblInvestidorDistribuidor> listaInvestidorDIstribuidores = new List<TblInvestidorDistribuidor>();
                TblInvestidorDistribuidor itensInvestidorDistribuidor = new TblInvestidorDistribuidor();

                foreach (var line in tblListInvestidorDistribuidorModel)
                {
                    itensInvestidorDistribuidor = new TblInvestidorDistribuidor
                    {
                        CodAdministrador = line.CodAdministrador,
                        CodDistribuidor = line.CodDistribuidor,
                        CodInvestCustodia = line.CodInvestCustodia,
                        CodInvestidor = line.CodInvestidor,
                        UsuarioModificacao = line.UsuarioModificacao,
                        DataModificacao = line.DataModificacao
                    };

                    listaInvestidorDIstribuidores.Add(itensInvestidorDistribuidor);
                }

                await _context.BulkInsertAsync(listaInvestidorDIstribuidores);
                await _context.SaveChangesAsync();

                return Ok(new { itensInvestidorDistribuidor, Mensagem.SucessoCadastrado });
            }
            catch (Exception e)
            {
                return BadRequest(new { Erro = e, Mensagem.ErroCadastrar, e });
            }
        }

        // DELETE: api/Investidor/DeletarInvestidorDistribuidor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarInvestidorDistribuidor(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_investidor_distribuidor");

            if (!existeRegistro)
            {
                TblInvestidorDistribuidor tblInvestidorDistribuidor = _context.TblInvestidorDistribuidor.Where(c => c.Id == id).FirstOrDefault();

                if (tblInvestidorDistribuidor == null)
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }

                try
                {
                    _context.TblInvestidorDistribuidor.Remove(tblInvestidorDistribuidor);
                    await _context.SaveChangesAsync();
                    return Ok(new { Mensagem.SucessoExcluido });
                }
                catch (Exception e)
                {
                    return BadRequest(new { Erro = e, Mensagem.ErroExcluir });
                }
            }
            else
            {
                return BadRequest(Mensagem.ExisteRegistroDesativar);
            }
        }

        #endregion
    }
}
