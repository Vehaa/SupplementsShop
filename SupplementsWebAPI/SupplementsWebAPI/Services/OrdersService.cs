using AutoMapper;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class OrdersService : BaseCRUDService<Supplements.Model.Models.Orders, OrderSearchRequest, Database.Orders, OrderUpsertRequest, OrderUpsertRequest>
    {
        public OrdersService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override Supplements.Model.Models.Orders Insert(OrderUpsertRequest request)
        {

            string errorMessage = null;
            if (request.orderProductList.Count() == 0)
                errorMessage = "Greška: Niste odabrali niti jedan proizvod!";
            var orderStatus = _context.OrderStatus.ToList();

            var dbProducts = new  List<Products>();
            foreach (var item in request.orderProductList)
            {
                var product = _context.Products.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                if (product.ProductId == item.ProductId)
                {
                    if (product.UnitInStock < item.UnitOnOrder)
                        errorMessage = "Greška: Za proizvod" + product.Name+ ", na stanju imamo " + product.UnitInStock + " komada.";
                    else
                    {
                        product.UnitInStock -= item.UnitOnOrder;
                        _context.Products.Update(product);
                        _context.SaveChanges();
                    }

                }
            }
            if (errorMessage != null)
            {

                throw new ValidationException(errorMessage);

            }
            var order = new Orders();
            order.CustomerId = request.CustomerId;
            order.ShippingPrice = request.ShippingPrice;
            order.OrderDate = DateTime.Now;
            //order.ShippedDate = null;
            order.OrderStatusId = orderStatus.Where(x => x.StatusName.ToLower() == "na čekanju").Select(x => x.OrderStatusId).FirstOrDefault();
            _context.Orders.Add(order);
            _context.SaveChanges();
            foreach (var item in request.orderProductList)
            {
                var orderDetails = new OrderDetails();
                orderDetails.UnitPrice = item.UnitPrice;
                orderDetails.Quantity = item.UnitOnOrder;
                orderDetails.Discount = item.Discount;
                orderDetails.TotalPrice = item.TotalPrice;
                orderDetails.OrderId = order.OrderId;
                orderDetails.ProductId = item.ProductId;
                _context.Add(orderDetails);
                _context.SaveChanges();
            }


            return _mapper.Map<Supplements.Model.Models.Orders>(order);

        }

        public override List<Supplements.Model.Models.Orders> Get(OrderSearchRequest search)
        {
            var query = _context.Set<Orders>().AsQueryable();

            if (search.CustomerId != null)
            {
                query = query.Where(x => x.CustomerId == search.CustomerId);
            }
            var list = query.ToList();
            List<Supplements.Model.Models.Orders> result = _mapper.Map<List<Supplements.Model.Models.Orders>>(list);

            return result;
        }
    }
}
