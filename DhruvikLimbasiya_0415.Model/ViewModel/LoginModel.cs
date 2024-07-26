using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhruvikLimbasiya_0415.Model.ViewModel
{
    public class LoginModel
    {
        [Required (ErrorMessage ="Plase enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Plase enter Password")]
        public string Password{ get; set; }
        public string Token { get; set; }
    }
}
