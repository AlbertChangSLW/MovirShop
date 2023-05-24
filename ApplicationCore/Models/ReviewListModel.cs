using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ReviewListModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Rating { get; set; }
        public string? ReviewText { get; set; }
        public int MovieId { get; set; }
    }
}
