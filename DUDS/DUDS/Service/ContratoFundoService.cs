using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ContratoFundoService: GenericService<ContratoFundoModel>, IContratoFundoService
    {
        public ContratoFundoService() : base(new ContratoFundoModel(),
            "tbl_contrato_fundo",
            new List<string> { "'id'", "'data_criacao'" },
            new List<string> { "Id", "DataCriacao", "NomeFundo", "TipoCondicao" },
            new List<string> { "'id'", "'data_criacao'", "'usuario_criacao'" },
            new List<string> { "Id", "DataCriacao", "UsuarioCriacao", "NomeFundo", "TipoCondicao" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(ContratoFundoModel item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ContratoFundoModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ContratoFundoModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ContratoFundoModel item)
        {
            throw new NotImplementedException();
        }
    }
}
