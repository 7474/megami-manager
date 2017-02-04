using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Models.MegamiModels
{
    public class Weapon : AbstractEntity
    {
        public int WeaponId { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        //public IList<Tag> Tags { get; set; }
    }
}
