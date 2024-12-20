﻿using AutoMapper;
using MediatR;
using PRM392.OnlineStore.Application.Interfaces;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Users.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        public RegisterCommandHandler(IUserRepository userRepository, IJwtService jwtService, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.FindAsync(u => u.Email == request.Email,cancellationToken);

            if (existingUser != null)
            {
                throw new DuplicationException("Account with this Email is already registered.");
            }

            if (request.Password != request.Repassword)
            {
                throw new ArgumentException("Passwords do not match."); 
            }

         

            var newUser = new User
            {
                Email = request.Email,
                Username = request.Username,    
                PasswordHash = HashPassword(request.Password), 
                Role = "Customer" 
                                          
            };
             _userRepository.Add(newUser);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var accessToken = _jwtService.CreateToken(newUser.UserId, newUser.Role, newUser.Email);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Update the refresh token in the user repository
            await _userRepository.UpdateRefreshTokenAsync(newUser, refreshToken, DateTime.UtcNow.AddDays(30));

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Registration successful!";

        }

        private string HashPassword(string password)
        {
            
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
