using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Base
{
    public class ChatMessageEntity : Entity
    {
        public string UserID { get; set; }

        public string Message { get; set; }

        public DateTime SentAt { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
