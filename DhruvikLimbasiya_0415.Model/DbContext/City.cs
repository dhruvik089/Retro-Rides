//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DhruvikLimbasiya_0415.Model.DbContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class City
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public City()
        {
            this.RegisterRide = new HashSet<RegisterRide>();
            this.ZipCode = new HashSet<ZipCode>();
        }
    
        public int CityId { get; set; }
        public string CityName { get; set; }
        public Nullable<int> state { get; set; }
    
        public virtual State State1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegisterRide> RegisterRide { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ZipCode> ZipCode { get; set; }
    }
}
