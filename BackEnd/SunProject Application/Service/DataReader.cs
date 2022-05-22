using ExcelMapper;
using SunProject_Application.Interface;

namespace SunProject_Application.Service
{
    public class DataReader : IDataReader
    {
        public async Task<List<T>> GetExcelDataAsync<T>(IFormFile file, CancellationToken token) where T : class
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            List<T> list  = new List<T>();

            using(var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, token);
                stream.Position = 0;
                using(var importer = new ExcelImporter(stream))
                {
                    ExcelSheet sheet = importer.ReadSheet();
                    list = sheet.ReadRows<T>().ToList();
                }
            }

            return list;
        }

        public async Task<List<string>> GetTxtDataAsync(IFormFile file, CancellationToken token)
        {
            List<string> list = new List<string>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, token);
                stream.Position = 0;
                using(TextReader reader = new StreamReader(stream))
                {
                    string line;
                    
                    //Skip Header
                    await reader.ReadLineAsync();

                    //Get ContentData
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        list.Add(line);
                    }
                }
            }

            return list;
        }
    }
}
