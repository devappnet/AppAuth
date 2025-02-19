using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity
{
    public class AspNetRole : BaseEntity
    {
        public string Role { get; set; } = string.Empty;
        public string NormalizedRole{ get; set; } = string.Empty;
        public ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
    }
}
