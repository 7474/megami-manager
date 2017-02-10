using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Models.MegamiModels
{
    public abstract class AbstractEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Timestamp]
        public DateTime Timestamp { get; set; }
    }

    public abstract class OwnableEntity : AbstractEntity
    {
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        public bool IsOwner(ApplicationUser user)
        {
            return OwnerId == user.Id;
        }
        public void AssertOwn(ApplicationUser user)
        {
            if (!IsOwner(user))
            {
                // XXX
                throw new InvalidOperationException();
            }
        }
    }
}
