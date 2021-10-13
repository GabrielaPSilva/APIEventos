﻿using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ICondicaoRemuneracaoService : IGenericOperationsService<CondicaoRemuneracaoModel>
    {
        Task<IEnumerable<CondicaoRemuneracaoModel>> GetContratoRemuneracaoByIdAsync(int id);
    }
}
