using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Models.MegamiModels
{
    public class Image : OwnableEntity
    {
        public Uri Uri { get; set; }
        public Uri ThumbnailUri { get; set; }
    }
}
