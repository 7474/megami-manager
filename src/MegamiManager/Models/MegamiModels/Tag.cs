using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Models.MegamiModels
{
    public class Tag : AbstractEntity
    {
        public int TagId { get; set; }

        // XXX Unique Index
        // https://docs.microsoft.com/en-us/ef/core/modeling/indexes
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        public IList<MegamiTag> MegamiTags { get; set; }

        //[InverseProperty("Weapon")]
        //public IList<Weapon> Weapons { get; set; }
    }

    public class MegamiTag
    {
        public int MegamiId { get; set; }
        public Megami Megami { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
