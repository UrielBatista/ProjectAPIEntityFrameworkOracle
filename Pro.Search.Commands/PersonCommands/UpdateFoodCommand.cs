using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.Commands.PersonCommands
{
    public class UpdateFoodCommand : ICommand<FoodDto>
    {
        public UpdateFoodCommand(FoodDto foodDto)
        {
            this.FoodDto = foodDto;
        }

        public FoodDto FoodDto { get; set; }
    }
}
