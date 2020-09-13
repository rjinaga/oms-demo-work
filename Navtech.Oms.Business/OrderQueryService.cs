namespace Navtech.Oms.Business
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using IocServiceStack;

    using Navtech.Oms.Dtos;
    using Navtech.Oms.Abstractions.Data;
    using Navtech.Oms.Abstractions.Business;


    [Service]
    public class OrderQueryService : BaseService, IOrderQuery
    {
        readonly AbstractOmsDbContext _dbContext;

        public OrderQueryService(AbstractOmsDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<OrderView> GetAllOrders()
        {
            // if we have pagination, and we know the page size then
            // specify the count parameter in list, so that it does not to recreate array dynalically 
            // as performance stand point

            var orders = new List<OrderView>(); 

            //TODO: Simplify procedure exuction by creating helper methods
            using (var command = _dbContext.Database.Connection.CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Sales.GetAllOrders";
                FillBuyerOrders(orders, command);
            }

            return orders;
        }


        public IEnumerable<OrderView> GetOrdersByBuyer(int buyerId)
        {
            // if we have pagination, and we know the page size then
            // specify the count parameter in list, so that it does not to recreate array dynalically 
            // as performance stand point

            var orders = new List<OrderView>();

            //TODO: Simplify procedure exuction by creating helper methods
            using (var command = _dbContext.Database.Connection.CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Sales.GetOrdersByBuyerId";
                command.Parameters.Add(new SqlParameter("@BuyerId", buyerId));

                FillBuyerOrders(orders, command);
            }

            return orders;

        }

        protected override void SafeDispose()
        {
            _dbContext.Dispose();
        }


        private void FillBuyerOrders(List<OrderView> orders, System.Data.Common.DbCommand command)
        {
            try
            {
                _dbContext.Database.Connection.Open();
                using (var reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        // Since all mandotary columns in tables used in this procedure,
                        // so we don't need to check for DBNULL for all here
                        // other approach, we can write extension and keep the code clean, 
                        // it takes care of DBNULL and conversion
                        orders.Add(new OrderView
                        {
                            OrderId = Convert.ToInt32(reader["OrderId"]),
                            Buyer = new Buyer
                            {
                                FirstName = reader["Firstname"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString()
                            },
                            ShippingAddress = new ShippingAddress
                            {
                                AddressLine1 = reader["AddressLine1"].ToString(),
                                AddressLine2 = reader["AddressLine2"] == DBNull.Value ? null : reader["AddressLine2"].ToString(),
                                City = reader["City"].ToString(),
                                State = reader["State"].ToString(),
                                Zip = reader["Zip"].ToString(),
                            },
                            ItemsCount = Convert.ToInt32(reader["OrderId"]),
                            OrderDateTime = Convert.ToDateTime(reader["OrderCreatedTimestamp"]),
                        });
                    }
                };
            }
            finally
            {
                _dbContext.Database.Connection.Close();
            }
        }
    }
}
