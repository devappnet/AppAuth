using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity
{
    public class AspNetUser : BaseEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string NormalizedUserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NormalizedEmail { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; } = false;
        public string PasswordHash { get; set; } = string.Empty;
        public string SecurityStamp { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool PhoneConfirmed { get; set; } = false;
        public bool TwoFactorEnabled { get; set; } = false;
        public DateTime LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; } = false;
        public int AccessFailedCount { get; set; } = 0; 
        public ICollection<AspNetUserRole> AspNetUserRoles { get; set; }    
    }
}
