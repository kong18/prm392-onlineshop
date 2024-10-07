using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Base
{
    public class OrderEntity : Entity
    {
        public string CartID { get; set; }
        public string UserID { get; set; }

        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        [MaxLength(255)]
        public string BillingAddress { get; set; }

        [MaxLength(50)]
        public string OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        // Navigation properties
        public virtual UserEntity User { get; set; }
        public virtual CartEntity Cart { get; set; }
        public virtual PaymentEntity Payment { get; set; }

    }
}
