using System;
using System.Collections.Generic;
using KB.Lib.Entity;

namespace KB.Web.Models
{
    public class AccountDetailsModel
    {

        public Account Account { get; set; }

        public List<Entry> EntryList { get; set; }

        public List<Entry> ReplyList { get; set; }

        public int TotalEntryCount { get; set; }
        public int TotalReplyCount { get; set; }
        public int TotalVotesCount { get; set; }

        public AccountDetailsModel()
        {
            this.EntryList = new List<Entry>();
        }

    }
}