using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;

namespace Js.DAO.Sys
{
    public class SysLabelDao
    {
        DBAccessLayer.IDBAccess ia;
        DBAccessLayer.IDBAccess iba;
        string cnKey = "";
        public SysLabelDao()
		{
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");
            iba = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");
        }
        public SysLabelDao(string cnKey)
        {
            this.cnKey = cnKey;
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
            iba = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");
        }

        /// <summary>
        ///  测试连接是否成功
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="DatabasePrefix"></param>
        /// <param name="Uid"></param>
        /// <param name="Pwd"></param>
        /// <param name="DataWind"></param>
        /// <returns></returns>
        public bool OpenConnection(string connectionString)
        {            
            try
            {
                ia = DBAccessLayer.DBFactory.GetDBAccess(connectionString);
                return ia.OpenConnection();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                ia.CloseConnection();
            }

        }
        /// <summary>
        ///  测试连接是否成功
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="DatabasePrefix"></param>
        /// <param name="Uid"></param>
        /// <param name="Pwd"></param>
        /// <param name="DataWind"></param>
        /// <returns></returns>
        public bool OpenConnection(string Server, string Database, string Uid, string Pwd, bool Authentication)
        {
            DBAccessLayer.IDBAccess idba = null;
            string strSysConnection = "";
            if (Authentication)
                strSysConnection = string.Format("server={0};database={1};integrated security=SSPI", Server, Database);
            else
                strSysConnection = string.Format("server={0};database={1};uid={2};pwd={3}", Server, Database, Uid, Pwd);

            try
            {
               idba = DBAccessLayer.DBFactory.GetDBAccess(strSysConnection);
                return idba.OpenConnection();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                idba.CloseConnection();
            }

        }

        /// <summary>
        ///  测试连接是否成功
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="DatabasePrefix"></param>
        /// <param name="Uid"></param>
        /// <param name="Pwd"></param>
        /// <param name="DataWind"></param>
        /// <returns></returns>
        public string GetConnection(string Server, string Database, string Uid, string Pwd, bool Authentication)
        {
            string strSysConnection = "";
            if (Authentication)
                strSysConnection = string.Format("server={0};database={1};integrated security=SSPI", Server, Database);
            else
                strSysConnection = string.Format("server={0};database={1};uid={2};pwd={3}", Server, Database, Uid, Pwd);

            try
            {
                ia = DBAccessLayer.DBFactory.GetDBAccess(strSysConnection);
                if (ia.OpenConnection())
                    return strSysConnection;
                else
                    return "";
            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                ia.CloseConnection();
            }

        }

        #region 单多选 查询
        /// <summary>
        /// 传回PageIndex页的资料。
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhere"></param>
        /// <param name="SelectField"></param>
        /// <returns></returns>
        public DataSet SelectTable(string FormID, int PageIndex, string strWhere, int PageSize, string OrderField, out int PageCount, out int RecrodCount)
        {
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(FormID);

            PageCount = 0;
            RecrodCount = 0;

            if (OrderField.Trim().Length == 0)
                OrderField = SysModel.OrderField;

            string PageWhere = "";
            if (SysModel.strWhere.Trim().Length > 0)
                PageWhere = SysModel.strWhere;
            else
                PageWhere = "1=1";
            if (strWhere != "")
                PageWhere += " and " + strWhere;

            SqlParameter[] parameter ={
                            new SqlParameter("@tbname",SqlDbType.NVarChar,100),
                            new SqlParameter("@FieldKey",SqlDbType.NVarChar,100),
                            new SqlParameter("@PageCurrent",SqlDbType.Int,13),
                            new SqlParameter("@PageSize",SqlDbType.Int,13),
                            new SqlParameter("@FieldShow",SqlDbType.NVarChar,1000),
                            new SqlParameter("@FieldOrder",SqlDbType.NVarChar,1000),
                            new SqlParameter("@Where",SqlDbType.NVarChar,4000),
                            new SqlParameter("@PageCount",SqlDbType.Int,13),
                            new SqlParameter("@RecordCount",SqlDbType.Int,13)
                                        };
            parameter[0].Value = SysModel.ViewName;
            parameter[1].Value = SysModel.KeyField;
            parameter[2].Value = PageIndex;
            parameter[3].Value = PageSize;
            parameter[4].Value = "*";
            parameter[5].Value = OrderField;
            parameter[6].Value = PageWhere;
            parameter[7].Direction = ParameterDirection.Output;
            parameter[8].Direction = ParameterDirection.Output;

            DataSet ds = ia.ExecuteDataSetProc("sp_PageView", parameter);
            if (parameter[7].Value.ToString() != "")
                PageCount = (int)parameter[7].Value;
            if (parameter[8].Value.ToString() != "")
                RecrodCount = (int)parameter[8].Value;
            return ds;
        }
        /// <summary>
        /// 传回PageIndex页的资料。
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhere"></param>
        /// <param name="SelectField"></param>
        /// <returns></returns>
        public DataSet SelectTable(int SysID, int PermissionID, int PageIndex, string strWhere, int PageSize, out int PageCount, out int RecrodCount)
        {
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(SysID, PermissionID);

            PageCount = 0;
            RecrodCount = 0;

            string PageWhere = "";
            if (SysModel.strWhere.Trim().Length > 0)
                PageWhere = SysModel.strWhere;
            else
                PageWhere = "1=1";
            if (strWhere != "")
                PageWhere += " and " + strWhere;

            SqlParameter[] parameter ={
                            new SqlParameter("@tbname",SqlDbType.NVarChar,100),
                            new SqlParameter("@FieldKey",SqlDbType.NVarChar,100),
                            new SqlParameter("@PageCurrent",SqlDbType.Int,13),
                            new SqlParameter("@PageSize",SqlDbType.Int,13),
                            new SqlParameter("@FieldShow",SqlDbType.NVarChar,1000),
                            new SqlParameter("@FieldOrder",SqlDbType.NVarChar,1000),
                            new SqlParameter("@Where",SqlDbType.NVarChar,4000),
                            new SqlParameter("@PageCount",SqlDbType.Int,13),
                            new SqlParameter("@RecordCount",SqlDbType.Int,13)
                                        };
            parameter[0].Value = SysModel.ViewName;
            parameter[1].Value = SysModel.KeyField;
            parameter[2].Value = PageIndex;
            parameter[3].Value = PageSize;
            parameter[4].Value = "*";
            parameter[5].Value = SysModel.OrderField;
            parameter[6].Value = PageWhere;
            parameter[7].Direction = ParameterDirection.Output;
            parameter[8].Direction = ParameterDirection.Output;

            DataSet ds = ia.ExecuteDataSetProc("sp_PageView", parameter);
            if (parameter[7].Value.ToString() != "")
                PageCount = (int)parameter[7].Value;
            if (parameter[8].Value.ToString() != "")
                RecrodCount = (int)parameter[8].Value;
            return ds;
        }
        /// <summary>
        /// 传回PageIndex页的资料,自行傳入排序字段。
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhere"></param>
        /// <param name="SelectField"></param>
        /// <returns></returns>
        public DataSet SelectTable(int SysID, int PermissionID, int PageIndex, string strWhere, int PageSize, string OrderField,out int PageCount, out int RecrodCount)
        {
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(SysID, PermissionID);

            PageCount = 0;
            RecrodCount = 0;

            string PageWhere = "";
            if (SysModel.strWhere.Trim().Length > 0)
                PageWhere = SysModel.strWhere;
            else
                PageWhere = "1=1";
            if (strWhere != "")
                PageWhere += " and " + strWhere;

            SqlParameter[] parameter ={
                            new SqlParameter("@tbname",SqlDbType.NVarChar,100),
                            new SqlParameter("@FieldKey",SqlDbType.NVarChar,100),
                            new SqlParameter("@PageCurrent",SqlDbType.Int,13),
                            new SqlParameter("@PageSize",SqlDbType.Int,13),
                            new SqlParameter("@FieldShow",SqlDbType.NVarChar,1000),
                            new SqlParameter("@FieldOrder",SqlDbType.NVarChar,1000),
                            new SqlParameter("@Where",SqlDbType.NVarChar,4000),
                            new SqlParameter("@PageCount",SqlDbType.Int,13),
                            new SqlParameter("@RecordCount",SqlDbType.Int,13)
                                        };
            parameter[0].Value = SysModel.ViewName;
            parameter[1].Value = SysModel.KeyField;
            parameter[2].Value = PageIndex;
            parameter[3].Value = PageSize;
            parameter[4].Value = "*";
            parameter[5].Value = OrderField;
            parameter[6].Value = PageWhere;
            parameter[7].Direction = ParameterDirection.Output;
            parameter[8].Direction = ParameterDirection.Output;

            DataSet ds = ia.ExecuteDataSetProc("sp_PageView", parameter);
            if (parameter[7].Value.ToString() != "")
                PageCount = (int)parameter[7].Value;
            if (parameter[8].Value.ToString() != "")
                RecrodCount = (int)parameter[8].Value;
            return ds;
        }
        /// <summary>
        /// 传回PageIndex页的资料，并对资料中的日期欄位转化为字符串形式。
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhere"></param>
        /// <param name="SelectField"></param>
        /// <returns></returns>
        public DataSet SelectTable(int SysID, int PermissionID, int PageIndex, string strWhere, int PageSize, out int PageCount, out int RecrodCount,string DateFormat)
        {

            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(SysID, PermissionID);

            PageCount = 0;
            RecrodCount = 0;

            string PageWhere = "";
            if (SysModel.strWhere.Trim().Length > 0)
                PageWhere = SysModel.strWhere;
            else
                PageWhere = "1=1";
            if (strWhere != "")
                PageWhere += " and " + strWhere;

            SqlParameter[] parameter ={
                new SqlParameter("@tbname",SqlDbType.NVarChar,100),
                new SqlParameter("@FieldKey",SqlDbType.NVarChar,100),
                new SqlParameter("@PageCurrent",SqlDbType.Int,13),
                new SqlParameter("@PageSize",SqlDbType.Int,13),
                new SqlParameter("@FieldShow",SqlDbType.NVarChar,1000),
                new SqlParameter("@FieldOrder",SqlDbType.NVarChar,1000),
                new SqlParameter("@Where",SqlDbType.NVarChar,4000),
                new SqlParameter("@PageCount",SqlDbType.Int,13),
                new SqlParameter("@RecordCount",SqlDbType.Int,13)
                                        };
            parameter[0].Value = SysModel.ViewName;
            parameter[1].Value = SysModel.KeyField;
            parameter[2].Value = PageIndex;
            parameter[3].Value = PageSize;
            parameter[4].Value = "*";
            parameter[5].Value = SysModel.OrderField;
            parameter[6].Value = PageWhere;
            parameter[7].Direction = ParameterDirection.Output;
            parameter[8].Direction = ParameterDirection.Output;
            
            DataSet ds = ia.ExecuteDataSetProc("sp_PageView", parameter);

            if (parameter[7].Value.ToString() != "")
                PageCount = (int)parameter[7].Value;
            if (parameter[8].Value.ToString() != "")
                RecrodCount = (int)parameter[8].Value;

            DataTable dtNew = new DataTable();
            foreach (DataColumn dr in ds.Tables[0].Columns)
            {
                if (dr.DataType.ToString() != "System.DateTime")
                {
                    dtNew.Columns.Add(dr.ColumnName, dr.DataType);
                }
                else
                {
                    dtNew.Columns.Add(dr.ColumnName, typeof(string));
                }
            }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataRow drNew = dtNew.NewRow();
                for (int i = 0; i < dr.ItemArray.Length; i++)
                {
                    if (dr[i].GetType().ToString() != "System.DateTime")
                    {
                        drNew[i] = dr[i];
                    }
                    else
                    {
                        drNew[i] = Common.GetDateTimeString((DateTime)dr[i], DateFormat, false);
                    }
                }
                dtNew.Rows.Add(drNew);
            }
            ds.Tables.RemoveAt(0);
            ds.Tables.Add(dtNew);
            return ds;
        }
        public static void setDtDateFormat(ref DataSet ds, string strDateFormat)
        {
            DataTable dtNew = new DataTable();
            foreach (DataColumn dr in ds.Tables[0].Columns)
            {
                if (dr.DataType.ToString() != "System.DateTime")
                {
                    dtNew.Columns.Add(dr.ColumnName, dr.DataType);
                }
                else
                {
                    dtNew.Columns.Add(dr.ColumnName, typeof(string));
                }
            }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataRow drNew = dtNew.NewRow();
                for (int i = 0; i < dr.ItemArray.Length; i++)
                {
                    if (dr[i].GetType().ToString() != "System.DateTime")
                    {
                        drNew[i] = dr[i];
                    }
                    else
                    {
                        drNew[i] = Common.GetDateTimeString((DateTime)dr[i], strDateFormat, false);
                    }
                }
                dtNew.Rows.Add(drNew);
            }
            ds.Tables.RemoveAt(0);
            ds.Tables.Add(dtNew);            
        }
        /// <summary>
        /// 多條件查詢欄位,blnSelectAll=true,返回所有栏位，false返回SingleSearch=true的欄位
        /// </true>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <returns></returns>
        public DataSet SearchTable(string FormID,bool blnSelectAll)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from  Sys_FormSearch ");
            strSql.Append("where ");
            strSql.AppendFormat("FormID='{0}' ", FormID);
            if (!blnSelectAll)
                strSql.Append("and SingleSearch=1 ");
            strSql.Append("order by RowID");

            DataSet ds = iba.ExecuteDataSetSql(strSql.ToString());
            return ds;
        }
        /// <summary>
        /// 资料表中，改栏位的所有值(重复只出现一次)。
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public DataSet FieldTable(int SysID, int PermissionID, string FieldName)
        {
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(SysID, PermissionID);

            StringBuilder strSql = new StringBuilder();
            if (SysModel.strWhere.Trim().Length > 0)            
                strSql.AppendFormat("select Distinct({0}) from {1} where {2} order by {0}", FieldName, SysModel.TableName, SysModel.strWhere);            
            else            
                strSql.AppendFormat("select Distinct({0}) from {1} Order by {0}", FieldName, SysModel.TableName);
            
            DataSet ds = ia.ExecuteDataSetProc(strSql.ToString());
            return ds;

        }

        /// <summary>
        /// 獲得欄位長度
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <returns></returns>
        public DataSet GetFieldLength(int SysID, int PermissionID)
        {
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(SysID, PermissionID);

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select name, (case xtype when 239 then length/2 when 231 then length/2  else length end) as Length, " + 
                          "(CASE xtype WHEN 239 THEN 'string' WHEN 231 THEN 'string' WHEN 61 THEN 'datetime' WHEN '108' THEN 'decimal' WHEN 48 THEN 'byte' WHEN 104 THEN 'bool' END) as DataType " + 
                          "from sys.syscolumns WHERE id = OBJECT_ID('{0}')",SysModel.TableName);

            DataSet ds = ia.ExecuteDataSetProc(strSql.ToString());
            return ds;
        }

        #endregion 

        #region 笔数
        /// <summary>
        /// 單[多]選 筆數  
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int RecordCount(int SysID, int PermissionID, string strWhere)
        {
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(SysID, PermissionID);

            string PageWhere = "";
            if (SysModel.strWhere.Trim().Length > 0)
                PageWhere = SysModel.strWhere;
            else
                PageWhere = "1=1";
            if (strWhere != "")
                PageWhere += " and " + strWhere;

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select Count(1) from {0} where {1}",SysModel.TableName,PageWhere);

            int row = (int)ia.ExecuteScalarSql(strSql.ToString());
            return row;
        }
       
        /// <summary>
        /// 當前編號的筆數
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="strWhere"></param>
        /// <param name="CurrentValue"></param>
        /// <returns></returns>
        public int CurrentRecord(int SysID, int PermissionID, string strWhere,string CurrentValue,out ArrayList FieldOrderValue) //單[多]選 筆數  
        {
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(SysID, PermissionID);

            string PageWhere = "";
            if (SysModel.strWhere.Trim().Length > 0)
                PageWhere = SysModel.strWhere;
            else
                PageWhere = "1=1";
            if (strWhere != "")
                PageWhere += " and " + strWhere;

            FieldOrderValue = new ArrayList();
            string FieldOrder = "";
            if (string.IsNullOrEmpty(SysModel.OrderField.Trim()))
                FieldOrder = SysModel.KeyField.Trim();
            else
                FieldOrder = SysModel.OrderField.Trim();

            char[] spliter = new char[1];
            spliter[0] = char.Parse(",");

            string strFieldOrder= FieldOrder.ToLower().Replace("convert(nvarchar(10),", "").Replace(",111)","");
            string strFieldOrderNosc = strFieldOrder.ToLower().Replace("desc", "").Replace("asc", "");
            string[] fieldName = strFieldOrderNosc.Split(spliter);

            if (fieldName.Length > 0)
            {
                for (int i = 0; i < fieldName.Length; i++)
                {
                    FieldOrderValue.Add(fieldName[i]);
                }
            }

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select RowNumber,{0} from (select {1} as IDTwo,{0},ROW_NUMBER () OVER ( Order By {2}) as RowNumber from {3} where {4}) temp Where Temp.IDTwo=N'{5}'", strFieldOrderNosc,SysModel.KeyField,SysModel.TableName,PageWhere,CurrentValue);

            DataTable dtValue = ia.ExecuteDataSetSql(strSql.ToString()).Tables[0];
            int intReturn = 0;
            if (dtValue.Rows.Count > 0)
            {
                for (int i = 0; i < FieldOrderValue.Count; i++)
                {
                    if (dtValue.Columns[FieldOrderValue[i].ToString().Trim()].DataType.Name == "DateTime")
                        FieldOrderValue[i] = Common.GetDateTimeString((DateTime)dtValue.Rows[0][FieldOrderValue[i].ToString().Trim()], "");
                    else
                        FieldOrderValue[i] = dtValue.Rows[0][FieldOrderValue[i].ToString().Trim()].ToString();
                }

                intReturn = int.Parse(dtValue.Rows[0][0].ToString());
            }
            else
            {
                for (int i = 0; i < FieldOrderValue.Count; i++)
                {
                    FieldOrderValue[i] = "";
                }
                intReturn = 0;
            }
            return intReturn;
        }
        
      
        /// <summary>
        /// 返回沒有沒有資料的表單名稱
        /// </summary>
        /// <returns></returns>
        public string GetHasNotRecrodName(int SysID, int PermissionID)
        {
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(SysID, PermissionID);

            string PageWhere = "";
            if (SysModel.strWhere.Trim().Length > 0)
                PageWhere = SysModel.strWhere;
            else
                PageWhere = "1=1";

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select top 1 {0} from {1} where {2}",SysModel.KeyField,SysModel.TableName,PageWhere);

            int row = (int)ia.ExecuteScalarSql(strSql.ToString());
            if (row == 0)
            {
                if (SysModel.BaseName.IndexOf("(") >= 0)
                {
                    return SysModel.BaseName.Substring(0, SysModel.BaseName.IndexOf("("));
                }
                else
                {
                    return SysModel.BaseName;
                }
            }

            else
                return "";

        }
        #endregion 

        #region 地址输入法，常用词库设定
        /// <summary>
        /// 獲取資料;
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetAddress(string TableName, int strNo)
        {
            string strSql = ""; ;
            DataSet ds = new DataSet();
            switch (TableName)
            {
                case "Sys_Address1":
                    strSql = "select * from Sys_Address1 order by SerNo";
                    break;
                case "Sys_Address2":
                    strSql = string.Format("select * from Sys_Address2 where Source={0} order by SerNo",strNo);
                    break;
                case "Sys_Address3":
                    strSql = string.Format("select * from Sys_Address3 where Source={0} order by SerNo",strNo);
                    break;
                case "Sys_Words":
                    strSql = "select * from Sys_Words order by SerNo";
                    break;
                default:
                    break;
            }
            if (strSql.Length > 0)
            {

                ds = iba.ExecuteDataSetSql(strSql);
            }
            return ds;
        }

        /// <summary>
        /// 刪除資料表(TableName)的內容;
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="strWhere"></param>
        public void DelAddress(string TableName, int strNo)
        {
            string strSql = "";

            switch (TableName)
            {
                case "Sys_Address1":
                    strSql = string.Format("delete from Sys_Address1 where SerNo={0}",strNo);
                    iba.ExecuteNonQuerySql(strSql);
                    strSql = string.Format("delete from Sys_Address3 where Source in (select SerNo from Sys_Address2 where Source={0})", strNo);
                    iba.ExecuteNonQuerySql(strSql);
                    strSql = string.Format("delete from Sys_Address2 where Source={0}",strNo);
                    iba.ExecuteNonQuerySql(strSql);
                    break;
                case "Sys_Address2":

                    strSql = string.Format("delete from Sys_Address2 where SerNo={0}",strNo);
                    iba.ExecuteNonQuerySql(strSql);
                    strSql = string.Format("delete from Sys_Address3 where Source={0}",strNo);
                    iba.ExecuteNonQuerySql(strSql);
                    break;
                case "Sys_Address3":
                    strSql = string.Format("delete from Sys_Address3 where SerNo={0}",strNo);
                    iba.ExecuteNonQuerySql(strSql);
                    break;
                case "Sys_Words":
                    strSql = string.Format("delete from Sys_Words where SerNo=={0}",strNo);
                    iba.ExecuteNonQuerySql(strSql);
                    break;
                default:
                    break;
            }
           
        }
        /// <summary>
        /// 插入資料表(tableName);
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="array"></param>
        public void InsertAddress(string TableName, ArrayList array)
        {
            string strSql = "";

            switch(TableName)
            {
                case "Sys_Address1":
                    strSql = string.Format("insert into Sys_Address1 (SerNo,Name) values ({0},'{1}')",array[0],array[1]);
                    break;
                case "Sys_Address2":
                    strSql = string.Format("insert into Sys_Address2 (SerNo,Name,ZipNo,Source) values({0},'{1}','{2}',{3})", array[0], array[1],array[2],array[3]);
                    break;
                case "Sys_Address3":
                    strSql = string.Format("insert into Sys_Address3 (SerNo,Name,source) values ({0},'{1}',{2})", array[0], array[1],array[2]);
                    break;
                case "Sys_Words":
                    strSql = string.Format("insert into Sys_Words (SerNo,Name) values ({0},'{1}')", array[0], array[1]);
                    break;
                default:
                    break;
            }
            if(strSql.Length>0)
                iba.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 獲得地址最大編號
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public int GetAddressID(string TableName)
        {
            string strSql = string.Format("select Max(SerNo) as MaxSerNo from {0}", TableName);

            DataTable dt = iba.ExecuteDataSetSql(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                    return 1;
                else
                    return (int.Parse(dt.Rows[0][0].ToString()) + 1);
            }
            else
                return 1;
        }
        #endregion 
        
        #region 获得单笔资料
        /// <summary>
        /// 首笔，上笔，下笔，末笔资料
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="ListType"></param>
        /// <param name="strKeyValue"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public string GetBaseModel(int SysID, int PermissionID, int ListType, ArrayList strKeyValue, string strWhere)
        {
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(SysID, PermissionID);

            string PageWhere = "";
            if (SysModel.strWhere.Trim().Length > 0)
                PageWhere = SysModel.strWhere;
            else
                PageWhere = "1=1";

            //排列
            string strOrder = "";
            //条件 
            string Where = "";



            string FieldOrder = "";
            if (string.IsNullOrEmpty(SysModel.OrderField.Trim()))
                FieldOrder = SysModel.KeyField.Trim().ToLower();
            else
                FieldOrder = SysModel.OrderField.Trim().ToLower();

            char[] spliter = new char[1];
            spliter[0] = char.Parse(",");

            string strFieldOrder = FieldOrder.ToLower().Replace("convert(nvarchar(10),", "").Replace(",111)", "");
            string[] fieldName = strFieldOrder.ToLower().Split(spliter);
            string[] orderField = new string[2];
            //处理日期转换
            if (fieldName.Length == 2)
            {
                if (fieldName[0].IndexOf("asc") < 0 && fieldName[0].IndexOf("desc") < 0)
                {
                    orderField[0] = "convert(nvarchar(10)," + fieldName[0] + ",111) asc";
                }
                else
                {
                    if (fieldName[0].IndexOf("asc") >= 0)
                        orderField[0] = "convert(nvarchar(10)," + fieldName[0].Replace("asc", "").Trim() + ",111) asc";
                    else
                        orderField[0] = "convert(nvarchar(10)," + fieldName[0].Replace("desc", "").Trim() + ",111) desc";

                }
                if (fieldName[1].IndexOf("asc") < 0 && fieldName[1].IndexOf("desc") < 0)
                {
                    orderField[1] = fieldName[1] + " asc ";
                }
            }
            else
            {
                if (fieldName[0].IndexOf("asc") < 0 && fieldName[0].IndexOf("desc") < 0)
                {
                    orderField[0] = fieldName[0] + " asc ";
                }
            }
            for (int i = 0; i < fieldName.Length; i++)
            {
                fieldName[i] = fieldName[i].Replace("desc", "").Replace("asc", "");
            }

            string[] fieldNameWhere = fieldName;
            string strSql = string.Format("select top 1 {0} from {1}", SysModel.KeyField, SysModel.TableName);

            if (ListType == 0)
            {
                strKeyValue[0] = strKeyValue[0].ToString().Replace("'", "''");
                if (fieldName.Length == 1)
                {
                    Where = fieldName[0].Trim() + "=N'" + strKeyValue[0].ToString() + "'";
                }
                else
                {
                    strKeyValue[1] = strKeyValue[1].ToString().Replace("'", "''");
                    Where = "DATEDIFF(d," + fieldName[0] + ",'" + strKeyValue[0].ToString().Trim() + "')=0 and " + fieldName[1] + "=N'" + strKeyValue[1].ToString() + "'";
                }
                strOrder = FieldOrder;
            }
            if (ListType == 1)
            {
                strOrder = FieldOrder;
                Where = "1=1";
            }
            if (ListType == 2)
            {
                //一个排序栏位
                if (fieldName.Length == 1)
                {
                    strKeyValue[0] = strKeyValue[0].ToString().Replace("'", "''");
                    if (orderField[0].IndexOf("asc") >= 0)
                    {
                        strOrder = orderField[0].Replace("asc", "desc");
                        Where = fieldName[0] + "<N'" + strKeyValue[0].ToString() + "'";
                    }
                    else
                    {
                        strOrder = orderField[0].Replace("desc", "asc");
                        Where = fieldName[0] + ">N'" + strKeyValue[0].ToString() + "'";
                    }
                }
                //两个排序栏位
                else
                {
                    strKeyValue[0] = strKeyValue[0].ToString().Replace("'", "''");
                    strKeyValue[1] = strKeyValue[1].ToString().Replace("'", "''");
                    //日期，单号排列
                    if (orderField[0].IndexOf("asc") >= 0 && orderField[1].IndexOf("asc") >= 0)
                    {
                        strOrder = orderField[0].Replace("asc", "desc,") + orderField[1].Replace("asc", "desc");
                        Where = "DATEDIFF(d," + fieldName[0] + ",'" + strKeyValue[0].ToString().Trim() + "')=0 and " + fieldName[1] + "<N'" + strKeyValue[1].ToString() + "' or DATEDIFF(d," + fieldName[0] + ",'" + strKeyValue[0].ToString() + "')>0";
                    }
                    else if (orderField[0].IndexOf("asc") >= 0 && orderField[1].IndexOf("desc") >= 0)
                    {
                        strOrder = orderField[0].Replace("asc", "desc,") + orderField[1].Replace("desc", "asc");
                        Where = "DATEDIFF(d," + fieldName[0] + ",'" + strKeyValue[0].ToString().Trim() + "')=0 and " + fieldName[1] + ">N'" + strKeyValue[1].ToString() + "' or DATEDIFF(d," + fieldName[0] + ",'" + strKeyValue[0].ToString() + "')>0";
                    }
                    else if (orderField[0].IndexOf("desc") >= 0 && orderField[1].IndexOf("asc") >= 0)
                    {
                        strOrder = orderField[0].Replace("desc", "asc,") + orderField[1].Replace("asc", "desc");
                        Where = "DATEDIFF(d," + fieldName[0] + ",'" + strKeyValue[0].ToString().Trim() + "')=0 and " + fieldName[1] + "<N'" + strKeyValue[1].ToString() + "' or DATEDIFF(d," + fieldName[0] + ",'" + strKeyValue[0].ToString() + "')<0";
                    }
                    else
                    {
                        strOrder = orderField[0].Replace("desc", "asc,") + orderField[1].Replace("desc", "asc");
                        Where = "DATEDIFF(d," + fieldName[0] + ",'" + strKeyValue[0].ToString().Trim() + "')=0 and " + fieldName[1] + ">N'" + strKeyValue[1].ToString() + "' or DATEDIFF(d," + fieldName[0] + ",'" + strKeyValue[0].ToString() + "')<0";
                    }
                }
            }
            //下一笔
            if (ListType == 3)
            {
                //一个排序栏位
                strKeyValue[0] = strKeyValue[0].ToString().Replace("'", "''");
                if (fieldName.Length == 1)
                {
                    if (orderField[0].IndexOf("asc") >= 0)
                    {
                        strOrder = orderField[0];
                        Where = fieldName[0] + ">N'" + strKeyValue[0].ToString() + "'";
                    }
                    else
                    {
                        strOrder = orderField[0];
                        Where = fieldName[0] + "<N'" + strKeyValue[0].ToString() + "'";
                    }
                }
                //两个排序栏位
                else
                {
                    strKeyValue[0] = strKeyValue[0].ToString().Replace("'", "''");
                    strKeyValue[1] = strKeyValue[1].ToString().Replace("'", "''");
                    //日期，单号排列
                    if (orderField[0].IndexOf("asc") >= 0 && orderField[1].IndexOf("asc") >= 0)
                    {
                        strOrder = orderField[0].Replace("asc", "asc,") + orderField[1];
                        Where = "DATEDIFF(d," + fieldName[0] + ",'" + strKeyValue[0].ToString().Trim() + "')=0 and " + fieldName[1] + ">N'" + strKeyValue[1].ToString() + "' or DATEDIFF(d,'" + strKeyValue[0].ToString() + "'," + fieldName[0] + ")>0";
                    }
                    else if (orderField[0].IndexOf("asc") >= 0 && orderField[1].IndexOf("desc") >= 0)
                    {
                        strOrder = orderField[0].Replace("asc", "asc,") + orderField[1];
                        Where = "DATEDIFF(d," + fieldName[0] + ",'" + strKeyValue[0].ToString().Trim() + "')=0 and " + fieldName[1] + "<N'" + strKeyValue[1].ToString() + "'  or DATEDIFF(d,'" + strKeyValue[0].ToString() + "'," + fieldName[0] + ")>0";
                    }
                    else if (orderField[0].IndexOf("desc") >= 0 && orderField[1].IndexOf("asc") >= 0)
                    {
                        strOrder = orderField[0].Replace("desc", "desc,") + orderField[1];
                        Where = "DATEDIFF(d," + fieldName[0] + ",'" + strKeyValue[0].ToString().Trim() + "')=0 and " + fieldName[1] + ">N'" + strKeyValue[1].ToString() + "'  or DATEDIFF(d,'" + strKeyValue[0].ToString() + "'," + fieldName[0] + ")<0";
                    }
                    else
                    {
                        strOrder = orderField[0].Replace("desc", "desc,") + orderField[1];
                        Where = "DATEDIFF(d," + fieldName[0] + ",'" + strKeyValue[0].ToString().Trim() + "')=0 and " + fieldName[1] + "<N'" + strKeyValue[1].ToString() + "'  or DATEDIFF(d,'" + strKeyValue[0].ToString() + "'," + fieldName[0] + ")<0";
                    }
                }

            }
            if (ListType == 4)
            {
                //一个排序栏位
                if (fieldName.Length == 1)
                {
                    if (orderField[0].IndexOf("asc") >= 0)
                        strOrder = orderField[0].Replace("asc", "desc");
                    else
                        strOrder = orderField[0].Replace("desc", "asc");
                }
                //两个排序栏位
                else
                {
                    //日期，单号排列
                    if (orderField[0].IndexOf("asc") >= 0 && orderField[1].IndexOf("asc") >= 0)
                        strOrder = orderField[0].Replace("asc", "desc,") + orderField[1].Replace("asc", "desc");

                    else if (orderField[0].IndexOf("asc") >= 0 && orderField[1].IndexOf("desc") >= 0)
                        strOrder = orderField[0].Replace("asc", "desc,") + orderField[1].Replace("desc", "asc");
                    else if (orderField[0].IndexOf("desc") >= 0 && orderField[1].IndexOf("asc") >= 0)
                        strOrder = orderField[0].Replace("desc", "asc,") + orderField[1].Replace("asc", "desc");

                    else
                        strOrder = orderField[0].Replace("desc", "asc,") + orderField[1].Replace("desc", "asc");

                }
                Where = "1=1";
            }
            Where = PageWhere + " and (" + Where + ")";
            strSql += " where " + Where + " order by " + strOrder;


            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }

        #endregion

        #region 当前值被用情况
        /// <summary>
        /// 判斷當前值是否被用
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="CurrentValue"></param>
        /// <returns></returns>
        public bool GetCurrentIsUsed(string FormID, string CurrentValue) 
        {
            string strSql = GetIsUsedString(false, FormID, CurrentValue);

            if (!string.IsNullOrEmpty(strSql))
            {
                DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
                return (dt.Rows.Count > 0);
            }
            else
                return false;
        }
        /// <summary>
        /// 當前值被使用記錄
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="CurrentValue"></param>
        /// <returns></returns>
        public DataSet GetCurrentUsed(string FormID, string CurrentValue)
        {
            string strSql = GetIsUsedString(true, FormID, CurrentValue);
            if (strSql.Length > 0)
            {
                DataSet ds = ia.ExecuteDataSetSql(strSql);
                return ds;
            }
            else
                return null;
        }        
        #region 被用查詢語句

        private string GetIsUsedString(bool blnReturn, string FormID, string CurrentValue)
        {
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(FormID);
            string[] sViewTableName = new string[2];

            sViewTableName = SysModel.IsUsedView.Split(',');

            string strSql = "";
            if (sViewTableName[0] != null && sViewTableName[0].Length > 0)
            {
                if (blnReturn)
                    strSql = string.Format("select * from {0} where ID = '{1}'",sViewTableName[0],CurrentValue);
                else
                    strSql = string.Format("select top 1 ID from {0} where ID = '{1}'", sViewTableName[0], CurrentValue);
            }

            return strSql;
        }
        #endregion
        
        #endregion 

        #region Sys_ColFozen 凍結
        /// <summary>
        /// 保存凍結
        /// </summary>
        public void SaveColFozen(string User, string frmCaption, string DgvName, int Fozen)
        {       
            try
            {
                DeleteColFozen(User, frmCaption, DgvName);
                char[] spilt = { ' ' };
                string strSql = string.Format("INSERT INTO Sys_ColFozen(UserName ,frmCaption ,DgvName ,Fozen) values('{0}','{1}','{2}',{3})", User, frmCaption.Trim().Split(spilt)[0], DgvName, Fozen);
    

                ia.ExecuteNonQuerySql(strSql);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 刪除凍結
        /// </summary>
        public void DeleteColFozen(string User, string frmCaption, string DgvName)
        {
            char[] spilt = { ' ' };
            string strSql = string.Format("Delete Sys_ColFozen where UserName='{0}' and frmCaption='{1}' and DgvName='{2}'", User, frmCaption.Trim().Split(spilt)[0], DgvName);


            ia.ExecuteNonQuerySql(strSql);
        }

        /// <summary>
        /// 取得凍結
        /// </summary>
        public DataSet GetColFozen(string User, string frmCaption)
        {
            try
            {
                char[] spilt ={ ' ' };
                string strSql = string.Format("select DgvName, Fozen from Sys_ColFozen where UserName='{0}' and frmCaption='{1}'", User, frmCaption.Trim().Split(spilt)[0]);
    

                DataSet ds = ia.ExecuteDataSetSql(strSql);
                return ds;
            }
            catch { }
            return null;
        }
        #endregion


        ///
        /// <summary>
        /// 多项查询--- 插入 欄
        /// </summary>
        public void InsertResults(int SysID, int PermissionID, string Flag, string strKey)
        {
            try
            {
                string strSql = string.Format("INSERT INTO Sys_MulSelValue(sysid,Permissionid,Flag, KeyValue,HostName) values({0},{1},'{2}','{3}',host_name())", SysID, PermissionID, Flag, strKey);
    

                ia.ExecuteNonQuerySql(strSql);
            }
            catch 
            {
            }
        }
        /// <summary>
        /// 刪除多選記錄
        /// </summary>
        public void DeleteAllResults(int SysID, int PermissionID, string Flag)
        {
            try
            {
                string strSql = string.Format("delete from dbo.Sys_MulSelValue where HostName =host_name() and Flag='{0}' and SysID={1} and PermissionID={2}", Flag,SysID, PermissionID);
    

                ia.ExecuteNonQuerySql(strSql);
            }
            catch { }
        }

        public void AddParamentSub(string CodeName,string strUser)
        {
            string strSql = string.Format("select max(RowID) from Sys_ParameterSub where CodeName='{0}' and flag={1}",CodeName,strUser);
            string Count = "0";

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            if (dt.Rows.Count > 0)
                Count = dt.Rows[0][0].ToString();
            if (Count.Trim() == "")
                Count = "0";
            
            switch (strUser.Trim())
            {
                case "0":
                    strSql = "";
                    break;
                case "1":
                    strSql = string.Format("insert into Sys_ParameterSub(Flag,CodeName,RowID,PersonID,KValue) " +
                             "select {0},'{1}',  ROW_NUMBER () OVER (Order By DepartmentID) + {2} as RowNumber,DepartmentID," +
                             "case when len(DepartmentID)>4 then SUBSTRING(DepartmentID,0,4) else DepartmentID end " +
                             "from Com_Department where DepartmentID not in (select PersonID from Sys_ParameterSub " +
                             "where Flag={0} and CodeName='{1}')",strUser,CodeName,Count);
                    break;
                case "2":
                    strSql = string.Format("insert into Sys_ParameterSub(Flag,CodeName,RowID,PersonID,KValue) " +
                             "select {0},'{1}',  ROW_NUMBER () OVER (Order By PersonID) + {2} as RowNumber,PersonID," +
                             "case when len(PersonID)>4 then SUBSTRING(PersonID,0,4) else PersonID end " +
                             "from Com_Person where PersonID not in (select PersonID from Sys_ParameterSub " +
                             "where Flag={0} and CodeName='{1}')",strUser,CodeName,Count);
                    break;
                case "3":
                    strSql = string.Format("insert into Sys_ParameterSub(Flag,CodeName,RowID,PersonID,KValue) " +
                             "select {0},'{1}',  ROW_NUMBER () OVER (Order By ProjectID) + {2} as RowNumber,ProjectID," +
                             "case when len(ProjectID)>4 then SUBSTRING(ProjectID,0,4) else ProjectID end " +
                             "from Com_Project where ProjectID not in (select PersonID from Sys_ParameterSub " +
                             "where Flag={0} and CodeName='{1}')",strUser,CodeName,Count);
                    break;
                default:
                    strSql = "";
                    break;
            }
            if (strSql.Length>0)
                ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 根據sysid,permissionid，获得欄位，组合成语句，并返回所查询的DataSet
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <returns></returns>
        public DataSet GetSearchSelectSQL(string FormID, int PageIndex, string strWhere, int PageSize, out int PageCount, out int RecrodCount)
        {
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(FormID);

            PageCount = 0;
            RecrodCount = 0;

            DataTable dt = SearchTable(FormID, true).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string strSql = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strTmp = "";

                    strTmp = dt.Rows[i]["FieldName"].ToString().Trim();
                    strTmp += " as " + dt.Rows[i]["FieldCName"].ToString();

                    if (i == 0)
                        strSql = strTmp;
                    else
                        strSql += "," + strTmp;
                }
                string PageWhere = SysModel.strWhere == "" ? "1=1" : SysModel.strWhere;

                if (!string.IsNullOrEmpty(strWhere))
                    PageWhere += " and " + strWhere;

                SqlParameter[] parameter ={
                                    new SqlParameter("@tbname",SqlDbType.NVarChar,100),
                                    new SqlParameter("@FieldKey",SqlDbType.NVarChar,100),
                                    new SqlParameter("@PageCurrent",SqlDbType.Int,13),
                                    new SqlParameter("@PageSize",SqlDbType.Int,13),
                                    new SqlParameter("@FieldShow",SqlDbType.NVarChar,4000),
                                    new SqlParameter("@FieldOrder",SqlDbType.NVarChar,1000),
                                    new SqlParameter("@Where",SqlDbType.NVarChar,4000),
                                    new SqlParameter("@PageCount",SqlDbType.Int,13),
                                    new SqlParameter("@RecordCount",SqlDbType.Int,13)
                                        };
                parameter[0].Value = SysModel.TableName;
                parameter[1].Value = SysModel.KeyField;
                parameter[2].Value = PageIndex;
                parameter[3].Value = PageSize;
                parameter[4].Value = strSql;
                parameter[5].Value = SysModel.OrderField;
                parameter[6].Value = PageWhere;
                parameter[7].Direction = ParameterDirection.Output;
                parameter[8].Direction = ParameterDirection.Output;


                DataSet ds = ia.ExecuteDataSetProc("sp_PageView", parameter);

                if (parameter[7].Value.ToString() != "")
                    PageCount = (int)parameter[7].Value;
                if (parameter[8].Value.ToString() != "")
                    RecrodCount = (int)parameter[8].Value;

                DataTable dtNew = new DataTable();
                foreach (DataColumn dr in ds.Tables[0].Columns)
                {
                    if (dr.DataType.ToString() != "System.DateTime")
                    {
                        dtNew.Columns.Add(dr.ColumnName, dr.DataType);
                    }
                    else
                    {
                        dtNew.Columns.Add(dr.ColumnName, typeof(string));
                    }
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow drNew = dtNew.NewRow();
                    for (int i = 0; i < dr.ItemArray.Length; i++)
                    {
                        if (dr[i].GetType().ToString() != "System.DateTime")
                        {
                            drNew[i] = dr[i];
                        }
                        else
                        {
                            drNew[i] = Common.GetDateTimeString((DateTime)dr[i], "yyyy/MM/dd", false);
                        }
                    }
                    dtNew.Rows.Add(drNew);
                }
                ds.Tables.RemoveAt(0);
                ds.Tables.Add(dtNew);
                return ds;
            }

            return null;
        }
        /// <summary>
        /// 根據sysid,permissionid，获得欄位，组合成语句，并返回所查询的DataSet
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <returns></returns>
        public DataSet GetSearchSelectSQL(string FormID, int PageIndex, string strWhere, int PageSize, out int PageCount, out int RecrodCount, out string SelectField)
        {
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(FormID);

            SelectField = "";
            PageCount = 0;
            RecrodCount = 0;


            DataTable dt = SearchTable(FormID, true).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string strSql = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strTmp = "";

                    strTmp = dt.Rows[i]["FieldName"].ToString().Trim();
                    strTmp += " as " + dt.Rows[i]["FieldCName"].ToString();

                    if (i == 0)
                    {
                        strSql = dt.Rows[i]["FieldName"].ToString() + " as ID," + strTmp;
                        SelectField = dt.Rows[i]["FieldName"].ToString();
                    }
                    else
                    {
                        strSql += "," + strTmp;
                        SelectField += "@|$" + dt.Rows[i]["FieldName"].ToString();
                    }
                }
                string PageWhere = SysModel.strWhere == "" ? "1=1" : SysModel.strWhere;

                if (!string.IsNullOrEmpty(strWhere))
                    PageWhere += " and " + strWhere;

                SqlParameter[] parameter ={
                                    new SqlParameter("@tbname",SqlDbType.NVarChar,100),
                                    new SqlParameter("@FieldKey",SqlDbType.NVarChar,100),
                                    new SqlParameter("@PageCurrent",SqlDbType.Int,13),
                                    new SqlParameter("@PageSize",SqlDbType.Int,13),
                                    new SqlParameter("@FieldShow",SqlDbType.NVarChar,4000),
                                    new SqlParameter("@FieldOrder",SqlDbType.NVarChar,1000),
                                    new SqlParameter("@Where",SqlDbType.NVarChar,4000),
                                    new SqlParameter("@PageCount",SqlDbType.Int,13),
                                    new SqlParameter("@RecordCount",SqlDbType.Int,13)
                                        };
                parameter[0].Value = SysModel.TableName;
                parameter[1].Value = SysModel.KeyField;
                parameter[2].Value = PageIndex;
                parameter[3].Value = PageSize;
                parameter[4].Value = strSql;
                parameter[5].Value = SysModel.OrderField;
                parameter[6].Value = PageWhere;
                parameter[7].Direction = ParameterDirection.Output;
                parameter[8].Direction = ParameterDirection.Output;

    
                DataSet ds = ia.ExecuteDataSetProc("sp_PageView", parameter);

                if (parameter[7].Value.ToString() != "")
                    PageCount = (int)parameter[7].Value;
                if (parameter[8].Value.ToString() != "")
                    RecrodCount = (int)parameter[8].Value;

                DataTable dtNew = new DataTable();
                foreach (DataColumn dr in ds.Tables[0].Columns)
                {
                    if (dr.DataType.ToString() != "System.DateTime")
                    {
                        dtNew.Columns.Add(dr.ColumnName, dr.DataType);
                    }
                    else
                    {
                        dtNew.Columns.Add(dr.ColumnName, typeof(string));
                    }
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow drNew = dtNew.NewRow();
                    for (int i = 0; i < dr.ItemArray.Length; i++)
                    {
                        if (dr[i].GetType().ToString() != "System.DateTime")
                        {
                            drNew[i] = dr[i];
                        }
                        else
                        {
                            drNew[i] = Common.GetDateTimeString((DateTime)dr[i], "yyyy/MM/dd", false);
                        }
                    }
                    dtNew.Rows.Add(drNew);
                }
                ds.Tables.RemoveAt(0);
                ds.Tables.Add(dtNew);
                return ds;
            }

            return null;
        }

        /// <summary>
        /// 設定當前 SysID PermissionID
        /// </summary>
        /// <param name="strFormclass">Form.tostring()</param>
        /// <param name="SysID">回寫</param>
        /// <param name="PermissionID">回寫</param>
        public bool SetCurrentSysIDPermissionID(string strFormclass, ref int SysID, ref int PermissionID)
        {
            //初始
            SysID = 0; PermissionID = -1;
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(strFormclass);
            if (SysModel.FormID.Trim().Length > 0)
            {
                SysID = SysModel.SysID;
                PermissionID = SysModel.PermissionID;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 單據可否被編輯
        /// 返回
        /// 0：可做任何操作
        /// 1：已複核
        /// 2：被用
        /// </summary>
        public int GetBillCanBeEdit(string FormID, string IDValue)
        {
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(FormID);

            //check
            string strSql = string.Format("select top 1 * from {0} where {1} = '{2}' and {3}", SysModel.TableName, SysModel.KeyField, IDValue, SysModel.strWhere);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            if (dt.Rows.Count > 0 && dt.Columns.IndexOf("CheckUserName") != -1 && dt.Rows[0]["CheckUserName"].ToString().Length != 0)
                return 1;
            //isused
            if (SysModel.IsUsedView.Trim().Length > 0)
            {
                strSql = string.Format("select top 1 ID from {0} where ID='{1}'", SysModel.IsUsedView, IDValue);

                dt = ia.ExecuteDataSetSql(strSql).Tables[0];
                if (dt.Rows.Count > 0)
                    return 2;
            }

            return 0;
        }
        /// <summary>
        /// 單據可否被編輯
        /// 返回
        /// 0：可做任何操作
        /// 1：已複核
        /// 2：被用
        /// </summary>
        public int GetBillCanBeEdit(string FormID, string IDValue,string filter)
        {
            Sys.TreeListDao dao = new Sys.TreeListDao();
            Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(FormID);

            //check
            string strSql = string.Format("select top 1 * from {0} where {1} = '{2}' and {3} and {4}", SysModel.TableName, SysModel.KeyField, IDValue, SysModel.strWhere,filter);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            if (dt.Rows.Count > 0 && dt.Columns.IndexOf("CheckUserName") != -1 && dt.Rows[0]["CheckUserName"].ToString().Length != 0)
                return 1;
            //isused
            if (SysModel.IsUsedView.Trim().Length > 0)
            {
                strSql = string.Format("select top 1 ID from {0} where ID='{1}' and {2}", SysModel.IsUsedView, IDValue,filter);

                dt = ia.ExecuteDataSetSql(strSql).Tables[0];
                if (dt.Rows.Count > 0)
                    return 2;
            }

            return 0;
        }
        /// <summary>
        /// 單據可否被編輯
        /// 返回
        /// 0：可做任何操作
        /// 1：單據時間不在交易時間內
        /// 2：單據時間＜＝關帳日期
        /// 3：已複核
        /// 4：被用
        /// </summary>
        public int GetBillCanBeEdit(string FormID, string IDValue, object date, object beginDate, object enddate, object closedate)
        {
            try
            {
                if (date != null)
                {
                    DateTime _date = (DateTime)date, _begindate, _enddate, _closedate;

                    if (beginDate == null) 
                        _begindate = DateTime.Parse("0001/01/01");
                    else
                        _begindate = (DateTime)beginDate;

                    if (enddate == null) 
                        _enddate = DateTime.Parse("0001/01/01");
                    else
                        _enddate = (DateTime)enddate;
                    if (closedate == null)
                        _closedate = DateTime.Parse("0001/01/01");
                    else
                        _closedate = (DateTime)closedate;
                    //交易區間
                    if (!(_date >= _begindate && _date <= _enddate))
                        return (int)10;
                    //closedate
                    if (_closedate >= _date)
                        return (int)20;
                }

                Sys.TreeListDao dao = new Sys.TreeListDao();
                Js.Model.Sys.TreeListInfo SysModel = dao.GetModel(FormID);

    
                //check
                string strSql = string.Format("select top 1 * from {0} where {1} = '{2}' and {3}", SysModel.TableName, SysModel.KeyField, IDValue, SysModel.strWhere);
                

                DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
                if (dt.Rows.Count > 0 && dt.Columns.IndexOf("CheckUserName") != -1 && dt.Rows[0]["CheckUserName"].ToString().Length != 0)
                    return (int)30;
                //isused
                if (SysModel.IsUsedView.Trim().Length > 0)
                {
                    strSql = string.Format("select top 1 ID from {0} where ID='{1}'", SysModel.IsUsedView, IDValue);

                    dt = ia.ExecuteDataSetSql(strSql).Tables[0];
                    if (dt.Rows.Count > 0)
                        return (int)40;
                }
            }
            catch
            {
            }
            return (int)0;
        }
        /// <summary>
        /// 開立資料庫
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="strDataBase"></param>
        /// <param name="strPath"></param>
        public bool CreateDataBase(string IP, string strDataBase, string strPath)
        {
            string strSysConnection = string.Format("server={0};database={1};integrated security=SSPI", IP, "master");
            
            string strCreateDataBase = "CREATE DATABASE "+strDataBase+
                            " ON "+
                            "( NAME = " + strDataBase + "_dat, " +
                                "FILENAME = 'F:\\Data\\"+strDataBase+".mdf', "+
                               " SIZE = 10, "+
                                "MAXSIZE = 50, "+
                                "FILEGROWTH = 5 ) "+
                            "LOG ON "+
                            "( NAME =  " + strDataBase + "_log, " +
                             "   FILENAME = 'F:\\Data\\" + strDataBase + ".ldf', " +
                              "  SIZE = 5MB, "+
                               " MAXSIZE = 25MB, "+
                                "FILEGROWTH = 5MB )";

            ia = DBAccessLayer.DBFactory.GetDBAccess(strSysConnection);
            ia.ExecuteNonQuerySql(strCreateDataBase);
            if (File.Exists(strPath))
            {
                string strExcuteSQL =File.ReadAllText(strPath, Encoding.Default);
                string[] strSQL = strExcuteSQL.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strSQL.Length; i++)
                {
                    string str = " use " + strDataBase + " " + strSQL[i];
                    ia.ExecuteNonQuerySql(str);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteDataBase(string IP, string strDataBase)
        {
           string strSysConnection = string.Format("server={0};database={1};integrated security=SSPI", IP, "master");
           string strSql = "ALTER DATABASE " +strDataBase + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE;GO drop database " + strDataBase;

           DBAccessLayer.IDBAccess idbNew = DBAccessLayer.DBFactory.GetDBAccess(strSysConnection);
            idbNew.ExecuteNonQuerySql(strSql);
        }

        /// <summary>
        /// 返回所裱中的所有起始編號及結束編號。
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTable(string Flag)
        {
            string strSQL = "select GroupStartNo,GroupEndNo from Com_LabelNoDb where Single=0 and Flag=" + Flag + " order by GroupStartNo";
            DataTable dt = ia.ExecuteDataSetSql(strSQL).Tables[0];
            return dt;
        }

    }
}


