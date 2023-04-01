using MediatR;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.Commands.PersonCommands
{
    public class UpdateFoodCommand : IRequest<FoodAllInfoDto>
    {
        public UpdateFoodCommand(FoodAllInfoDto foodAllInfoDto)
        {
            this.FoodAllInfoDto = foodAllInfoDto;
        }

        public FoodAllInfoDto FoodAllInfoDto { get; set; }
    }
}
