using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using webapi.Dao;
using webapi.Models;

namespace webapi.Services
{
    public class AccountService
    {
        private readonly AccountDao accountDao = new AccountDao();

        // returns appropriate http response message
        public HttpResponseMessage ValidateLogin(Users user)
        {
            // TODO: field validation
            if (user.Password.Length == 0)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                };
            }

            if (user.EmailAddress.Length == 0)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                };
            }

            string actualPassword = accountDao.GetPasswordForEmailAddress(user.EmailAddress);

            if (actualPassword.CompareTo("") == 0)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                };
            }

            if (actualPassword.CompareTo(user.Password) == 0)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Accepted
                };
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Unauthorized,
            };
        }

        public HttpStatusCode Register(Users user)
        {
            //check if a user with same email address is not already registered
            if (accountDao.CheckIfEmailAddressAlreadyExists(user.EmailAddress))
                return HttpStatusCode.BadRequest;

            accountDao.AddNewUserRegistration(user.UserName, user.EmailAddress, user.Password, user.MobileNumber, user.ResidentialAddress);

            return HttpStatusCode.Accepted;
        }
    }
}