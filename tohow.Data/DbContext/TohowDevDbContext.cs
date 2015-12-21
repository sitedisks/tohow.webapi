using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
            //db.Configuration.LazyLoadingEnabled = true;
            //db.Configuration.ProxyCreationEnabled = false;
            return db;
        }

        public TohowDevDbContext() : base("TohowDevDbWinhostContext") { }
        public TohowDevDbContext(string connectionString) : base(connectionString) { }

        #region entities
        public DbSet<tohow.Domain.Database.tbImage> tblImages { get; set; }
        public DbSet<tohow.Domain.Database.AspNetUser> AspNetUsers { get; set; }
        public DbSet<tohow.Domain.Database.tbProfile> tbProfiles { get; set; }
        public DbSet<tohow.Domain.Database.tbSession> tbSessions { get; set; }
        #endregion

        public System.Data.Entity.Core.Objects.ObjectContext BaseContext
        {
            get { return ((IObjectContextAdapter)this).ObjectContext; }
        }
    }
}
