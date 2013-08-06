using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
namespace Js.BLL.Account
{
    public class RoleDal
    {

        Js.DAO.Account.RoleDao dao;

        public RoleDal()
        {
            dao = new Js.DAO.Account.RoleDao();
        }
        
        public RoleDal(string FormID)
        {
            dao = new Js.DAO.Account.RoleDao(FormID);
        }
        public RoleDal(string FormID,string cnKey)
        {
            dao = new Js.DAO.Account.RoleDao(FormID, cnKey);
        }
        
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.Account.RoleInfo GetModel(int RoleID)
        {
            return dao.GetModel(RoleID);
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.Account.RoleInfo GetModel(string Description)
        {
            return dao.GetModel(Description);
        }
        
        /// <summary>
        /// 新增一筆記錄
        /// </summary>
        /// <param name="_Com_DepartmentModel">SBAA_Department實體</param>
        public void Add(Js.Model.Account.RoleInfo model)
        {
            dao.Add(model);
        }
        public void Update(Js.Model.Account.RoleInfo model)
        {
            dao.Update(model);
        }

        public void Delete(int RoleID)
        {
            dao.Delete(RoleID);
        }

        public void AddRolePermission(int RoleID, ArrayList SysID, ArrayList PermissionID, ArrayList permission)
        {
            dao.AddRolePermission(RoleID, SysID, PermissionID, permission);
        }
        /// <summary>
        /// 角色指派用戶
        /// </summary>
        /// <param name="CompanyList"></param>
        /// <param name="UserList"></param>
        public void RoleToUser(int RoleID,ArrayList UserList)
        {
            dao.RoleToUser(RoleID, UserList);
        }
        /// <summary>
        /// 角色指定用戶
        /// </summary>
        public void RoleToUser(int RoleID, ArrayList UserList, string UserName)
        {
            dao.RoleToUser(RoleID, UserList, UserName);
        }
        /// <summary>
        /// 用戶指定角色
        /// </summary>
        public void UserToRole(int UserID, ArrayList RoleList, string UserName)
        {
            dao.UserToRole(UserID, RoleList, UserName);
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
    }
}
