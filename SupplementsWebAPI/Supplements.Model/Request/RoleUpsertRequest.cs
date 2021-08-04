using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class RoleUpsertRequest
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
}
