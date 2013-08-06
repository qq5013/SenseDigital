using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Js.DAO.Account
{
    public class PermissionDao
    {
        DBAccessLayer.IDBAccess ia;
        Js.Model.Sys.TreeListInfo SysModel;
        TableFieldInfo tf;
        string cnKey;

        public PermissionDao()
        {
            ia =  DBAccessLayer.DBFactory.GetDBAccess();
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public PermissionDao(string FormID)
        {
            this.cnKey = "";
            ia = DBAccessLayer.DBFactory.GetDBAccess();
            string strSql = string.Format("select * from Sys_TreeList Where FormID = '{0}'", FormID);
            Sys.TreeListDao dao = new Sys.TreeListDao();
            SysModel = dao.GetModel(FormID);
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public PermissionDao(string FormID, string cnKey)
        {
            this.cnKey = cnKey;
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
            string strSql = string.Format("select * from Sys_TreeList Where FormID = '{0}'", FormID);
            Sys.TreeListDao dao = new Sys.TreeListDao();
            SysModel = dao.GetModel(FormID);            
        }
        public void Create(int categoryID, string description)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 8), new SqlParameter("@Description", SqlDbType.VarChar, 50) };
            param[0].Value = categoryID;
            param[1].Value = description;
            ia.ExecuteNonQueryProc("sp_Accounts_CreatePermission", param);
        }
        public void Delete(int id)
        {            
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@PermissionID", SqlDbType.Int, 4) };
            param[0].Value = id;
            ia.ExecuteNonQueryProc("sp_Accounts_DeletePermission", param);            
        }
        public DataSet GetNoPermissionList(int roleId)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@RoleID", SqlDbType.VarChar, 4) };
            param[0].Value = roleId;
            using (DataSet ds = ia.ExecuteDataSetProc("sp_Accounts_GetPermissionCategories", new IDataParameter[0]))
            {
                //base.RunProcedure("sp_Accounts_GetNoPermissionList", param, ds, "Permissions");
                DataRelation relation1 = new DataRelation("drSysID", ds.Tables["Categories"].Columns["SysID"], ds.Tables["Permissions"].Columns["SysID"], false);
                DataRelation relation2 = new DataRelation("drCategoryID", ds.Tables["Categories"].Columns["CategoryID"], ds.Tables["Permissions"].Columns["CategoryID"], false);
                ds.Relations.Add(relation1);
                ds.Relations.Add(relation2);
                DataColumn[] columnArray1 = new DataColumn[] { ds.Tables["Categories"].Columns["SysID"], ds.Tables["Categories"].Columns["CategoryID"] };
                DataColumn[] columnArray2 = new DataColumn[] { ds.Tables["Permissions"].Columns["SysID"], ds.Tables["Permissions"].Columns["PermissionID"] };
                ds.Tables["Categories"].PrimaryKey = columnArray1;
                ds.Tables["Permissions"].PrimaryKey = columnArray2;
                return ds;
            }
        }
        public DataSet GetPermissionList()
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@RoleID", SqlDbType.VarChar, 4) };
            using (DataSet ds = ia.ExecuteDataSetProc("sp_Accounts_GetPermissionCategories", new IDataParameter[0]))
            {
                //base.RunProcedure("sp_Accounts_GetPermissionList", param, ds, "Permissions");
                DataRelation relation1 = new DataRelation("drSysID", ds.Tables["Categories"].Columns["SysID"], ds.Tables["Permissions"].Columns["SysID"], false);
                DataRelation relation2 = new DataRelation("drCategoryID", ds.Tables["Categories"].Columns["CategoryID"], ds.Tables["Permissions"].Columns["CategoryID"], false);
                ds.Relations.Add(relation1);
                ds.Relations.Add(relation2);
                DataColumn[] columnArray1 = new DataColumn[] { ds.Tables["Categories"].Columns["SysID"], ds.Tables["Categories"].Columns["CategoryID"] };
                DataColumn[] columnArray2 = new DataColumn[] { ds.Tables["Permissions"].Columns["SysID"], ds.Tables["Permissions"].Columns["PermissionID"] };
                ds.Tables["Categories"].PrimaryKey = columnArray1;
                ds.Tables["Permissions"].PrimaryKey = columnArray2;
                return ds;
            }
        }
        public DataSet GetPermissionList(int roleId)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@RoleID", SqlDbType.VarChar, 4) };
            param[0].Value = roleId;
            using (DataSet ds = ia.ExecuteDataSetProc("sp_Accounts_GetPermissionCategories", new IDataParameter[0]))
            {
                //base.RunProcedure("sp_Accounts_GetPermissionList", param, ds, "Permissions");
                DataRelation relation1 = new DataRelation("drSysID", ds.Tables["Categories"].Columns["SysID"], ds.Tables["Permissions"].Columns["SysID"],false);
                DataRelation relation2 = new DataRelation("drCategoryID", ds.Tables["Categories"].Columns["CategoryID"], ds.Tables["Permissions"].Columns["CategoryID"], false);
                ds.Relations.Add(relation1);
                ds.Relations.Add(relation2);
                DataColumn[] columnArray1 = new DataColumn[] { ds.Tables["Categories"].Columns["SysID"], ds.Tables["Categories"].Columns["CategoryID"] };
                DataColumn[] columnArray2 = new DataColumn[] { ds.Tables["Permissions"].Columns["SysID"], ds.Tables["Permissions"].Columns["PermissionID"] };
                ds.Tables["Categories"].PrimaryKey = columnArray1;
                ds.Tables["Permissions"].PrimaryKey = columnArray2;
                return ds;
            }
        }
        public DataSet GetRolePermission(int roleId)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@RoleID", SqlDbType.Int, 4) };
            param[0].Value = roleId;
            return ia.ExecuteDataSetProc("sp_Accounts_GetRolePermission", param);
        }
        public DataSet GetStdPermissionList()
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@SysID", SqlDbType.Int, 4) };
            return ia.ExecuteDataSetProc("sp_Accounts_GetStdPermissionList", param);
        }
        public DataSet GetStdPermissionList(int SysID)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@SysID", SqlDbType.Int, 4) };
            param[0].Value = SysID;
            return ia.ExecuteDataSetProc("sp_Accounts_GetStdPermissionList", param);
        }
        public DataRow Retrieve(int permissionId)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@PermissionID", SqlDbType.Int, 4) };
            param[0].Value = permissionId;
            using (DataSet ds = ia.ExecuteDataSetProc("sp_Accounts_GetPermissionDetails", param))
            {
                if (ds.Tables[0].Rows.Count == 0)
                {
                    throw new Exception("\u627e\u4e0d\u5230\u6743\u9650 \uff08" + permissionId + "\uff09");
                }
                return ds.Tables[0].Rows[0];
            }
        }

        public void Update(int PermissionID, string description)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@PermissionID", SqlDbType.Int, 8), new SqlParameter("@Description", SqlDbType.VarChar, 50) };
            param[0].Value = PermissionID;
            param[1].Value = description;
            ia.ExecuteNonQueryProc("sp_Accounts_UpdatePermission", param);
        }
    }
}
