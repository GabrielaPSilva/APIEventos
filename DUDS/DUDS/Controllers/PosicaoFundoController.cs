using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;
namespace DUDS.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class PosicaoFundoController : Controller
    {
        private readonly DataContext _context;
        public PosicaoFundoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoAcao>>> Acao([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoAcao = await _context.TblPosicaoAcao.AsNoTracking()
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();
            if (posicaoAcao == null)
            {
                NotFound();
            }

            return posicaoAcao;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoAdr>>> Adr([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }
            var posicaoAdr = await _context.TblPosicaoAdr.AsNoTracking()
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();

            if (posicaoAdr == null)
            {
                NotFound();
            }
            return posicaoAdr;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoBdr>>> Bdr([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoBdr = await _context.TblPosicaoBdr.AsNoTracking()
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();

            if (posicaoBdr == null)
            {
                NotFound();
            }
            return posicaoBdr;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoCambio>>> Cambio([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoCambio = await _context.TblPosicaoCambio.AsNoTracking()
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();

            if (posicaoCambio == null)
            {
                NotFound();
            }

            return posicaoCambio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoCotaFundo>>> CotaFundo([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoCotaFundo = await _context.TblPosicaoCotaFundo.AsNoTracking()
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();

            if (posicaoCotaFundo == null)
            {
                NotFound();
            }

            return posicaoCotaFundo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoCpr>>> Cpr([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoCpr = await _context.TblPosicaoCpr.AsNoTracking()
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();

            if (posicaoCpr == null)
            {
                NotFound();
            }

            return posicaoCpr;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoFuturo>>> Futuro([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }
            var posicaoFuturo = await _context.TblPosicaoFuturo.AsNoTracking()
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();

            if (posicaoFuturo == null)
            {
                NotFound();
            }
            return posicaoFuturo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoOpcaoAcao>>> OpcaoAcao([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoOpcaoAcao = await _context.TblPosicaoOpcaoAcao.AsNoTracking()
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();

            if (posicaoOpcaoAcao == null)
            {
                NotFound();
            }
            return posicaoOpcaoAcao;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoOpcaoFuturo>>> OpcaoFuturo([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoOpcaoFuturo = await _context.TblPosicaoOpcaoFuturo.AsNoTracking()
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();

            if (posicaoOpcaoFuturo == null)
            {
                NotFound();
            }
            return posicaoOpcaoFuturo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoPatrimonio>>> Patrimonio([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoPatrimonio = await _context.TblPosicaoPatrimonio.AsNoTracking()
                //.Include(p => p.CodFundoNavigation)
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();

            if (posicaoPatrimonio == null)
            {
                NotFound();
            }

            return posicaoPatrimonio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoRendafixa>>> Rendafixa([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoRendafixa = await _context.TblPosicaoRendafixa.AsNoTracking()
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();

            if (posicaoRendafixa == null)
            {
                NotFound();
            }

            return posicaoRendafixa;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoTesouraria>>> Tesouraria([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoTesouraria = await _context.TblPosicaoTesouraria.AsNoTracking()
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();

            if (posicaoTesouraria == null)
            {
                NotFound();
            }

            return posicaoTesouraria;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoEmprAcao>>> EmprAcao([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoEmprAcao = await _context.TblPosicaoEmprAcao.AsNoTracking()
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();

            if (posicaoEmprAcao == null)
            {
                NotFound();
            }

            return posicaoEmprAcao;
        }

    }
}

