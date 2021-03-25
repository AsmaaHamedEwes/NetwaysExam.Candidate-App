using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Bases
{
    public class EntityBase : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Code { set; get; } = Guid.NewGuid();
    }
}
