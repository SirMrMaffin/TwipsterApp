using System;
using System.ComponentModel.DataAnnotations;

namespace TwipsterApp.ViewModels
{
    public class MessegeVievModel
    {
        public int Id { get; set; }
        [Display(Order = -1)]
        public int FromUserId { get; set; }
        [Display(Order = -2)]
        public int ToUserId { get; set; }
        [Display(ShortName = "From")]
        public string FromUserFullName { get; set; }
        [Display(ShortName = "To")]
        public string ToUserFullName { get; set; }
        public string Content { get; set; }
        public DateTime DateTimeSend { get; set; }
    }
}
