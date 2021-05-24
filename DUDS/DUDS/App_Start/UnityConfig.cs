using DUDS.BLL;
using DUDS.BLL.Interfaces;
using DUDS.DAL;
using DUDS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace DUDS
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            //Injeção das BLLs
            container.RegisterType<ILogErrorBLL, LogErrorBLL>();

            //Injeção das DALs
            container.RegisterType<ILogErrorDAL, LogErrorDAL>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}