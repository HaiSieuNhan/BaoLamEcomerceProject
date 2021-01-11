using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceCommon.Infrastructure.Enums;

using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Dto;
using Ecommerce.Service.Interface;
using EcommerceCommon.Infrastructure.Helper;
using System.Globalization;
using EcommerceCommon.Infrastructure.Ultil;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Service.Services
{
    public class UserService : EcommerceServices<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IUserProfileRepository userProfileRepository, IMapper mapper) : base(userRepository)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
            
        }

        public UserService(IUserRepository userRepository,string key) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Register(UserDto userDto,string role)
        {
            var acc = await _userRepository.GetFirstOrDefaultAsync(x => x.Username.ToLower() == userDto.Username.ToLower());
            if (acc == null)
            {
                byte[] passwordHash;
                byte[] passwordSalt;
                AuthenUserHelper.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);
                // Create user
                var user = new User
                {
                    Username = userDto.Username,
                    Roles = role,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = Status.InActive,
                    IsConfirmation = false
                };

                await _userRepository.AddAsync(user);

                // Create user profile
                var userProfile = new UserProfile
                {
                    Name = userDto.Username,
                    Gender = userDto.Gender,
                    Birthday = DateTime.ParseExact(userDto.Birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture) ,
                    Email = userDto.Email,
                    Phone = userDto.Phone,
                    UserId = user.Id,
                };

                await _userProfileRepository.AddAsync(userProfile);

                // Send mail

                // Thanh cong
                return userDto;
            }



            return null;
        }

        public async Task<UserDto> Login(string username, string password)
        {
            var user = await _userRepository.Authenticate(username, password);
            if(user == null)
            {
                return null;
            }
            var userProfile = await _userProfileRepository.GetFirstOrDefaultAsync(x => x.UserId == user.Id);
            var userdto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Name = userProfile.Name,
                Gender = userProfile.Gender,
                Birthday = userProfile.Birthday == null ? "" : userProfile.Birthday.Value.ToString("dd/MM/yyyy"),
                Email = userProfile.Email,
                Phone = userProfile.Phone,
                Address = userProfile.Address,
                Avatar = userProfile.AvatarUrl
            };
            // map useDto
            return userdto;
        }

        public async Task<bool> ActiveUser(string username)
        {
            var acc = await _userRepository.GetFirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());

            if (acc.IsConfirmation == false)
            {
                acc.IsConfirmation = true;
                acc.Status = Status.Active;

                await _userRepository.UpdateAsync(acc);
                return true;
            }

            return false;
        }

        public async Task<UserDto> GetUserByUsername(string username)
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());
            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteUser(User user,string wwwRootPath)
        {
            var userProfile = await _userProfileRepository.GetFirstOrDefaultAsync(x => x.UserId == user.Id);
            if(userProfile.AvatarUrl != "default.png")
            {
                Ultil.DeleteFile(userProfile.AvatarUrl, wwwRootPath, "images");
            }
            await _userRepository.DeleteAsync(user);
        }

        public async Task<string> Authenticate(string username, string password)
        {
            var key = Token.Key;
            var user = await _userRepository.Authenticate(username, password);
            if(user == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var claims = new []
                {
                    new Claim(ClaimTypes.Name, username)
                };
            var credentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                issuer: key,
                audience: key,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            var encodetoken = tokenHandler.WriteToken(token);
            return encodetoken;
        }
    }
}
