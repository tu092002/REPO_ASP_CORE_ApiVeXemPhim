using System;
using System.Collections.Generic;

#nullable disable

namespace ASP_CORE_ApiVeXemPhim.Models
{
    public partial class User
    {
        public int MaUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
