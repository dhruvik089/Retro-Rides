using DhruvikLimbasiya_0415.Model.DbContext;
using DhruvikLimbasiya_0415.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhruvikLimbasiya_0415.Repository.Services
{
    public class RegistrationService
    {
        DhruvikLimbasiya_0415Entities _DbContext = new DhruvikLimbasiya_0415Entities();

        public List<StateModel> GetStateByCountry(int CountryId)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
                {
                      new SqlParameter("@id",CountryId)
                };
            List<StateModel> getState = _DbContext.Database.SqlQuery<StateModel>("exec getState @id", sqlParameters).ToList();

            return getState;
        }
        public List<CityModel> GetCityStateId(int stateid)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                  new SqlParameter("@id",stateid)
            };
            List<CityModel> getCity = _DbContext.Database.SqlQuery<CityModel>("Exec getCity @id", sqlParameters).ToList();
            return getCity;
        }
        public List<ZipCodeModel> GetZipcode(int Cityid)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                 new SqlParameter("@id",Cityid)
            };
            List<ZipCodeModel> getZipCode = _DbContext.Database.SqlQuery<ZipCodeModel>("Exec getZipCode @id", sqlParameters).ToList();
            return getZipCode;
        }

        public bool addUser(RegistrationModel db)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                 new SqlParameter("@Firstname",db.FirstName),
                 new SqlParameter("@Middlename",db.MiddleName),
                 new SqlParameter("@Lastname",db.LastName),
                 new SqlParameter("@Email",db.Email),
                 new SqlParameter("@PhoneNumber",db.PhoneNumber),
                 new SqlParameter("@DateOfBirth",db.DateofBirth),
                 new SqlParameter("@Address",db.Address),
                 new SqlParameter("@AddressLineTwo",db.AddressLineTwo),
                 new SqlParameter("@Country",db.Country),
                 new SqlParameter("@state",db.State),
                 new SqlParameter("@city",db.City),
                 new SqlParameter("@zipCode",db.ZipCode),
                 new SqlParameter("@profilePhoto",db.ProfilePhoto),
                 new SqlParameter("@password",db.Password),
            };

            _DbContext.Database.SqlQuery<RegistrationModel>("Exec Adduser @Firstname, @Middlename, @Lastname, @Email, @PhoneNumber, @DateOfBirth, @Address, @AddressLineTwo, @Country, @state, @city, @zipCode, @profilePhoto,@password", sqlParameters).ToList();
            return true;
        }

    }
}
