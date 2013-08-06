using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Js.BLL.Account
{
    public class PermissionDal
    {
        Js.DAO.Account.PermissionDao dao;
        public PermissionDal()
        {
            dao = new Js.DAO.Account.PermissionDao();
        }
        public PermissionDal(string FormID)
        {
            dao = new Js.DAO.Account.PermissionDao(FormID);
        }
        public PermissionDal(string FormID, string cnKey)
        {
            dao = new Js.DAO.Account.PermissionDao(FormID, cnKey);            
        }

        public string GetPermissionName(int pID)
        {
            return dao.Retrieve(pID)["Description"].ToString();
        }

        public void Update(int pcID, string description)
        {
            dao.Update(pcID, description);
        }
        //public DataSet GetAllCategories()
        //{
        //    return dao.GetCategoryList();
        //}

        public DataSet GetAllPermissions()
        {
            return dao.GetPermissionList();
        }
        public DataSet GetRolePermissions(int RoleID)
        {
            return dao.GetRolePermission(RoleID);
        }
        public DataSet GetStdPermissions()
        {
            return dao.GetStdPermissionList();
        }
        public DataSet GetStdPermissions(int SysID)
        {
            return dao.GetStdPermissionList(SysID);
        }
    }

}
