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

        // returns appropriate http response status code
        //public HttpStatusCode ValidateLogin(Users user)
        //{
        //    // TODO: field validation
        //    if (user.Password.Length == 0 || user.EmailAddress.Length == 0)
        //    {
        //        return HttpStatusCode.Unauthorized;   
        //    }

        //    string actualPassword = accountDao.GetPasswordForEmailAddress(user.EmailAddress);

        //    if (actualPassword.CompareTo("") == 0)
        //    {
        //        return HttpStatusCode.Unauthorized;
        //    }

        //    if (actualPassword.CompareTo(user.Password) == 0)
        //    {
        //        return HttpStatusCode.Accepted;
        //    }

        //    return HttpStatusCode.Unauthorized;
        //}

        public bool ValidateLoginFields(Users user)
        {
            if (user == null || user.Password == null || user.EmailAddress == null || user.Password.Length == 0
                || user.EmailAddress.Length == 0)
                return false;

            return true;
        }

        public bool ValidateLogin(Users user)
        {
            string actualPassword = accountDao.GetPasswordForEmailAddress(user.EmailAddress);

            // no such email present
            if (actualPassword.CompareTo("") == 0)
            {
                return false;
            }
            
            if (actualPassword.CompareTo(user.Password) == 0)
            {
                return true;
            }

            return false;
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