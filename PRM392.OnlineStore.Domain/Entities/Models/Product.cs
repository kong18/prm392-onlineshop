using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRM392.OnlineStore.Domain.Entities.Models;

public partial class Product
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? BriefDescription { get; set; }

    public string? FullDescription { get; set; }

    public string? TechnicalSpecifications { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? DeletedAt {  get; set; } 
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int? CategoryId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Category { get; set; }
}
