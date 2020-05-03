using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MessageService.Swagger.Filters
{
    /// <summary>
    /// Представляет фильтр операций Swagger/Swashbuckle, используемый для документирования неявного параметра версии API.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class OperationApiVersionFilter : IOperationFilter
    {
        /// <inheritdoc/>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated |= apiDescription.IsDeprecated();

            if (operation.Parameters == default(IList<IParameter>))
            {
                return;
            }

            foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                if (parameter.Description == default)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (parameter.Default == default)
                {
                    parameter.Default = description.DefaultValue;
                }
            }
        }
    }
}
