
namespace SalesManagementControl.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SalesManagementEntities : DbContext
    {
        public SalesManagementEntities()
            : base("name=SalesManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Client> Client { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<PurchaseStatus> PurchaseStatus { get; set; }
        public DbSet<User> User { get; set; }
    }
}
