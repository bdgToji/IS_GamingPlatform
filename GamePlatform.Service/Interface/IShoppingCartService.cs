using GamePlatform.Domain.Domain;
using GamePlatform.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Service.Interface
{
    public interface IShoppingCartService
    {
        bool AddToShoppingCartConfirmed(GameInCart model, string userId);
        bool DeleteFromCart(string userId, Guid gameId);
        bool Order(string userId);
        ShoppingCartDto GetCartInfo(string userId); 
    }
}
