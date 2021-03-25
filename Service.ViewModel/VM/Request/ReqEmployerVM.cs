using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ViewModel.VM.Request
{
    public class ReqEmployerVM
    {
        public string Code { get; set; }
        public ReqEmployerTypeVM  ReqEmployerType { get; set; }
        public string EmployerName { get; set; }
    }
    public enum ReqEmployerTypeVM
    {
        CurrentEmployer = 0 , 
        PreviousEmployer = 1
    }
}
