using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace LeopardToolKit.AspNetCore.Swagger
{
    internal class AnnotationSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if(context.MemberInfo != null && schema.Description == null)
            {
                var swaggerSchemaAttribute = context.MemberInfo.GetCustomAttribute<SwaggerSchemaAttribute>();
                if(swaggerSchemaAttribute != null)
                {
                    schema.Description = swaggerSchemaAttribute.Description;
                }
            }
        }
    }
}
