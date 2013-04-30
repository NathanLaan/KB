using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KB.Lib.Data
{
    public class Entry : BaseEntity
    {
        public int ParentID { get; set; } // IF null THEN Entry is top-level entry
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public DateTime Created { get; set; }
        public List<string> TagList { get; set; }
    }
}