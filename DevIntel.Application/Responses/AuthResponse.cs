using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIntel.Application.Responses
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string? Token { get; set; }

        public AuthResponse(string message, bool success = false, string token = null )
        {
            Message = message;
            Success = success;
            Token = token;
        }

    }
}
