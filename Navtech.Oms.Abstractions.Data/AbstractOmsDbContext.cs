namespace Navtech.Oms.Abstractions.Data
{
    using System.Data.Entity;
    using IocServiceStack;

    using Navtech.Oms.Entities;

    [Contract]
    public abstract class AbstractOmsDbContext : DbContext
    {
        public AbstractOmsDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }

        public DbSet<ProductEntity> Products { get; set; }
        
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }

        public DbSet<BuyerEntity> Buyers { get; set; }
        public DbSet<ShippingAddressEntity> ShippingAddresses { get; set; }
    }
}
