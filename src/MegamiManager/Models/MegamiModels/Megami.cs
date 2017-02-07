using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Models.MegamiModels
{
    public class Megami : OwnableEntity
    {
        public int MegamiId { get; set; }
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        [Required]
        [MaxLength(64)]
        public string Design { get; set; }
        [Required]
        [MaxLength(64)]
        public string Type { get; set; }
        public IList<Weapon> Armed { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(1000)]
        public string Comment { get; set; }
        public IList<MegamiTag> MegamiTags { get; set; }
        public IList<MegamiTeam> Teams { get; set; }
        public IList<MegamiImage> Images { get; set; }

        #region Parameters
        //"近距離戦闘", "中距離戦闘", "遠距離戦闘", "装甲・防御", "重量　　　",
        //"稼働時間　", "隠密　　　", "索敵　　　", "空中機動　", "地上機動　"],
        [Range(0, 100)]
        public int CloseRangeBattle { get; set; }
        [Range(0, 100)]
        public int MidRangeBattle { get; set; }
        [Range(0, 100)]
        public int LongRangeBattle { get; set; }
        [Range(0, 100)]
        public int ArmorDefense { get; set; }
        [Range(0, 100)]
        public int Weight { get; set; }
        [Range(0, 100)]
        public int ActiveTime { get; set; }
        [Range(0, 100)]
        public int Stealth { get; set; }
        [Range(0, 100)]
        public int Recon { get; set; }
        [Range(0, 100)]
        public int AerialMobility { get; set; }
        [Range(0, 100)]
        public int GroundMobility { get; set; }
        #endregion

        // 非永続化プロパティ
        // https://docs.microsoft.com/en-us/ef/core/modeling/included-properties
        public IList<int> Parameters
        {
            get
            {
                return new int[] {
                    CloseRangeBattle,
                    MidRangeBattle,
                    LongRangeBattle,
                    ArmorDefense,
                    Weight,
                    ActiveTime,
                    Stealth,
                    Recon,
                    AerialMobility,
                    GroundMobility
                };
            }
        }
    }
}
