using Microsoft.AspNetCore.Mvc;
using SunProject_Application.Command.AddPromotions;
using SunProject_Application.Command.AddStores;
using SunProject_Application.Command.GetStores;
using SunProject_Application.Command.GetValidId;
using SunProject_Infrastructure;

namespace SunProject_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoresController : BaseController
    {
        [HttpPost("/UploadStores")]
        public async Task<ActionResult<AddStoresCommandResponse>> AddStores([FromForm] AddStoresCommandRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("/UploadPromotions")]
        public async Task<ActionResult<AddPromotionsCommandResponse>> AddPromotions([FromForm] AddPromotionsCommandRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpGet("/")]
        public async Task<ActionResult<GetStoreCommandResponse>> GetStores()
        {
            return await Mediator.Send(new GetStoresCommandRequest());
        }

        [HttpGet("/Id")]
        public async Task<ActionResult<GetValidIdCommandResponse>> GetId()
        {
            return await Mediator.Send(new GetValidIdCommandRequest());
        }
    }
}