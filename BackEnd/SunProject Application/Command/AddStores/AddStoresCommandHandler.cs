global using Microsoft.Extensions.Logging;
global using SunProject_Infrastructure;
global using SunProject_Application.Interface;

namespace SunProject_Application.Command.AddStores
{

    public class AddStoresCommandHandler : IRequestHandler<AddStoresCommandRequest, AddStoresCommandResponse>
    {
        private readonly ILogger<AddStoresCommandHandler> _logger;
        private readonly SunProjectContext _context;
        private readonly IDataReader _dataReader;

        public AddStoresCommandHandler(SunProjectContext context, ILogger<AddStoresCommandHandler> logger, IDataReader dataReader)
        {
            _context = context;
            _logger = logger;
            _dataReader = dataReader;
        }

        public async Task<AddStoresCommandResponse> Handle(AddStoresCommandRequest request, CancellationToken cancellationToken)
        {
            var storeList = await _dataReader.GetExcelDataAsync<Stores>(request.File, cancellationToken);
            if (storeList == null || storeList.Count == 0) return PrepareResponse(null);

            var checkList = _context.Store.Select(x => x.Store).ToList();
            var filteredList = storeList.Where(x => !checkList.Contains(x.Store)).ToList();

            if(filteredList.Count == 0) return PrepareResponse(null);

            await _context.Store.AddRangeAsync(filteredList, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return PrepareResponse(filteredList);
        }

        private AddStoresCommandResponse PrepareResponse(List<Stores> storeList)
        {
            return new AddStoresCommandResponse()
            {
                SuccesfullyInsertedStore = storeList
            };
        }
    }
}
