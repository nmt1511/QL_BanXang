﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel;

    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            this.NHANVIENs = new HashSet<NHANVIEN>();
            this.QUANLies = new HashSet<QUANLY>();
        }
    
        public int ID { get; set; }
        [DisplayName("Username")]
        public string Username { get; set; }
        public string Password { get; set; }
        [DisplayName("Chức vụ")]
        public string Chucvu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHANVIEN> NHANVIENs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QUANLY> QUANLies { get; set; }
    }
}
