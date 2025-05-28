using DevIntel.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIntel.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        public async Task<string?> SaveImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var folderPath = Path.Combine("wwwroot", "uploads");
            var filePath = Path.Combine(folderPath, fileName);

            Directory.CreateDirectory(folderPath); // Ensure folder exists

            using var stream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            return $"/uploads/{fileName}";
        }

    }
}
