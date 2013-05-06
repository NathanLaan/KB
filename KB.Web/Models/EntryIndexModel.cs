using System;
using System.Collections.Generic;
using KB.Lib.Entity;

namespace KB.Web.Models
{
    public class EntryIndexModel
    {

        public int Page { get; set; }

        public int PageSize { get; set; }

        public List<Entry> EntryList { get; set; }

        public EntryIndexModel()
        {
            this.EntryList = new List<Entry>();
        }

    }
}