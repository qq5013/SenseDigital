using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Data;

namespace Js.BLL.Account
{
    public class SiteIdentity : IIdentity
    {
        // Fields
        private int userID;
        private string userName;

        Js.Model.Account.UsersInfo model = new Model.Account.UsersInfo();
        // Methods
        public SiteIdentity(int currentUserID)
        {
            //DataRow row1 = new Js.DAO.Account.UserDao().Retrieve(currentUserID);
            this.model = new Js.DAO.Account.UserDao().GetModel(currentUserID);
            this.userName = this.model.UserName;
            this.userID = currentUserID;
        }

        public SiteIdentity(string currentUserName)
        {
            //DataRow row1 = new Js.DAO.Account.UserDao().Retrieve(currentUserName);
            this.model = new Js.DAO.Account.UserDao().GetModel(currentUserName);
            this.userName = currentUserName;
            this.userID = this.model.UserID;
        }
        public SiteIdentity(int currentUserID,string cnKey)
        {
            //DataRow row1 = new Js.DAO.Account.UserDao().Retrieve(currentUserID);
            this.model = new Js.DAO.Account.UserDao(cnKey).GetModel(currentUserID);
            this.userName = this.model.UserName;
            this.userID = currentUserID;
        }

        public SiteIdentity(string currentUserName, string cnKey)
        {
            //DataRow row1 = new Js.DAO.Account.UserDao().Retrieve(currentUserName);
            this.model = new Js.DAO.Account.UserDao(cnKey).GetModel(currentUserName);
            this.userName = currentUserName;
            this.userID = this.model.UserID;
        }
        public int TestPassword(string password)
        {
            byte[] buffer1 = new UnicodeEncoding().GetBytes(password);
            byte[] buffer2 = new SHA1CryptoServiceProvider().ComputeHash(buffer1);
            Js.DAO.Account.UserDao user1 = new Js.DAO.Account.UserDao();
            return user1.TestPassword(this.model.UserID, buffer2);
        }
        public int TestPassword(string password, string cnKey)
        {
            byte[] buffer1 = new UnicodeEncoding().GetBytes(password);
            byte[] buffer2 = new SHA1CryptoServiceProvider().ComputeHash(buffer1);
            Js.DAO.Account.UserDao user1 = new Js.DAO.Account.UserDao(cnKey);
            return user1.TestPassword(this.model.UserID, buffer2);
        }
        // Properties
        public string AuthenticationType
        {
            get
            {
                return "Custom Authentication";
            }
            set
            {
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return this.userName;
            }
        }    


        public int UserID
        {
            get
            {
                return this.userID;
            }
        }


        public string UserName
        {
            get
            {
                return this.userName;
            }
        }
    }
}
