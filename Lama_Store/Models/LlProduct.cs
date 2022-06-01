using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Lama_Store.Models
{
    public partial class LlProduct
    {
       

        public decimal ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string ImagePathP { get; set; }
        public decimal? ProductCost { get; set; }
        public decimal? StoreId { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }


        public virtual LlStore Store { get; set; }
        public virtual ICollection<LlProductOrder> LlProductOrders { get; set; }
    }
}
