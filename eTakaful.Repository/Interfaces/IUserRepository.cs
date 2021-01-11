using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;

namespace Ecommerce.Repository.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> Authenticate(string username, string password);
        Task<User> RegisterUser(User user);
        //IEnumerable<User> GetAll();
        //User GetById(int id);
        //User Create(User user, string password);
        //void Update(User user, string password = null);
        //void Delete(Guid id);
    } 
}
