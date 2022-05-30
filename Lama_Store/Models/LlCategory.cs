using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Lama_Store.Models
{
    public partial class LlCategory
    {
        public LlCategory()
        {
            LlStores = new HashSet<LlStore>();
        }

        public decimal CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }

        public virtual ICollection<LlStore> LlStores { get; set; }
    }
}
