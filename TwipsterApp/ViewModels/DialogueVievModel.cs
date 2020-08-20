using System;
using System.ComponentModel.DataAnnotations;
using TwipsterApp.Models;

namespace TwipsterApp.ViewModels
{
    public class DialogueVievModel
    {
        public DateTime DateTimeSend { get; set; }
        public string FromUserFullName { get; set; }
        public string ToUserFullName { get; set; }
        public User FromUser { get; set; }
        public User ToUser { get; set; }
    }
}
