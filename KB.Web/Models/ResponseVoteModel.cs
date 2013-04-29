using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KB.Web.Models
{
    public class ResponseVoteModel
    {
        public int ID { get; set; }
        public int ResponseID { get; set; }
        public int AuthorID { get; set; }
        public int Score { get; set; }
    }
}