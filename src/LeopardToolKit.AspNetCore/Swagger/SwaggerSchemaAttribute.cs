using System;
using System.Collections.Generic;
using System.Text;

namespace LeopardToolKit.AspNetCore.Swagger
{
    public class SwaggerSchemaAttribute : Attribute
    {
        public string Description { get; private set; }

        public SwaggerSchemaAttribute(string description)
        {
            this.Description = description;
        }
    }
}
