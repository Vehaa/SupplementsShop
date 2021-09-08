using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SupplementsWebAPI.Database;
using SupplementsWebAPI.Interfaces;
using SupplementsWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supplements.Model.Models;
using Supplements.Model.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SupplementsWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAutoMapper(typeof(Startup));
            services.AddCors();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SupplementsWebAPI", Version = "v1" });
            });

            //Entity Framework
            services.AddDbContext<SupplementsContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SupplementsConnection")));

            //Identity
            //services.AddIdentityCore<Database.Users>()
            //    .AddEntityFrameworkStores<SupplementsContext>()
            //    .AddDefaultTokenProviders();

            //Authentication
           
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ValidAudience=Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
            services.AddScoped<IJwtAuthenticationManager, JwtAuthenticationManager>();
            services.AddScoped<ICRUDService<Supplements.Model.Models.Products, ProductSearchRequest, ProductUpsertRequest, ProductUpsertRequest>, ProductsService>();
            services.AddScoped<ICRUDService<Supplements.Model.Models.ProductCategories, ProductCategorySearchRequest, ProductCategoryUpsertRequest, ProductCategoryUpsertRequest>, ProductCategoriesService>();
            services.AddScoped<ICRUDService<Supplements.Model.Models.ProductSubCategories, ProductSubCategorySearchRequest, ProductSubCategoryUpsertRequest, ProductSubCategoryUpsertRequest>, ProductSubCategoryService>();
            services.AddScoped<ICRUDService<Supplements.Model.Models.Brands, BrandSearchRequest, BrandUpsertRequest, BrandUpsertRequest>, BrandService>();
            services.AddScoped<ICRUDService<Supplements.Model.Models.Roles, RoleSearchRequest, RoleUpsertRequest, RoleUpsertRequest>, RoleService>();
            services.AddScoped<ICRUDService<Supplements.Model.Models.OrderStatus, OrderStatusSearchRequest, OrderStatusUpsertRequest, OrderStatusUpsertRequest>, OrderStatusService>();
            services.AddScoped<ICRUDService<Supplements.Model.Models.Cities, CitySearchRequest, CityUpsertRequest, CityUpsertRequest>, CityService>();
            services.AddScoped<ICRUDService<Supplements.Model.Models.Users, UsersSearchRequest, UsersUpsertRequest, UsersUpsertRequest>, UserService>();
            services.AddScoped<ICRUDService<Supplements.Model.Models.Users, EmployeeSearchRequest, EmployeeUpsertRequest, EmployeeUpsertRequest>, EmployeeService>();
            services.AddScoped<ICRUDService<Supplements.Model.Models.Orders, OrderSearchRequest, OrderUpsertRequest, OrderUpsertRequest>, OrdersService>();
            services.AddScoped<ICRUDService<Supplements.Model.Models.OrderDetails, OrderDetailsSearchRequest, OrderDetailsUpsertRequest, OrderDetailsUpsertRequest>, OrderDetailsService>();
            services.AddScoped<ICRUDService<Supplements.Model.Models.Notifications, NotificationSearchRequest, NotificationUpsertRequest, NotificationUpsertRequest>, NotificationsService>();
            services.AddScoped<ICRUDService<Supplements.Model.Models.Evaluations, EvaluationSearchRequest, EvaluationUpsertRequest, EvaluationUpsertRequest>, EvaluationService>();
            services.AddScoped<ICRUDService<Supplements.Model.Models.Comments, CommentSearchRequest, CommentUpsertRequest, CommentUpsertRequest>, CommentsService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SupplementsWebAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
