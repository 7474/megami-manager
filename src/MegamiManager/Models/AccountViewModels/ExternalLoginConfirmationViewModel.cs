﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [StringLength(32)]
        [Display(Name = "NickName")]
        public string NickName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
