using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Models.ManageViewModels
{
    public class ChangeProfileViewModel
    {
        [Required]
        [StringLength(32)]
        [Display(Name = "NickName")]
        public string NickName { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }
    }
}
