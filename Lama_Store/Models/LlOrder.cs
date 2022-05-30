using System;
using System.Collections.Generic;

#nullable disable

namespace Lama_Store.Models
{
    public partial class LlOrder
    {
        public LlOrder()
        {
            LlProductOrders = new HashSet<LlProductOrder>();
        }

        public decimal OrderId { get; set; }
        public string OrderName { get; set; }
        public decimal? UserId { get; set; }
        public decimal? VisaId { get; set; }

        public virtual LlUser User { get; set; }
        public virtual LlVisa Visa { get; set; }
        public virtual ICollection<LlProductOrder> LlProductOrders { get; set; }
    }
}
