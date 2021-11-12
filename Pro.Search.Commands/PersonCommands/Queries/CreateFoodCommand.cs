using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.Commands.PersonCommands.Queries
{
    public class CreateFoodCommand : ICommand<FoodDto>
    {
        public CreateFoodCommand(FoodDto foodDto)
        {
            this.FoodDto = foodDto;
        }

        public FoodDto FoodDto { get; set; }
    }
}
