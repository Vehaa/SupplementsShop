﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class NotificationSearchRequest
    {
        public int? NotificationId { get; set; }
        public int? OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Text { get; set; }
        public bool isOpen { get; set; }

    }
}
