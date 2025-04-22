using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public DateTime orderDate { get; set; }
    }
}
