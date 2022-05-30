using System;
using System.Collections.Generic;

#nullable disable

namespace Lama_Store.Models
{
    public partial class LlRole
    {
        public LlRole()
        {
            LlLogins = new HashSet<LlLogin>();
        }

        public decimal RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<LlLogin> LlLogins { get; set; }
    }
}
