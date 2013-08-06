using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.Security.Principal;

namespace Js.BLL.Account
{
    public class UserPrincipal : IPrincipal
    {
        // Fields
        protected IIdentity identity;
        protected ArrayList permissionListid;
        protected ArrayList roleList;
        /// <summary>
        /// ノめ┮Τ舦
        /// </summary>
        protected DataSet permission;
        /// <summary>
        /// эノめ┮局Τà︹舦.
        /// </summary>
        private DataSet rolepermission;
        /// <summary>
        /// ノめ局Τ舦(ぃ珹à︹舦)
        /// </summary>
        private DataSet userpermission;

        /// <summary>
        /// ノめ局Τà︹┮舦
        /// </summary>
        private DataSet rolesubpermission;
        /// <summary>
        /// ノめセō局Τ疭舦 
        /// </summary>
        private DataSet usersubpermission;

        /// <summary>
        /// ノめ局Τ┮Τ疭舦
        /// </summary>
        private DataSet subpermission;

        // Methods
        public UserPrincipal(int userID)
        {
            Js.DAO.Account.UserDao user1 = new Js.DAO.Account.UserDao();
            this.identity = new SiteIdentity(userID);
            this.permissionListid = user1.GetEffectivePermissionListID(userID, ((SiteIdentity)this.identity).UserName);
            this.roleList = user1.GetUserRoles(userID);
            this.permission = user1.GetUserPermission(userID, ((SiteIdentity)this.identity).UserName);
            this.rolepermission = user1.GetUserRolePermission(userID, ((SiteIdentity)this.identity).UserName);
            this.userpermission = user1.GetPermission(userID);
        }

        public UserPrincipal(string userName)
        {
            Js.DAO.Account.UserDao user1 = new Js.DAO.Account.UserDao();
            this.identity = new SiteIdentity(userName);
            this.permissionListid = user1.GetEffectivePermissionListID(((SiteIdentity)this.identity).UserID, ((SiteIdentity)this.identity).UserName);
            this.roleList = user1.GetUserRoles(((SiteIdentity)this.identity).UserID);
            this.permission = user1.GetUserPermission(((SiteIdentity)this.identity).UserID, userName);
            this.rolepermission = user1.GetUserRolePermission(((SiteIdentity)this.identity).UserID, userName);
            this.userpermission = user1.GetPermission(((SiteIdentity)this.identity).UserID);
        }
        public UserPrincipal(int userID,string cnKey)
        {
            Js.DAO.Account.UserDao user1 = new Js.DAO.Account.UserDao(cnKey);
            this.identity = new SiteIdentity(userID, cnKey);
            this.permissionListid = user1.GetEffectivePermissionListID(userID, ((SiteIdentity)this.identity).UserName);
            this.roleList = user1.GetUserRoles(userID);
            this.permission = user1.GetUserPermission(userID, ((SiteIdentity)this.identity).UserName);
            this.rolepermission = user1.GetUserRolePermission(userID, ((SiteIdentity)this.identity).UserName);
            this.userpermission = user1.GetPermission(userID);
        }

        public UserPrincipal(string userName, string cnKey)
        {
            Js.DAO.Account.UserDao user1 = new Js.DAO.Account.UserDao(cnKey);
            this.identity = new SiteIdentity(userName, cnKey);
            this.permissionListid = user1.GetEffectivePermissionListID(((SiteIdentity)this.identity).UserID, ((SiteIdentity)this.identity).UserName);
            this.roleList = user1.GetUserRoles(((SiteIdentity)this.identity).UserID);
            this.permission = user1.GetUserPermission(((SiteIdentity)this.identity).UserID, userName);
            this.rolepermission = user1.GetUserRolePermission(((SiteIdentity)this.identity).UserID, userName);
            this.userpermission = user1.GetPermission(((SiteIdentity)this.identity).UserID);
        }
        public static byte[] EncryptPassword(string password)
        {
            byte[] buffer1 = new UnicodeEncoding().GetBytes(password);
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            return sha1.ComputeHash(buffer1);
        }

        
        public bool HasPermissionID(int sysid, int permissionid)
        {
            return this.permissionListid.Contains(sysid.ToString().PadLeft(2,'0') + permissionid.ToString());
        }

        public bool IsInRole(string role)
        {
            return this.roleList.Contains(role);
        }

        public static UserPrincipal ValidateLogin(string userName, string password)
        {
            byte[] buffer1 = UserPrincipal.EncryptPassword(password);
            byte[] buffer2 = UserPrincipal.EncryptPassword("admin");
            Js.DAO.Account.UserDao user1 = new Js.DAO.Account.UserDao();
            int num1 = user1.ValidateLogin(userName, buffer1,buffer2);
            if (num1 > 0)
            {
                return new UserPrincipal(num1);
            }
            return null;
        }
        public static UserPrincipal ValidateLogin(string userName, string password,string cnKey)
        {
            byte[] buffer1 = UserPrincipal.EncryptPassword(password);
            byte[] buffer2 = UserPrincipal.EncryptPassword("admin");
            Js.DAO.Account.UserDao user1 = new Js.DAO.Account.UserDao(cnKey);
            int num1 = user1.ValidateLogin(userName, buffer1, buffer2);
            if (num1 > 0)
            {
                return new UserPrincipal(num1, cnKey);
            }
            return null;
        }
        // Properties
        public IIdentity Identity
        {
            get
            {
                return this.identity;
            }
            set
            {
                this.identity = value;
            }
        }
        public DataSet Permission
        {
            get
            {
                return this.permission;
            }
        }
        /// <summary>
        /// ノめ┮局Τà︹舦
        /// </summary>
        public DataSet RolePermission
        {
            get
            {
                return this.rolepermission;
            }
        }
        
        /// <summary>
        /// ノめ局Τ舦(ぃ珹à︹舦)
        /// </summary>
        public DataSet UserPermission
        {
            get
            {
                return this.userpermission;
            }
        }

        public ArrayList PermissionsID
        {
            get
            {
                return this.permissionListid;
            }
        }

        public ArrayList Roles
        {
            get
            {
                return this.roleList;
            }
        }        
    }
}
