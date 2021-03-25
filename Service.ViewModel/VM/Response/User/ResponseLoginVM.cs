using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ViewModel.VM.Response.User
{
    public class ResponseLoginVM
    {
        public string UserId { set; get; }
      
        public TokenVM Token { set; get; }
        public string UserName { set; get; }
        public string Email { set; get; }
       
        public string PhoneNumber { set; get; }
    }
}
