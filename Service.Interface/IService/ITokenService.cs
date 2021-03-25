using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface.IService
{
    public interface ITokenService
    {
        bool CheckToken(string userId, string Token);
        bool CheckToken(string Token);
        string GetConnectionId(string userId);
        bool UpdateConnectionId(string userId, string ConnectionId);
    }
}
