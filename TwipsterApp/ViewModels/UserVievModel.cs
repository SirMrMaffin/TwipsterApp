using System;
using System.ComponentModel.DataAnnotations;

namespace TwipsterApp.ViewModels
{
    public class UserVievModel
    {
        [Display(Order = -1)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Login { get; set; }
    }
}
