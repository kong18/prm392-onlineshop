using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Base
{
    public class StoreLocationEntity : Entity
    {
        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        public ICollection<OrderEntity> Orders { get; set; }
    }
}
