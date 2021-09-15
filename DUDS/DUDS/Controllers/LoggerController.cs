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

        // GET: api/Logger/GetLogErroById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblLogErros>> GetLogErroById(int id)
        {
            TblLogErros tblLogErros = await _context.TblLogErros.FindAsync(id);

            try
            {
                if (tblLogErros == null)
                {
                    return NotFound();
                }
                return Ok(tblLogErros);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Logger/AddLogErro/LogErrosModel
        [HttpPost]
        public async Task<ActionResult<LogErrosModel>> AddLogErro(LogErrosModel tblLogErros)
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
                    nameof(GetLogErroById),
                    new { id = itensLogger.Id }, itensLogger);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
