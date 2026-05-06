using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_Learn.Data.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }
        public bool Finaly { get; set; }
        public List<OrderDetailEntity>? OrderDetails { get; set; }
        public int Total_Price { get; set; }
        public DateTime? BuyTime { get; set; }
    }
}
