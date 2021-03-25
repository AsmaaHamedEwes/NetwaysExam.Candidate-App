using Domain.Entities.Entity;
using Infrastructure.IRepository;
using Service.Interface.IService;
using Service.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Implementation.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork unitOfWork;

        public TokenService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool CheckToken(string userId, string Token)
        {
            var token = unitOfWork.GetRepository<Tbl_Token>().GetSingle(e => e.UserId.Equals(userId) && e.Token.Equals(Token));

            if (token == null)
            {
                return false;
            }



            return true;
        }

        public bool CheckToken(string Token)
        {
            var token = unitOfWork.GetRepository<Tbl_Token>().GetSingle(e => e.Token.Equals(Token));

            if (token == null)
            {
                return false;
            }

            if (token.DateOfCreate.AddDays(1) < DateTime.UtcNow)
                return false;

          

            return true;
        }

     

        public string GetConnectionId(string userId)
        {
            var tblToken = unitOfWork.GetRepository<Tbl_Token>().GetSingle(e => e.UserId.Equals(userId));

            if (tblToken == null)
                throw ExceptionBase.ThrowException(400, "You Don't have token", "You Don't have token");

            return tblToken.ConnectionId;
        }

        public bool UpdateConnectionId(string userId, string ConnectionId)
        {
            var tblToken = unitOfWork.GetRepository<Tbl_Token>().GetSingle(e => e.UserId.Equals(userId));

            if (tblToken == null)
                throw ExceptionBase.ThrowException(400, "You Don't have token", "You Don't have token");

            tblToken.ConnectionId = ConnectionId;

            unitOfWork.GetRepository<Tbl_Token>().Update(tblToken);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}
