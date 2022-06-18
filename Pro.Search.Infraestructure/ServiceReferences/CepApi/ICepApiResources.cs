using Pro.Search.Infraestructure.ServiceReferences.CepApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.ServiceReferences.CepApi
{
    public interface ICepApiResources
    {
        Task<object> GetLocalization(string cep);
    }
}
