using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ViewModel.Base
{
    public abstract class ProfileBase : Profile
    {
        public ProfileBase()
        {
            Response();
            Request();
        }
        public abstract void Response();
        public abstract void Request();
    }
}
