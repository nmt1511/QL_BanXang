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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QL_CayXangEntities : DbContext
    {
        public QL_CayXangEntities()
            : base("name=QL_CayXangEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CATRUC> CATRUCs { get; set; }
        public virtual DbSet<CHITIETNGAYLAM> CHITIETNGAYLAMs { get; set; }
        public virtual DbSet<COTBOMXANG> COTBOMXANGs { get; set; }
        public virtual DbSet<CTPHIEUBAN> CTPHIEUBANs { get; set; }
        public virtual DbSet<CTPHIEUNHAP> CTPHIEUNHAPs { get; set; }
        public virtual DbSet<HANGHOA> HANGHOAs { get; set; }
        public virtual DbSet<LOAIHANG> LOAIHANGs { get; set; }
        public virtual DbSet<LUONG> LUONGs { get; set; }
        public virtual DbSet<NHACUNGCAP> NHACUNGCAPs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<PHIEUBAN> PHIEUBANs { get; set; }
        public virtual DbSet<PHIEUNHAP> PHIEUNHAPs { get; set; }
        public virtual DbSet<PHUCAP> PHUCAPs { get; set; }
        public virtual DbSet<QUANLY> QUANLies { get; set; }
        public virtual DbSet<QUYCACH> QUYCACHes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<CTGIA> CTGIAs { get; set; }
    }
}
