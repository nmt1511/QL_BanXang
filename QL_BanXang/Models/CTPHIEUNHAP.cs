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
    
    public partial class CTPHIEUNHAP
    {
        public int MaCTPN { get; set; }
        public Nullable<int> MaPN { get; set; }
        public Nullable<int> MaHH { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<decimal> GiaNhap { get; set; }
        public Nullable<int> MaQC { get; set; }
    
        public virtual HANGHOA HANGHOA { get; set; }
        public virtual PHIEUNHAP PHIEUNHAP { get; set; }
        public virtual QUYCACH QUYCACH { get; set; }
    }
}
