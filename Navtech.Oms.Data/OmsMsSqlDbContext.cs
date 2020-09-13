namespace Navtech.Oms.Data
{
    using System.Data.Entity;
    using IocServiceStack;

    using Navtech.Oms.Abstractions.Data;
    using Navtech.Oms.Entities;

    [Service]
    public class OmsMsSqlDbContext : AbstractOmsDbContext
    {
        public OmsMsSqlDbContext(): base("name=OrderManagementDb") //TODO: This string injection can be done through Ioc
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Sales");

            modelBuilder.Entity<ProductEntity>().ToTable("Product", "Inventory");

            modelBuilder.Entity<OrderEntity>().ToTable("Order");
            modelBuilder.Entity<OrderItemEntity>().ToTable("OrderItem");

            modelBuilder.Entity<BuyerEntity>().ToTable("Buyer");
            modelBuilder.Entity<ShippingAddressEntity>().ToTable("ShippingAddress");
        }
    }
}
