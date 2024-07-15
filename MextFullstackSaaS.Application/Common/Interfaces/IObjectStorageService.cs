namespace MextFullstackSaaS.Application.Common.Interfaces
{
    public interface IObjectStorageService
    {
        Task<string> UploadImageAsync(string imageData, CancellationToken cancellationToken);
        Task<List<string>> UploadImagesAsync(List<string> imagesData, CancellationToken cancellationToken);
         Task<bool> RemoveAsync(string url, CancellationToken cancellationToken);
        Task<bool> RemoveAsync(List<string> urls, CancellationToken cancellationToken);
        
        
    }
}