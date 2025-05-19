
using DevIntel.Application.DTO;
using DevIntel.Application.Interfaces;
using DevIntel.Application.Responses;
using DevIntel.Domain.Entities;
using DevIntel.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIntel.Infrastructure.Services
{
    public class IntelService : IIntelService
    {
        private readonly AppDbContext _context;

        public IntelService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<IntelDto>> CreateAsync(CreateIntelDto dto, Guid userId)
        {
            var entry = new IntelEntry
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Tags = string.Join(',', dto.Tags),
                ImagePath = dto.ImagePath,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow
            };
            _context.IntelEntries.Add(entry);
            await _context.SaveChangesAsync();

            var intel = new IntelDto
            {
                Id = entry.Id,
                Title = entry.Title,
                Description = entry.Description,
                Tags = entry.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                ImagePath = entry.ImagePath,
                CreatedAt = entry.CreatedAt
            };

            return new Response<IntelDto>(intel, "Intel entry created.", true);
        }

        public async Task<Response<List<IntelDto>>> GetAllAsync(Guid userId)
        {
            // 1. Fetch all entries created by this user, most recent first
            var entries = await _context.IntelEntries
                .Where(e => e.CreatedById == userId)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();


            // 2. Map to DTOs, splitting the comma-separated tags into a list
            var dtos = entries.Select(entry => new IntelDto
            {
                Id = entry.Id,
                Title = entry.Title,
                Description = entry.Description,
                Tags = entry.Tags
                             .Split(',', StringSplitOptions.RemoveEmptyEntries)
                             .ToList(),
                ImagePath = entry.ImagePath,
                CreatedAt = entry.CreatedAt
            }).ToList();

            // 3. Wrap in your Response<T> and return
            return new Response<List<IntelDto>>(dtos,
                "Intel entries retrieved successfully.",
                true);
        }

        public async Task<Response<IntelDto>> GetByIdAsync(Guid id, Guid userId)
        {
            // 1. Find the entry by ID and ensure it belongs to the user
            var entry = await _context.IntelEntries
                .FirstOrDefaultAsync(e => e.Id == id && e.CreatedById == userId);

            // 2. Handle not found case
            if (entry == null)
                return new Response<IntelDto>(null, "Intel entry not found.", false);

            // 3. Map to IntelDto
            var dto = new IntelDto
            {
                Id = entry.Id,
                Title = entry.Title,
                Description = entry.Description,
                Tags = entry.Tags
                             .Split(',', StringSplitOptions.RemoveEmptyEntries)
                             .ToList(),
                ImagePath = entry.ImagePath,
                CreatedAt = entry.CreatedAt
            };

            // 4. Wrap in Response<T> and return
            return new Response<IntelDto>(dto, "Intel entry retrieved successfully.", true);
        }
    }
}
