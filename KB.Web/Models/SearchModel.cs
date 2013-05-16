using System;
using System.Collections.Generic;
using KB.Lib.Entity;

namespace KB.Web.Models
{

    public class SearchModel : PagedModel
    {
        public string SearchString { get; set; }
        public List<Entry> List { get; set; }
        public SearchModel()
        {
            this.List = new List<Entry>();
        }
    }

}