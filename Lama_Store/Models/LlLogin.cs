using System;
using System.Collections.Generic;

#nullable disable

namespace Lama_Store.Models
{
    public partial class LlLogin
    {
        public decimal LoginId { get; set; }
        public string UserName { get; set; }
        public string LoginPass { get; set; }
        public decimal? RoleId { get; set; }
        public decimal? UserId { get; set; }

        public virtual LlRole Role { get; set; }
        public virtual LlUser User { get; set; }
    }
}
