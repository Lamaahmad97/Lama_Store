using System;
using System.Collections.Generic;

#nullable disable

namespace Lama_Store.Models
{
    public partial class LlConatctu
    {
        public decimal ContactusId { get; set; }
        public string Namee { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public decimal? UserId { get; set; }

        public virtual LlUser User { get; set; }
    }
}
