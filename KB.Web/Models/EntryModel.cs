using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KB.Web.Models
{
    public class EntryModel
    {
        //public int ParentID { get; set; } // IF null THEN Entry is top-level entry
        public int AccountID { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string TagListString { get; set; }
    }
}