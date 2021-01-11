using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using EcommerceCommon.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;

namespace Ecommerce.Repository
{
    public class UserProfileRepository : BaseRepository<UserProfile>, IUserProfileRepository
    {

        public UserProfileRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }

        public async Task UpdateUserProfile(UserProfile userprofile, string password = null)
        {
            // find user profile
            var user = DbContext.UserProfiles.Find(userprofile.Id);

            if (user != null)
            {
                // update
                userprofile.UpdatedDate = DateTime.Now;
                await UpdateAsync(user);

            }
        }

        public async Task<IList<CustomerAdminViewModel>> GetCustomerAdminViewModels()
        {
            var customerlist = await (from up in DbContext.UserProfiles
                                      join us in DbContext.Users
                                      on up.UserId equals us.Id
                                      where us.Roles == Role.Customer
                                      select new CustomerAdminViewModel
                                      {
                                          Id = up.Id,
                                          UserName = us.Username,
                                          Name = up.Name,
                                          Gender = up.Gender,
                                          Birthday = up.Birthday == null ? "" : up.Birthday.Value.ToString("dd/MM/yyyy"),
                                          Phone = up.Phone,
                                          Email = up.Email,
                                          Address = up.Address,
                                          AvatarUrl = up.AvatarUrl,
                                      }).ToListAsync();
            return customerlist;


        }

        public async Task<IList<StaffAdminViewModel>> GetStaffAdminViewModels()
        {
            var staffList = await (from up in DbContext.UserProfiles
                                   join us in DbContext.Users
                                   on up.UserId equals us.Id
                                   where us.Roles == Role.Staff
                                   select new StaffAdminViewModel
                                   {
                                       Id = us.Id,
                                       UserProfileId = up.Id,
                                       UserName = us.Username,
                                       Name = up.Name,
                                       Gender = up.Gender,
                                       Birthday = up.Birthday == null ? "" : up.Birthday.Value.ToString("dd/MM/yyyy"),
                                       Phone = up.Phone,
                                       Email = up.Email,
                                       Address = up.Address,
                                       AvatarUrl = up.AvatarUrl,
                                   }).ToListAsync();
            return staffList;
        }

        public async Task<CheckOutInfoViewModel> GetCheckOutInfoViewModel(Guid UserId)
        {
            var info = await (from up in DbContext.UserProfiles                              
                              where up.UserId == UserId
                              select new CheckOutInfoViewModel
                              {
                                  Name = up.Name,
                                  Phone = up.Phone,
                                  Address = up.Address                                  
                              }).FirstOrDefaultAsync();
            return info;
        }

        public async Task<IList<CustomerAdminViewModel>> GetNewCustomerAdminViewModels()
        {
            
                var newcustomerlist = await (from up in DbContext.UserProfiles
                                          join us in DbContext.Users
                                          on up.UserId equals us.Id
                                          where us.Roles == Role.Customer && DateTime.Compare(DateTime.Now, us.CreatedDate.Value.AddMonths(1)) <0
                                             select new CustomerAdminViewModel
                                          {
                                              Id = up.Id,
                                              UserName = us.Username,
                                              Name = up.Name,
                                              Gender = up.Gender,
                                              Birthday = up.Birthday == null ? "" : up.Birthday.Value.ToString("dd/MM/yyyy"),
                                              Phone = up.Phone,
                                              Email = up.Email,
                                              Address = up.Address,
                                              AvatarUrl = up.AvatarUrl,
                                              CreatedDate = up.CreatedDate
                                          }).ToListAsync();
                return newcustomerlist;


            
        }



        //public async Task<ICollection<UserProfile>> ShowUserprofile()
        //{
        //    return await DbContext.Userprofiles.Where(c => c.NSXId == null).ToListAsync();
        //}
    }
}

