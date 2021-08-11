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
    public class GestorController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public GestorController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        // GET: api/Gestor/Gestor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblGestor>>> Gestor()
        {
            try
            {
                List<TblGestor> gestores = await _context.TblGestor.Where(c => c.Ativo == true).OrderBy(c => c.NomeGestor).AsNoTracking().ToListAsync();

                if (gestores.Count() == 0)
                {
                    return BadRequest(Mensagem.ErroListar);
                }

                if (gestores != null)
                {
                    return Ok(new { gestores, Mensagem.SucessoListar });
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

        // GET: api/Gestor/GetGestor/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGestor>> GetGestor(int id)
        {
            TblGestor tblGestor = await _context.TblGestor.FindAsync(id);

            try
            {
                if (tblGestor != null)
                {
                    return Ok(new { tblGestor, Mensagem.SucessoCadastrado });
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

        //POST: api/Gestor/CadastrarGestor/GestorModel
        [HttpPost]
        public async Task<ActionResult<GestorModel>> CadastrarGestor(GestorModel tblGestorModel)
        {
            TblGestor itensGestor = new TblGestor
            {
                NomeGestor = tblGestorModel.NomeGestor,
                Cnpj = tblGestorModel.Cnpj,
                DataModificacao = tblGestorModel.DataModificacao,
                UsuarioModificacao = tblGestorModel.UsuarioModificacao,
                Ativo = tblGestorModel.Ativo
            };

            try
            {
                _context.TblGestor.Add(itensGestor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetGestor),
                    new { id = itensGestor.Id },
                    Ok(new { itensGestor, Mensagem.SucessoCadastrado }));
            }
            catch (Exception e)
            {
                return BadRequest(new { Erro = e, Mensagem.ErroCadastrar });
            }
        }

        //PUT: api/Gestor/EditarGestor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarGestor(int id, GestorModel gestor)
        {
            try
            {
                TblGestor registroGestor = _context.TblGestor.Find(id);

                if (registroGestor != null)
                {
                    registroGestor.NomeGestor = gestor.NomeGestor == null ? registroGestor.NomeGestor : gestor.NomeGestor;
                    registroGestor.Cnpj = gestor.Cnpj == null ? registroGestor.Cnpj : gestor.Cnpj;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(new { registroGestor, Mensagem.SucessoAtualizado });
                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { Erro = e, Mensagem.ErroAtualizar });
                    }
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (DbUpdateConcurrencyException e) when (!GestorExists(gestor.Id))
            {
                return NotFound(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        // DELETE: api/Gestor/DeletarGestor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarGestor(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_gestor");

            if (!existeRegistro)
            {
                var tblGestor = await _context.TblGestor.FindAsync(id);

                if (tblGestor == null)
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }

                try
                {
                    _context.TblGestor.Remove(tblGestor);
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

        // DESATIVA: api/Gestor/DesativarGestor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarGestor(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_gestor");

            if (!existeRegistro)
            {
                TblGestor registroGestor = _context.TblGestor.Find(id);

                if (registroGestor != null)
                {
                    registroGestor.Ativo = false;

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

        private bool GestorExists(int id)
        {
            return _context.TblGestor.Any(e => e.Id == id);
        }
    }
}
