using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity
{
    public class AspNetUserRolePermission : BaseEntity
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string Permission { get; set; }
        public AspNetUser AspNetUser { get; set; }
        public AspNetRole Role { get; set; }    
    }
}
