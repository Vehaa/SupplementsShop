using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class CommentUpsertRequest
    {
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Komentar je obavezno polje!")]
        [MaxLength(2000, ErrorMessage = "Polje Komentar ne smije biti duže od 2000 karaktera!")]
        [MinLength(2)]

        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public int? ProductId { get; set; }

    }
}
