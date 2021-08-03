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
    public class AlocadorController : ControllerBase
    {
        private readonly DataContext _context;

        public AlocadorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Alocador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAlocador>>> Alocador()
        {
            return await _context.TblAlocador.ToListAsync();
        }

        // GET: api/Alocador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAlocador>> GetAlocador(int id)
        {
            var tblAlocador = await _context.TblAlocador.FindAsync(id);

            if (tblAlocador == null)
            {
                return NotFound();
            }

            return Ok(tblAlocador);
        }

        [HttpPost]
        public async Task<ActionResult<AlocadorController>> CadastrarAdministrador(AlocadorModel tblAlocadorModel)
        {
            var itensAlocador = new TblAlocador
            {
                CodCliente = tblAlocadorModel.CodCliente,
                CodContratoFundo = tblAlocadorModel.CodContratoFundo
            };

            _context.TblAlocador.Add(itensAlocador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAlocador),
                new { id = itensAlocador.Id },
                Ok(itensAlocador));
        }

        private bool AlocadorExists(int id)
        {
            return _context.TblAlocador.Any(e => e.Id == id);
        }
    }
}
