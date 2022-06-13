using System;

namespace Pro.Search.PersonDomains.PersonEngine.Events
{
    public interface PersonCreatedEvent
    {
        string Id_Pessoas { get; }

        string Nome { get; }

        string Sobrenome { get; }

        string Email { get; }

        decimal Pessoas_Calc_Number { get; }

        DateTime DataHora { get; }
    }
}
