using Azure;
using DevIntel.Application.DTO;
using DevIntel.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIntel.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(UserRegisterDto dto);
        Task<AuthResponse> LoginAsync(LoginDto dto);
        Task<AuthResponse> PromoteUserAsync(PromoteUserDto dto);
    }
}
