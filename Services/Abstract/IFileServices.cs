namespace WebAppForFile.Services.Abstract
{
    public interface IFileServices
    {
        void UploadFileToAzure(IFormFile file);
    }
}
