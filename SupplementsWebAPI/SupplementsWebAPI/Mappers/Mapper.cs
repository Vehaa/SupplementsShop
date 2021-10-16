using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SupplementsWebAPI.Mappers
{
    public class Mapper:Profile
    {

        public Mapper()
        {
            CreateMap<Database.Users, Supplements.Model.Models.Users>().ReverseMap();
            CreateMap<Database.Users, Supplements.Model.Request.UsersUpsertRequest>().ReverseMap();
            CreateMap<Database.Users, Supplements.Model.Request.EmployeeUpsertRequest>().ReverseMap();
            CreateMap<Database.Users, Supplements.Model.Request.PasswordRequest>().ReverseMap();

            CreateMap<Database.Brands, Supplements.Model.Models.Brands>().ReverseMap();
            CreateMap<Database.Brands, Supplements.Model.Request.BrandUpsertRequest>().ReverseMap();

            CreateMap<Database.Cities, Supplements.Model.Models.Cities>().ReverseMap();
            CreateMap<Database.Cities, Supplements.Model.Request.CityUpsertRequest>().ReverseMap();

            CreateMap<Database.Comments, Supplements.Model.Models.Comments>().ReverseMap();
            CreateMap<Database.Comments, Supplements.Model.Request.CommentUpsertRequest>().ReverseMap();

            CreateMap<Database.OrderDetails, Supplements.Model.Models.OrderDetails>().ReverseMap();
            CreateMap<Database.OrderDetails, Supplements.Model.Request.OrderDetailsUpsertRequest>().ReverseMap();

            CreateMap<Database.Orders, Supplements.Model.Models.Orders>().ReverseMap();
            CreateMap<Database.Orders, Supplements.Model.Request.OrderUpsertRequest>().ReverseMap();

            CreateMap<Database.ProductCategories, Supplements.Model.Models.ProductCategories>().ReverseMap();
            CreateMap<Database.ProductCategories, Supplements.Model.Request.ProductCategoryUpsertRequest>().ReverseMap();

            CreateMap<Database.ProductSubCategories, Supplements.Model.Models.ProductSubCategories>().ReverseMap();
            CreateMap<Database.ProductSubCategories, Supplements.Model.Request.ProductSubCategoryUpsertRequest>().ReverseMap();

            CreateMap<Database.Products, Supplements.Model.Models.Products>().ReverseMap();
            CreateMap<Database.Products, Supplements.Model.Request.ProductUpsertRequest>().ReverseMap();


            CreateMap<Database.Evaluations, Supplements.Model.Models.Evaluations>().ReverseMap();
            CreateMap<Database.Evaluations, Supplements.Model.Request.EvaluationUpsertRequest>().ReverseMap();

            CreateMap<Database.Roles, Supplements.Model.Models.Roles>().ReverseMap();
            CreateMap<Database.Roles, Supplements.Model.Request.RoleUpsertRequest>().ReverseMap();

            CreateMap<Database.OrderStatus, Supplements.Model.Models.OrderStatus>().ReverseMap();
            CreateMap<Database.OrderStatus, Supplements.Model.Request.OrderStatusUpsertRequest>().ReverseMap();





        }
    }
}
