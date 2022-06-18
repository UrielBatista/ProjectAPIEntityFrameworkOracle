namespace Pro.Search.Infraestructure.ServiceReferences.CepApi.Dtos
{
    public sealed class CepDataResultDto
    {
        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Localidade { get; set; }

        public string Uf { get; set; }
    }
}
