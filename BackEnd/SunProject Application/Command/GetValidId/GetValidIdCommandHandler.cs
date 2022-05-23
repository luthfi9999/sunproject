using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunProject_Application.Command.GetValidId
{
    public class GetValidIdCommandHandler : IRequestHandler<GetValidIdCommandRequest, GetValidIdCommandResponse>
    {
        private readonly ILogger<GetValidIdCommandHandler> _logger;
        private readonly SunProjectContext _context;
        private readonly IDataReader _dataReader;

        public GetValidIdCommandHandler(SunProjectContext context, ILogger<GetValidIdCommandHandler> logger, IDataReader dataReader)
        {
            _context = context;
            _logger = logger;
            _dataReader = dataReader;
        }

        public async Task<GetValidIdCommandResponse> Handle(GetValidIdCommandRequest request, CancellationToken cancellationToken)
        {
            DirectoryInfo d = new DirectoryInfo(@"generatedFiles/"); //Assuming Test is your Folder

            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
            string newFile = "P";

            if (Files.Length == 0)
            {
                newFile += $"{DateTime.Today.ToString("yyyyMMdd")}0001";
            }
            else
            {
                var lastName = Path.GetFileNameWithoutExtension( Files.OrderByDescending(x => x.Name).FirstOrDefault().Name);
                var getLatestDateStr = lastName.Substring(1, 8);
                var getLatestDate = DateTime.ParseExact(getLatestDateStr, "yyyyMMdd", null);
                if (DateTime.Today == getLatestDate)
                {
                    newFile += getLatestDateStr;
                    var z = lastName.Substring(9, 4);
                    var seq = Int32.Parse(lastName.Substring(9, 4)) + 1;
                    newFile += seq.ToString("0000");
                }
                else
                {
                    newFile += $"{DateTime.Today.ToString("yyyyMMdd")}0001";
                }
            }

            string file = newFile + ".txt";

            File.Create($"generatedFiles/{file}").Dispose();
            return new GetValidIdCommandResponse()
            {
                Id = newFile
            };
        }
    }
}
