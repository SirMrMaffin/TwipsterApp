using System;

namespace TwipsterApp.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Content { get; set; }
        public DateTime PostTime { get; set; }
    }
}
