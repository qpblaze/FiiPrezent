using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FiiPrezent.Core.Interfaces
{
    public interface IFileManager
    {
        /// <summary>
        /// Upload a file to the server.
        /// </summary>
        /// <param name="file">The file to be uploaded.</param>
        /// <returns>The path to the uploaded file.</returns>
        Task<string> UploadAsync(IFormFile file);

        /// <summary>
        /// Delete a file from server.
        /// </summary>
        /// <param name="path">The path to the file to delete.</param>
        void Delete(string path);
    }
}