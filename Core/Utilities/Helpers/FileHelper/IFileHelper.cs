using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers.FileHelper.Abstract
{
    public interface IFileHelper
    {
        string Add(IFormFile file, string root);

        string Update(IFormFile file, string filePath, string root);

        void Delete(string filePath);
    }
}
