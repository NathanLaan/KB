using System;
using KB.Lib.Entity;

namespace KB.Web.Models
{
    public class AccountManageModel
    {

        /// <summary>
        /// For display purposes only.
        /// </summary>
        public Account Account { get; set; }

        public string Email { get; set; }

        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }

        public bool SendEmailNotifications { get; set; }

    }
}