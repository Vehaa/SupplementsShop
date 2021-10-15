using AutoMapper;
using Supplements.Model.Models;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using SupplementsWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class ReportsService : IReports
    {
        private readonly SupplementsContext _context;
        protected readonly IMapper _mapper;


        public ReportsService(SupplementsContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public EarningReport GetEarningReport(ReportRequest request)
        {
            EarningReport report = new EarningReport();
            string errorMessage = null;

            if (request.DateFrom == null || request.DateTo == null)
                errorMessage = "Molimo vas da izaberete datume!";
            if (request.DateTo < request.DateFrom)
                errorMessage = "Molimo vas da unesete validne datume!";
            if (errorMessage != null)
            {
                throw new ValidationException(errorMessage);
            }
            var orders = _context.Orders.Where(x => (x.ShippedDate <= request.DateTo) && (x.ShippedDate >= request.DateFrom)).ToList();
            var orderDetails = new List<Database.OrderDetails>();
            var allOrderDetails = _context.OrderDetails.ToList();
            foreach (var item in orders)
            {
                foreach (var o in allOrderDetails)
                {
                    if (item.OrderId == o.OrderId)
                    {
                        orderDetails.Add(o);

                    }
                }
            }
            var products = new List<Database.Products>();

            foreach (var item in orderDetails)
            {
                report.Total += item.TotalPrice;
                report.ProductSold+=item.Quantity;
            }

            foreach (var item in products)
            {
                report.ProductSold += item.Counter;
            }
            report.TotalOrders = orders.Count();
            report.DateFrom = request.DateFrom;
            report.DateTo = request.DateTo;
            

            
            return report;
        }

        public List<Supplements.Model.Models.Products> GetProductsReport(ReportRequest request)
        {

            string errorMessage = null;
            if (request.DateFrom == null || request.DateTo == null)
                errorMessage = "Molimo vas da izaberete datume!";
            if (request.DateTo < request.DateFrom)
                errorMessage = "Molimo vas da unesete validne datume!";
            if (errorMessage != null)
            {
                throw new ValidationException(errorMessage);
            }
            var orders = _context.Orders.Where(x => (x.ShippedDate <= request.DateTo) && (x.ShippedDate >= request.DateFrom)).ToList();
            var orderDetails = new List<Database.OrderDetails>();
            var allOrderDetails = _context.OrderDetails.ToList();
            foreach (var item in orders)
            {
                foreach (var o in allOrderDetails)
                {
                    if (item.OrderId == o.OrderId)
                    {
                        orderDetails.Add(o);

                    }
                }
            }
            var products = new List<Database.Products>();

            foreach (var item in orderDetails)
            {
                products.Add(_context.Products.Where(x => x.ProductId == item.ProductId).FirstOrDefault());
            }
            products = products.Distinct().ToList();
            List<Supplements.Model.Models.Products> result = _mapper.Map<List<Supplements.Model.Models.Products>>(products.OrderByDescending(x=>(x.TotalPrice * x.Counter)));


            return result;


        }

        public List<Supplements.Model.Models.Users> GetCustomersReport(ReportRequest request)
        {
            string errorMessage = null;
            if (request.DateFrom == null || request.DateTo == null)
                errorMessage = "Molimo vas da izaberete datume!";
            if (request.DateTo < request.DateFrom)
                errorMessage = "Molimo vas da unesete validne datume!";
            if (errorMessage != null)
            {
                throw new ValidationException(errorMessage);
            }

            var orders = _context.Orders.Where(x => (x.ShippedDate <= request.DateTo) && (x.ShippedDate >= request.DateFrom)).ToList();
            var customers = new List<Database.Users>();

            foreach (var item in orders)
            {
               customers.Add(_context.Users.Where(x => x.UserId == item.CustomerId).FirstOrDefault());

            }

            customers = customers.Distinct().ToList();
            

            var orderDetails = new List<Database.OrderDetails>();
            var allOrderDetails = _context.OrderDetails.ToList();
            foreach (var item in orders)
            {
                foreach (var o in allOrderDetails)
                {
                    if (item.OrderId == o.OrderId)
                    {
                        orderDetails.Add(o);

                    }
                }
            }

            List<Supplements.Model.Models.Users> result = _mapper.Map<List<Supplements.Model.Models.Users>>(customers);

            foreach (var item in orders)
            {
                foreach (var i in result)
                {
                    if (item.CustomerId == i.UserId)
                    {
                        i.TotalOrders++;
                        foreach (var o in orderDetails)
                        {
                            if(item.OrderId==o.OrderId)
                                i.TotalMoney += o.TotalPrice;
                        }
                    }
                }
            }

            foreach (var item in result)
            {
                item.CityName = _context.Users.Where(x => x.CityId == item.CityId).Select(x => x.City.Name).FirstOrDefault();
            }

            result = result.OrderByDescending(x => x.TotalMoney).ToList();
            
            return result;
        }
    }
}
