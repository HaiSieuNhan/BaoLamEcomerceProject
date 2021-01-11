using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoeWeb.Domain.Model
{
    public class Coupon:BaseModel
    {
        [Required]
        [MaxLength(256)]
        public string Code { get; set; }
        public decimal Percent { get; set; }
        public decimal MaxValue { get; set; }
        public int TimesLimit { get; set; }
        public DateTime? ExpirationDate { get; set; }

    }
}
