﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class CommentSearchRequest
    {
        public int? CommentId { get; set; }
        public string Text { get; set; }
        public DateTime? DateTime { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }

    }
}
