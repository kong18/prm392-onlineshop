using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Infrastructure.Repositories
{
    public class CartItemRepository : RepositoryBase<CartItem, CartItem, ApplicationDbContext>, ICartItemRepository
    {
        public CartItemRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<decimal> GetCartTotalPrice(int cartId)
        {
            // Sum the prices of all items in the cart with the specified CartId
            return await _dbContext.CartItems
                .Where(cartItem => cartItem.CartId == cartId)
                .SumAsync(cartItem => cartItem.Price);
        }
    }
}
