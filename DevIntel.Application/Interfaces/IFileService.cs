using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIntel.Application.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Saves the uploaded file and returns a relative URL path (e.g. "/uploads/abc.png").
        /// </summary>
        Task<string> SaveAsync(IFormFile file);
    }
}
