using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class UserService : BaseCRUDService<Supplements.Model.Models.Users, UsersSearchRequest, Database.Users, UsersUpsertRequest, UsersUpsertRequest>
    {
        public UserService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Supplements.Model.Models.Users> Get(UsersSearchRequest search)
        {
            var query = _context.Set<Database.Users>().Include(x => x.City).Include(x=>x.Role).AsQueryable().Where(x => x.Role.Name.ToLower() == "klijent");

            
            
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

            List<Supplements.Model.Models.Users> result = _mapper.Map<List<Supplements.Model.Models.Users>>(list.OrderBy(x=>x.FirstName));

            foreach (var item in result)
            {
                item.CityName = _context.Users.Where(x => x.CityId == item.CityId).Select(x => x.City.Name).FirstOrDefault();
            }

            return result;
        }
        public override Supplements.Model.Models.Users Insert(UsersUpsertRequest request)
        {
          
                var entity = _mapper.Map<Users>(request);
                var roles = _context.Roles.ToList();


                entity.RoleId = roles.Where(x => x.Name.ToLower() == "klijent").Select(x => x.RoleId).FirstOrDefault();



                entity.PasswordSalt = Helpers.Hashing.GenerateSalt();
                entity.PasswordHash = Helpers.Hashing.GenerateHash(entity.PasswordSalt, request.Password);
                entity.RegistrationDate = DateTime.Now;
                entity.Status = true;
                entity.Comments = true;

                _context.Users.Add(entity);
                _context.SaveChanges();

                return _mapper.Map<Supplements.Model.Models.Users>(entity);

        }

        public override Supplements.Model.Models.Users Update(int id, UsersUpsertRequest request)
        {
            
                var entity = _context.Users.Find(id);
   
            if (!string.IsNullOrEmpty(request.Password) && !string.IsNullOrEmpty(request.PasswordConfirmation)
                && (request.Password == request.PasswordConfirmation) && entity != null)
            {
                request.PasswordSalt = entity.PasswordSalt;
                request.PasswordHash = Helpers.Hashing.GenerateHash(request.PasswordSalt, request.Password);

                entity.PasswordSalt = request.PasswordSalt;
                entity.PasswordHash = request.PasswordHash;
            }

            if (request.Status != entity.Status)
            {
                entity.Status = request.Status;
            }


                _context.Users.Update(entity);
                _context.SaveChanges();

                _mapper.Map(request, entity);

                return _mapper.Map<Supplements.Model.Models.Users>(entity);

        }

        public override Supplements.Model.Models.Users GetById(int id)
        {
            var entity = _context.Users.Find(id);
            if (entity == null)
            {
                throw new ValidationException("Korisnik nije pronađen!");
            }
            var result = _mapper.Map<Supplements.Model.Models.Users>(entity);
            result.CityName = _context.Cities.Where(x => x.CityId == result.CityId).Select(x => x.Name).FirstOrDefault();
            return result;

        }







    }
}
