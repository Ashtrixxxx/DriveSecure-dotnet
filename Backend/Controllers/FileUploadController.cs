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

        // Initialize Google Drive credentials
        var credential = GoogleCredential.FromFile("C:\\Users\\Ashtrixx\\Downloads\\swift-cursor-436310-c3-36484a557f82.json")
            .CreateScoped(Scopes);

        // Initialize the DriveService
        var service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });

        // Set up the metadata for the file
        var fileMetadata = new Google.Apis.Drive.v3.Data.File()
        {
            Name = file.FileName,
            MimeType = file.ContentType
        };

        // Upload the file to Google Drive
        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            var request = service.Files.Create(fileMetadata, stream, file.ContentType);
            request.Fields = "id, webViewLink"; // "webContentLink" removed for file viewing
            var fileResponse = await request.UploadAsync();

            // Check if the upload was successful
            if (fileResponse.Status != UploadStatus.Completed)
            {
                return BadRequest("Error uploading file.");
            }

            var fileId = request.ResponseBody.Id;
            var webViewLink = request.ResponseBody.WebViewLink; // Link to view the file

            // Set permissions to allow anyone with the link to view the file
            var permission = new Google.Apis.Drive.v3.Data.Permission()
            {
                Type = "anyone",   // Make the file accessible to anyone
                Role = "reader"    // Provide read-only access
            };

            try
            {
                // Assign the "anyone" permission to the uploaded file
                await service.Permissions.Create(permission, fileId).ExecuteAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error setting permissions: {ex.Message}");
            }

            return Ok(new { fileId, fileLink = webViewLink });
        }
    }
}
