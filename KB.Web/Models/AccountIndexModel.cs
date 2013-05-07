using System;
using System.Collections.Generic;
using KB.Lib.Entity;

namespace KB.Web.Models
{
    public class AccountIndexModel
    {

        public int Page { get; set; }

        public int PageSize { get; set; }

        public List<Account> List { get; set; }

        public AccountIndexModel()
        {
            this.List = new List<Account>();
        }

        public int Min
        {
            get
            {
                return (this.Page - 1) * this.PageSize + 1;
            }
        }

        public int Max
        {
            get
            {
                return (this.Page) * this.PageSize;
            }
        }

    }
}