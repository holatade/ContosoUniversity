using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contoso.ViewModels
{
    public class StudentViewModel
    {
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
    }
}
