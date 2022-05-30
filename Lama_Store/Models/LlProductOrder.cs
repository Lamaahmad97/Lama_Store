using System;
using System.Collections.Generic;

#nullable disable

namespace Lama_Store.Models
{
    public partial class LlProductOrder
    {
        public decimal ProductOrderId { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal? OrderId { get; set; }
        public decimal? ProductId { get; set; }

        public virtual LlOrder Order { get; set; }
        public virtual LlProduct Product { get; set; }
    }
}
