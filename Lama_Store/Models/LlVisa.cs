using System;
using System.Collections.Generic;

#nullable disable

namespace Lama_Store.Models
{
    public partial class LlVisa
    {
        public LlVisa()
        {
            LlOrders = new HashSet<LlOrder>();
        }

        public decimal VisaId { get; set; }
        public string VisaName { get; set; }
        public string VisaPass { get; set; }
        public decimal? VisaBalance { get; set; }

        public virtual ICollection<LlOrder> LlOrders { get; set; }
    }
}
