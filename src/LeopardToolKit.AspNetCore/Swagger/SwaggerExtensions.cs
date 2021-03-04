using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LeopardToolKit.AspNetCore.Swagger
{
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Please add these properties in .csproj project file.
        /// <GenerateDocumentationFile>true</GenerateDocumentationFile>
        /// <NoWarn>$(NoWarn);1591</NoWarn>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Used to config the <see cref="SwaggerOption"/> option</param>
        /// <returns></returns>
        public static IServiceCollection AddOpenApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SwaggerOption>(configuration);
            if(configuration.GetValue("EnableVersion", false))
            {
                string verionParameterName = configuration.GetValue("VersionParameterName", PostSwaggerOption.DefaultVersionParameterName);
                services.AddApiVersioning(versionOption =>
                {
                    versionOption.DefaultApiVersion = ApiVersion.Default;// new ApiVersion(1,0);
                    versionOption.ReportApiVersions = true;// Report version info in response header
                    versionOption.UseApiBehavior = true; // Only include the Controller marked with ApiControllerAttribute
                    versionOption.ApiVersionReader = new QueryStringApiVersionReader(verionParameterName);
                    versionOption.AssumeDefaultVersionWhenUnspecified = true;
                });
                services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            }

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddTransient<IPostConfigureOptions<SwaggerOption>, PostSwaggerOption>();
            services.AddSwaggerGen();
            return services;
        }

        public static IApplicationBuilder UseOpenApiUI(this IApplicationBuilder app)
        {
            var swaggerOption = app.ApplicationServices.GetRequiredService<IOptions<SwaggerOption>>().Value;
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (swaggerOption.EnableVersion)
                {
                    var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint(
                            swaggerOption.IsStartPage ? $"swagger/{description.GroupName}/swagger.json" : $"{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                }
                else
                {
                    c.SwaggerEndpoint(swaggerOption.IsStartPage ? $"swagger/v1/swagger.json" : $"v1/swagger.json", "v1");
                }
             

                if (swaggerOption.IsStartPage)
                {
                    c.RoutePrefix = string.Empty;
                }
            });
            return app;
        }
    }
}
