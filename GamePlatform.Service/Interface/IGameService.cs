using GamePlatform.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Service.Interface
{
    public interface IGameService
    {
        List<Game> GetAllGames();
        Game GetGame(Guid? id);
        void CreateGame(Game game);
        void UpdateGame(Game game);
        void DeleteGame(Guid? id);

    }
}
