using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIntel.Application.DTO
{
    public class CreateIntelDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new();
        // For image upload, you might handle as a Base64 string or separate file upload endpoint
        public string? ImagePath { get; set; }
    }
}
