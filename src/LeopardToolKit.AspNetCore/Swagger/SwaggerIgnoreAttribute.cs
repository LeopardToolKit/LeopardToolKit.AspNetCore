using System;

namespace LeopardToolKit.AspNetCore.Swagger
{
    /// <summary>
    /// Mark an action is not exposed to swagger
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerIgnoreAttribute : Attribute
    {
    }
}
