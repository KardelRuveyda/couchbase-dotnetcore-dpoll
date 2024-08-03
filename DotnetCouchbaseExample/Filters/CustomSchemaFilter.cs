using DotnetCouchbaseExample.Models;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotnetCouchbaseExample.Filters
{
    public class CustomSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(UserInfo))
            {
                if (schema.Properties.ContainsKey("presentations"))
                {
                    schema.Properties.Remove("presentations");
                }

                if (schema.Properties.ContainsKey("slides"))
                {
                    schema.Properties.Remove("slides");
                }
                if (schema.Properties.ContainsKey("questions"))
                {
                    schema.Properties.Remove("questions");
                }
                if (schema.Properties.ContainsKey("options"))
                {
                    schema.Properties.Remove("options");
                }
            }

            if (context.Type == typeof(Presentation))
            {
                if (schema.Properties.ContainsKey("slides"))
                {
                    schema.Properties.Remove("slides");
                }
                if (schema.Properties.ContainsKey("questions"))
                {
                    schema.Properties.Remove("questions");
                }
                if (schema.Properties.ContainsKey("options"))
                {
                    schema.Properties.Remove("options");
                }
            }

            if (context.Type == typeof(Slide))
            {
                if (schema.Properties.ContainsKey("questions"))
                {
                    schema.Properties.Remove("questions");
                }
                if (schema.Properties.ContainsKey("options"))
                {
                    schema.Properties.Remove("options");
                }
            }

            if (context.Type == typeof(Question))
            {
                if (schema.Properties.ContainsKey("options"))
                {
                    schema.Properties.Remove("options");
                }
            }
        }
    }
}
