using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Dto;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Web.Customer;
using EcommerceCommon.Infrastructure.Helper;
using EcommerceCommon.Infrastructure.Ultil;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class UserProfileService : EcommerceServices<UserProfile>, IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;


        public UserProfileService(IUserProfileRepository userProfileRepository, IMapper mapper) : base(userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
        }


        public async Task<IList<CustomerAdminViewModel>> GetCustomerAdminViewModels()
        {
            var customerlist = await _userProfileRepository.GetCustomerAdminViewModels();
            return customerlist;
        }

        public async Task<CustomerProfileViewModel> GetCustomerProfileViewModel(Guid UserId)
        {
            var profile = await _userProfileRepository.GetFirstOrDefaultAsync(x => x.UserId == UserId);
            return _mapper.Map<CustomerProfileViewModel>(profile); ;
        }

    

        public async Task<IList<StaffAdminViewModel>> GetStaffAdminViewModels()
        {
            var stafflist = await _userProfileRepository.GetStaffAdminViewModels();
            return stafflist;
        }

        public async Task<UserProfileDto> GetUserProfileDto(Guid Id)
        {
            var profile = await _userProfileRepository.GetFirstOrDefaultAsync(x => x.Id == Id);
            return _mapper.Map<UserProfileDto>(profile);
        }

        public async Task<Validate> UpdateCustomerProfile(Guid UserId,CustomerProfileViewModel customerProfileViewModel )
        {
            try
            {
                var profile = await _userProfileRepository.GetFirstOrDefaultAsync(x => x.UserId == UserId);
                profile.Name = customerProfileViewModel.Name;
                profile.Phone = customerProfileViewModel.Phone;
                profile.Gender = customerProfileViewModel.Gender;
                profile.Address = customerProfileViewModel.Address;
                if(customerProfileViewModel.Birthday != null)
                {
                    profile.Birthday = DateTime.ParseExact(customerProfileViewModel.Birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }              
                await _userProfileRepository.UpdateAsync(profile);
                return new Validate { IsValid = true, Message = "Cập nhật thành công" };
            }
            catch
            {
                return new Validate { IsValid = false, Message = "Có lỗi trong quá trình cập nhật thông tin !" };
            }
            
        }

        public async Task UpdateUserProfile(UserProfileDto userProfileDto,string wwwRootPath)
        {
            var profile = await _userProfileRepository.GetFirstOrDefaultAsync(x => x.Id == userProfileDto.Id);
            profile.Name = userProfileDto.Name;
            profile.Phone = userProfileDto.Phone;
            profile.Address = userProfileDto.Address;
            profile.Birthday = DateTime.ParseExact(userProfileDto.Birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            profile.Gender = userProfileDto.Gender;
            profile.Email = userProfileDto.Email;
            if(userProfileDto.ImageFile != null)
            {
                if (profile.AvatarUrl != "default.png")
                {
                    Ultil.DeleteFile(profile.AvatarUrl, wwwRootPath, "images");
                }
                profile.AvatarUrl = await Ultil.UploadFileAsync(userProfileDto.ImageFile, wwwRootPath, "images");

            }
            await _userProfileRepository.UpdateAsync(profile);
        }

        public async Task<IList<CustomerAdminViewModel>> GetNewCustomerAdminViewModels()
        {
            var customerlist = await _userProfileRepository.GetNewCustomerAdminViewModels();
            return customerlist;

        }

        public async Task<int> GetCountNewCustomer()
        {
            var newcustomerlist = await _userProfileRepository.GetNewCustomerAdminViewModels();
            var count = newcustomerlist.Count;
            return count;
        }


        // Thay đổi thông tin 
        //public void Update(UserDto userDto, string password = null)
        //{
        //    var user = _mapper.Map<UserProfile>(userDto);
        //    _userprofileRepository.Update(user, password);
        //}


    }
}
