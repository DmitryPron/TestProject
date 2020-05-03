using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;

namespace MessageService.Swagger.Filters
{
    /// <summary>
    /// Фильтр добавления базовой авторизации для каждой операции <see cref="IOperationFilter"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class OperationApiBasicSecurityFilter : IOperationFilter
    {
        /// <inheritdoc/>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var requiredScopes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>()
                .Select(attr => attr.AuthenticationSchemes);

            if (requiredScopes.Any())
            {
                operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                {
                    new Dictionary<string, IEnumerable<string>>
                    {
                        {
                            nameof(AuthenticationSchemes.Basic),
                            requiredScopes
                        }
                    }
                };

                operation.Responses.Add(
                    StatusCodes.Status401Unauthorized.ToString(),
                    new Response { Description = "Неавторизованный пользователь" });
            }
        }
    }
}