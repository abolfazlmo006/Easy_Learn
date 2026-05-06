using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy_Learn.Data.Entities
{
    public class NotificationEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }
        public DateTime DateTime { get; set; }
    }
}
