using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Service.Dto;

namespace Ecommerce.Service.Interface
{
    public interface IUserService : IServices<User>
    {
        Task<UserDto> Register(UserDto user,string role);
        Task<UserDto> Login(string username, string password);
        Task<string> Authenticate(string username, string password);
        Task<bool> ActiveUser(string username);
        Task<UserDto> GetUserByUsername(string username);
        //IEnumerable<User> GetAll();
        //User GetById(int id);
        //User Create(UserDto user, string password);
        //void Update(UserDto user, string password = null);
        //void Delete(Guid id);
        Task DeleteUser(User user,string wwwRootPath);
    }
}
