using GamePlatform.Domain.Domain;
using GamePlatform.Domain.DTO;
using GamePlatform.Repository.Interface;
using GamePlatform.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<GameInCart> _gameInCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<GameInOrder> _gameInOrderRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<GameInCart> gameInCartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<GameInOrder> gameInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _gameInCartRepository = gameInCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _gameInOrderRepository = gameInOrderRepository;
        }

        public bool AddToShoppingCartConfirmed(GameInCart model, string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            var userCart = loggedInUser.ShoppingCart;

            if (userCart.GamesInCart == null)
            {
                userCart.GamesInCart = new List<GameInCart>(); 
            }

            userCart.GamesInCart.Add(model);
            _shoppingCartRepository.Update(userCart);
            return true;
        }

        public bool DeleteFromCart(string userId, Guid gameId)
        {
            if(gameId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userCart = loggedInUser.ShoppingCart;
                var game = userCart.GamesInCart.Where(z => z.GameId == gameId).FirstOrDefault();

                userCart.GamesInCart.Remove(game);

                _shoppingCartRepository.Update(userCart);
                return true;
            }
            return false;
        }

        public ShoppingCartDto GetCartInfo(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            var userCart = loggedInUser.ShoppingCart;
            var allGames = userCart?.GamesInCart?.ToList();

            var totalPrice = allGames.Select(x => (x.Game.Price * x.Quantity)).Sum();

            ShoppingCartDto dto = new ShoppingCartDto
            {
                Games = allGames,
                TotalPrice = totalPrice
            };

            return dto;
        }

        public bool Order(string userId)
        {
            if(userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userCart = loggedInUser.ShoppingCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    User = loggedInUser
                };

                _orderRepository.Insert(order);

                List<GameInOrder> gamesInOrder = new List<GameInOrder>();

                var list = userCart.GamesInCart.Select(
                    x => new GameInOrder
                    {
                        Id = Guid.NewGuid(),
                        GameId = x.Game.Id,
                        Game = x.Game,
                        OrderId = order.Id,
                        Order = order,
                        Quantity = x.Quantity,
                    }).ToList();

                gamesInOrder.AddRange(list);

                foreach(var game in gamesInOrder)
                {
                    _gameInOrderRepository.Insert(game);
                }

                loggedInUser.ShoppingCart.GamesInCart.Clear();
                _userRepository.Update(loggedInUser);

                return true;
            }

            return false;
        }
    }
}
