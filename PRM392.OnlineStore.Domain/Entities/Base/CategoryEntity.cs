using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Base
{
    public class CategoryEntity : Entity
    {
        [Required]
        [MaxLength(255)]
        public string CategoryName { get; set; }

        // Navigation property
        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
