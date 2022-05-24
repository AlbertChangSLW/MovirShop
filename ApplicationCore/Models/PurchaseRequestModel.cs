using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PurchaseRequestModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Guid PurchaseNumber { get; set; }
        public decimal? Price { get; set; }
    }
}
