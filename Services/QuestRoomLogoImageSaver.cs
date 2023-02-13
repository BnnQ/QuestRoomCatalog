using Homework.Services.Abstractions;

namespace Homework.Services
{
    public class QuestRoomLogoImageSaver : IFormImageProcessor
    {
        private readonly IWebHostEnvironment environment;
        private readonly IFileNameGenerator fileNameGenerator;

        public QuestRoomLogoImageSaver(IWebHostEnvironment environment, IFileNameGenerator fileNameGenerator)
        {
            this.environment = environment;
            this.fileNameGenerator = fileNameGenerator;
        }

        public string Process(IFormFile formImage)
        {
            string fileName = fileNameGenerator.GenerateFileName(baseFileName: Path.GetFileNameWithoutExtension(formImage.FileName),
                                                                 fileNameExtension: Path.GetExtension(formImage.FileName));

            const string rootDirectory = "media";
            const string controllerDirectory = "QuestRoom";
            const string logosDirectory = "Logos";
            string logoWebRelativePath = Path.Combine(rootDirectory, controllerDirectory, logosDirectory, fileName);

            string logoFullPath = Path.Combine(environment.WebRootPath, logoWebRelativePath);
            using (FileStream fileStream = new(logoFullPath, FileMode.Create, FileAccess.Write))
            {
                using (Stream imageStream = formImage.OpenReadStream())
                    imageStream.CopyTo(fileStream);
            }

            return logoWebRelativePath.Replace("\\", "/");
        }

    }
}