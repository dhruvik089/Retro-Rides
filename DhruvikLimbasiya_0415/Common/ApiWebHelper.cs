using DhruvikLimbasiya_0415.Model.DbContext;
using DhruvikLimbasiya_0415.Model.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace DhruvikLimbasiya_0415.Common
{
    public class ApiWebHelper
    {
        public async static Task<RegisterRide> Register(RegistrationModel db)

        {

            RegisterRide user = new RegisterRide();

            HttpClient client = new HttpClient();

            string content = JsonConvert.SerializeObject(db);

            client.BaseAddress = new Uri("http://localhost:50739/api/Registration/");

            HttpResponseMessage response = await client.PostAsync("Registers", new StringContent(content, System.Text.Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)

            {

                var data = await response.Content.ReadAsStringAsync();

                user = JsonConvert.DeserializeObject<RegisterRide>(data);

            }

            return user;

        }

        public async static Task<LoginModel> Login(LoginModel db)

        {

            LoginModel login = new LoginModel();

            HttpClient client = new HttpClient();

            string content = JsonConvert.SerializeObject(db);

            client.BaseAddress = new Uri("http://localhost:50739/api/Registration");

            HttpResponseMessage response = await client.PostAsync("Login", new StringContent(content, System.Text.Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)

            {

                var data = await response.Content.ReadAsStringAsync();

                login = JsonConvert.DeserializeObject<LoginModel>(data);

                return login;

            }

            return login;

        }

        public async static Task<List<FavouriteModel>> AddToFavourite(int userId, int BikeId)
        {
            List<FavouriteModel> favouriteModelList = new List<FavouriteModel>();


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50739/api/User/");
            HttpResponseMessage response = await client.GetAsync($"AddToFavourite?userId={userId}&BikeId={BikeId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                favouriteModelList = JsonConvert.DeserializeObject<List<FavouriteModel>>(data);

            }
            return favouriteModelList;
        }

        public async static Task<List<FavouriteModel>> RemoveFavourite(int userId, int BikeId)
        {
            List<FavouriteModel> favouriteModelList = new List<FavouriteModel>();


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50739/api/User/");
            HttpResponseMessage response = await client.GetAsync($"RemoveFavourite?userId={userId}&BikeId={BikeId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                favouriteModelList = JsonConvert.DeserializeObject<List<FavouriteModel>>(data);

            }
            return favouriteModelList;
        }


        public async static Task<List<FavouriteModel>> Favourite(int userId)
        {
            List<FavouriteModel> favouriteModelList = new List<FavouriteModel>();


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50739/api/User/");
            HttpResponseMessage response = await client.GetAsync($"Favourite?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                favouriteModelList = JsonConvert.DeserializeObject<List<FavouriteModel>>(data);

            }
            return favouriteModelList;
        } 
        
        
        public async static Task<List<BikeDetailsModel>> BikeDetailsbyFilter(FilterBikeDetailsModel _BikeDetails)
        {
            List<BikeDetailsModel> bikeFilterlList = new List<BikeDetailsModel>();


            HttpClient client = new HttpClient();
            string content = JsonConvert.SerializeObject(_BikeDetails);
            client.BaseAddress = new Uri("http://localhost:50739/api/User/");
            HttpResponseMessage response = await client.PostAsync($"BikeDetailsbyFilter", new StringContent(content, System.Text.Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                bikeFilterlList = JsonConvert.DeserializeObject<List<BikeDetailsModel>>(data);

            }
            return bikeFilterlList;
        }

    }
}