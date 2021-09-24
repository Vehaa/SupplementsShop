using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Database
{
    public class Comments
    {
        [Key]
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public int? ProductId { get; set; }
        public Products Product { get; set; }
    }
}
