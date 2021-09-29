using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Supplements.Model.Models;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using SupplementsWebAPI.Helpers;
using SupplementsWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;

namespace SupplementsWebAPI.Services
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly SupplementsContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public JwtAuthenticationManager(SupplementsContext context, IMapper mapper, IWebHostEnvironment hostingEnvironment,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }
        public string Authenticate(LoginModel model)
        {
            string errorMessage = null;
            bool validPw = false;
            var entity = _context.Users.Where(x => x.UserName == model.userName).FirstOrDefault();
            if (entity == null)
            {
                errorMessage = "Greška: Korisničko ime ili lozinka nisu ispravno unešeni!";

            }

            if (entity != null)
            {
                var hash = Helpers.Hashing.GenerateHash(entity.PasswordSalt, model.password);
                if (entity.PasswordHash != hash)
                {
                    errorMessage = "Greška: Korisničko ime ili lozinka nisu ispravno unešeni!";

                }
                else
                    validPw = true;

            }
            var token = "";
            if (entity != null && validPw)
            {
                entity.Role = _context.Roles.Where(x => x.RoleId == entity.RoleId).FirstOrDefault();
                var clientRole = _context.Roles.Where(x => x.RoleId == entity.RoleId).FirstOrDefault();
                var role = entity.Role;

                if (role.Name.ToLower() == clientRole.Name.ToLower())
                {
                    if (entity.Status == false)
                        errorMessage = "Žao nam je. Deaktivirani ste sta stranice zbog neprimjerenog sadržaja!";
                }

                


                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
                IdentityOptions _options = new IdentityOptions();
                var tokenDescriptopr = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, entity.UserId.ToString()),
                    new Claim(_options.ClaimsIdentity.RoleClaimType,role.Name.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Audience = _configuration["JWT:ValidAudience"],
                    Issuer = _configuration["JWT:ValidIssuer"],
                    SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)
                };


                var securityToken = tokenHandler.CreateToken(tokenDescriptopr);
                token = tokenHandler.WriteToken(securityToken);

            }

            if (errorMessage != null)
            {
                throw new ValidationException(errorMessage);
            }

            return token;



        }

        public Supplements.Model.Models.Users Register(UsersUpsertRequest model)
        {
            var entity = _mapper.Map<Database.Users>(model);
            var users = _context.Users.ToList();
            var roles = _context.Roles.ToList();
            string errorMessage = null;

            entity.RoleId = roles.Where(x => x.Name.ToLower() == "klijent").Select(x => x.RoleId).FirstOrDefault();
            entity.Role = roles.Where(x => x.Name.ToLower() == "klijent").FirstOrDefault();

            #region secure
            if (model.UserName != null)
            {
                foreach (var item in users)
                {
                    if (item.UserName == model.UserName)
                    {

                        errorMessage="Greška: Korisničko ime je već zauzeto!";

                    }
                }
            }
            if (model.Email != null)
            {
                foreach (var item in users)
                {
                    if (item.Email == model.Email)
                    {
                        errorMessage = "Greška: Unešeni e-mail se več koristi!";
                    }
                }
            }
            if (model.Password != model.PasswordConfirmation)
            {
                errorMessage = "Greška: Lozinka i Lozinka potvrda se ne podudaraju!";

            }

            if (errorMessage != null)
            {
                throw new ValidationException(errorMessage);
            }
            var filePathName = Path.GetFileNameWithoutExtension(model.FirstName) + "-" +
            DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") +
            Path.GetExtension(model.FirstName);

            if (!string.IsNullOrEmpty(model.PhotoAsBase64))
            {
                if (model.PhotoAsBase64.Contains(","))
                {
                    model.PhotoAsBase64 = model.PhotoAsBase64.Substring(model.PhotoAsBase64.IndexOf(",") + 1);

                }
                byte[] bytes = Convert.FromBase64String(model.PhotoAsBase64);

                model.Picture = bytes;

            }
            else
            {
                string path = _hostingEnvironment.WebRootPath + "/MyImages/" + "noProfile.png";
                byte[] b = System.IO.File.ReadAllBytes(path);
                model.PhotoAsBase64 = Convert.ToBase64String(b);
                model.Picture = b;
            }
            #endregion

            entity.PhotoAsBase64 = model.PhotoAsBase64;
            entity.Photo = model.Picture;
            entity.PasswordSalt = Helpers.Hashing.GenerateSalt();
            entity.PasswordHash = Helpers.Hashing.GenerateHash(entity.PasswordSalt, model.Password);
            entity.RegistrationDate = DateTime.Now;
            entity.Status = true;
            entity.Comments = true;

            _context.Users.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Supplements.Model.Models.Users>(entity);


        }

    }
}
