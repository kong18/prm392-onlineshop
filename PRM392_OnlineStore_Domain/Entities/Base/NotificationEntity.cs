using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Base
{
    public class NotificationEntity : Entity
    {
        public string UserID { get; set; }

        public string Message { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation property
        public virtual UserEntity User { get; set; }
    }
}
