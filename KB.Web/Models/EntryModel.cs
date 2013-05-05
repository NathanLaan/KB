using System;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using KB.Lib.Entity;

namespace KB.Web.Models
{
    public class EntryModel
    {
        //public int ParentID { get; set; } // IF null THEN Entry is top-level entry
        //public int AccountID { get; set; }


        [Required]
        [StringLength(255)]
        [Display(Name = "KB Entry Title")]
        public string Title { get; set; }

        public string Contents { get; set; }


        public string TagListString { get; set; }
    }
}