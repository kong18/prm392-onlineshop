using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Base
{
    public class CartItemEntity : Entity
    {
        public string CartID { get; set; }
        public string ProductID { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Navigation properties
        public virtual CartEntity Cart { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}
