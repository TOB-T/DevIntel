using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DevIntel.API.Swagger
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasFormFile = context.ApiDescription.ParameterDescriptions
                .Any(p => p.Type == typeof(IFormFile) || (p.ModelMetadata?.ElementType == typeof(IFormFile)));

            if (!hasFormFile)
                return;

            operation.RequestBody = new OpenApiRequestBody
            {
                Content =
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties =
                            {
                                ["title"] = new OpenApiSchema { Type = "string" },
                                ["description"] = new OpenApiSchema { Type = "string" },
                                ["tags"] = new OpenApiSchema { Type = "array", Items = new OpenApiSchema { Type = "string" } },
                                ["image"] = new OpenApiSchema { Type = "string", Format = "binary" }
                            },
                            Required = new HashSet<string> { "title", "description", "tags", "image" }
                        }
                    }
                }
            };
        }
    }
}
