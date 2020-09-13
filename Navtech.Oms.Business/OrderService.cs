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
    using Navtech.Oms.Abstractions.Communication;
    

    [Service]
    public class OrderService : BaseService, IOrder
    {
        readonly AbstractOmsDbContext _dbContext;
        readonly IOrderValidator _orderValidator;
        readonly IOrderItemCollectionValidator _orderItemsValidator;
        readonly IEmailTemplate _emailTemplate;
        readonly IEmail _email;

        public OrderService(AbstractOmsDbContext dbContext,
                            IOrderValidator orderValidator,
                            IOrderItemCollectionValidator orderItemsValidator,
                            IEmailTemplate emailTemplate,
                            IEmail email)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            _orderValidator = orderValidator ?? throw new ArgumentNullException(nameof(orderValidator));
            _orderItemsValidator = orderItemsValidator ?? throw new ArgumentNullException(nameof(orderItemsValidator));

            _emailTemplate = emailTemplate ?? throw new ArgumentNullException(nameof(emailTemplate));
            _email = email ?? throw new ArgumentNullException(nameof(email));
            
        }

        public void PlaceOrder(Order order)
        {
            /*Validate*/
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            // TODO: Data Cleanse: Merge similar products that exist in collection for easy process by adding together their quantity
            // 

            _orderValidator.ValidateAndThrow(order);

            /*Ensure products that are ordered must be in stock, check available count*/
            _orderItemsValidator.ValidateItemsAndThrow(order.OrderItems, _dbContext);

            /*Place Order*/
            int orderId = default(int);
            using (DbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Create Order 
                    (int buyerId, int shippingAddressId) = CreateBuyer(order);
                    orderId = PlaceOrder(order, buyerId, shippingAddressId);

                    //TODO: Update Inventory

                    transaction.Commit();
                }
                catch 
                {
                    transaction.Rollback();
                    throw;
                }
            }

            /* Send notification */
            SendNotification(order, orderId);
        }


        protected override void SafeDispose()
        {
            _dbContext.Dispose();
        }

        #region Private Methods
        private (int buyerId, int shippingAddressId) CreateBuyer(Order order)
        {
            // Add Buyer Information
            var buyer = new BuyerEntity
            {
                FirstName = order.Buyer.FirstName,
                LastName = order.Buyer.LastName,
                Email = order.Buyer.Email,
                Phone = order.Buyer.Phone,
            };
            _dbContext.Buyers.Add(buyer);

            // Add Shipping Address
            var shippingAddress = new ShippingAddressEntity
            {
                AddressLine1 = order.ShippingAddress.AddressLine1,
                AddressLine2 = order.ShippingAddress.AddressLine2,
                City = order.ShippingAddress.City,
                State = order.ShippingAddress.State,
                Zip = order.ShippingAddress.Zip
            };
            _dbContext.ShippingAddresses.Add(shippingAddress);

            _dbContext.SaveChanges();

            return (buyer.Id, shippingAddress.Id);
        }

        private int PlaceOrder(Order order, int buyerId, int shippingAddressId)
        {
            //*. Add Order 
            OrderEntity orderEntity = CreateOrderEntity(buyerId, shippingAddressId);

            //*. Add Order Items
            CreateOrderItems(order, orderEntity);
           
            return orderEntity.Id; //return orderid
        }

        private void CreateOrderItems(Order order, OrderEntity orderEntity)
        {

            //*. Get product information in order to store the information that has been viewed by buyer.
            // Get all the data in single call
            var distinctProducts = order.OrderItems.Select(item => item.ProductId).Distinct().ToArray();
            List<ProductEntity> products = (from product in _dbContext.Products
                                           where distinctProducts.Contains(product.Id) && product.IsActive
                                           select product).ToList();

            // Iterate through each item and add 
            foreach (var orderItem in order.OrderItems)
            {
                var productEntity = products.SingleOrDefault(product => product.Id == orderItem.ProductId);
                if (productEntity == null)
                {
                    throw new ProductNotFoundException("Specified product is not found.");
                }

                AddOrderItem(orderEntity, productEntity, orderItem);
            }

            //*. Save all changes in database
            _dbContext.SaveChanges();

        }

        private void AddOrderItem(OrderEntity orderEntity, ProductEntity productEntity, OrderItem orderItem)
        {
            var orderItemEntity = new OrderItemEntity
            {
                OrderId = orderEntity.Id,
                Quantity = orderItem.Quantity,

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

        private void SendNotification(Order order, int orderId)
        {
            // This can be queued and send it by diffent service (communication service)
            TemplateResult templateResult = _emailTemplate.GetInvoiceEmailMessage(order, orderId);
            EmailMessage emailMessage = new EmailMessage
            {
                ToAddress = new string[] { order.Buyer.Email }
            };

            try
            {
                // TODO: async
                _email.Send(emailMessage);
            }
            catch (Exception)
            {
                // DONT RETHROW HERE
                // TODO: handle exception and log it, ensure retry/queue it for sending message
            }
        }
        #endregion
    }
}
