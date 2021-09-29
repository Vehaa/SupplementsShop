using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Database
{
    public class Notifications
    {
        [Key]
        public int NotificationId { get; set; }
        public int OrderId { get; set; }
        public Orders Order { get; set; }
        public int CustomerId { get; set; }
        public Users User { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }

        public bool isOpen { get; set; }

    }
}
