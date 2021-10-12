using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supplements.Model.Models;
using Supplements.Model.Request;
using SupplementsWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReports _reportService;

        public ReportsController(IReports reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [Route("Earning")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik")]

        public IActionResult GetEarningReport([FromQuery] ReportRequest request)
        {
            try
            {
                return Ok(_reportService.GetEarningReport(request));
            }
            catch (ValidationException e)
            {

                return BadRequest(e.Message);

            }
        }

        [HttpGet]
        [Route("TopProducts")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik")]
        public IActionResult GetProductsReport([FromQuery] ReportRequest request)
        {
            try
            {
                return Ok(_reportService.GetProductsReport(request));
            }
            catch (ValidationException e)
            {

                return BadRequest(e.Message);

            }
        }

        [HttpGet]
        [Route("TopCustomers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik")]
        public IActionResult GetCustomersReport([FromQuery] ReportRequest request)
        {
            try
            {
                return Ok(_reportService.GetCustomersReport(request));
            }
            catch (ValidationException e)
            {

                return BadRequest(e.Message);

            }
        }
    }
}
