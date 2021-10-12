using Supplements.Model.Models;
using Supplements.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Interfaces
{
    public interface IReports
    {

        EarningReport GetEarningReport(ReportRequest request);
        List<Products> GetProductsReport(ReportRequest request);
        List<Users> GetCustomersReport(ReportRequest request);
    }
}
