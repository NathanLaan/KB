﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KB.Lib.Entity
{
    public class Entry : BaseEntity
    {
        public int ParentID { get; set; } // IF null THEN Entry is top-level entry
        public int AccountID { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public DateTime Created { get; set; }
        public List<string> TagList { get; set; }
    }
}