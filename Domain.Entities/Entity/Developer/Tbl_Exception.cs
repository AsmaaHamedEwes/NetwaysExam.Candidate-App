using Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Entity.Developer
{
    public class Tbl_Exception : EntityBase
    {
        public string UserName { set; get; }
        public string StackTrace { set; get; }
        public string ExceptionMessage { set; get; }
        public string ExceptionType { set; get; }
        public string RequestBody { set; get; }
        public bool IsDone { set; get; }
    }
}
