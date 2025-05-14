using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevIntel.Application.DTO;
using DevIntel.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using DevIntel.Infrastructure.Persistence.Context;
using DevIntel.Domain.Entities;
using DevIntel.Application.Responses;
using DevIntel.Domain.Enums;

namespace DevIntel.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(AppDbContext context, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResponse> RegisterAsync(UserRegisterDto dto)
        {
            // Step 1: Check if the user already exists
            var userExists = await _context.Users.AnyAsync(u => u.Email == dto.Email); // Using async to check if any record exists

            if (userExists)
            {
                return new AuthResponse("User already exists.", false);

            }


            // Step 2: Hash the password using BCrypt
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Step 3: Create a new user entity
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Role = Role.User
            };


            // Step 4: Add the user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Step 5: Return success message
            return new AuthResponse("Registration successful.", true);
        }


        public async Task<AuthResponse> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return new AuthResponse("Invalid email or password.", false);
                
            }

          
            //  Generate token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponse("Login successful", true, token);
           
        }

        public async Task<AuthResponse> PromoteUserAsync(PromoteUserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return new AuthResponse("User not found", false);

            user.Role = dto.NewRole;
            await _context.SaveChangesAsync();

            return new AuthResponse($"User promoted to {dto.NewRole}.", true);
        }

    }
}
