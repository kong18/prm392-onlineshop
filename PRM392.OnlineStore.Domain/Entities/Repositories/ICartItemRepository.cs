﻿using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Repositories
{
    public interface ICartItemRepository : IEFRepository<CartItem, CartItem>
    {
        Task<decimal> GetCartTotalPrice(int cartId);
    }
}
