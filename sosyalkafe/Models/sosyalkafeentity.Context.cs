﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class sosyalkafeEntities : DbContext
    {
        public sosyalkafeEntities()
            : base("name=sosyalkafeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ayarlar> ayarlar { get; set; }
        public virtual DbSet<firma_ayarlari> firma_ayarlari { get; set; }
        public virtual DbSet<firma_kodlari> firma_kodlari { get; set; }
        public virtual DbSet<firma_odeme_hareketi> firma_odeme_hareketi { get; set; }
        public virtual DbSet<firmalar> firmalar { get; set; }
        public virtual DbSet<kod_tipi> kod_tipi { get; set; }
        public virtual DbSet<musteri_gonderileri> musteri_gonderileri { get; set; }
        public virtual DbSet<musteriler> musteriler { get; set; }
        public virtual DbSet<odeme_secenekleri> odeme_secenekleri { get; set; }
    }
}
