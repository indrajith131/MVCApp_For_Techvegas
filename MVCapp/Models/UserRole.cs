using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCapp.Models
{
    public class UserRole
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public String RoleName { get; set; }

        public List<Users>Users { get; set; }
    }
}