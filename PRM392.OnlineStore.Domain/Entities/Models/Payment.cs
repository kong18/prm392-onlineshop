using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRM392.OnlineStore.Domain.Entities.Models;

public partial class Payment
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int PaymentId { get; set; }

    public int? OrderId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public virtual Order? Order { get; set; }
}
