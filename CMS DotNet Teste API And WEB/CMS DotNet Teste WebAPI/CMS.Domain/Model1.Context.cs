﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMS.Domain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CMS_DOTNETEntities : DbContext
    {
        public CMS_DOTNETEntities()
            : base("name=CMS_DOTNETEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TbAula> TbAula { get; set; }
        public virtual DbSet<TbCurso> TbCurso { get; set; }
        public virtual DbSet<TBEmpresa> TBEmpresa { get; set; }
        public virtual DbSet<TbItemVenda> TbItemVenda { get; set; }
        public virtual DbSet<TbRequisito> TbRequisito { get; set; }
        public virtual DbSet<TBTarefa> TBTarefa { get; set; }
        public virtual DbSet<TbVaga> TbVaga { get; set; }
        public virtual DbSet<TbVenda> TbVenda { get; set; }
    }
}
