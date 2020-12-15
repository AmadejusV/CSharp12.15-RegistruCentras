using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistruCentras.Web.Models
{
    public class QuestionViewModel
    {
        [Required]
        [StringLength(100)]
        public string Question { get; set; }

        public bool IsPrivate { get; set; }
    }
}
