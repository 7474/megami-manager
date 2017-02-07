using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Models.MegamiModels
{

    public class MegamiImage : AbstractEntity
    {
        public int MegamiId { get; set; }
        public Megami Megami { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }

        public int DisplayOrder { get; set; }
    }
}
