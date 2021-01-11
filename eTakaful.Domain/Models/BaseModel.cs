using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcommerceCommon.Infrastructure.Enums;
using System.Text;
namespace Ecommerce.Domain.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            Id = Guid.NewGuid();      
            
        }

        [Key]
        public Guid Id { get; set; }
        [DisplayName("Sắp xếp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sort { get; set; }
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayFormat(DataFormatString ="{0:MM/dd/yyyy }")]
        [DisplayName("Ngày tạo")]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        [DisplayName("Người tạo")]
        public string CreatedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Ngày sửa")]
        public DateTime? UpdatedDate { get; set; }
        [DisplayName("Người sửa")]
        public string UpdatedBy { get; set; }
        [DisplayName("Bị xóa")]
        public bool IsDeleted { get; set; } = false;
        [DisplayName("Trạng thái")]
        public Status Status { get; set; } = Status.Active;
    }

   
}