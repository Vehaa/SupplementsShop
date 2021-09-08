﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class EmployeeService: BaseCRUDService<Supplements.Model.Models.Users, EmployeeSearchRequest, Database.Users, EmployeeUpsertRequest, EmployeeUpsertRequest>
    {
        public EmployeeService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
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
