using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;

namespace Js.BLL.Account
{
    public sealed class UserDal
    {
        Js.Model.Account.UsersInfo model;
        Js.DAO.Account.UserDao dao;

        public UserDal()
        {
            dao = new Js.DAO.Account.UserDao();
        }
        public UserDal(UserPrincipal existingPrincipal)
        {
            dao = new Js.DAO.Account.UserDao();
            this.model = dao.GetModel(((SiteIdentity)existingPrincipal.Identity).UserID);
        }
        public UserDal(UserPrincipal existingPrincipal,string cnKey)
        {
            dao = new Js.DAO.Account.UserDao(cnKey);
            this.model = dao.GetModel(((SiteIdentity)existingPrincipal.Identity).UserID);
        }
        public UserDal(int existingUserID)
        {
            dao = new Js.DAO.Account.UserDao();
            this.model = dao.GetModel(existingUserID);
        }
        public UserDal(string cnKey)
        {
            dao = new Js.DAO.Account.UserDao(cnKey);
        }
        public UserDal(string UserName,string cnKey)
        {
            dao = new Js.DAO.Account.UserDao(cnKey);
            this.model = dao.GetModel(UserName);
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.Account.UsersInfo GetModel(int UserID)
        {
            return dao.GetModel(UserID);
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.Account.UsersInfo GetModel(string UserName)
        {
            return dao.GetModel(UserName);
        }

        /// <summary>
        /// 新增一筆記錄
        /// </summary>
        /// <param name="_Com_DepartmentModel">SBAA_Department實體</param>
        public void Add(Js.Model.Account.UsersInfo model)
        {
            dao.Add(model);
        }
        public void Update(Js.Model.Account.UsersInfo model)
        {
            dao.Update(model);
        }
        public DataRow GetUserLevel(string UserName)
        {
            return dao.GetUserLevel(UserName);
        }
        public DataTable GetParentUser(int UserID)
        {
            return dao.GetParentUser(UserID);
        }
        public void Delete(int UserID)
        {
            dao.Delete(UserID);
        }
        public DataSet GetAllUsers()
        {
            return dao.GetAllUsers();
        }
        public DataSet GetAllUsers(string key)
        {
            return dao.GetAllUsers(key);
        }
        public DataTable GetUserRole(int userID)
        {
            return dao.GetUserRole(userID);
        }
        public DataSet GetRoleUsers(int roleID)
        {
            return dao.GetRoleUsers(roleID);
        }
        
        public ArrayList GetEffectivePermissionListID(int userID, string UserName)
        {
            return dao.GetEffectivePermissionListID(userID, UserName);
        }
        public DataSet GetUserPermission(int userID, string UserName)
        {
            return dao.GetUserPermission(userID, UserName);
        }
        public DataSet GetUserRolePermission(int userID, string UserName)
        {
            return dao.GetUserRolePermission(userID, UserName);
        }
        /// <summary>
        /// 用戶擁有的權限(不包括角色權限)
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="companyID"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataSet GetPermission(string culture)
        {
            return dao.GetPermission(culture);
        }
        /// <summary>
        /// 用戶擁有的權限(不包括角色權限)
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="companyID"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataSet GetPermission(int UserID)
        {
            return dao.GetPermission(UserID);
        }
        public string GetUserPermissionByFormID(string FormID)
        {
            return dao.GetUserPermissionByFormID(this.model.UserID, this.model.UserName, FormID);
        }
        public string GetUserPermissionByPermissionID(int SysID, int PermissionID)
        {
            return dao.GetUserPermissionByPermissionID(this.model.UserID, SysID, PermissionID, this.model.UserName);
        }
        public ArrayList GetUserRoles(int userID)
        {
            return dao.GetUserRoles(userID);
        }
        public int HasUser(string userName)
        {
            return dao.HasUser(userName);
        }

        public void RemoveRole(int roleId)
        {
            dao.RemoveRole(this.model.UserID, roleId);
        }

        /// <summary>
        /// 用戶角色管理
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="CompanyID"></param>
        public void UserToRole(ArrayList roleId)
        {
            dao.UserToRole(this.model.UserID, roleId);
        }
        public void SetPassword(string UserName, string password)
        {
            byte[] buffer1 = UserPrincipal.EncryptPassword(password);
            dao.SetPassword(UserName, buffer1);
        }
        public int TestPassword(int userID, byte[] encPassword)
        {
            return dao.TestPassword(userID, encPassword);
        }
        public bool UserPwdConfirm(string UserName, byte[] encPassword)
        {
            return dao.UserPwdConfirm(UserName, encPassword);
        }

        public int ValidateLogin(string userName, byte[] encPassword, byte[] Password)
        {
            return dao.ValidateLogin(userName, encPassword, Password);
        }

        /// <summary>
        /// 加入個人權限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="sysId"></param>
        /// <param name="permissionId"></param>
        /// <param name="permission"></param>
        public void AddUserPermission(ArrayList SysID, ArrayList PermissionID, ArrayList permission)
        {
            dao.AddUserPermission(this.model.UserID, SysID, PermissionID, permission);
        }
        /// <summary>
        /// 加入個人權限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="sysId"></param>
        /// <param name="permissionId"></param>
        /// <param name="permission"></param>
        public void AddUserPermission(int UserID,ArrayList SysID, ArrayList PermissionID, ArrayList permission)
        {
            dao.AddUserPermission(UserID, SysID, PermissionID, permission);
        }

        /// <summary>
        /// 單筆查詢的時候調用,用檢視表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataTable GetRecord(string filter)
        {
            return dao.GetRecord(filter);
        }
        /// <summary>
        /// 上下筆移動時獲取記錄
        /// </summary>
        /// <param name="move"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataTable GetRecord(string move, int userID)
        {
            return dao.GetRecord(move, userID);
        }
        /// <summary>
        /// 用戶狀態切換
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="roleId"></param>
        /// <param name="CompanyID"></param>
        public void ChangeUserState(int UserID)
        {
            dao.ChangeUserState(UserID);
        }
        /// <summary>
        /// 單筆查詢的時候調用,用檢視表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataTable GetEnterprise()
        {
            return dao.GetEnterprise();
        }
    }
}
