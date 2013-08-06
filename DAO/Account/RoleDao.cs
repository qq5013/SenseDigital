using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Js.DAO.Account
{
    public class RoleDao
    {
        DBAccessLayer.IDBAccess ia;
        Js.Model.Sys.TreeListInfo SysModel;
        TableFieldInfo tf;
        string cnKey;

        public RoleDao()
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess();
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public RoleDao(string FormID)
        {
            this.cnKey = "";
            ia = DBAccessLayer.DBFactory.GetDBAccess();
            string strSql = string.Format("select * from Sys_TreeList Where FormID = '{0}'", FormID);
            Sys.TreeListDao dao = new Sys.TreeListDao();
            SysModel = dao.GetModel(FormID);
            tf = new TableFieldInfo(SysModel.TableName, this.cnKey);
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public RoleDao(string FormID, string cnKey)
        {
            this.cnKey = cnKey;
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
            string strSql = string.Format("select * from Sys_TreeList Where FormID = '{0}'", FormID);
            Sys.TreeListDao dao = new Sys.TreeListDao();
            SysModel = dao.GetModel(FormID);
            tf = new TableFieldInfo(SysModel.TableName, this.cnKey);
        }

        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.Account.RoleInfo GetModel(int RoleID)
        {
            string strSql = string.Format("select * from Accounts_Roles Where RoleID={0}", RoleID);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            Js.Model.Account.RoleInfo model = new Js.Model.Account.RoleInfo();
            if (dt.Rows.Count != 0)
            {
                model.RoleID = int.Parse(dt.Rows[0]["RoleID"].ToString());
                model.RoleName = dt.Rows[0]["RoleName"].ToString();
                model.UserLevel = int.Parse(dt.Rows[0]["UserLevel"].ToString());
                if (dt.Rows[0]["CreateDate"].ToString().Length > 0)
                    model.CreateDate = DateTime.Parse(dt.Rows[0]["CreateDate"].ToString());
                model.CreateUserName = dt.Rows[0]["CreateUserName"].ToString();
                if (dt.Rows[0]["LastModifyDate"].ToString().Length > 0)
                    model.LastModifyDate = DateTime.Parse(dt.Rows[0]["LastModifyDate"].ToString());
                model.LastModifyUserName = dt.Rows[0]["LastModifyUserName"].ToString();
            }
            return model;
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.Account.RoleInfo GetModel(string Description)
        {
            string strSql = string.Format("select * from Accounts_Roles Where Description='{0}'", Description);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            Js.Model.Account.RoleInfo model = new Js.Model.Account.RoleInfo();
            if (dt.Rows.Count != 0)
            {
                model.RoleID = int.Parse(dt.Rows[0]["RoleID"].ToString());
                model.RoleName = dt.Rows[0]["RoleName"].ToString();
                model.UserLevel = int.Parse(dt.Rows[0]["UserLevel"].ToString());
                if (dt.Rows[0]["CreateDate"].ToString().Length > 0)
                    model.CreateDate = DateTime.Parse(dt.Rows[0]["CreateDate"].ToString());
                model.CreateUserName = dt.Rows[0]["CreateUserName"].ToString();
                if (dt.Rows[0]["LastModifyDate"].ToString().Length > 0)
                    model.LastModifyDate = DateTime.Parse(dt.Rows[0]["LastModifyDate"].ToString());
                model.LastModifyUserName = dt.Rows[0]["LastModifyUserName"].ToString();
            }
            return model;
        }

        /// <summary>
        /// 新增一筆記錄
        /// </summary>
        /// <param name="_Accounts_UsersModel">SBAA_unts_Users實體</param>
        public void Add(Js.Model.Account.RoleInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_Roles");
            strSql.Append("(RoleName,UserLevel,CreateDate,CreateUserName,LastModifyDate,LastModifyUserName)");
            strSql.Append(" values ");
            strSql.Append("(@RoleName,@UserLevel,GetDate(),@CreateUserName, GetDate(),@LastModifyUserName)");

            SqlParameter[] parameters ={
					 new SqlParameter("@RoleName",SqlDbType.NVarChar,tf.GetTypeLength("RoleName")),
				     new SqlParameter("@UserLevel",SqlDbType.Int,tf.GetTypeLength("UserLevel")),
					 new SqlParameter("@CreateUserName",SqlDbType.NVarChar,tf.GetTypeLength("CreateUserName")),
					 new SqlParameter("@LastModifyUserName",SqlDbType.NVarChar,tf.GetTypeLength("LastModifyUserName"))
			};
            parameters[0].Value = model.RoleName.Trim();
            parameters[1].Value = model.UserLevel;
            parameters[2].Value = model.CreateUserName.Trim();
            parameters[3].Value = model.LastModifyUserName.Trim();

            ia.ExecuteNonQuerySql(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 更新一筆記錄
        /// </summary>		
        /// <param name="_Accounts_UsersModel">_Accounts_UsersModel</param>
        public void Update(Js.Model.Account.RoleInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update Accounts_Roles Set ");
            strSql.Append("RoleName=@RoleName , ");
            strSql.Append("UserLevel=@UserLevel , ");
            strSql.Append("CreateDate=@CreateDate , ");
            strSql.Append("CreateUserName=@CreateUserName , ");
            strSql.Append("LastModifyDate=GetDate() , ");
            strSql.Append("LastModifyUserName=@LastModifyUserName  ");
            strSql.Append(" where 1=1");
            strSql.Append(" And RoleID=@RoleID ");

            SqlParameter[] parameters ={
					 new SqlParameter("@RoleID",SqlDbType.Int,tf.GetTypeLength("RoleID")),
					 new SqlParameter("@RoleName",SqlDbType.NVarChar,tf.GetTypeLength("RoleName")),
					 new SqlParameter("@UserLevel",SqlDbType.Int,tf.GetTypeLength("UserLevel")),
					 new SqlParameter("@CreateDate",SqlDbType.DateTime,tf.GetTypeLength("CreateDate")),
					 new SqlParameter("@CreateUserName",SqlDbType.NVarChar,tf.GetTypeLength("CreateUserName")),
					 new SqlParameter("@LastModifyUserName",SqlDbType.NVarChar,tf.GetTypeLength("LastModifyUserName"))
		
			};
            parameters[0].Value = model.RoleID;
            parameters[1].Value = model.RoleName.Trim();
            parameters[2].Value = model.UserLevel;
            parameters[3].Value = model.CreateDate;
            parameters[4].Value = model.CreateUserName.Trim();
            parameters[5].Value = model.LastModifyUserName.Trim();

            ia.ExecuteNonQuerySql(strSql.ToString(), parameters);
        }
        public void AddRolePermission(int RoleID, ArrayList SysID, ArrayList PermissionID, ArrayList permission)
        {
            string strSql = "delete from Accounts_RolePermissions where roleID=" + RoleID;
            if (SysID.Count > 0)
                strSql += ";Insert into Accounts_RolePermissions(RoleID, SysID, PermissionID, Permission) ";
            for (int i = 0; i < SysID.Count; i++)
            {
                strSql += "Select " + RoleID + "," + SysID[i] + "," + PermissionID[i] + ",'" + permission[i] + "' Union All ";
            }
            if (SysID.Count > 0)
                strSql = strSql.Substring(0, strSql.Length - 10);
            ia.ExecuteNonQuerySql(strSql);
        }

        public void Create(string RoleName)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@RoleName", SqlDbType.VarChar, 50) };
            param[0].Value = RoleName;
            ia.ExecuteNonQueryProc("sp_Accounts_CreateRole", param);
        }
        public void Update(int roleId, string RoleName)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@RoleID", SqlDbType.Int, 4), new SqlParameter("@RoleName", SqlDbType.VarChar, 50) };
            param[0].Value = roleId;
            param[1].Value = RoleName;
            ia.ExecuteNonQueryProc("sp_Accounts_UpdateRole", param);
        }
        public void Delete(int RoleID)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@RoleID", SqlDbType.Int, 4) };
            param[0].Value = RoleID;

            ia.ExecuteNonQueryProc("sp_Accounts_DeleteRole", param);
        }

        public void RemovePermission(int roleId, int permissionId)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@RoleID", SqlDbType.Int, 4), new SqlParameter("@PermissionID", SqlDbType.Int, 4) };
            param[0].Value = roleId;
            param[1].Value = permissionId;
            ia.ExecuteNonQueryProc("sp_Accounts_RemovePermissionFromRole", param);
        }

        /// <summary>
        /// 角色指定用戶
        /// </summary>
        public void RoleToUser(int RoleID, ArrayList UserList)
        {
            string strSql = "";
            strSql = "delete from Accounts_UserRoles where RoleID=" + RoleID;
            if (UserList.Count > 0)
                strSql += ";Insert into Accounts_UserRoles(UserID, RoleID) ";
            for (int i = 0; i < UserList.Count; i++)
            {
                strSql += "Select '" + UserList[i] + "'," + RoleID + " Union All ";
            }
            if (UserList.Count > 0)
                strSql = strSql.Substring(0, strSql.Length - 10);
            ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 角色指定用戶
        /// </summary>
        public void RoleToUser(int RoleID, ArrayList UserList,string UserName)
        {
            string strSql = "";
            strSql = "delete from Accounts_UserRoles where RoleID=" + RoleID;
            if (UserList.Count > 0)
                strSql += ";Insert into Accounts_UserRoles(UserID, RoleID,LastModifyDate,LastModifyUserName) ";
            for (int i = 0; i < UserList.Count; i++)
            {
                strSql += string.Format("select '{0}',{1},getdate(),'{2}' Union All ", UserList[i], RoleID, UserName);
            }
            if (UserList.Count > 0)
                strSql = strSql.Substring(0, strSql.Length - 10);
            ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 用戶指定角色
        /// </summary>
        public void UserToRole(int UserID, ArrayList RoleList, string UserName)
        {
            string strSql = "";
            strSql = "delete from Accounts_UserRoles where UserID=" + UserID;
            if (RoleList.Count > 0)
                strSql += ";Insert into Accounts_UserRoles(UserID, RoleID,LastModifyDate,LastModifyUserName) ";
            for (int i = 0; i < RoleList.Count; i++)
            {
                strSql += string.Format("select '{0}',{1},getdate(),'{2}' Union All ", UserID,RoleList[i], UserName);
            }
            if (RoleList.Count > 0)
                strSql = strSql.Substring(0, strSql.Length - 10);
            ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 單筆查詢的時候調用,用檢視表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataTable GetRecord(string filter)
        {
            string strSql = string.Format("select * from Accounts_Roles where {0}", filter);
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            return dt;
        }
    }
}
