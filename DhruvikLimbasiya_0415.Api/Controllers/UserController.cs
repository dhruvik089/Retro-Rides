using DhruvikLimbasiya_0415.Model.DbContext;
using DhruvikLimbasiya_0415.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DhruvikLimbasiya_0415.Api.Controllers
{
    public class UserController : ApiController
    {
        DhruvikLimbasiya_0415Entities _DbContext = new DhruvikLimbasiya_0415Entities();

        [HttpGet]
        [Route("api/User/AddToFavourite")]
        public List<FavouriteModel> AddToFavourite(int userId, int BikeId)
        {
            List<FavouriteModel> favouriteModel;

            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@userId",userId),
                new SqlParameter("@itemId ",BikeId),
            };

            favouriteModel = _DbContext.Database.SqlQuery<FavouriteModel>("exec AddFevorit @userId ,@itemId ", sp).ToList();

            return favouriteModel;
        }

        [HttpGet]
        [Route("api/User/RemoveFavourite")]
        public List<FavouriteModel> RemoveFavourite(int userId, int BikeId)
        {
            List<FavouriteModel> favouriteModel;

            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@userId",userId),
                new SqlParameter("@itemId ",BikeId),
            };

            favouriteModel = _DbContext.Database.SqlQuery<FavouriteModel>("exec removeFevorite @userId ,@itemId ", sp).ToList();

            return favouriteModel;
        }

        [HttpGet]
        [Route("api/User/Favourite")]
        public List<FavouriteModel> Favourite(int userId)
        {
            List<FavouriteModel> favouriteModel;

            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@userId",userId),
            };

            favouriteModel = _DbContext.Database.SqlQuery<FavouriteModel>("exec getAllFavourite @userId ", sp).ToList();

            return favouriteModel;
        }

        [HttpPost]
        [Route("api/User/BikeDetailsbyFilter")]
        public List<BikeDetailsModel> BikeDetailsbyFilter(FilterBikeDetailsModel _bikeDetails)
        {
            
            SqlParameter[] sp = new SqlParameter[]
             {
                new SqlParameter("@Brand",(object)_bikeDetails.Brand??DBNull.Value),
                new SqlParameter("@Models",(object)_bikeDetails.Model??DBNull.Value),
                new SqlParameter("@kilometerMin",(object)_bikeDetails.kilometerMin??DBNull.Value),
                new SqlParameter("@KilomterMax",(object)_bikeDetails.kilometerMax??DBNull.Value),
             };

            List<BikeDetailsModel> bikeList = _DbContext.Database.SqlQuery<BikeDetailsModel>("GetBikesFilter @Brand  ,  @Models  ,@kilometerMin  ,@KilomterMax", sp).ToList();

            return bikeList;
        }

    }
}