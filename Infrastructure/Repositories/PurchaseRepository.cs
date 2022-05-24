using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        //public async Task<List<Purchase>> GetPurchasesByUser(int id)
        //{
        //    var purchase = await _dbContext.Purchase.Where(x => x.UserId == id).ToListAsync();
        //    return purchase;
        //}

        public async Task<PaginatedResultSet<Purchase>> GetPurchasesByUser(int id)
        {
            var totalPurchaseCount = await _dbContext.Purchase.Where(m => m.UserId == id).CountAsync();
            var purchase = await _dbContext.Purchase.Where(x => x.UserId == id).ToListAsync();
            var purchaseList = new PaginatedResultSet<Purchase>(purchase, 1, 30, totalPurchaseCount);
            return purchaseList;
        }

    }
}
