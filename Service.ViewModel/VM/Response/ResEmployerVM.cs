using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ViewModel.VM.Response
{
    public class ResEmployerVM
    {
        public string Code { get; set; }
        public ResEmployerTypeVM ResEmployerType { get; set; }
        public string EmployerName { get; set; }
    }
    public enum ResEmployerTypeVM
    {
        CurrentEmployer = 0,
        PreviousEmployer = 1
    }
}
