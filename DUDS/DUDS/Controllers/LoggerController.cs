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

        [HttpGet("{id}")]
        public async Task<ActionResult<TblLogErros>> GetLogErros(int id)
        {
            var tblLogErros = await _context.TblLogErros.FindAsync(id);

            if (tblLogErros == null)
            {
                return NotFound();
            }

            return Ok(tblLogErros);
        }

        [HttpPost]
        public async Task<ActionResult<LogErrosModel>> CadastrarLogErro(LogErrosModel tblLogErros)
        {
            var itensLogger = new TblLogErros
            {
                Sistema = tblLogErros.Sistema,
                Metodo = tblLogErros.Metodo,
                Linha = tblLogErros.Linha,
                Mensagem = tblLogErros.Mensagem,
                Descricao = tblLogErros.Descricao,
                UsuarioModificacao = tblLogErros.UsuarioModificacao,
                DataCadastro = tblLogErros.DataCadastro
            };

            _context.TblLogErros.Add(itensLogger);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetLogErros),
                new { id = itensLogger.Id },
                Ok(itensLogger));
        }

    }
}
