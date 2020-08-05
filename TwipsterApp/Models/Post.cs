using System;
using System.ComponentModel.DataAnnotations;

namespace TwipsterApp.ViewModels
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime PostTime { get; set; }
        [Required]
        [MinLength(3), MaxLength(225)]
        public string Content { get; set; }

        public virtual User User { get; set; }
    }
}
