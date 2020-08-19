using System;
using System.ComponentModel.DataAnnotations;
using TwipsterApp.Models;

namespace TwipsterApp.ViewModels
{
    public class DialogueVievModel
    {
        public DateTime DateTimeSend { get; set; }
        [Display(ShortName = "From")]
        public string FromUserFullName { get; set; }
        [Display(ShortName = "To")]
        public string ToUserFullName { get; set; }
        [Display(Order = -1)]
        public User FromUser { get; set; }
        [Display(Order = -2)]
        public User ToUser { get; set; }
    }
}
