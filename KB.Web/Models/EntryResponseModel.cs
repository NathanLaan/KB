using System;
using System.Collections.Generic;
using KB.Lib.Entity;

namespace KB.Web.Models
{
    public class EntryResponseModel
    {
        public Entry Entry { get; set; }

        public EntryResponseModel()
        {
            this.Entry = new Entry();
        }
    }
}