using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoeWeb.Domain.Model
{
    public class User: BaseModel
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(256)]
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Phone { get; set; }
        #region Relationship
        public ICollection<Order> Orders { get; set; }
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
        #endregion
    }
}
