using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public abstract class Entity
    {
        public virtual Guid Id { get; protected set; }
    }
}
