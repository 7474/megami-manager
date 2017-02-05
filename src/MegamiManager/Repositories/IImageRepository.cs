using MegamiManager.Models.MegamiModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Repositories
{
    /// <remarks>
    /// IFormFileに依存するのはダサいが、事実上の問題がなく楽なので良しとする。
    /// </remarks>
    public interface IImageRepository
    {
        Task<Image> Create(IFormFile file);
        Task Update(Image image, IFormFile file);
        Task Delete(Image image);
        Task CreateThumbnail(Image image);
    }
}
