using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Js.DAO.Account
{
    public class UserDao
    {
        DBAccessLayer.IDBAccess ia;        
        TableFieldInfo tf;
        
        public UserDao()
        {
            ia =  DBAccessLayer.DBFactory.GetDBAccess();
            tf = new TableFieldInfo("Accounts_Users");
        }
        public UserDao(string cnKey)
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL,cnKey);
            tf = new TableFieldInfo("Accounts_Users", cnKey);
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.Account.UsersInfo GetModel(int UserID)
        {
            string strSql = string.Format("select * from Accounts_Users Where UserID={0}",UserID);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            Js.Model.Account.UsersInfo model = new Js.Model.Account.UsersInfo();
            if (dt.Rows.Count != 0)
            {
                model.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
                model.UserName = dt.Rows[0]["UserName"].ToString();
                model.UserLevel = int.Parse(dt.Rows[0]["UserLevel"].ToString());
                model.TrueName = dt.Rows[0]["TrueName"].ToString();
                model.PersonID = dt.Rows[0]["PersonID"].ToString();
                model.DepartmentID = dt.Rows[0]["DepartmentID"].ToString();
                model.PersonName = dt.Rows[0]["PersonName"].ToString();
                model.Sex = bool.Parse(dt.Rows[0]["Sex"].ToString());
                model.Phone = dt.Rows[0]["Phone"].ToString();
                model.CellPhone = dt.Rows[0]["CellPhone"].ToString();
                model.Email = dt.Rows[0]["Email"].ToString();
                model.State = byte.Parse(dt.Rows[0]["State"].ToString());
                if (dt.Rows[0]["EnableDate"].ToString().Length > 0)
                    model.EnableDate = DateTime.Parse(dt.Rows[0]["EnableDate"].ToString());
                if (dt.Rows[0]["StopDate"].ToString().Length > 0)
                    model.StopDate = DateTime.Parse(dt.Rows[0]["StopDate"].ToString());
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
        public Js.Model.Account.UsersInfo GetModel(string UserName)
        {
            string strSql = string.Format("select * from Accounts_Users Where UserName='{0}'", UserName);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            Js.Model.Account.UsersInfo model = new Js.Model.Account.UsersInfo();
            if (dt.Rows.Count != 0)
            {
                model.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
                model.UserName = dt.Rows[0]["UserName"].ToString();
                model.UserLevel = int.Parse(dt.Rows[0]["UserLevel"].ToString());
                model.TrueName = dt.Rows[0]["TrueName"].ToString();
                model.PersonID = dt.Rows[0]["PersonID"].ToString();
                model.DepartmentID = dt.Rows[0]["DepartmentID"].ToString();
                model.PersonName = dt.Rows[0]["PersonName"].ToString();
                model.Sex = bool.Parse(dt.Rows[0]["Sex"].ToString());
                model.Phone = dt.Rows[0]["Phone"].ToString();
                model.CellPhone = dt.Rows[0]["CellPhone"].ToString();
                model.Email = dt.Rows[0]["Email"].ToString();
                model.State = byte.Parse(dt.Rows[0]["State"].ToString());
                if (dt.Rows[0]["EnableDate"].ToString().Length > 0)
                    model.EnableDate = DateTime.Parse(dt.Rows[0]["EnableDate"].ToString());
                if (dt.Rows[0]["StopDate"].ToString().Length > 0)
                    model.StopDate = DateTime.Parse(dt.Rows[0]["StopDate"].ToString());
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
        public void Add(Js.Model.Account.UsersInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_Users");
            strSql.Append("(UserName,TrueName,UserLevel,ParentLevel,Password,PersonID,DepartmentID,PersonName,Sex,Phone,CellPhone,Email,State,EnableDate,StopDate,CreateDate,CreateUserName,LastModifyDate,LastModifyUserName)");
            strSql.Append(" values ");
            strSql.Append("(@UserName,@TrueName,@UserLevel,@ParentLevel,@Password,@PersonID,@DepartmentID,@PersonName,@Sex,@Phone,@CellPhone,@Email,@State,@EnableDate,@StopDate, GetDate(),@CreateUserName, GetDate(),@LastModifyUserName)");

            SqlParameter[] parameters ={
					 new SqlParameter("@UserName",SqlDbType.NVarChar,tf.GetTypeLength("UserName")),
					 new SqlParameter("@TrueName",SqlDbType.NVarChar,tf.GetTypeLength("TrueName")),
					 new SqlParameter("@UserLevel",SqlDbType.Int,tf.GetTypeLength("UserLevel")),
					 new SqlParameter("@ParentLevel",SqlDbType.Int,tf.GetTypeLength("ParentLevel")),
					 new SqlParameter("@Password",SqlDbType.Binary,tf.GetTypeLength("Password")),
					 new SqlParameter("@PersonID",SqlDbType.NVarChar,tf.GetTypeLength("PersonID")),
					 new SqlParameter("@DepartmentID",SqlDbType.NVarChar,tf.GetTypeLength("DepartmentID")),
					 new SqlParameter("@PersonName",SqlDbType.NVarChar,tf.GetTypeLength("PersonName")),
					 new SqlParameter("@Sex",SqlDbType.Bit,tf.GetTypeLength("Sex")),
					 new SqlParameter("@Phone",SqlDbType.NVarChar,tf.GetTypeLength("Phone")),
					 new SqlParameter("@CellPhone",SqlDbType.NVarChar,tf.GetTypeLength("CellPhone")),
					 new SqlParameter("@Email",SqlDbType.NVarChar,tf.GetTypeLength("Email")),
					 new SqlParameter("@State",SqlDbType.TinyInt,tf.GetTypeLength("State")),
					 new SqlParameter("@EnableDate",SqlDbType.DateTime,tf.GetTypeLength("EnableDate")),
					 new SqlParameter("@StopDate",SqlDbType.DateTime,tf.GetTypeLength("StopDate")),
					 new SqlParameter("@CreateUserName",SqlDbType.NVarChar,tf.GetTypeLength("CreateUserName")),
					 new SqlParameter("@LastModifyUserName",SqlDbType.NVarChar,tf.GetTypeLength("LastModifyUserName"))
			};
            parameters[0].Value = model.UserName.Trim();
            parameters[1].Value = model.TrueName.Trim();
            parameters[2].Value = model.UserLevel;
            parameters[3].Value = model.ParentLevel;
            parameters[4].Value = model.Password;
            parameters[5].Value = model.PersonID.Trim();
            parameters[6].Value = model.DepartmentID.Trim();
            parameters[7].Value = model.PersonName.Trim();
            parameters[8].Value = model.Sex;
            parameters[9].Value = model.Phone.Trim();
            parameters[10].Value = model.CellPhone.Trim();
            parameters[11].Value = model.Email.Trim();
            parameters[12].Value = model.State;
            if(model.EnableDate==null)
                parameters[13].Value = DBNull.Value;
            else
                parameters[13].Value = model.EnableDate;
            if (model.StopDate == null)
                parameters[14].Value = DBNull.Value;
            else
                parameters[14].Value = model.StopDate;

            parameters[15].Value = model.CreateUserName.Trim();
            parameters[16].Value = model.LastModifyUserName.Trim();

            ia.ExecuteNonQuerySql(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 更新一筆記錄
        /// </summary>		
        /// <param name="_Accounts_UsersModel">_Accounts_UsersModel</param>
        public void Update(Js.Model.Account.UsersInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update Accounts_Users Set ");
            strSql.Append("UserName=@UserName , ");
            strSql.Append("TrueName=@TrueName , ");
            strSql.Append("UserLevel=@UserLevel , ");
            strSql.Append("ParentLevel=@ParentLevel , ");
            //strSql.Append("Password=@Password , ");
            strSql.Append("PersonID=@PersonID , ");
            strSql.Append("DepartmentID=@DepartmentID , ");
            strSql.Append("PersonName=@PersonName , ");
            strSql.Append("Sex=@Sex , ");
            strSql.Append("Phone=@Phone , ");
            strSql.Append("CellPhone=@CellPhone , ");
            strSql.Append("Email=@Email , ");
            strSql.Append("State=@State , ");
            strSql.Append("EnableDate=@EnableDate , ");
            strSql.Append("StopDate=@StopDate , ");
            strSql.Append("CreateDate=@CreateDate , ");
            strSql.Append("CreateUserName=@CreateUserName , ");
            strSql.Append("LastModifyDate=GetDate() , ");
            strSql.Append("LastModifyUserName=@LastModifyUserName  ");
            strSql.Append(" where 1=1");
            strSql.Append(" And UserID=@UserID ");

            SqlParameter[] parameters ={
					 new SqlParameter("@UserID",SqlDbType.Int,tf.GetTypeLength("UserID")),
					 new SqlParameter("@UserName",SqlDbType.NVarChar,tf.GetTypeLength("UserName")),
					 new SqlParameter("@TrueName",SqlDbType.NVarChar,tf.GetTypeLength("TrueName")),
					 new SqlParameter("@UserLevel",SqlDbType.Int,tf.GetTypeLength("UserLevel")),
					 new SqlParameter("@ParentLevel",SqlDbType.Int,tf.GetTypeLength("ParentLevel")),
					 //new SqlParameter("@Password",SqlDbType.Binary,tf.GetTypeLength("Password")),
					 new SqlParameter("@PersonID",SqlDbType.NVarChar,tf.GetTypeLength("PersonID")),
					 new SqlParameter("@DepartmentID",SqlDbType.NVarChar,tf.GetTypeLength("DepartmentID")),
					 new SqlParameter("@PersonName",SqlDbType.NVarChar,tf.GetTypeLength("PersonName")),
					 new SqlParameter("@Sex",SqlDbType.Bit,tf.GetTypeLength("Sex")),
					 new SqlParameter("@Phone",SqlDbType.NVarChar,tf.GetTypeLength("Phone")),
					 new SqlParameter("@CellPhone",SqlDbType.NVarChar,tf.GetTypeLength("CellPhone")),
					 new SqlParameter("@Email",SqlDbType.NVarChar,tf.GetTypeLength("Email")),
					 new SqlParameter("@State",SqlDbType.TinyInt,tf.GetTypeLength("State")),
					 new SqlParameter("@EnableDate",SqlDbType.DateTime,tf.GetTypeLength("EnableDate")),
					 new SqlParameter("@StopDate",SqlDbType.DateTime,tf.GetTypeLength("StopDate")),
					 new SqlParameter("@CreateDate",SqlDbType.DateTime,tf.GetTypeLength("CreateDate")),
					 new SqlParameter("@CreateUserName",SqlDbType.NVarChar,tf.GetTypeLength("CreateUserName")),
					 new SqlParameter("@LastModifyUserName",SqlDbType.NVarChar,tf.GetTypeLength("LastModifyUserName"))
		
			};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserName.Trim();
            parameters[2].Value = model.TrueName.Trim();
            parameters[3].Value = model.UserLevel;
            parameters[4].Value = model.ParentLevel;
            //parameters[5].Value = model.Password;
            parameters[5].Value = model.PersonID.Trim();
            parameters[6].Value = model.DepartmentID.Trim();
            parameters[7].Value = model.PersonName.Trim();
            parameters[8].Value = model.Sex;
            parameters[9].Value = model.Phone.Trim();
            parameters[10].Value = model.CellPhone.Trim();
            parameters[11].Value = model.Email.Trim();
            parameters[12].Value = model.State;
            if (model.EnableDate == null)
                parameters[13].Value = DBNull.Value;
            else
                parameters[13].Value = model.EnableDate;
            if (model.StopDate == null)
                parameters[14].Value = DBNull.Value;
            else
                parameters[14].Value = model.StopDate;
            parameters[15].Value = model.CreateDate;
            parameters[16].Value = model.CreateUserName.Trim();
            parameters[17].Value = model.LastModifyUserName.Trim();

            ia.ExecuteNonQuerySql(strSql.ToString(), parameters);
        }
        public DataRow GetUserLevel(string UserName)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 20) };
            param[0].Value = UserName;
            //base.RunProcedure("sp_Accounts_AddUserToRole", param, out num1);
            DataTable dt = ia.ExecuteDataSetProc("sp_Accounts_GetUserLevel", param).Tables[0];

            DataRow dr = dt.Rows[0];           

            return dr;
        }

        public DataTable GetParentUser(int UserID)
        {
            string strSql = string.Format("SELECT * FROM VAccounts_Users where UserID ={0}", UserID);
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];            

            return dt;
        }
        public void Delete(int userID)
        {            
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            param[0].Value = userID;
            ia.ExecuteNonQueryProc("sp_Accounts_DeleteUser", param);            
        }
        public DataSet GetAllUsers()
        {
            string strSql = "SELECT * FROM Accounts_Users where State =1";
            return ia.ExecuteDataSetSql(strSql);
        }
        public DataSet GetAllUsers(string key)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@key", SqlDbType.VarChar, 50) };

            param[0].Value = key;
            string strSql = string.Format("SELECT * FROM Accounts_Users where TrueName like '%{0}%' order by UserID", key);
            return ia.ExecuteDataSetSql(strSql);
        }
        public DataTable GetUserRole(int userID)
        {
            string strSql = string.Format("select top 1 * from VAccounts_UserRoles where UserID={0} order by LastModifyDate", userID);
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            return dt;
        }
        public DataSet GetRoleUsers(int roleID)
        {
            string strSql = string.Format("SELECT Accounts_Users.* FROM Accounts_Users inner join Accounts_UserRoles on Accounts_Users.UserID=Accounts_UserRoles.UserID where Accounts_UserRoles.RoleID={0} order by Accounts_Users.UserID",roleID);
            return ia.ExecuteDataSetSql(strSql);
        }
        
        /// <summary>
        /// 當UserName=="admin"取得所有權限
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public ArrayList GetEffectivePermissionListID(int userID, string UserName)
        {
            ArrayList list1 = new ArrayList();
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 20) };
            param[0].Value = userID;
            param[1].Value = UserName;
            IDataReader reader1 = ia.ExecuteReaderProc("sp_Accounts_GetEffectivePermissionListID", param);
            while (reader1.Read())
            {
                list1.Add(reader1.GetString(0));
            }
            reader1.Close();
            return list1;
        }
        /// <summary>
        /// Username=="admin"傳回所有權限
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public DataSet GetUserPermission(int userID, string UserName)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 20) };
            param[0].Value = userID;
            param[1].Value = UserName;
            return ia.ExecuteDataSetProc("sp_Accounts_GetUserPermission", param);
        }
        /// <summary>
        /// 該用戶所擁有的角色權限
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="companyID"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataSet GetUserRolePermission(int userID, string UserName)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 20) };
            param[0].Value = userID;
            param[1].Value = UserName;
            return ia.ExecuteDataSetProc("sp_Accounts_GetUserRolePermission", param);
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
            SqlParameter[] param = new SqlParameter[] {new SqlParameter("@culture", SqlDbType.NVarChar, 20) };
            param[0].Value = culture;
            return ia.ExecuteDataSetProc("sp_Accounts_GetUserPermissions", param);
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
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            param[0].Value = UserID;
            return ia.ExecuteDataSetProc("sp_Accounts_GetPermission", param);
        }
        public string GetUserPermissionByFormID(int userID, string UserName,string FormID)
        {
            SqlParameter[] param = new SqlParameter[] { 
                                                                    new SqlParameter("@UserID", SqlDbType.Int, 4),
                                                                    new SqlParameter("@UserName", SqlDbType.NVarChar, 20),
                                                                    new SqlParameter("@FormID", SqlDbType.NVarChar, 50)
                                                       };
            param[0].Value = userID;
            param[1].Value = UserName;
            param[2].Value = FormID;

            IDataReader dr = ia.ExecuteReaderProc("sp_Accounts_GetUserPermissionByFormID", param);
            if (dr.Read())
                return dr.GetString(0);
            else
                return "0000000000";
        }
        public string GetUserPermissionByPermissionID(int userID, int SysID, int PermissionID, string UserName)
        {
            SqlParameter[] param = new SqlParameter[] { 
                                                                    new SqlParameter("@UserID", SqlDbType.Int, 4), 
                                                                    new SqlParameter("@SysID", SqlDbType.Int, 4), 
                                                                    new SqlParameter("@PermissionID", SqlDbType.Int, 4), 
                                                                    new SqlParameter("@UserName", SqlDbType.NVarChar, 20)
                                                                };
            param[0].Value = userID;
            param[1].Value = SysID;
            param[2].Value = PermissionID;
            param[3].Value = UserName;


            IDataReader dr = ia.ExecuteReaderProc("sp_Accounts_GetUserPermissionByPermissionID", param);
            if (dr.Read())
                return dr.GetString(0);
            else
                return "000000";
        }

        
        public ArrayList GetUserRoles(int userID)
        {
            ArrayList list1 = new ArrayList();
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            param[0].Value = userID;
            IDataReader reader1 = ia.ExecuteReaderProc("sp_Accounts_GetUserRoles", param);
            while (reader1.Read())
            {
                list1.Add(reader1.GetInt32(0));
            }
            return list1;
        }

        public int HasUser(string userName)
        {
            string strSql = string.Format("SELECT * FROM Accounts_Users WHERE UserName = '{0}'", userName);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            if (dt.Rows.Count == 0)
                return 0;

            return int.Parse(dt.Rows[0]["UserID"].ToString());
        }

        public void RemoveRole(int userId, int roleId)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@RoleID", SqlDbType.Int, 4) };
            param[0].Value = userId;
            param[1].Value = roleId;
            ia.ExecuteNonQueryProc("sp_Accounts_RemoveUserFromRole", param);
        }


        public void SetPassword(string UserName, byte[] encPassword)
        {            
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 20), new SqlParameter("@EncryptedPassword", SqlDbType.Binary, 20) };
            param[0].Value = UserName;
            param[1].Value = encPassword;
            ia.ExecuteNonQueryProc("sp_Accounts_SetPassword", param);            
        }

        public int TestPassword(int userID, byte[] encPassword)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@EncryptedPassword", SqlDbType.Binary, 20) };
            param[0].Value = userID;
            param[1].Value = encPassword;
            object result = ia.ExecuteScalarProc("sp_Accounts_TestPassword", param);
            if (result != null)
                return (int)result;
            else
                return 0;
        }
        public bool UserPwdConfirm(string UserName,byte[] encPassword)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 20), new SqlParameter("@EncryptedPassword", SqlDbType.Binary, 20) };
            param[0].Value = UserName;
            param[1].Value = encPassword;

            DataTable dt = ia.ExecuteDataSetProc("sp_Accounts_ConfirmPassword", param).Tables[0];
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public int ValidateLogin(string userName, byte[] encPassword, byte[] Password)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 20), new SqlParameter("@EncryptedPassword", SqlDbType.Binary, 20), new SqlParameter("@Password", SqlDbType.Binary, 20) };
            param[0].Value = userName;
            param[1].Value = encPassword;
            param[2].Value = Password;
            DataSet ds = ia.ExecuteDataSetProc("sp_Accounts_ValidateLogin", param);
            if (ds.Tables[0].Rows.Count > 0)
                return int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
            else
                return -1;
            //return (int)ia.ExecuteReturnProc("sp_Accounts_ValidateLogin", param);           
            
        }

        /// <summary>
        /// 加入個人權限
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <param name="sysId"></param>
        /// <param name="permissionId"></param>
        /// <param name="permission"></param>
        public void AddUserPermission(int UserID, ArrayList SysID, ArrayList PermissionID, ArrayList permission)
        {

            string strSql = "";
            strSql = "delete from Accounts_UserPermissions where UserID=" + UserID;
            if (SysID.Count > 0)
                strSql += ";Insert into Accounts_UserPermissions(UserID, SysID, PermissionID, Permission) ";
            for (int i = 0; i < SysID.Count; i++)
            {
                strSql += "Select " + UserID + "," + SysID[i] + "," + PermissionID[i] + ",'" + permission[i] + "' Union All ";
            }
            if (SysID.Count > 0)
                strSql = strSql.Substring(0, strSql.Length - 10);
            ia.ExecuteNonQuerySql(strSql);

        }
        /// <summary>
        /// 用戶指定角色
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="roleId"></param>
        /// <param name="CompanyID"></param>
        public void UserToRole(int UserID, ArrayList RoleList)
        {

            string strSql = "";
            strSql = "delete from Accounts_UserRoles where UserID=" + UserID;
            if (RoleList.Count > 0)
                strSql += ";Insert into Accounts_UserRoles(RoleID,UserID) ";
            for (int i = 0; i < RoleList.Count; i++)
            {
                strSql += "Select " + RoleList[i] + "," + UserID + " Union All ";
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
            string strSql = string.Format("select * from Accounts_Users where {0}", filter);
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            return dt;
        }
        /// <summary>
        /// 上下筆移動時獲取記錄
        /// </summary>
        /// <param name="move"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual DataTable GetRecord(string move, int userID)
        {
            string strSql = "select Top 1 * from VAccounts_UserRoles ";
            if (move == "F")
                strSql += " Order by UserID,LastModifyDate desc";
            else if (move == "P")
                strSql += string.Format(" Where UserID<{0} Order by UserID Desc,LastModifyDate desc", userID);
            else if (move == "N")
                strSql += string.Format(" Where UserID>{0} Order by UserID,LastModifyDate desc", userID);
            else
                strSql += " Order by UserID Desc,LastModifyDate desc";
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            return dt;
        }
        /// <summary>
        /// 用戶狀態切換
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="roleId"></param>
        /// <param name="CompanyID"></param>
        public void ChangeUserState(int UserID)
        {
            string strSql = "";
            strSql = "update Accounts_Users set state = (case State when 0 then 1 when 1 then 2 else 0 end) where UserID=" + UserID;            
            ia.ExecuteNonQuerySql(strSql);
            strSql = "update Accounts_Users set EnableDate =(case state when 1 then getdate() else null end),StopDate=(case state when 2 then getdate() else null end) where UserID=" + UserID;
            ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 單筆查詢的時候調用,用檢視表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataTable GetEnterprise()
        {
            string strSql = "select * from Com_EnterpriseDb where state=1";
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            return dt;
        }
    }
}
