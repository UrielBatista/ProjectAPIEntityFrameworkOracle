using MediatR;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.Commands.PersonCommands
{
    public class CreateFoodInMemoryCommand : IRequest<FoodAllInfoDto>
    {
        public CreateFoodInMemoryCommand(FoodAllInfoDto foodAllInfoDto)
        {
            this.FoodAllInfoDto = foodAllInfoDto;
        }

        public FoodAllInfoDto FoodAllInfoDto { get; set; }
    }
}
