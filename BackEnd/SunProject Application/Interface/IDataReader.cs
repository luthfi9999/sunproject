global using Microsoft.AspNetCore.Http;

namespace SunProject_Application.Interface
{
    public interface IDataReader
    {
        public Task<List<T>> GetExcelDataAsync<T> (IFormFile file, CancellationToken token) where T : class;

        public Task<List<string>> GetTxtDataAsync(IFormFile file, CancellationToken token);
    }
}
