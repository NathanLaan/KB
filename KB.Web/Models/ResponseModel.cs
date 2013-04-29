using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KB.Web.Models
{
    public class ResponseModel
    {
        public int ID { get; set; }
        public int AuthorID { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
    }
}