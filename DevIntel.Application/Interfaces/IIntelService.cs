
using DevIntel.Application.DTO;
using DevIntel.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIntel.Application.Interfaces
{
    public interface IIntelService
    {
        Task<Response<IntelDto>> CreateAsync(CreateIntelDto dto, Guid userId);
        Task<Response<List<IntelDto>>> GetAllAsync(Guid userId);
        Task<Response<IntelDto>> GetByIdAsync(Guid id, Guid userId);
    }
}
