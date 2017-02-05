using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Models.MegamiModels
{
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
    }
}
