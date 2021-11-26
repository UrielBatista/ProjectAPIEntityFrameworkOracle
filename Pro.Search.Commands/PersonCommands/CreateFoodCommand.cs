using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.Commands.PersonCommands
{
    public class CreateFoodCommand : ICommand<FoodAllInfoDto>
    {
        public CreateFoodCommand(FoodAllInfoDto foodAllInfoDto)
        {
            this.FoodAllInfoDto = foodAllInfoDto;
        }

        public FoodAllInfoDto FoodAllInfoDto { get; set; }
    }
}
