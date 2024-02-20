
using P5_Express_Voitures_Identity.Models;

namespace P5_Express_Voitures_Identity.Models.Service
{
    public class ImageService
    {
        private readonly PathService? pathService;

        public ImageService(PathService? pathService)
        {
            this.pathService = pathService;
        }

        public async Task<Photo> UploadAsync(Photo photo)
        {
            var uploadPath = pathService.GetUploadsPath();
            var imageFile = photo.Fichier;
            var imageFileName = GetRandomFileName(imageFile.FileName);
            var imageUploadPath = Path.Combine(uploadPath, imageFileName);

            using (var fs = new FileStream(imageUploadPath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fs);
            }

            photo.Nom = imageFile.FileName;
            photo.LienPhoto = pathService.GetUploadsPath(imageFileName, withWebRootPath: false);

            return photo;

        }

        private string GetRandomFileName(string filename)
        {
            return Guid.NewGuid() + Path.GetExtension(filename);
        }
    }
}
