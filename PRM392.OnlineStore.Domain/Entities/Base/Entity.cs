using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Base
{
    public abstract class Entity : IDisposable
    {
        protected Entity()
        {
            ID = Guid.NewGuid().ToString("N");
            CreatedDate = LastUpdated = DateTime.Now;
        }

        [Key]
        public string ID { get; set; }


        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }

        public string? DeletedBy { get; set; }
        public DateTime? DeletedDay { get; set; }


        [NotMapped]
        private bool IsDisposed { get; set; }

        #region Dispose
        public void Dispose()
        {
            Dispose(isDisposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!IsDisposed)
            {
                if (isDisposing)
                {
                    DisposeUnmanagedResources();
                }

                IsDisposed = true;
            }
        }

        protected virtual void DisposeUnmanagedResources()
        {
        }

        ~Entity()
        {
            Dispose(isDisposing: false);
        }
        #endregion Dispose

    }
}
