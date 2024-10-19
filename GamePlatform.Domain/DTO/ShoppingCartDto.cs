using GamePlatform.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<GameInCart>? Games;
        public int TotalPrice;
    }
}
