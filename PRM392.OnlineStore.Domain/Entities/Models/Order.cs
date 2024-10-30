using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRM392.OnlineStore.Domain.Entities.Models;

public partial class Order
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int OrderId { get; set; }

    public int? CartId { get; set; }

    public int? UserId { get; set; }
    public int? LocationId { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string BillingAddress { get; set; } = null!;

    public string OrderStatus { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual StoreLocation? StoreLocation { get; set; }
    public virtual User? User { get; set; }

}
