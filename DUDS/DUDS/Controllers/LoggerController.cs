using DUDS.Data;
using DUDS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class LoggerController : Controller
    {
        private readonly DataContext _context;

        public LoggerController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Logger/GetLogErros/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblLogErros>> GetLogErros(int id)
        {
            TblLogErros tblLogErros = await _context.TblLogErros.FindAsync(id);

            try
            {
                if (tblLogErros == null)
                {
                    return Ok(new { tblLogErros, Mensagem.SucessoCadastrado });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (Exception e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return NotFound(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        //POST: api/Logger/CadastrarLogErro/LogErrosModel
        [HttpPost]
        public async Task<ActionResult<LogErrosModel>> CadastrarLogErro(LogErrosModel tblLogErros)
        {
            TblLogErros itensLogger = new TblLogErros
            {
                Sistema = tblLogErros.Sistema,
                Metodo = tblLogErros.Metodo,
                Linha = tblLogErros.Linha,
                Mensagem = tblLogErros.Mensagem,
                Descricao = tblLogErros.Descricao,
                UsuarioModificacao = tblLogErros.UsuarioModificacao,
                DataCadastro = tblLogErros.DataCadastro
            };

            try
            {
                _context.TblLogErros.Add(itensLogger);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetLogErros),
                    new { id = itensLogger.Id },
                     Ok(new { itensLogger, Mensagem.SucessoCadastrado }));
            }
            catch (Exception e)
            {
                return BadRequest(new { Erro = e, Mensagem.ErroCadastrar });
            }
        }
    }
}
