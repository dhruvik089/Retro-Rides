using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhruvikLimbasiya_0415.Model.ViewModel
{
    public class RegistrationModel
    {
         [Required(ErrorMessage = "Plase enter First Name")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

         [Required(ErrorMessage = "Plase enter Last Name")]
        public string LastName { get; set; }

         [Required(ErrorMessage = "Plase enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Plase enter Password")]
        public string Password { get; set; }

         [Required(ErrorMessage = "Plase enter Phone Number")]
        public string PhoneNumber { get; set; }

        public string DateofBirth { get; set; }

         [Required(ErrorMessage = "Plase enter Address line one")]
        public string Address { get; set; }
        public string AddressLineTwo { get; set; }

         [Required(ErrorMessage = "Plase Select Country")]
        public int? Country { get; set; }

        [Required(ErrorMessage = "Plase Select Country")]
        public int? State { get; set; }

        [Required(ErrorMessage = "Plase Select city")]
        public int? City { get; set; }
        
        [Required(ErrorMessage = "Plase Select ZipCode")]
        public int? ZipCode { get; set; }

         [Required(ErrorMessage = "Plase Select Photo")]
        public string ProfilePhoto { get; set; }
    }
}
