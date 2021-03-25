using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Bases
{
    public class IdentityBase : IdentityUser
    {
        public DateTime DateOfCreate { set; get; } = DateTime.UtcNow;
        public bool IsDeleted { set; get; } = false;
        public bool IsBlocked { set; get; }
        public bool IsArchive { set; get; }

    }
}
