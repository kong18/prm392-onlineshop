using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Base
{
    public class CartEntity : Entity
    {
        public string UserID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        // Navigation properties
        public virtual UserEntity User { get; set; }
        public OrderEntity Order { get; set; }

        public virtual ICollection<CartItemEntity> CartItems { get; set; }
    }
}
