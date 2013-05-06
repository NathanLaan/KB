using System;
using System.Collections.Generic;
using KB.Lib.Entity;

namespace KB.Web.Models
{
    public class EntryDetailsModel
    {

        public Entry Entry { get; set; }

        public List<Entry> ResponseList { get; set; }

        public EntryDetailsModel()
        {
            this.ResponseList = new List<Entry>();
        }

    }
}