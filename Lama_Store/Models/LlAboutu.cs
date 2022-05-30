using System;
using System.Collections.Generic;

#nullable disable

namespace Lama_Store.Models
{
    public partial class LlAboutu
    {
        public decimal AboutusId { get; set; }
        public string Descriptionn { get; set; }
        public decimal? UserId { get; set; }

        public virtual LlUser User { get; set; }
    }
}
