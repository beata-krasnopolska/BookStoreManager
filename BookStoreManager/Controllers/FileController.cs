using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

namespace BookStoreManager.Controllers
{
    [Route("file")]
    [Authorize]
    public class FileController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetFile([FromQuery] string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();

            var filePath = $"{rootPath}/PrivateFiles/{fileName}";

            var fileExist = System.IO.File.Exists(filePath);

            if (!fileExist)
            {
                return NotFound();
            }

            var fileContentProvider = new FileExtensionContentTypeProvider();
            fileContentProvider.TryGetContentType(fileName, out string fileContentType);

            var fileContents = System.IO.File.ReadAllBytes(filePath);

            return File(fileContents, fileContentType, fileName);
        }
    }
}
