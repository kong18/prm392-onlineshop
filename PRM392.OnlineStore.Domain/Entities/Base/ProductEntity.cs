using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Base
{
    public class ProductEntity : Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string BriefDescription { get; set; }
        public string FullDescription { get; set; }
        public string TechnicalSpecifications {  get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string CategoryID {  get; set; }

        public virtual ICollection<CartItemEntity> CartItems { get; set; }

        public virtual CategoryEntity Category { get; set; }

        //public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; }
    }
}
