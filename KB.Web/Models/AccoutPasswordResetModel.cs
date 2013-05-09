﻿using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KB.Web.Models
{
    public class AccoutPasswordResetModel
    {

        [Required]
        [Display(Name = "Account Name or Email Address")]
        public string AccountNameOrEmail { get; set; }

    }
}