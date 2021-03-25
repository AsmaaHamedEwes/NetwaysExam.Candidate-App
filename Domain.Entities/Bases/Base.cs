using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Bases
{
    public class Base
    {
        public DateTime DateOfCreate { set; get; } = DateTime.UtcNow;
        public bool IsDeleted { set; get; }

    }
}
