using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Lama_Store.Models
{
    public partial class LlStore
    {
   

        public decimal StoreId { get; set; }
        public string StoreName { get; set; }
        public string ImagePathS { get; set; }
        public decimal? CategoryId { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual LlCategory Category { get; set; }
        public virtual ICollection<LlProduct> LlProducts { get; set; }
    }
}
