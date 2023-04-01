namespace Pro.Search.PersonDomains.PersonEngine.Dtos
{
    public class ValidityFlagDto
    {
        public ValidityFlagDto(bool existPerson, bool existFood)
        {
            ExistPerson = existPerson;
            ExistFood = existFood;
        }

        public bool ExistPerson { get; set; }

        public bool ExistFood { get; set; }
    }
}
