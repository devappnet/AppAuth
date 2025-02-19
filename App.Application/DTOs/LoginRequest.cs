using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs
{
    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; } = true;
    }
}
