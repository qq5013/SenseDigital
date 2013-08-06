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
        /// �o��sys_TreeList�ƾڹ���
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.Account.RoleInfo GetModel(int RoleID)
        {
            return dao.GetModel(RoleID);
        }
        /// <summary>
        /// �o��sys_TreeList�ƾڹ���
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.Account.RoleInfo GetModel(string Description)
        {
            return dao.GetModel(Description);
        }
        
        /// <summary>
        /// �s�W�@���O��
        /// </summary>
        /// <param name="_Com_DepartmentModel">SBAA_Department����</param>
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
        /// ��������Τ�
        /// </summary>
        /// <param name="CompanyList"></param>
        /// <param name="UserList"></param>
        public void RoleToUser(int RoleID,ArrayList UserList)
        {
            dao.RoleToUser(RoleID, UserList);
        }
        /// <summary>
        /// ������w�Τ�
        /// </summary>
        public void RoleToUser(int RoleID, ArrayList UserList, string UserName)
        {
            dao.RoleToUser(RoleID, UserList, UserName);
        }
        /// <summary>
        /// �Τ���w����
        /// </summary>
        public void UserToRole(int UserID, ArrayList RoleList, string UserName)
        {
            dao.UserToRole(UserID, RoleList, UserName);
        }
        /// <summary>
        /// �浧�d�ߪ��ɭԽե�,���˵���
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataTable GetRecord(string filter)
        {
            return dao.GetRecord(filter);
        }
    }
}
