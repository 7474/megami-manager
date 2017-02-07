using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Models.MegamiModels
{
    public enum ImageType
    {
        None = 0,
        Main = 1,
        Cutin = 2,
        Icon = 4
    }

    /// <remarks>
    /// 面倒くさいので便宜上サムネイルも同一レコードで扱う。
    /// 正規化はしない。
    /// そもそも、サムネイルを作成しないかもしれない。
    /// </remarks>
    public class Image : OwnableEntity
    {
        // Getter
        public string Uri { get { return PublicUri; } }
        public string ThumbnailUri { get { return PublicThumbnailUri ?? PublicUri; } }

        public int ImageId { get; set; }
        /// <summary>
        /// ストレージ上でイメージを特定するためのキー値です。
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Key { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(1000)]
        public string Comment { get; set; }
        [MaxLength(32)]
        public string ImageType { get; set; }

        [Required]
        [MaxLength(512)]
        public string PublicUri { get; set; }
        [MaxLength(512)]
        public string PublicThumbnailUri { get; set; }

        [Required]
        [MaxLength(512)]
        public string PrivateUri { get; set; }
        [MaxLength(512)]
        public string PrivateThumbnailUri { get; set; }

        public IList<MegamiImage> Megamis { get; set; }
    }
}
