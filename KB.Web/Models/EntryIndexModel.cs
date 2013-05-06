using System;
using System.Collections.Generic;
using KB.Lib.Entity;

namespace KB.Web.Models
{
    public class EntryIndexModel
    {

        public List<Entry> EntryList { get; set; }

        public EntryIndexModel()
        {
            this.EntryList = new List<Entry>();
        }

    }
}