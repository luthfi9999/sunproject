global using System.ComponentModel.DataAnnotations;
global using MediatR;

namespace SunProject_Application.Command.AddStores
{
    public class AddStoresCommandRequest : IRequest<AddStoresCommandResponse>
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
