//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sosyalkafe.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class musteriler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public musteriler()
        {
            this.musteri_gonderileri = new HashSet<musteri_gonderileri>();
        }
    
        public int musteri_id { get; set; }
        public string musteri_adi { get; set; }
        public string musteri_kodu { get; set; }
        public Nullable<System.DateTime> kayit_tarihi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<musteri_gonderileri> musteri_gonderileri { get; set; }
    }
}
