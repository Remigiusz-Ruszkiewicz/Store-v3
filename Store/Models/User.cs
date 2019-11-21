using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class User
    {
        public string Token { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}
