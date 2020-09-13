namespace Navtech.Oms.Business
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity;
    using IocServiceStack;
    using FluentValidation;

    using Navtech.Oms.Entities;
    using Navtech.Oms.Dtos;
    using Navtech.Oms.Abstractions.Data;
    using Navtech.Oms.Abstractions.Business;
    using Navtech.Oms.Abstractions.Business.Exceptions;
    using Navtech.Oms.Abstractions.DataValidators;

    [Service]
    public class OrderEditService : BaseService, IOrderEdit
    {
        readonly AbstractOmsDbContext _dbContext;
        readonly IOrderValidator _orderValidator;
        readonly IOrderItemCollectionValidator _orderItemsValidator;

        public OrderEditService(AbstractOmsDbContext dbContext,
                                IOrderValidator orderValidator,
                                IOrderItemCollectionValidator orderItemsValidator)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            _orderValidator = orderValidator ?? throw new ArgumentNullException(nameof(orderValidator));
            _orderItemsValidator = orderItemsValidator ?? throw new ArgumentNullException(nameof(orderItemsValidator));
        }

        //NOTE: Order can ONLY be updated when the order status is not yet dispatched or in draft order status
        public void Update(OrderEdit orderEdit)
        {

            /*Validate*/
            if (orderEdit == null)
            {
                throw new ArgumentNullException(nameof(orderEdit));
            }

            var orderEntity = _dbContext.Orders.SingleOrDefault(o => o.Id == orderEdit.OrderId);
            if (orderEntity == null)
            {
                throw new OrderNotFoundException();
            }

            // Validate data
            var onlyOrderItems = orderEdit.OrderItems.Select(item => item.OrderItem);
            _orderValidator.ValidateAndThrow(new Order {
                Buyer = orderEdit.Buyer,
                ShippingAddress = orderEdit.ShippingAddress,
                OrderItems = onlyOrderItems
            });

            /*Ensure products that are ordered must be in stock, check available count*/
            _orderItemsValidator.ValidateItemsAndThrow(onlyOrderItems, _dbContext);

            /*Update Order Object*/
            using (DbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    UpdateBuyer(orderEdit, orderEntity);
                    //UpdateOrderItems(order, buyerId, shippingAddressId);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Delete(int orderId)
        {
            // TODO: delete order can be done by that user only
            // This must be validated against that user
            // Order to be deleted only the person who created the order
            // Get user principle from current thread to validate 

            // DONT DELETE BUYER AND SHIPPING ADDRESS, IT CAN BE RESUSED FOR NEXT ORDERS

            // Delete order items
            // Delete order

            /*Update Order Object*/
            using (DbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Items to delete
                    var itemsToDelete = _dbContext.OrderItems.Where(item => item.OrderId == orderId);
                    _dbContext.OrderItems.RemoveRange(itemsToDelete);
                    _dbContext.SaveChanges();

                    // Order to delete
                    var orderToDelete = _dbContext.Orders.Single(order => order.Id == orderId);
                    _dbContext.Orders.Remove(orderToDelete);
                    _dbContext.SaveChanges();

                    // Shipping to delete
                    var addressToDelete = _dbContext.ShippingAddresses.Where(address => address.Id == orderToDelete.ShippingAddressId);
                    _dbContext.ShippingAddresses.RemoveRange(addressToDelete);

                    //Buyer to delete
                    var buyerToDelete = _dbContext.Buyers.Where(buyer => buyer.Id == orderToDelete.BuyerId);
                    _dbContext.Buyers.RemoveRange(buyerToDelete);

                    _dbContext.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        protected override void SafeDispose()
        {
            _dbContext.Dispose();
        }

        #region Private Methods
        private void UpdateBuyer(OrderEdit order, OrderEntity orderEntity)
        {
            // Get Buyer Information of this order
            BuyerEntity buyerEntity = _dbContext.Buyers.SingleOrDefault(buyer => buyer.Id == orderEntity.BuyerId);

            // Update Buyer Information
            buyerEntity.FirstName = order.Buyer.FirstName;
            buyerEntity.LastName = order.Buyer.LastName;
            buyerEntity.Email = order.Buyer.Email;
            buyerEntity.Phone = order.Buyer.Phone;

            // Get Shipping Information of this order
            ShippingAddressEntity shippingAddressEntity = _dbContext.ShippingAddresses
                                                                .SingleOrDefault
                                                                    (shippingAddress => shippingAddress.Id == orderEntity.ShippingAddressId);

            // Update Shipping Address
            shippingAddressEntity.AddressLine1 = order.ShippingAddress.AddressLine1;
            shippingAddressEntity.AddressLine2 = order.ShippingAddress.AddressLine2;
            shippingAddressEntity.City = order.ShippingAddress.City;
            shippingAddressEntity.State = order.ShippingAddress.State;
            shippingAddressEntity.Zip = order.ShippingAddress.Zip;

            _dbContext.SaveChanges();

        }

        private void UpdateOrder(OrderEntity orderEntity)
        {
            //*. Update Order 
            orderEntity.LastUpdatedTimestamp = DateTime.Now;

            //*. Save all changes in database
            _dbContext.SaveChanges();
        }

        private void UpdateOrderItems(OrderEdit order, OrderEntity orderEntity)
        {
            //*. Get product information in order to store the information that has been viewed by buyer.
            // Get all the data in single call
            var distinctProducts = order.OrderItems.Select(item => item.OrderItem.ProductId).Distinct().ToArray();
            List<ProductEntity> products = (from product in _dbContext.Products
                                            where distinctProducts.Contains(product.Id) && product.IsActive
                                            select product).ToList();

            // Iterate through each item and add 
            foreach (var orderItem in order.OrderItems)
            {
                if (orderItem == null)
                {
                    continue;
                }

                switch (orderItem.State)
                {
                    case ObjectState.Add:
                        AddOrderItem(orderEntity, products, orderItem);
                        break;

                    case ObjectState.Modify:
                        UpdateOrderItem(orderEntity, products, orderItem);
                        break;

                    case ObjectState.Delete:
                        DeleteOrderItem(orderEntity, orderItem);
                        break;
                }
            }

            //*. Save all changes in database
            _dbContext.SaveChanges();
        }

        private void AddOrderItem(OrderEntity orderEntity, List<ProductEntity> products, OrderItemWithState orderItemWithState)
        {
            var productEntity = products.SingleOrDefault(product => product.Id == orderItemWithState.OrderItem.ProductId);
            if (productEntity == null)
            {
                throw new ProductNotFoundException("Specified product is not found.");
            }

            var orderItemEntity = new OrderItemEntity
            {
                OrderId = orderEntity.Id,
                Quantity = orderItemWithState.OrderItem.Quantity,

                /* Product information needs to be recorded, in case 
                 if master product data is changed then you'll lose the orginal information that has been viewed 
                 at the time of order creation */

                ProductId = productEntity.Id,
                ProductName = productEntity.Name,
                Height = productEntity.Height,
                Weight = productEntity.Weight,
                Barcode = productEntity.Name,
                Image = productEntity.Name,
                SKU = productEntity.SKU,
            };
            _dbContext.OrderItems.Add(orderItemEntity);
        }

        private void UpdateOrderItem(OrderEntity orderEntity, List<ProductEntity> products, OrderItemWithState orderItemWithState)
        {
            if (orderItemWithState.OrderItemId == null)
            {
                throw new OrderItemIdNotProvidedException();
            }

            var productEntity = products.SingleOrDefault(product => product.Id == orderItemWithState.OrderItem.ProductId);
            if (productEntity == null)
            {
                throw new ProductNotFoundException("Specified product is not found.");
            }

            // Get order item to update
            var orderItemEntity = _dbContext.OrderItems.SingleOrDefault(item => item.Id == orderItemWithState.OrderItemId.Value && item.OrderId == orderEntity.Id);
            if (orderItemEntity == null)
            {
                throw new OrderItemNotFoundException();
            }

            //Update Order Item
            orderItemEntity.Quantity = orderItemWithState.OrderItem.Quantity;

            /* Product information needs to be recorded, in case 
             if master product data is changed then you'll lose the orginal information that has been viewed 
             at the time of order creation */

            orderItemEntity.ProductId = productEntity.Id;
            orderItemEntity.ProductName = productEntity.Name;
            orderItemEntity.Height = productEntity.Height;
            orderItemEntity.Weight = productEntity.Weight;
            orderItemEntity.Barcode = productEntity.Name;
            orderItemEntity.Image = productEntity.Name;
            orderItemEntity.SKU = productEntity.SKU;
        }

        private void DeleteOrderItem(OrderEntity orderEntity, OrderItemWithState orderItemWithState)
        {
            if (orderItemWithState.OrderItemId == null)
            {
                throw new OrderItemIdNotProvidedException();
            }

            // Get order item to update
            var orderItemEntity = _dbContext.OrderItems.SingleOrDefault(item => item.Id == orderItemWithState.OrderItemId.Value && item.OrderId == orderEntity.Id);
            if (orderItemEntity == null)
            {
                throw new OrderItemNotFoundException();
            }

            // Get order item to delete
            var orderItemEntityToDelete = _dbContext.OrderItems.Where(item => item.Id == orderItemWithState.OrderItemId.Value && item.OrderId == orderEntity.Id);
            if (orderItemEntity == null)
            {
                throw new OrderItemNotFoundException();
            }
            _dbContext.OrderItems.RemoveRange(orderItemEntityToDelete);

        }

        private OrderEntity CreateOrderEntity(int buyerId, int shippingAddressId)
        {
            // Add Order 
            var orderEntity = new OrderEntity
            {
                BuyerId = buyerId,
                ShippingAddressId = shippingAddressId,
                OrderCreatedTimestamp = DateTime.Now,
                OrderStatusId = OrderStatus.Placed
            };
            _dbContext.Orders.Add(orderEntity);
            _dbContext.SaveChanges();
            return orderEntity;
        }
        #endregion

    }
}
