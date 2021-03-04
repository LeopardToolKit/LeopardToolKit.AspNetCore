using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LeopardToolKit.AspNetCore.Swagger
{
    class PostSwaggerOption : IPostConfigureOptions<SwaggerOption>
    {
        internal const string DefaultVersionParameterName = "api-version";

        public void PostConfigure(string name, SwaggerOption options)
        {
            if (string.IsNullOrEmpty(options.CommentsFilename))
            {
                options.CommentsFilename = Assembly.GetEntryAssembly().GetName().Name;
            }

            if (string.IsNullOrEmpty(options.VersionParameterName))
            {
                options.VersionParameterName = DefaultVersionParameterName;
            }
        }
    }
}
