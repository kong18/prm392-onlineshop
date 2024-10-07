using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Base
{
    public class PaymentEntity : Entity
    {
        public string OrderID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        [MaxLength(50)]
        public string PaymentStatus { get; set; }

        // Navigation property
        public virtual OrderEntity Order { get; set; }
    }
}
