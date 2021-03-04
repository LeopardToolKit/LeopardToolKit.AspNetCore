namespace LeopardToolKit.AspNetCore.Swagger
{
    public class SwaggerOption
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsStartPage { get; set; }

        public string CommentsFilename { get; set; }

        public bool EnableVersion { get; set; }

        public string VersionParameterName { get; set; }
    }
}
