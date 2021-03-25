using Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Entity
{
    public class Tbl_Token : EntityBase
    {
        public string Token { set; get; }

        public string ConnectionId { set; get; }
        public string TokenRefresh { set; get; }
        public string AccountType { set; get; }
        public string UserId { set; get; }
        [ForeignKey("UserId")]
        public User User { set; get; }


    }
}
