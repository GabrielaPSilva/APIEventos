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

        // GET: api/Investidor/GetInvestidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblInvestidor>>> GetInvestidor()
        {
            try
            {
                var listaDistribuidores = await (
                                    from investidor in _context.TblInvestidor
                                    from administrador in _context.TblAdministrador.Where(c => c.Id == investidor.CodAdministrador).DefaultIfEmpty()
                                    from gestor in _context.TblGestor.Where(c => c.Id == investidor.CodGestor).DefaultIfEmpty()
                                    from tipoContrato in _context.TblTipoContrato.Where(c => c.Id == investidor.CodTipoContrato)
                                    from grupoRebate in _context.TblGrupoRebate.Where(c => c.Id == investidor.CodTipoContrato).DefaultIfEmpty()
                                    where investidor.Ativo == true
                                    orderby investidor.NomeCliente
                                    select new
                                    {
                                        investidor.Id,
                                        investidor.NomeCliente,
                                        investidor.Cnpj,
                                        investidor.TipoCliente,
                                        investidor.CodAdministrador,
                                        investidor.CodGestor,
                                        investidor.CodTipoContrato,
                                        investidor.CodGrupoRebate,
                                        investidor.DataModificacao,
                                        investidor.UsuarioModificacao,
                                        investidor.Ativo,
                                        NomeAdministrador = administrador == null ? String.Empty : administrador.NomeAdministrador,
                                        NomeGestor = gestor == null ? String.Empty : gestor.NomeGestor,
                                        tipoContrato.TipoContrato,
                                        NomeGrupoRebate = grupoRebate == null ? String.Empty : grupoRebate.NomeGrupoRebate,
                                    }).AsNoTracking().ToListAsync();

                if (listaDistribuidores.Count == 0)
                {
                    return NotFound();
                }

                return Ok(listaDistribuidores);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Investidor/GetInvestidorById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGestor>> GetInvestidorById(int id)
        {
            try
            {
                var tblInvestidor = await (
                                           from investidor in _context.TblInvestidor
                                           from administrador in _context.TblAdministrador.Where(c => c.Id == investidor.CodAdministrador).DefaultIfEmpty()
                                           from gestor in _context.TblGestor.Where(c => c.Id == investidor.CodGestor).DefaultIfEmpty()
                                           from tipoContrato in _context.TblTipoContrato.Where(c => c.Id == investidor.CodTipoContrato)
                                           from grupoRebate in _context.TblGrupoRebate.Where(c => c.Id == investidor.CodTipoContrato).DefaultIfEmpty()
                                           where investidor.Id == id
                                           orderby investidor.NomeCliente
                                           select new
                                           {
                                               investidor.Id,
                                               investidor.NomeCliente,
                                               investidor.Cnpj,
                                               investidor.TipoCliente,
                                               investidor.CodAdministrador,
                                               investidor.CodGestor,
                                               investidor.CodTipoContrato,
                                               investidor.CodGrupoRebate,
                                               investidor.DataModificacao,
                                               investidor.UsuarioModificacao,
                                               investidor.Ativo,
                                               NomeAdministrador = administrador == null ? String.Empty : administrador.NomeAdministrador,
                                               NomeGestor = gestor == null ? String.Empty : gestor.NomeGestor,
                                               tipoContrato.TipoContrato,
                                               NomeGrupoRebate = grupoRebate == null ? String.Empty : grupoRebate.NomeGrupoRebate,
                                           }).FirstOrDefaultAsync();

                if (tblInvestidor == null)
                {
                    return NotFound();
                }
                return Ok(tblInvestidor);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Investidor/GetInvestidorExistsBase/cnpj
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<TblInvestidor>> GetInvestidorExistsBase(string cnpj)
        {
            var tblInvestidor = await (
                                    from investidor in _context.TblInvestidor
                                    from administrador in _context.TblAdministrador.Where(c => c.Id == investidor.CodAdministrador).DefaultIfEmpty()
                                    from gestor in _context.TblGestor.Where(c => c.Id == investidor.CodGestor).DefaultIfEmpty()
                                    from tipoContrato in _context.TblTipoContrato.Where(c => c.Id == investidor.CodTipoContrato)
                                    from grupoRebate in _context.TblGrupoRebate.Where(c => c.Id == investidor.CodTipoContrato).DefaultIfEmpty()
                                    where investidor.Ativo == false && investidor.Cnpj == cnpj
                                    orderby investidor.NomeCliente
                                    select new
                                    {
                                        investidor.Id,
                                        investidor.NomeCliente,
                                        investidor.Cnpj,
                                        investidor.TipoCliente,
                                        investidor.CodAdministrador,
                                        investidor.CodGestor,
                                        investidor.CodTipoContrato,
                                        investidor.CodGrupoRebate,
                                        investidor.DataModificacao,
                                        investidor.UsuarioModificacao,
                                        investidor.Ativo,
                                        NomeAdministrador = administrador == null ? String.Empty : administrador.NomeAdministrador,
                                        NomeGestor = gestor == null ? String.Empty : gestor.NomeGestor,
                                        tipoContrato.TipoContrato,
                                        NomeGrupoRebate = grupoRebate == null ? String.Empty : grupoRebate.NomeGrupoRebate,
                                    }).FirstOrDefaultAsync();

            if (tblInvestidor != null)
            {
                return Ok(tblInvestidor);
            }
            else
            {
                tblInvestidor = await (
                                   from investidor in _context.TblInvestidor
                                   from administrador in _context.TblAdministrador.Where(c => c.Id == investidor.CodAdministrador).DefaultIfEmpty()
                                   from gestor in _context.TblGestor.Where(c => c.Id == investidor.CodGestor).DefaultIfEmpty()
                                   from tipoContrato in _context.TblTipoContrato.Where(c => c.Id == investidor.CodTipoContrato)
                                   from grupoRebate in _context.TblGrupoRebate.Where(c => c.Id == investidor.CodTipoContrato).DefaultIfEmpty()
                                   where investidor.Cnpj == cnpj
                                   orderby investidor.NomeCliente
                                   select new
                                   {
                                       investidor.Id,
                                       investidor.NomeCliente,
                                       investidor.Cnpj,
                                       investidor.TipoCliente,
                                       investidor.CodAdministrador,
                                       investidor.CodGestor,
                                       investidor.CodTipoContrato,
                                       investidor.CodGrupoRebate,
                                       investidor.DataModificacao,
                                       investidor.UsuarioModificacao,
                                       investidor.Ativo,
                                       NomeAdministrador = administrador == null ? String.Empty : administrador.NomeAdministrador,
                                       NomeGestor = gestor == null ? String.Empty : gestor.NomeGestor,
                                       tipoContrato.TipoContrato,
                                       NomeGrupoRebate = grupoRebate == null ? String.Empty : grupoRebate.NomeGrupoRebate,
                                   }).FirstOrDefaultAsync();

                if (tblInvestidor != null)
                {
                    return Ok(tblInvestidor);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        //POST: api/Investidor/AddInvestidor/InvestidorModel
        [HttpPost]
        public async Task<ActionResult<InvestidorModel>> AddInvestidor(InvestidorModel tblInvestidorModel)
        {
            TblInvestidor itensInvestidor = new TblInvestidor
            {
                NomeCliente = tblInvestidorModel.NomeCliente,
                Cnpj = tblInvestidorModel.Cnpj,
                TipoCliente = tblInvestidorModel.TipoCliente,
                CodAdministrador = tblInvestidorModel.CodAdministrador,
                CodGestor = tblInvestidorModel.CodGestor,
                CodTipoContrato = tblInvestidorModel.CodTipoContrato,
                UsuarioModificacao = tblInvestidorModel.UsuarioModificacao
            };

            try
            {
                _context.TblInvestidor.Add(itensInvestidor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetInvestidor),
                    new { id = itensInvestidor.Id }, itensInvestidor);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //POST: api/Investidor/AddInvestidores/List<InvestidorModel>
        [HttpPost]
        public async Task<ActionResult<List<InvestidorModel>>> AddInvestidores(List<InvestidorModel> tblListInvestidorModel)
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
                        CodGestor = line.CodGestor,
                        CodTipoContrato = line.CodTipoContrato,
                        UsuarioModificacao = line.UsuarioModificacao
                    };

                    listaInvestidores.Add(itensInvestidor);
                }

                await _context.BulkInsertAsync(listaInvestidores);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                     nameof(GetInvestidor), listaInvestidores);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //PUT: api/Investidor/UpdateInvestidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvestidor(int id, InvestidorModel investidor)
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
                    registroInvestidor.CodTipoContrato = investidor.CodTipoContrato == 0 ? registroInvestidor.CodTipoContrato : investidor.CodTipoContrato;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroInvestidor);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.InnerException.Message);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException e) when (!InvestidorExists(investidor.Id))
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DELETE: api/Investidor/DeleteInvestidor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestidor(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_investidor");

            if (!existeRegistro)
            {
                TblInvestidor tblInvestidor = await _context.TblInvestidor.FindAsync(id);

                if (tblInvestidor == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblInvestidor.Remove(tblInvestidor);
                    await _context.SaveChangesAsync();
                    return Ok(tblInvestidor);
                }
                catch (Exception e)
                {
                    return BadRequest(e.InnerException.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }

        // DESATIVA: api/Investidor/DisableInvestidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DisableInvestidor(int id)
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
                        return Ok(registroInvestidor);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.InnerException.Message);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        // ATIVAR: api/Investidor/ActivateInvestidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateInvestidor(int id)
        {
            TblInvestidor registroInvestidor = await _context.TblInvestidor.FindAsync(id);

            if (registroInvestidor != null)
            {
                registroInvestidor.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroInvestidor);
                }
                catch (Exception e)
                {
                    return BadRequest(e.InnerException.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }

        private bool InvestidorExists(int id)
        {
            return _context.TblInvestidor.Any(e => e.Id == id);
        }

        #region Investidor Distribuidor
        // GET: api/Investidor/GetInvestidorDistribuidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblInvestidorDistribuidor>>> GetInvestidorDistribuidor()
        {
            try
            {
                List<TblInvestidorDistribuidor> investidorDistribuidores = await _context.TblInvestidorDistribuidor.AsNoTracking().ToListAsync();

                if (investidorDistribuidores.Count == 0)
                {
                    return NotFound();
                }

                return Ok(investidorDistribuidores);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Investidor/GetInvestidorDistribuidorByIds/cod_investidor/cod_distribuidor/cod_administrador
        [HttpGet("{cod_investidor}/{cod_distribuidor}/{cod_administrador}")]
        public async Task<ActionResult<TblInvestidorDistribuidor>> GetInvestidorDistribuidorByIds(int cod_investidor, int cod_distribuidor, int cod_administrador)
        {
            try
            {
                TblInvestidorDistribuidor tblInvestidorDistribuidor = await _context.TblInvestidorDistribuidor.FindAsync(cod_investidor, cod_distribuidor, cod_administrador);
                if (tblInvestidorDistribuidor == null)
                {
                    return NotFound();
                }
                return Ok(tblInvestidorDistribuidor);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //POST: api/Investidor/AddInvestidorDistribuidor/InvestidorDistribuidorModel
        [HttpPost]
        public async Task<ActionResult<InvestidorDistribuidorModel>> AddInvestidorDistribuidor(InvestidorDistribuidorModel tblInvestidorDistribuidorModel)
        {
            TblInvestidorDistribuidor itensInvestidorDistribuidor = new TblInvestidorDistribuidor
            {
                CodAdministrador = tblInvestidorDistribuidorModel.CodAdministrador,
                CodDistribuidor = tblInvestidorDistribuidorModel.CodDistribuidor,
                CodInvestAdministrador = tblInvestidorDistribuidorModel.CodInvestAdministrador,
                CodInvestidor = tblInvestidorDistribuidorModel.CodInvestidor,
                UsuarioModificacao = tblInvestidorDistribuidorModel.UsuarioModificacao
            };

            try
            {
                _context.TblInvestidorDistribuidor.Add(itensInvestidorDistribuidor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetInvestidorDistribuidor),
                    new
                    {
                        cod_investidor = itensInvestidorDistribuidor.CodInvestidor,
                        cod_distribuidor = itensInvestidorDistribuidor.CodDistribuidor,
                        cod_administrador = itensInvestidorDistribuidor.CodAdministrador
                    }, itensInvestidorDistribuidor);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //POST: api/Investidor/AddInvestidorDistribuidores/List<InvestidorDistribuidorModel>
        [HttpPost]
        public async Task<ActionResult<List<InvestidorDistribuidorModel>>> AddInvestidorDistribuidores(List<InvestidorDistribuidorModel> tblListInvestidorDistribuidorModel)
        {
            try
            {
                List<TblInvestidorDistribuidor> listaInvestidorDistribuidores = new List<TblInvestidorDistribuidor>();
                TblInvestidorDistribuidor itensInvestidorDistribuidor = new TblInvestidorDistribuidor();

                foreach (var line in tblListInvestidorDistribuidorModel)
                {
                    itensInvestidorDistribuidor = new TblInvestidorDistribuidor
                    {
                        CodAdministrador = line.CodAdministrador,
                        CodDistribuidor = line.CodDistribuidor,
                        CodInvestAdministrador = line.CodInvestAdministrador,
                        CodInvestidor = line.CodInvestidor,
                        UsuarioModificacao = line.UsuarioModificacao
                    };

                    listaInvestidorDistribuidores.Add(itensInvestidorDistribuidor);
                }

                await _context.BulkInsertAsync(listaInvestidorDistribuidores);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                   nameof(GetInvestidorDistribuidor), listaInvestidorDistribuidores);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //PUT: api/Investidor/UpdateInvestidorDistribuidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvestidorDistribuidor(int id, InvestidorDistribuidorModel investidorDistribuidor)
        {
            try
            {
                TblInvestidorDistribuidor registroInvestidorDistribuidor = _context.TblInvestidorDistribuidor.Where(c => c.Id == id).FirstOrDefault();

                if (registroInvestidorDistribuidor != null)
                {
                    registroInvestidorDistribuidor.CodInvestAdministrador = investidorDistribuidor.CodInvestAdministrador == null ? registroInvestidorDistribuidor.CodInvestAdministrador : investidorDistribuidor.CodInvestAdministrador;
                    registroInvestidorDistribuidor.CodInvestidor = investidorDistribuidor.CodInvestidor == 0 ? registroInvestidorDistribuidor.CodInvestidor : investidorDistribuidor.CodInvestidor;
                    registroInvestidorDistribuidor.CodDistribuidor = investidorDistribuidor.CodDistribuidor == 0 ? registroInvestidorDistribuidor.CodDistribuidor : investidorDistribuidor.CodDistribuidor;
                    registroInvestidorDistribuidor.CodAdministrador = investidorDistribuidor.CodAdministrador == 0 ? registroInvestidorDistribuidor.CodAdministrador : investidorDistribuidor.CodAdministrador;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroInvestidorDistribuidor);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.InnerException.Message);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException e) when (!InvestidorDistribuidorExists(investidorDistribuidor.Id))
            {
                return NotFound(e.InnerException.Message);
            }
        }

        // DELETE: api/Investidor/DeleteInvestidorDistribuidor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestidorDistribuidor(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_investidor_distribuidor");

            if (!existeRegistro)
            {
                TblInvestidorDistribuidor tblInvestidorDistribuidor = _context.TblInvestidorDistribuidor.Where(c => c.Id == id).FirstOrDefault();

                if (tblInvestidorDistribuidor == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblInvestidorDistribuidor.Remove(tblInvestidorDistribuidor);
                    await _context.SaveChangesAsync();
                    return Ok(tblInvestidorDistribuidor);
                }
                catch (Exception e)
                {
                    return BadRequest(e.InnerException.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private bool InvestidorDistribuidorExists(int id)
        {
            return _context.TblInvestidorDistribuidor.Any(e => e.Id == id);
        }

        #endregion
    }
}
