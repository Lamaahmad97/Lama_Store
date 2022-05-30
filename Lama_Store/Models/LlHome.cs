using System;
using System.Collections.Generic;

#nullable disable

namespace Lama_Store.Models
{
    public partial class LlHome
    {
        public decimal HomeId { get; set; }
        public string BgImagePath { get; set; }
        public string BbgImagePath { get; set; }
        public string BggImagePath { get; set; }
        public string Texitt { get; set; }
        public string Texittt { get; set; }
        public decimal? UserId { get; set; }

        public virtual LlUser User { get; set; }
    }
}
