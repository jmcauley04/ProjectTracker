using Microsoft.AspNetCore.Components.Forms;
using ProjectTracker.Shared.Attributes;

namespace ProjectTracker.Services;

[InjectService(InjectServiceAttribute.ServiceLifetime.Singleton)]
public class FileService
{
    const int _maxWidth = 480;
    const int _maxHeight = 640;

    private readonly IConfiguration _configuration;

    public FileService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> SaveImage(IBrowserFile file)
    {
        var path = GetPath();
        var fileName = GetValidFilename(path, file.Name);
        await WriteImage(file, Path.Combine(path, fileName));

        return fileName;
    }

    static async Task WriteImage(IBrowserFile file, string fullPath)
    {
        var resizedFile = await file.RequestImageFileAsync(file.ContentType, _maxWidth, _maxHeight);
        await using FileStream fs = new(fullPath, FileMode.Create);
        await resizedFile.OpenReadStream().CopyToAsync(fs);
    }

    /// <summary>
    /// Returns the (fullPath, fileName) of the file.  The fullPath includes the fileName.
    /// </summary>
    static string GetValidFilename(string path, string uploadedFilename)
    {
        string fileName;

        do
        {
            var guid = Guid.NewGuid();
            var ext = uploadedFilename.Split('.').Last();
            fileName = $"{guid}.{ext}";
        } while (File.Exists(Path.Combine(path, fileName)));

        return fileName;
    }

    string GetPath()
    {
        try
        {
            string path = _configuration["ImageStore"]!;
            Directory.CreateDirectory(path);
            return path;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
