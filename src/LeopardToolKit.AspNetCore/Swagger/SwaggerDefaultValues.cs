using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace LeopardToolKit.AspNetCore.Swagger
{
    public class SwaggerDefaultValues : IOperationFilter
    {
        private readonly SwaggerOption _swaggerOption;

        public SwaggerDefaultValues(IOptions<SwaggerOption> swaggerOption)
        {
            _swaggerOption = swaggerOption.Value;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated |= apiDescription.IsDeprecated();

            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters.Where(p => p.Name?.Equals(_swaggerOption.VersionParameterName, StringComparison.OrdinalIgnoreCase) ?? false))
            {
                var apiParameter = apiDescription.ParameterDescriptions
                                                .First(p => p.Name == parameter.Name);

                if (parameter.Description == null)
                {
                    parameter.Description = apiParameter.ModelMetadata?.Description;
                }

                if (parameter.Schema.Default == null)
                {
                    parameter.Schema.Default = new OpenApiString(apiParameter.DefaultValue.ToString());
                }

                parameter.Required |= apiParameter.IsRequired;
            }
        }
    }
}
