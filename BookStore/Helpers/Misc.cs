using System;
using BookStore.Services;

namespace BookStore.Helpers
{
	public class Misc
	{
		public Misc()
		{
		}

		public static string UploadFile(IFormFile file, IWebHostEnvironment environment)
		{
			var folder = Path.Combine(environment.WebRootPath, "Thumbnails");
            string fileExtension = Path.GetExtension(file.FileName);
            var acceptedType = new[] { "jpg", "png", "jpeg", "gif", "JPEG" };
            string fileName = $"{Misc.GenerateReference()}{fileExtension}";

            if (acceptedType.Contains(fileExtension.Substring(1)))
            {
                string storagePath = Path.Combine(folder, fileName);
                FileStream stream = new FileStream(path: storagePath, mode: FileMode.Create);
                file.CopyTo(stream);

                stream.Dispose();

                

                return fileName;
            }
            else
            {
                throw new Exception("Unsupported file type.");
            }
        }

        public static string GenerateReference()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

