using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoeWeb.Domain.Model
{
    public class Supplier:BaseModel
    {
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string CodeName { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(30)]
        public string Phone { get; set; }
        [MaxLength(30)]
        public string Fax { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
