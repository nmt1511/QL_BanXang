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
    
    public partial class QUYCACH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QUYCACH()
        {
            this.CTPHIEUBANs = new HashSet<CTPHIEUBAN>();
            this.CTPHIEUNHAPs = new HashSet<CTPHIEUNHAP>();
            this.CTGIAs = new HashSet<CTGIA>();
        }
    
        public int MaQC { get; set; }
        public string TenQC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPHIEUBAN> CTPHIEUBANs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPHIEUNHAP> CTPHIEUNHAPs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTGIA> CTGIAs { get; set; }
    }
}
