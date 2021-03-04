using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LeopardToolKit.AspNetCore.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly SwaggerOption swaggerOption;
        private readonly IServiceProvider _serviceProvider;

        public ConfigureSwaggerOptions(IServiceProvider serviceProvider, IOptions<SwaggerOption> swaggerOptionAccessor)
        {
            this.swaggerOption = swaggerOptionAccessor.Value;
            _serviceProvider = serviceProvider;
        }
    
        public void Configure(SwaggerGenOptions options)
        {
            if (swaggerOption.EnableVersion)
            {
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    return !apiDesc.ActionDescriptor.EndpointMetadata.OfType<SwaggerIgnoreAttribute>().Any() && apiDesc.GroupName == docName;
                });
            }
            else
            {
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    return !apiDesc.ActionDescriptor.EndpointMetadata.OfType<SwaggerIgnoreAttribute>().Any();
                });
            }

            // Set the comments path for the Swagger JSON and UI.
            var xmlPath = Path.Combine(Directory.GetCurrentDirectory(), $"{this.swaggerOption.CommentsFilename}.xml");

            if (!File.Exists(xmlPath))
            {
                string location = Assembly.GetEntryAssembly().Location;
                xmlPath = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(location)), $"{this.swaggerOption.CommentsFilename}.xml");
            }
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
            
            if (swaggerOption.EnableVersion)
            {
                var provider = _serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                      description.GroupName,
                        new OpenApiInfo()
                        {
                            Title = $"{swaggerOption.Title} {description.ApiVersion}",
                            Version = description.ApiVersion.ToString(),
                            Description = swaggerOption.Description
                        });
                }

            }
            else
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = swaggerOption.Title, Version = "v1", Description = swaggerOption.Description });
            }

            options.OperationFilter<SwaggerDefaultValues>();
            options.SchemaFilter<AnnotationSchemaFilter>();
        }
    }
}
