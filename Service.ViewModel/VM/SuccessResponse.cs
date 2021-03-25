using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ViewModel.VM
{
    public class SuccessResponse<T>
    {
        public int Code { set; get; }
        public T Data { set; get; } 
    }
}
