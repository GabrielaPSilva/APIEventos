using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace DUDS
{
    public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public SwaggerConfigureOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var desc in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(desc.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Dahlia Unified Data Service",
                    Version = "DUDS v" + desc.ApiVersion.ToString().Split(".")[0],
                    Description = "This is a Web API for Movies operations",
                    TermsOfService = new Uri("http://www.dahliacapital.com.br"),
                    License = new OpenApiLicense()
                    {
                        Name = "MIT"
                    },
                    Contact = new OpenApiContact()
                    {
                        Name = "Dahlia DevOps",
                        Email = "dahlia.devops@dahliacapuital.com.br",
                        Url = new Uri("http://www.dahliacapital.com.br")
                    }
                });
            }
        }
    }
}
