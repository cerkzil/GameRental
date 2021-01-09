using GR.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GR.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<Game> GetGameByIdAsync(Guid id);
        Task DeleteGameByIdAsync(Guid id);
        Task CreateGameAsync(string Title, Uri ImgLink, List<Genres> GenreList, List<Platforms> PlatfromList);
        Task UpdateGameByIdAsync(Guid id, string title, Uri imglink, List<Genres> genreList, List<Platforms> platformList);
    }
}
