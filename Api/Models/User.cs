using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Account { get; set; } = null!;
        public string Pwd { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Role { get; set; }
        public int Status { get; set; }
        public int? Profile { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
