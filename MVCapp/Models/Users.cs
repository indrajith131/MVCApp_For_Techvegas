using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCapp.Models
{
    public class Users
    {
        
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public String Email { get; set; }
        [Required]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public UserRole Role { get; set; }

        public Users()
        {
            RoleId = 2;
        }


    }
}