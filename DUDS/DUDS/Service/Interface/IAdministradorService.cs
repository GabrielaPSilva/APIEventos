﻿using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IAdministradorService : IGenericOperationsService<AdministradorModel>
    {
        Task<AdministradorModel> GetAdministradorExistsBase(string cnpj, string nome);
    }
}
