using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhruvikLimbasiya_0415.Model.ViewModel
{
    public class BikeDetailsModel
    { 
        public int BikeId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int KilometresDriven { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrls { get; set; }
        public string SellerFirstName { get; set; }
        public string SellerLastName { get; set; }
        public string SellerEmail { get; set; }
        public string SellerPhoneNumber { get; set; }
    }
}
