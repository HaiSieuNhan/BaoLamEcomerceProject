using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class Product :BaseModel
    {
        #region Property
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [MaxLength(255)]
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [DisplayName("Mô tả ngắn")]
        public string ShortDescription { get; set; }
        [DisplayName("Ngày phát hành")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime PublicationDate { get; set; } = DateTime.Now;
        public string Keyword { get; set; }
        public string Sku { get; set; }
        public int View { get; set; }
        [DisplayName("Trạng thái")]
        public ProductStatus ProductStatus { get; set; } = ProductStatus.New;
        [DisplayName("Bộ sưu tập")]
        public Guid? CollectionId { get; set; }
        #endregion
        #region Relationship
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
        [DisplayName("Danh mục ")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [DisplayName("Nhà sản xuất")]
        public Guid ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [DisplayName("Nhà cung cấp")]
        public Guid SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        #endregion
    }
}
