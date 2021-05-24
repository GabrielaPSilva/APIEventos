using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;
using System.ComponentModel.DataAnnotations;
using DUDS.MOD;
using Newtonsoft.Json;
using System.Diagnostics;
using DUDS.BLL.Interfaces;

namespace DUDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundoController : ControllerBase
    {
        private readonly ILogErrorBLL _logErrorBLL;

        private readonly DataContext _context;

        public FundoController(DataContext context, ILogErrorBLL logErrorBLL)
        {
            _context = context;
            _logErrorBLL = logErrorBLL;
        }

        // GET: api/Fundo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblFundo>>> GetTblFundo()
        {
            return await _context.TblFundo.ToListAsync();
        }

        // GET: api/Fundo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblFundo>> GetTblFundo(int id)
        {
            //try
            //{
            //    var tblFundo = await _context.TblFundo.FindAsync(id);
            //    return tblFundo;
            //}
            //catch (ValidationException e)
            //{
                var logErro = new LogErrorMOD
                {
                    Sistema = "DUDS",
                    //Mensagem = e.Message
                };

                //var outros = new
                //{
                //    CodigosDebitos = string.Join(",", proposta.CodigosDebitos)
                //};

                //logErro.Outros = JsonConvert.SerializeObject(outros);

                StackTrace st = new StackTrace(true);

                List<object> listaObjCaminhoParcialErro = new List<object>();
                for (int i = 0; i < st.FrameCount; i++)
                {
                    StackFrame sf = st.GetFrame(i);

                    var objCaminhoParcialErro = new //CaminhoErroMOD
                    {
                        Arquivo = sf.GetFileName(),
                        Metodo = sf.GetMethod().ToString(),
                        Linha = sf.GetFileLineNumber()
                    };

                    listaObjCaminhoParcialErro.Add(objCaminhoParcialErro);

                    if (i == 0)
                    {
                        logErro.Arquivo = objCaminhoParcialErro.Arquivo;
                        logErro.Metodo = objCaminhoParcialErro.Metodo;
                        logErro.Linha = objCaminhoParcialErro.Linha;
                    }
                }

                var descricao = new
                {
                    logErro.Sistema,
                    logErro.Mensagem,
                    //ListaCaminhoErro = listaObjCaminhoParcialErro
                    //logErro.Outros
                };

                logErro.Descricao = JsonConvert.SerializeObject(descricao);

                await _logErrorBLL.CadastrarLogErroAsync(logErro);
                //return BadRequest(e.Message);
            //}

            return NoContent();

            //var tblFundo = await _context.TblFundo.FindAsync(id);

            //if (tblFundo == null)
            //{
            //    return NotFound();
            //}

            //return tblFundo;
        }

        // PUT: api/Fundo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblFundo(int id, TblFundo tblFundo)
        {
            if (id != tblFundo.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblFundo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (ValidationException e)
            {
                var logErro = new LogErrorMOD
                {
                    Sistema = "DUDS",
                    Mensagem = e.Message
                };

                //var outros = new
                //{
                //    CodigosDebitos = string.Join(",", proposta.CodigosDebitos)
                //};

                //logErro.Outros = JsonConvert.SerializeObject(outros);

                StackTrace st = new StackTrace(true);

                List<object> listaObjCaminhoParcialErro = new List<object>();
                for (int i = 0; i < st.FrameCount; i++)
                {
                    StackFrame sf = st.GetFrame(i);

                    var objCaminhoParcialErro = new //CaminhoErroMOD
                    {
                        Arquivo = sf.GetFileName(),
                        Metodo = sf.GetMethod().ToString(),
                        Linha = sf.GetFileLineNumber()
                    };

                    listaObjCaminhoParcialErro.Add(objCaminhoParcialErro);

                    if (i == 0)
                    {
                        logErro.Arquivo = objCaminhoParcialErro.Arquivo;
                        logErro.Metodo = objCaminhoParcialErro.Metodo;
                        logErro.Linha = objCaminhoParcialErro.Linha;
                    }
                }

                var descricao = new
                {
                    logErro.Sistema,
                    logErro.Mensagem,
                    ListaCaminhoErro = listaObjCaminhoParcialErro
                    //logErro.Outros
                };

                logErro.Descricao = JsonConvert.SerializeObject(descricao);

                await _logErrorBLL.CadastrarLogErroAsync(logErro);
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // POST: api/Fundo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblFundo>> PostTblFundo(TblFundo tblFundo)
        {
            _context.TblFundo.Add(tblFundo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblFundo", new { id = tblFundo.Id }, tblFundo);
        }

        // DELETE: api/Fundo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblFundo(int id)
        {
            var tblFundo = await _context.TblFundo.FindAsync(id);
            if (tblFundo == null)
            {
                return NotFound();
            }

            _context.TblFundo.Remove(tblFundo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblFundoExists(int id)
        {
            return _context.TblFundo.Any(e => e.Id == id);
        }
    }
}
