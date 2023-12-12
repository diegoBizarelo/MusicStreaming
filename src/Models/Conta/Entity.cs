using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Conta
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
