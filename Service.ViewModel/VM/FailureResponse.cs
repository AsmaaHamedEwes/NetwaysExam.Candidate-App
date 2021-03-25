using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ViewModel.VM
{
    public class FailureResponse
    {
        public int Code { set; get; }
        public List<string> Error { set; get; }
    }
}
