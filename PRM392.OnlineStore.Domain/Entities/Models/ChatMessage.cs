using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRM392.OnlineStore.Domain.Entities.Models;

public partial class ChatMessage
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int ChatMessageId { get; set; }

    public int? UserId { get; set; }
    public int? RecipientId { get; set; }

    public string? Message { get; set; }

    public DateTime SentAt { get; set; }

    public virtual User? User { get; set; }
}
