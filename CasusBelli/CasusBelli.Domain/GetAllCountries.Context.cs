﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CasusBelli.Domain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CasusBelliDbEntities : DbContext
    {
        public CasusBelliDbEntities()
            : base("name=CasusBelliDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<GetAllCountries_Result> GetAllCountries()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllCountries_Result>("GetAllCountries");
        }
    }
}
