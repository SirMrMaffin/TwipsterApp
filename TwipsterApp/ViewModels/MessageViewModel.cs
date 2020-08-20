using System;
using System.ComponentModel.DataAnnotations;

namespace TwipsterApp.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string FromUserFullName { get; set; }
        public string ToUserFullName { get; set; }
        public string Content { get; set; }
        public DateTime DateTimeSend { get; set; }
    }
}
