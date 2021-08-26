using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Models
{
    public class Brands
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoAsBase64 { get; set; }
        public byte[] LogoAsByteArray { get; set; }


        public int ProductCounter { get; set; }
    }
}
