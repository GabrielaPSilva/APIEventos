using Dapper;
using DUDS.Data;
using DUDS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUDS.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class ConfiguracaoController : Controller
    {
        private readonly DataContext _context;
        private IConfiguration _config;
        string Sistema = "DUDS";

        public ConfiguracaoController(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetValidacaoExisteIdOutrasTabelas(int id)
        {
            var tabelasValidacao = _config["TabelasValidacao"].Split(',').Select(t => t.Trim()).ToArray();
            var connection = _context.Database.GetDbConnection();
            var transaction = _context.Database.CurrentTransaction?.GetDbTransaction();
            var commandTimeout = _context.Database.GetCommandTimeout();

            try
            {
                foreach (var item in tabelasValidacao)
                {
                    var command = new CommandDefinition(
                        "SELECT * FROM @TabelasValidacao WHERE Id = @id",
                    new
                    {
                        TabelasValidacao = item,
                        Id = id
                    },
                    transaction,
                    commandTimeout
                );

                    var resposta = await connection.QueryAsync<int>(command);
                }
            }
            catch (Exception e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
            }

            return NoContent();
        }
    }
}
