using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TwipsterApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MinLength(3), MaxLength(75)]
        public string Surname { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [MinLength(3), MaxLength(15)]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(3), MaxLength(15)]
        public string Password { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
