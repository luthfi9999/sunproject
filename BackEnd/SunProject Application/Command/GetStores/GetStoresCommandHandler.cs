using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunProject_Application.Command.GetStores
{
    public class GetStoresCommandHandler : IRequestHandler<GetStoresCommandRequest, GetStoreCommandResponse>
    {
        private readonly ILogger<GetStoresCommandHandler> _logger;
        private readonly SunProjectContext _context;
        private readonly IDataReader _dataReader;

        public GetStoresCommandHandler(SunProjectContext context, ILogger<GetStoresCommandHandler> logger, IDataReader dataReader)
        {
            _context = context;
            _logger = logger;
            _dataReader = dataReader;
        }

        public async Task<GetStoreCommandResponse> Handle(GetStoresCommandRequest request, CancellationToken cancellationToken)
        {
            List<Stores> storeList = _context.Store.ToList();
            return new GetStoreCommandResponse()
            {
                StoreList = storeList
            };
        }
    }
}
