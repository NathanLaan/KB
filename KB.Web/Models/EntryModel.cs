using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KB.Web.Models
{
    public class EntryModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public List<string> TagList { get; set; }
    }
}