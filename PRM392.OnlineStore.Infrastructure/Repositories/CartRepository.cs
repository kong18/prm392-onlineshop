﻿using AutoMapper;
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
    public class CartRepository : RepositoryBase<Cart, Cart, ApplicationDbContext>, ICartRepository
    {
        public CartRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
    }
}
