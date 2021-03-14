using System.IO;
using System.Threading.Tasks;

namespace VUTProjectApp.Services
{
    public interface IFileStorage
    {
        Task<string> EditFile(MemoryStream content, string extension, string containerName, string fileRoute);
        Task DeleteFile(string fileRoute, string containerName);
        Task<string> SaveFile(MemoryStream content, string extension, string containerName);
    }
}
