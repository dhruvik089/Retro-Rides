using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhruvikLimbasiya_0415.Model.ViewModel
{
    public class FilterBikeDetailsModel
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int? kilometerMin { get; set; }
        public int? kilometerMax { get; set; }
    }
}
