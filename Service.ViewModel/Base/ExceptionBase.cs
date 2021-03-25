using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ViewModel.Base
{
    public class ExceptionBase
    {
        public static Exception ThrowException(int code, string msg, string shortMsg)
        {
            var e = new Exception(msg);
            e.Data.Add("code", code);
            e.Data.Add("shortMsg", shortMsg);
            return e;
        }

        public static Exception ThrowException<T>(int code, string msg, List<T> error)
        {
            var e = new Exception(msg);
            e.Data.Add("code", code);
            e.Data.Add("shortMsg", "");
            e.Data.Add("ErrorList", error);
            return e;
        }
    }
}
