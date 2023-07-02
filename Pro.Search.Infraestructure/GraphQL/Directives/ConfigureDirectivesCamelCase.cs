using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.GraphQL.Directives
{
    public class ConfigureDirectivesCamelCase : DirectiveTypeDescriptorAttribute
    {
        protected override void OnConfigure(
            IDescriptorContext context,
            IDirectiveTypeDescriptor descriptor,
            Type type)
        {
            _ = descriptor.Use((next, directive) =>
            {
                {
                    var modified = char.ToUpper(directive.Type.Name[0]) +
                    directive.Type.Name[1..];

                    return async context =>
                    {
                        await next(context);

                        if (modified == nameof(ToUpperCase)
                            && context.Result is string u)
                        {
                            context.Result = u.ToUpper();
                        }
                        if (modified == nameof(ToLowerCase)
                            && context.Result is string l)
                        {
                            context.Result = l.ToLower();
                        }
                    };
                }
            });
        }
    }
}

