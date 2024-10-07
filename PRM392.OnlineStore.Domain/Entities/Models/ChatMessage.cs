using System;
using System.Collections.Generic;

namespace PRM392.OnlineStore.Domain.Entities.Models;

public partial class ChatMessage
{
    public int ChatMessageId { get; set; }

    public int? UserId { get; set; }

    public string? Message { get; set; }

    public DateTime SentAt { get; set; }

    public virtual User? User { get; set; }
}
