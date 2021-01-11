using EcommerceCommon.Infrastructure.Enums;
using EcommerceCommon.Infrastructure.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class UserProfile: BaseModel
    {
        [MaxLength(64)]
        [Required]
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime? Birthday { get; set; }
        [PhoneVN(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage ="Vui lòng nhập lại Email")]
        public string Email { get; set; }
        public string Address { get; set; }
        public string AvatarUrl { get; set; } = "default.png";
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
