using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Lama_Store.Models
{
    public partial class LlUser
    {
        public LlUser()
        {
            LlAboutus = new HashSet<LlAboutu>();
            LlConatctus = new HashSet<LlConatctu>();
            LlHomes = new HashSet<LlHome>();
            LlLogins = new HashSet<LlLogin>();
            LlOrders = new HashSet<LlOrder>();
            LlTestimonials = new HashSet<LlTestimonial>();
        }

        public decimal UserId { get; set; }
        public string UserFname { get; set; }
        public string UserLname { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual ICollection<LlAboutu> LlAboutus { get; set; }
        public virtual ICollection<LlConatctu> LlConatctus { get; set; }
        public virtual ICollection<LlHome> LlHomes { get; set; }
        public virtual ICollection<LlLogin> LlLogins { get; set; }
        public virtual ICollection<LlOrder> LlOrders { get; set; }
        public virtual ICollection<LlTestimonial> LlTestimonials { get; set; }
    }
}
