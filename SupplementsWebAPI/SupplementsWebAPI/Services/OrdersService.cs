using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
                        errorMessage = "Greška: Za proizvod " + product.Name+ ", na stanju imamo " + product.UnitInStock + " komada.";
                    //else
                    //{
                        
                    //    _context.Products.Update(product);
                    //    _context.SaveChanges();
                    //}

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
            order.ShippedDate = null;
            order.OrderStatusId = orderStatus.Where(x => x.StatusName.ToLower() == "na čekanju").Select(x => x.OrderStatusId).FirstOrDefault();
            _context.Orders.Add(order);
            _context.SaveChanges();
            foreach (var item in request.orderProductList)
            {
                var orderDetails = new OrderDetails();
                orderDetails.UnitPrice = item.UnitPrice;
                orderDetails.Quantity = item.UnitOnOrder;
                orderDetails.Discount = item.Discount;
                orderDetails.TotalPrice = item.TotalPrice * item.UnitOnOrder;
                orderDetails.OrderId = order.OrderId;
                orderDetails.ProductId = item.ProductId;
                _context.Add(orderDetails);
                _context.SaveChanges();
            }

            return _mapper.Map<Supplements.Model.Models.Orders>(order);

        }

        public override List<Supplements.Model.Models.Orders> Get(OrderSearchRequest search)
        {
            var query = _context.Set<Orders>().Include(x=>x.OrderStatus).AsQueryable();
            var oDetails = _context.OrderDetails.ToList();

            if (search.OrderStatusName != null)
            {
                query = query.Where(x => x.OrderStatus.StatusName == search.OrderStatusName);
            }

            if (search.OrderId != null)
            {
                query = query.Where(x => x.OrderId == search.OrderId);
            }

            if (search.CustomerId != null)
            {
                query = query.Where(x => x.CustomerId == search.CustomerId);
            }

            if (search.CustomerId != null && search.OrderId!=null)
            {
                query = query.Where(x => x.CustomerId == search.CustomerId);
            }

            if (search.OrderStatusId != null)
            {
                query = query.Where(x => x.OrderStatusId == search.OrderStatusId);
            }

            var list = query.ToList();
            List<Supplements.Model.Models.Orders> result = _mapper.Map<List<Supplements.Model.Models.Orders>>(list.OrderByDescending(x=>x.OrderDate));
            List<Supplements.Model.Models.OrderDetails> orderDetails = _mapper.Map<List<Supplements.Model.Models.OrderDetails>>(oDetails);

            foreach (var item in result)
            {
                item.OrderStatusName = _context.OrderStatus.Where(x => x.OrderStatusId == item.OrderStatusId).Select(x => x.StatusName).FirstOrDefault();
                item.Client = _mapper.Map<Supplements.Model.Models.Users>(_context.Users.Where(x => x.UserId == item.CustomerId).FirstOrDefault());
                item.Client.CityName = _context.Cities.Where(x => x.CityId == item.Client.CityId).Select(x => x.Name).FirstOrDefault();
            }
            


            foreach (var item in result)
            {
                item.OrderDetailsList = new List<Supplements.Model.Models.OrderDetails>();
                foreach (var o in orderDetails)
                {
                    if (item.OrderId == o.OrderId)
                    {
                        item.OrderDetailsList.Add(o);

                    }
                }
                
            }
            foreach (var item in orderDetails)
            {
                item.ProductName = _context.Products.Where(x => x.ProductId == item.ProductId).Select(x => x.Name).FirstOrDefault();
                item.Photo = _context.Products.Where(x => x.ProductId == item.ProductId).Select(x => x.PhotoAsBase64).FirstOrDefault();
            }
            

            return result;
        }

        public override Supplements.Model.Models.Orders Update(int id, OrderUpsertRequest request)
        {
            var entity = _context.Orders.Where(x=>x.OrderId==id).Include(x=>x.OrderStatus).FirstOrDefault();
            var orderDetails = _context.OrderDetails.Where(x=>x.OrderId==id).ToList();

            var orderStatus = _context.OrderStatus.ToList();

            if (request.OrderStatusName.ToLower() == "odobrena")
            {
                entity.OrderStatusId = orderStatus.Where(x => x.StatusName.ToLower() == "odobrena").Select(x => x.OrderStatusId).FirstOrDefault();
                
            }

            if (request.OrderStatusName.ToLower() == "odbijena")
            {
                entity.OrderStatusId = orderStatus.Where(x => x.StatusName.ToLower() == "odbijena").Select(x => x.OrderStatusId).FirstOrDefault();
                entity.Reason = request.Reason;
                var products = new List<Products>();
                foreach (var item in orderDetails)
                {
                    products.Add(_context.Products.Where(x => x.ProductId == item.ProductId).FirstOrDefault());
                    foreach (var p in products)
                    {
                        if(entity.OrderStatus.StatusName.ToLower()!="odobrena")
                            p.UnitInStock += item.Quantity;
                    }
                }
                foreach (var item in products)
                {
                    _context.Products.Update(item);
                }
                _context.SaveChanges();

            }

            if (request.OrderStatusName.ToLower() == "isporučena")
            {
                entity.OrderStatusId = orderStatus.Where(x => x.StatusName.ToLower() == "isporučena").Select(x => x.OrderStatusId).FirstOrDefault();
                entity.ShippedDate = DateTime.Now;
                var products = new List<Products>();

                foreach (var item in orderDetails)
                {
                    products.Add(_context.Products.Where(x => x.ProductId == item.ProductId).FirstOrDefault());
                    foreach (var p in products)
                    {
                        if (item.ProductId == p.ProductId)
                        {
                            p.UnitInStock -= item.Quantity;
                            p.Counter += item.Quantity;
                        }
                    }
                }
                foreach (var item in products)
                {
                    _context.Products.Update(item);
                }
                _context.SaveChanges();

            }

            _context.Orders.Update(entity);
            _context.SaveChanges();

            var result= _mapper.Map<Supplements.Model.Models.Orders>(entity);

            result.OrderStatusName = orderStatus.Where(x => x.OrderStatusId == result.OrderStatusId).Select(x => x.StatusName).FirstOrDefault();

            return result;
        }
    }
}
