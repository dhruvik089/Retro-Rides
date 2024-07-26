using DhruvikLimbasiya_0415.Model.DbContext;
using DhruvikLimbasiya_0415.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhruvikLimbasiya_0415.Helper.Helper
{
    public class LoginConvert
    {
        public static RegisterRide ConvertModelToDbModel(RegistrationModel registrationModel)
        {
            RegisterRide users = new RegisterRide();

            users.Firstname = registrationModel.Email;
            users.Middlename = registrationModel.MiddleName;
            users.Lastname = registrationModel.LastName;
            users.PhoneNumber = registrationModel.PhoneNumber;
            users.profilePhoto= registrationModel.ProfilePhoto;
            users.state= registrationModel.State;
            users.Country = registrationModel.Country;
            users.city = registrationModel.City;
            users.zipCode = registrationModel.ZipCode;

            return users;
        }
    }
}
 