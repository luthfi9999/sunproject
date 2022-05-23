using SunProject_Infrastructure.RelationEntity;

namespace SunProject_Application.Command.AddPromotions
{
    public class AddPromotionsCommandHandler : IRequestHandler<AddPromotionsCommandRequest, AddPromotionsCommandResponse>
    {
        private readonly ILogger<AddPromotionsCommandHandler> _logger;
        private readonly SunProjectContext _context;
        private readonly IDataReader _dataReader;

        public AddPromotionsCommandHandler(SunProjectContext context, ILogger<AddPromotionsCommandHandler> logger, IDataReader dataReader)
        {
            _context = context;
            _logger = logger;
            _dataReader = dataReader;
        }

        public async Task<AddPromotionsCommandResponse> Handle(AddPromotionsCommandRequest request, CancellationToken cancellationToken)
        {
            var itemNames = await _dataReader.GetTxtDataAsync(request.ItemList, cancellationToken);
            if (itemNames == null || itemNames.Count == 0) return PrepareResponse();

            await _context.Promotion.AddAsync(new Promotion()
            {
                Id = request.Id,
                Description = request.Description,
                Type = request.Type,
                Value = request.Value,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            });

            await AddPromotionItem(request.Id, itemNames, cancellationToken);
            await AddPromotionStore(request.Id, request.Stores, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await WriteToTxt(request, itemNames, cancellationToken);

            return PrepareResponse();
        }

        private AddPromotionsCommandResponse PrepareResponse()
        {
            return new AddPromotionsCommandResponse()
            {
                Status = 200,
                Message = "Promotions Successfully Added"
            };
        }

        private async Task AddPromotionItem(string id, List<string> items, CancellationToken cancellationToken)
        {
            foreach(var item in items)
            {
                await _context.Promotion_Item.AddAsync(new Promotion_Item()
                {
                    PromotionId = id,
                    ItemId = item
                }, cancellationToken);
            }
        }

        private async Task AddPromotionStore(string id, List<string> stores, CancellationToken cancellationToken)
        {
            foreach (var store in stores)
            {
                await _context.Promotion_Store.AddAsync(new Promotion_Store()
                {
                    PromotionId = id,
                    StoreId = store
                }, cancellationToken);
            }
        }

        private async Task WriteToTxt(AddPromotionsCommandRequest request, List<string> itemList, CancellationToken cancellationToken)
        {
            List<string> itemsToAdd = new List<string>();
            itemsToAdd.Add($"FHEAD|{request.Description}|||");

            foreach(string item in itemList)
            {
                itemsToAdd.Add($"FITEM|{item}|{request.Type}|{request.Value}");
            }

            foreach(string store in request.Stores)
            {
                itemsToAdd.Add($"FSTORE|{store}|{request.StartDate.ToString("yyyyMMdd")}|{request.EndDate.ToString("yyyyMMdd")}");
            }

            await File.WriteAllLinesAsync($"{request.Id}.txt", itemsToAdd, cancellationToken);
        }
    }
}
