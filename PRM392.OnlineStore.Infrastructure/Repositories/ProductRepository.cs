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
    public class ProductRepository : RepositoryBase<Product, Product, ApplicationDbContext>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<List<Product>> FindAllWithCategoriesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Product>()
                .Include(p => p.Category) 
                .Where(p => p.DeletedAt == null)
                .ToListAsync(cancellationToken);
        }
    }
}
