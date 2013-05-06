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

        public int EntryMin
        {
            get
            {
                return (this.Page - 1) * this.PageSize + 1;
            }
        }

        public int EntryMax
        {
            get
            {
                return (this.Page) * this.PageSize;
            }
        }

    }
}