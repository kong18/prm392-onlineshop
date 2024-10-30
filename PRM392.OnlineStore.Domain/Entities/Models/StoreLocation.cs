using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRM392.OnlineStore.Domain.Entities.Models;

public partial class StoreLocation
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int LocationId { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string Address { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
