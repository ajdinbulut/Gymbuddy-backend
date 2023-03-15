namespace Gymbuddy.Utilities
{
    public class FileManager
    {
        private string? postFile;
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly IConfiguration _configuration;
        public FileManager(IWebHostEnvironment hostEnviroment, IConfiguration configuration)
        {
            _hostEnviroment = hostEnviroment;
            _configuration = configuration;
        }

        public string postUpload(IFormFile? file)
        {
            string wwwRootPath = _hostEnviroment.WebRootPath;
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(@"wwwroot\images");
            var extension = Path.GetExtension(file.FileName);
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                file.CopyTo(fileStreams);
            }
            var apiUrl = _configuration.GetValue<string>("ApiUrl");
            return postFile = (apiUrl + "/images/" + fileName + extension).Replace("\\", "/");


        }
        public string profilePhoto(IFormFile? file)
        {

            string wwwRootPath = _hostEnviroment.WebRootPath;
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(wwwRootPath, @"images\profilePhotos");
            var extension = Path.GetExtension(file.FileName);
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                file.CopyTo(fileStreams);
            }
            return postFile = @"\images\profilePhotos\" + fileName + extension;


        }
    }
}
