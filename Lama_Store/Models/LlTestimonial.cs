using System;
using System.Collections.Generic;

#nullable disable

namespace Lama_Store.Models
{
    public partial class LlTestimonial
    {
        public decimal TestimonialId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string BgImagePath { get; set; }
        public decimal? UserId { get; set; }

        public virtual LlUser User { get; set; }
    }
}
