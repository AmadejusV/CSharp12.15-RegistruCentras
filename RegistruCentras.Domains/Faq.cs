using System;
using System.Collections.Generic;
using System.Text;

namespace RegistruCentras.Domains
{
    public class Faq
    {
        public Guid FaqID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsPrivate { get; set; }
        public AppUser AskedBy { get; set; }
        public AppUser AnsweredBy { get; set; }

    }
}
