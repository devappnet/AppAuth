using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity
{
    public class AspNetUserRole :BaseEntity
    {
        public long RoleId { get; set; }
        public AspNetRole Role { get; set; }
        public long UserId { get; set; }
        public AspNetUser User { get; set; }
    }
}
