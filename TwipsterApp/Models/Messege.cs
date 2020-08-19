using System;
using System.ComponentModel.DataAnnotations;

namespace TwipsterApp.Models
{
    public class Messege
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime DateTimeSend { get; set; }

        public virtual User FromUser { get; set; }
        public virtual User ToUser { get; set; }
    }
}
