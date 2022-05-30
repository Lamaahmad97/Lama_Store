using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lama_Store.Models
{
    public class JoinTables
    {
        public LlOrder order { get; set; }
        public LlProductOrder ordpro { get; set; }

        public LlProduct Product { get; set; }
        public LlUser users { get; set; }
    }
}
