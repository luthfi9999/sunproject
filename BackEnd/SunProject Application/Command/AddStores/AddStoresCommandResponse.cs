global using SunProject_Infrastructure.Entity;

namespace SunProject_Application.Command.AddStores
{
    public class AddStoresCommandResponse
    {
        public List<Stores> SuccesfullyInsertedStore { get; set; }
    }
}
