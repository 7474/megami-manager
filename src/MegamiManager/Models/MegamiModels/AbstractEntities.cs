using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Models.MegamiModels
{
    public abstract class AbstractEntity
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }

    public abstract class OwnableEntity : AbstractEntity
    {
        public int OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
    }
}
