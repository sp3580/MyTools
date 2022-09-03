using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class User_Session
    {
        public int Uid { get; set; }
        public string Account { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Token { get; set; } = String.Empty;
        public string Role { get; set; } = String.Empty;
        // public bool isLogin { get; set; }
    }
}