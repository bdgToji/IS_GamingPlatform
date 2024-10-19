using GamePlatform.Domain.Domain;
using GamePlatform.Repository.Interface;
using GamePlatform.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Service.Implementation
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _gameRepository;

        public GameService(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public List<Game> GetAllGames()
        {
            return _gameRepository.GetAll().ToList();
        }

        public Game GetGame(Guid? id)
        {
            return _gameRepository.Get(id);
        }

        public void CreateGame(Game game)
        {
            _gameRepository.Insert(game);
        }

        public void UpdateGame(Game game)
        {
            _gameRepository.Update(game);
        }
        public void DeleteGame(Guid? id)
        {
            var game = _gameRepository.Get(id);
            _gameRepository.Delete(game);
        }
    }
}
