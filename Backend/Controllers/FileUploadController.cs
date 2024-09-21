using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FileUploadController : ControllerBase
{
    private readonly string[] Scopes = { DriveService.Scope.DriveFile };
    private readonly string ApplicationName = "GoogleDriveProject";

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var credential = GoogleCredential.FromFile("C:\\Users\\Ashtrixx\\Downloads\\swift-cursor-436310-c3-36484a557f82.json")
            .CreateScoped(Scopes);

        var service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });

        var fileMetadata = new Google.Apis.Drive.v3.Data.File()
        {
            Name = file.FileName,
            MimeType = file.ContentType
        };

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            var request = service.Files.Create(fileMetadata, stream, file.ContentType);
            request.Fields = "id, webContentLink";
            var fileResponse = await request.UploadAsync();

            if (fileResponse.Status != UploadStatus.Completed)
            {
                return BadRequest("Error uploading file.");
            }

            var fileId = request.ResponseBody.Id;
            var webContentLink = request.ResponseBody.WebContentLink;

            // Set permissions for the uploaded file
            var permission = new Google.Apis.Drive.v3.Data.Permission()
            {
                Type = "user", // or "anyone" if you want to share it publicly
                Role = "writer", // "reader" for read-only access
                EmailAddress = "ashwin94429@gmail.com" // Replace with the email you want to share with
            };

            try
            {
                await service.Permissions.Create(permission, fileId).ExecuteAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error setting permissions: {ex.Message}");
            }

            return Ok(new { fileId, fileLink = webContentLink });
        }
    }
}
