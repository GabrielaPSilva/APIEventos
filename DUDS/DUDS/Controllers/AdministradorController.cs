﻿using System;
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
    public class AdministradorController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public AdministradorController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        // GET: api/Administrador/Administrador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAdministrador>>> Administrador()
        {
            try
            {
                List<TblAdministrador> administradores = await _context.TblAdministrador.Where(c => c.Ativo == true).OrderBy(c => c.NomeAdministrador).AsNoTracking().ToListAsync();

                if (administradores.Count() == 0)
                {
                    return BadRequest(Mensagem.ErroListar);
                }

                if (administradores != null)
                {
                    return Ok(new { administradores, Mensagem.SucessoListar });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (Exception e)
            {
                return NotFound(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        // GET: api/Administrador/GetAdministrador/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAdministrador>> GetAdministrador(int id)
        {
            TblAdministrador tblAdministrador = await _context.TblAdministrador.FindAsync(id);

            try
            {
                if (tblAdministrador != null)
                {
                    return Ok(new { tblAdministrador, Mensagem.SucessoCadastrado });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        //POST: api/Administrador/CadastrarAdministrador/AdministradorModel
        [HttpPost]
        public async Task<ActionResult<AdministradorModel>> CadastrarAdministrador(AdministradorModel tblAdministradorModel)
        {
            TblAdministrador itensAdministrador = new TblAdministrador
            {
                NomeAdministrador = tblAdministradorModel.NomeAdministrador,
                Cnpj = tblAdministradorModel.Cnpj,
                DataModificacao = tblAdministradorModel.DataModificacao,
                UsuarioModificacao = tblAdministradorModel.UsuarioModificacao,
                Ativo = tblAdministradorModel.Ativo
            };

            try
            {
                _context.TblAdministrador.Add(itensAdministrador);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                   nameof(GetAdministrador),
                   new { id = itensAdministrador.Id },
                   Ok(new { itensAdministrador, Mensagem.SucessoCadastrado }));
            }
            catch (Exception e)
            {
                return BadRequest(new { Erro = e, Mensagem.ErroCadastrar });
            }
        }

        //PUT: api/Administrador/EditarAdministrador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarAdministrador(int id, AdministradorModel administrador)
        {
            try
            {
                TblAdministrador registroAdministrador = _context.TblAdministrador.Find(id);

                if (registroAdministrador != null)
                {
                    registroAdministrador.NomeAdministrador = administrador.NomeAdministrador == null ? registroAdministrador.NomeAdministrador : administrador.NomeAdministrador;
                    registroAdministrador.Cnpj = administrador.Cnpj == null ? registroAdministrador.Cnpj : administrador.Cnpj;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(new { registroAdministrador, Mensagem.SucessoAtualizado });
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
            catch (DbUpdateConcurrencyException e) when (!AdministradorExists(administrador.Id))
            {
                return NotFound(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        // DELETE: api/Administrador/DeletarAdministrador/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAdministrador(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_administrador");

            if (!existeRegistro)
            {
                TblAdministrador tblAdministrador = await _context.TblAdministrador.FindAsync(id);

                if (tblAdministrador == null)
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }

                try
                {
                    _context.TblAdministrador.Remove(tblAdministrador);
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

        // DESATIVA: api/Administrador/DesativarAdministrador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarAdministrador(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_administrador");

            if (!existeRegistro)
            {
                TblAdministrador registroAdministrador = _context.TblAdministrador.Find(id);

                if (registroAdministrador != null)
                {
                    registroAdministrador.Ativo = false;

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

        private bool AdministradorExists(int id)
        {
            return _context.TblAdministrador.Any(e => e.Id == id);
        }
    }
}
