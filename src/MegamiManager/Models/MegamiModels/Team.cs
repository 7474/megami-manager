using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Models.MegamiModels
{
    public class Team : OwnableEntity
    {
        public int TeamId { get; set; }
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(1000)]
        public string Comment { get; set; }
        public IList<MegamiTeam> Members { get; set; }
    }

    public class MegamiTeam
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int MegamiId { get; set; }
        public Megami Megami { get; set; }
    }
}
