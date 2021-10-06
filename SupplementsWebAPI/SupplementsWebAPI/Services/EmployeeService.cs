﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class EmployeeService: BaseCRUDService<Supplements.Model.Models.Users, EmployeeSearchRequest, Database.Users, EmployeeUpsertRequest, EmployeeUpsertRequest>
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EmployeeService(SupplementsContext context, IMapper mapper, IWebHostEnvironment hostEnvironment) : base(context, mapper)
        {
            _hostingEnvironment = hostEnvironment;
        }

        public override List<Supplements.Model.Models.Users> Get(EmployeeSearchRequest search)
        {
            var query = _context.Set<Database.Users>().Include(x => x.City).Include(x=>x.Role).AsQueryable().Where(x=>x.Role.Name.ToLower()=="uposlenik");


            if (!string.IsNullOrWhiteSpace(search?.FirstName))
            {
                query = query.Where(x => x.FirstName.ToLower().StartsWith(search.FirstName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(search?.LastName))
            {
                query = query.Where(x => x.LastName.ToLower().StartsWith(search.LastName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(search?.Email))
            {
                query = query.Where(x => x.Email.ToLower() == search.Email.ToLower());
            }
            if (!string.IsNullOrWhiteSpace(search?.UserName))
            {
                query = query.Where(x => x.UserName.ToLower().StartsWith(search.UserName.ToLower()));
            }

            if (search.Status == true)
            {
                query = query.Where(x => x.Status == true);
            }

            if (search.Status == false)
            {
                query = query.Where(x => x.Status == false);
            }

            var list = query.ToList();

            List<Supplements.Model.Models.Users> result = _mapper.Map<List<Supplements.Model.Models.Users>>(list.OrderBy(x => x.FirstName));

            foreach (var item in result)
            {
                item.CityName = _context.Users.Where(x => x.CityId == item.CityId).Select(x => x.City.Name).FirstOrDefault();
            }

            return result;
        }

        public override Supplements.Model.Models.Users Insert(EmployeeUpsertRequest request)
        {
            
            var entity = _mapper.Map<Users>(request);
            string errorMessage = null;
            var users = _context.Users.ToList();

            #region secure
            if (request.UserName != null)
            {
                foreach (var item in users)
                {
                    if (item.UserName == request.UserName)
                    {

                        errorMessage = "Greška: Korisničko ime je već zauzeto!";

                    }
                }
            }
            if (request.Email != null)
            {
                foreach (var item in users)
                {
                    if (item.Email == request.Email)
                    {
                        errorMessage = "Greška: Unešeni e-mail se več koristi!";
                    }
                }
            }
            if (request.Password != request.PasswordConfirmation)
            {
                errorMessage = "Greška: Lozinka i Lozinka potvrda se ne podudaraju!";

            }

            if (errorMessage != null)
            {
                throw new ValidationException(errorMessage);
            }
            var filePathName = Path.GetFileNameWithoutExtension(request.FirstName) + "-" +
            DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") +
            Path.GetExtension(request.FirstName);

            if (!string.IsNullOrEmpty(request.PhotoAsBase64))
            {
                if (request.PhotoAsBase64.Contains(","))
                {
                    request.PhotoAsBase64 = request.PhotoAsBase64.Substring(request.PhotoAsBase64.IndexOf(",") + 1);

                }
                byte[] bytes = Convert.FromBase64String(request.PhotoAsBase64);

            }
            else
            {
                string path = _hostingEnvironment.WebRootPath + "/MyImages/" + "noProfile.png";
                byte[] b = System.IO.File.ReadAllBytes(path);
                request.PhotoAsBase64 = Convert.ToBase64String(b);
            }
            #endregion
            var roles = _context.Roles.ToList();

                entity.RoleId = roles.Where(x => x.Name.ToLower() == "uposlenik").Select(x => x.RoleId).FirstOrDefault();



                entity.PasswordSalt = Helpers.Hashing.GenerateSalt();
                entity.PasswordHash = Helpers.Hashing.GenerateHash(entity.PasswordSalt, request.Password);
                entity.RegistrationDate = DateTime.Now;
                entity.Status = true;
                entity.Comments = true;

                _context.Users.Add(entity);
                _context.SaveChanges();

                return _mapper.Map<Supplements.Model.Models.Users>(entity);


        }


    }
}
