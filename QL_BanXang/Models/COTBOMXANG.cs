//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QL_BanXang.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class COTBOMXANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COTBOMXANG()
        {
            this.CHITIETNGAYLAMs = new HashSet<CHITIETNGAYLAM>();
        }
    
        public int MaCot { get; set; }
        public string LoaiNhienlieu { get; set; }
        public string Tthoatdong { get; set; }
        public Nullable<System.DateTime> NgayCapNhatTT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETNGAYLAM> CHITIETNGAYLAMs { get; set; }
    }
}
