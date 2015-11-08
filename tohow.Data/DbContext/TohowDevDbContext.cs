﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tohow.Interface.DbContext;

namespace tohow.Data.DbContext
{
    public class TohowDevDbContext: System.Data.Entity.DbContext, ITohowDevDbContext
    {
        static TohowDevDbContext() {
            Database.SetInitializer<TohowDevDbContext>(null);
        }

        // db initial create
        public static TohowDevDbContext Create() {
            var db = new TohowDevDbContext();
            db.Configuration.LazyLoadingEnabled = false;
            return db;
        }

        public TohowDevDbContext() : base("TohowDevDbContext") { }
        public TohowDevDbContext(string connectionString) : base(connectionString) { }

        #region entities
        public DbSet<tohow.Domain.Database.tblImage> tblImages { get; set; }
        #endregion

        public System.Data.Entity.Core.Objects.ObjectContext BaseContext
        {
            get { return ((IObjectContextAdapter)this).ObjectContext; }
        }
    }
}
