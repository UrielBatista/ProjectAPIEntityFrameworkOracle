using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using Pro.Search.PersonDomains.PersonEngine.Entities;

namespace Pro.Search.Infraestructure.GraphQL.Directives
{
    public class ConfigureDirectivesOfDistinct : DirectiveTypeDescriptorAttribute
    {
        protected override void OnConfigure(
            IDescriptorContext context,
            IDirectiveTypeDescriptor descriptor,
            Type type)
        {
            _ = descriptor.Use((next, directive) =>
            {
                {
                    var modified = directive.Type.Name;

                    return async context =>
                    {
                        await next(context).ConfigureAwait(false);

                        List<object> numbers = new List<object> { 
                            new {
                                Id_Pessoa = "0001",
                                Nome = "Juliana",
                                Sobrenome= "Tavares",
                                Email = "ju_tava@email.com",
                                Pessoas_Calc_Number = 642,
                                DataHora = "2023-10-19T23:53:17.142Z"
                            },
                            new {
                                Id_Pessoa = "0002",
                                Nome = "Juliana",
                                Sobrenome= "Tavares",
                                Email = "ju_tava@email.com",
                                Pessoas_Calc_Number = 642,
                                DataHora = "2023-10-19T23:53:17.142Z"
                            },
                            new {
                                Id_Pessoa = "0003",
                                Nome = "Juliana",
                                Sobrenome= "Tavares",
                                Email = "ju_tava@email.com",
                                Pessoas_Calc_Number = 642,
                                DataHora = "2023-10-19T23:53:17.142Z"
                            },
                            new {
                                Id_Pessoa = "0004",
                                Nome = "Juliana",
                                Sobrenome= "Tavares",
                                Email = "ju_tava@email.com",
                                Pessoas_Calc_Number = 642,
                                DataHora = "2023-10-19T23:53:17.142Z"
                            },
                            new {
                                Id_Pessoa = "0005",
                                Nome = "Juliana",
                                Sobrenome= "Tavares",
                                Email = "ju_tava@email.com",
                                Pessoas_Calc_Number = 642,
                                DataHora = "2023-10-19T23:53:17.142Z"
                            },
                            new {
                                Id_Pessoa = "0006",
                                Nome = "Juliana",
                                Sobrenome= "Tavares",
                                Email = "ju_tava@email.com",
                                Pessoas_Calc_Number = 642,
                                DataHora = "2023-10-19T23:53:17.142Z"
                            },
                            new {
                                Id_Pessoa = "0007",
                                Nome = "Juliana",
                                Sobrenome= "Tavares",
                                Email = "ju_tava@email.com",
                                Pessoas_Calc_Number = 642,
                                DataHora = "2023-10-19T23:53:17.142Z"
                            } };
                        var distinctNumbers = numbers.Distinct();

                        if (context.Result is IQueryable<object> queryable)
                        {
                            context.Result = queryable.Distinct();
                        }
                    };
                }
            });
        }
    }
}