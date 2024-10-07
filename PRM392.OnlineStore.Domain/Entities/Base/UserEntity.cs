using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace PRM392.OnlineStore.Domain.Entities.Base
{
    public class UserEntity : Entity
    {
        public string Name { get; set; }
       
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        [Column(TypeName = "decimal(18,4)")]

        public string Address {  get; set; }
        public string Phone { get; set; }


        public virtual ICollection<OrderEntity> Orders { get; set; }
        public virtual ICollection<CartEntity> Carts { get; set; }
        public virtual ICollection<NotificationEntity> Notifications { get; set; }
        public virtual ICollection<ChatMessageEntity> ChatMessages { get; set; }


        //public virtual ICollection<OrderEntity> Orders { get; set; }





    }
}
