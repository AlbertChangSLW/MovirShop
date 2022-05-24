using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<PaginatedResultSet<Purchase>> GetPurchasesByUser(int Id);
        //Task<List<Purchase>> GetPurchasesByUser(int id);
    }
}
