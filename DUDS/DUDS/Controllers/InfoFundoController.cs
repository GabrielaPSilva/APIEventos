using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;

namespace DUDS.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class InfoFundoController : Controller
    {
        private readonly DataContext _context;
        public InfoFundoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDeparaFundoproduto>>> DeparaFundoproduto()
        {
            return await _context.TblDeparaFundoproduto.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblFundo>>> Fundo()
        {
            var Fundos = await _context.TblFundo.AsNoTracking()
                //.Where(p => id.Contains(p.Id))
               .ToListAsync();

            if (Fundos == null)
            {
                NotFound();
            }
            return Fundos;
        }
    }
}
