using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Js.DAO
{
    public class BaseDao
    {
        DBAccessLayer.IDBAccess ia;
        Js.Model.Sys.TreeListInfo SysModel;
        TableFieldInfo tf;
        string cnKey;

        public BaseDao()
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess();
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public BaseDao(string FormID)
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
        public BaseDao(string FormID, string cnKey)
        {
            this.cnKey = cnKey;
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
            string strSql = string.Format("select * from Sys_TreeList Where FormID = '{0}'", FormID);
            Sys.TreeListDao dao = new Sys.TreeListDao();
            SysModel = dao.GetModel(FormID);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public BaseDao(int SId, int PId, string cnKey)
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
            string strSql = string.Format("select * from Sys_TreeList Where SysID = {0} and PermissionID={1}", SId, PId);
            Sys.TreeListDao dao = new Sys.TreeListDao();
            SysModel = dao.GetModel(SId, PId);
        }
        /// <summary>
        /// 判斷記錄是否存在
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual bool Exists(string ID)
        {
            string strSql = string.Format("select count(1) from {0} where {1} and {2}='{3}'", SysModel.TableName, SysModel.strWhere, SysModel.KeyField, ID);

            int row = (int)ia.ExecuteScalarSql(strSql);
            if (row > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 判斷記錄是否存在,可自定義查詢條件
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual bool Exists(string ID, string filter)
        {
            filter += " and " + SysModel.strWhere;
            string strSql = string.Format("select count(1) from {0} where {1} and {2}='{3}'", SysModel.TableName, filter, SysModel.KeyField, ID);

            int row = (int)ia.ExecuteScalarSql(strSql);
            if (row > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 刪除記錄
        /// </summary>
        /// <param name="ID"></param>
    
        public virtual void Delete(string ID)
        {
            string strSql = string.Format("Delete {0} where {1} and {2}='{3}'", SysModel.TableName, SysModel.strWhere, SysModel.KeyField, ID);
            ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 刪除記錄
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="filter"></param>
        public virtual void Delete(string ID, string filter)
        {
            filter += " and " + SysModel.strWhere;
            string strSql = string.Format("Delete {0} where {1} and {2} and {3}='{4}'", SysModel.TableName, SysModel.strWhere,filter, SysModel.KeyField, ID);
            ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 新增記錄
        /// </summary>
        /// <param name="dr"></param>
        public virtual void Add(DataRow dr)
        {
            tf = new TableFieldInfo(SysModel.TableName, this.cnKey);
            DataTable dtCol = tf.dtFields;

            StringBuilder strInsert = new StringBuilder();
            strInsert.Append("insert into " + SysModel.TableName + "(");
            string strFiledName1 = "";
            string strFiledName2 = "";

            SqlParameter[] parameters = new SqlParameter[dtCol.Rows.Count];

            for (int i = 0; i < dtCol.Rows.Count; i++)
            {
                parameters[i] = new SqlParameter("@" + dtCol.Rows[i]["Name"].ToString(), tf.GetColType(dtCol.Rows[i]["Name"].ToString()), tf.GetTypeLength(dtCol.Rows[i]["Name"].ToString()));
                parameters[i].Value = dr[dtCol.Rows[i]["Name"].ToString()];
                if (dtCol.Rows[i]["colstat"].ToString() != "1")
                {
                    if (strFiledName1.Length == 0)
                    {
                        strFiledName1 = dtCol.Rows[i]["Name"].ToString();
                        strFiledName2 = "@" + dtCol.Rows[i]["Name"].ToString();
                    }
                    else
                    {
                        strFiledName1 += "," + dtCol.Rows[i]["Name"].ToString();
                        strFiledName2 += ",@" + dtCol.Rows[i]["Name"].ToString();
                    }
                }
            }

            strInsert.Append(strFiledName1);
            strInsert.Append(") values( ");
            strInsert.Append(strFiledName2);
            strInsert.Append(") ");

            ia.ExecuteNonQuerySql(strInsert.ToString(), parameters);
        }
        /// <summary>
        /// 更新記錄
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="oldID"></param>
        public virtual void Update(DataRow dr, string oldID)
        {
            tf = new TableFieldInfo(SysModel.TableName, this.cnKey);
            DataTable dtCol = tf.dtFields;

            StringBuilder strInsert = new StringBuilder();
            strInsert.Append("update " + SysModel.TableName + " set ");
            string strFiledName1 = "";

            SqlParameter[] parameters = new SqlParameter[dtCol.Rows.Count + 1];

            for (int i = 0; i < dtCol.Rows.Count; i++)
            {
                parameters[i] = new SqlParameter("@" + dtCol.Rows[i]["Name"].ToString(), tf.GetColType(dtCol.Rows[i]["Name"].ToString()), tf.GetTypeLength(dtCol.Rows[i]["Name"].ToString()));
                parameters[i].Value = dr[dtCol.Rows[i]["Name"].ToString()];
                if (dtCol.Rows[i]["colstat"].ToString() != "1")
                {
                    if (strFiledName1.Length == 0)
                        strFiledName1 = dtCol.Rows[i]["Name"].ToString() + " = @" + dtCol.Rows[i]["Name"].ToString();
                    else
                        strFiledName1 += "," + dtCol.Rows[i]["Name"].ToString() + " = @" + dtCol.Rows[i]["Name"].ToString();
                }
            }

            strInsert.Append(strFiledName1);
            strInsert.Append(" where " + SysModel.strWhere + " and " + SysModel.KeyField + "=@Old" + SysModel.KeyField + "");

            parameters[dtCol.Rows.Count] = new SqlParameter("@Old" + SysModel.KeyField, SqlDbType.NVarChar);
            parameters[dtCol.Rows.Count].Value = oldID;

            ia.ExecuteNonQuerySql(strInsert.ToString(), parameters);
        }
        /// <summary>
        /// 更新資料，可加入自定義查詢條件
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="oldID"></param>
        /// <param name="filter"></param>
        public virtual void Update(DataRow dr, string oldID, string filter)
        {
            tf = new TableFieldInfo(SysModel.TableName, this.cnKey);
            DataTable dtCol = tf.dtFields;

            StringBuilder strInsert = new StringBuilder();
            strInsert.Append("update " + SysModel.TableName + " set ");
            string strFiledName1 = "";

            SqlParameter[] parameters = new SqlParameter[dtCol.Rows.Count + 1];

            for (int i = 0; i < dtCol.Rows.Count; i++)
            {
                parameters[i] = new SqlParameter("@" + dtCol.Rows[i]["Name"].ToString(), tf.GetColType(dtCol.Rows[i]["Name"].ToString()), tf.GetTypeLength(dtCol.Rows[i]["Name"].ToString()));
                parameters[i].Value = dr[dtCol.Rows[i]["Name"].ToString()];
                if (dtCol.Rows[i]["colstat"].ToString() != "1")
                {
                    if (strFiledName1.Length == 0)
                        strFiledName1 = dtCol.Rows[i]["Name"].ToString() + " = @" + dtCol.Rows[i]["Name"].ToString();
                    else
                        strFiledName1 += "," + dtCol.Rows[i]["Name"].ToString() + " = @" + dtCol.Rows[i]["Name"].ToString();
                }
            }

            strInsert.Append(strFiledName1);
            strInsert.Append(" where " + filter + " and " + SysModel.KeyField + "=@Old" + SysModel.KeyField + "");

            parameters[dtCol.Rows.Count] = new SqlParameter("@Old" + SysModel.KeyField, SqlDbType.NVarChar);
            parameters[dtCol.Rows.Count].Value = oldID;

            ia.ExecuteNonQuerySql(strInsert.ToString(), parameters);
        }
        /// <summary>
        /// 獲取記錄，用原表名
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual DataTable GetRecord(string filter)
        {
            if (this.cnKey == null)
                tf = new TableFieldInfo(SysModel.TableName);
            else
                tf = new TableFieldInfo(SysModel.TableName, this.cnKey);
            string strSql = string.Format("select * from {0} where {1} and {2}", SysModel.TableName, SysModel.strWhere, filter);
            if (SysModel.OrderField.Length > 0)
                strSql += " Order by " + SysModel.OrderField;
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    switch (tf.GetColType(dt.Columns[i].ColumnName))
                    {
                        case SqlDbType.TinyInt:
                            dr[i] = byte.Parse("0"); break;
                        case SqlDbType.Int:
                            dr[i] = int.Parse("0"); break;
                        case SqlDbType.Bit:
                            dr[i] = false; break;
                        case SqlDbType.Decimal:
                            dr[i] = decimal.Parse("0"); break;
                        case SqlDbType.DateTime:
                            //dr[i] = DateTime.Parse("0001/01/01");
                            break;
                        case SqlDbType.NVarChar:
                            dr[i] = ""; break;
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
        /// <summary>
        /// 獲取記錄，用原表名,加排序
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual DataTable GetOrderByRecord(string filter,string orderby)
        {
            if (this.cnKey == null)
                tf = new TableFieldInfo(SysModel.TableName);
            else
                tf = new TableFieldInfo(SysModel.TableName, this.cnKey);
            string strSql = string.Format("select * from {0} where {1} and {2} and RecordID in (SELECT MAX(RecordID) FROM {0} GROUP BY UseUnit, FileType, FileName)", SysModel.TableName, SysModel.strWhere, filter);
            if (orderby == "")
                strSql += string.Format(" Order by {0}", SysModel.OrderField);
            else
                strSql += string.Format(" Order by {0}", orderby);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    switch (tf.GetColType(dt.Columns[i].ColumnName))
                    {
                        case SqlDbType.TinyInt:
                            dr[i] = byte.Parse("0"); break;
                        case SqlDbType.Int:
                            dr[i] = int.Parse("0"); break;
                        case SqlDbType.Bit:
                            dr[i] = false; break;
                        case SqlDbType.Decimal:
                            dr[i] = decimal.Parse("0"); break;
                        case SqlDbType.DateTime:
                            //dr[i] = DateTime.Parse("0001/01/01");
                            break;
                        case SqlDbType.NVarChar:
                            dr[i] = ""; break;
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
        /// <summary>
        /// 獲取記錄，用原表名
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual DataTable GetRecord(string ID, string fieldName, string filter)
        {
            if (this.cnKey == null)
                tf = new TableFieldInfo(SysModel.TableName);
            else
                tf = new TableFieldInfo(SysModel.TableName, this.cnKey);
            string strSql = string.Format("select {0} from {1} where {2} and {3}='{4}'", fieldName, SysModel.ViewName, SysModel.strWhere, SysModel.KeyField, ID);
            if (filter.Trim().Length > 0)
                strSql += string.Format(" and {0}", filter);
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    switch (tf.GetColType(dt.Columns[i].ColumnName))
                    {
                        case SqlDbType.TinyInt:
                            dr[i] = byte.Parse("0"); break;
                        case SqlDbType.Int:
                            dr[i] = int.Parse("0"); break;
                        case SqlDbType.Bit:
                            dr[i] = false; break;
                        case SqlDbType.Decimal:
                            dr[i] = decimal.Parse("0"); break;
                        case SqlDbType.DateTime:
                            //dr[i] = DateTime.Parse("0001/01/01");
                            break;
                        case SqlDbType.NVarChar:
                            dr[i] = ""; break;
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
        /// <summary>
        /// 單筆查詢的時候調用,用檢視表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual DataTable GetViewRecord(string filter)
        {
            string strSql = string.Format("select * from {0} where {1} and {2}", SysModel.ViewName, SysModel.strWhere, filter);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            return dt;
        }
        /// <summary>
        /// 不用Tree_List中的固定Where條件是調用
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual DataTable GetRecordNoWhere(string filter)
        {
            if (this.cnKey == null)
                tf = new TableFieldInfo(SysModel.TableName);
            else
                tf = new TableFieldInfo(SysModel.TableName, this.cnKey);
            string strSql = string.Format("select * from {0} where {1}", SysModel.TableName, filter);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    switch (tf.GetColType(dt.Columns[i].ColumnName))
                    {
                        case SqlDbType.TinyInt:
                            dr[i] = byte.Parse("0"); break;
                        case SqlDbType.Int:
                            dr[i] = int.Parse("0"); break;
                        case SqlDbType.Bit:
                            dr[i] = false; break;
                        case SqlDbType.Decimal:
                            dr[i] = decimal.Parse("0"); break;
                        case SqlDbType.DateTime:
                            //dr[i] = DateTime.Parse("0001/01/01");
                            break;
                        case SqlDbType.NVarChar:
                            dr[i] = ""; break;
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
        /// <summary>
        /// 上下筆移動時獲取記錄
        /// </summary>
        /// <param name="move"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual DataTable GetRecord(string move, string ID)
        {
            if (this.cnKey == null)
                tf = new TableFieldInfo(SysModel.TableName);
            else
                tf = new TableFieldInfo(SysModel.TableName, this.cnKey);
            string strSql = string.Format("select Top 1 * from {0} where {1} ", SysModel.ViewName, SysModel.strWhere);
            if (move == "F")
                strSql += string.Format(" Order by {0}", SysModel.KeyField);
            else if (move == "P")
                strSql += string.Format(" and {0}<'{1}' Order by {0} Desc", SysModel.KeyField, ID);
            else if (move == "N")
                strSql += string.Format(" and {0}>'{1}' Order by {0}", SysModel.KeyField, ID);
            else
                strSql += string.Format(" Order by {0} Desc", SysModel.KeyField);
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            return dt;
        }
        /// <summary>
        /// 上下筆移動時獲取記錄
        /// </summary>
        /// <param name="move"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual DataTable GetEnterpriseRecord(string move, string EnterpriseID,string ID)
        {
            string filter = string.Format(" EnterpriseID='{0}'", EnterpriseID);
            if (this.cnKey == null)
                tf = new TableFieldInfo(SysModel.TableName);
            else
                tf = new TableFieldInfo(SysModel.TableName, this.cnKey);
            string strSql = string.Format("select Top 1 * from {0} where {1} and {2}", SysModel.TableName, SysModel.strWhere, filter);
            if (move == "F")
                strSql += string.Format(" Order by {0}", SysModel.KeyField);
            else if (move == "P")
                strSql += string.Format(" and  {0} <'{1}' Order by {0} Desc", SysModel.KeyField, ID);
            else if (move == "N")
                strSql += string.Format(" and {0} >'{1}' Order by {0}", SysModel.KeyField, ID);
            else
                strSql += string.Format(" Order by {0} Desc", SysModel.KeyField);
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            return dt;
        }
        /// <summary>
        /// 綁定可新增記憶下拉框
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public virtual DataTable GetDistinctRecord(string fieldName)
        {
            string strSql = string.Format("select distinct {0} from {1}", fieldName, SysModel.TableName);
            if (SysModel.strWhere.Trim() != "")
                strSql += string.Format(" Where {0} ", SysModel.strWhere);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            return dt;
        }
        /// <summary>
        /// 綁定可新增記憶下拉框，加入自定義條件
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public virtual DataTable GetDistinctRecord(string fieldName, string filter)
        {
            string strSql = string.Format("select distinct {0} from {1} where {2}", fieldName, SysModel.TableName, filter);
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            return dt;
        }
        /// <summary>
        /// 綁定下拉編號名稱
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual DataTable GetNameList(string filter)
        {
            string strSql = string.Format("select {0} as ID,{1}Name as IDName from {2} ", SysModel.KeyField, SysModel.KeyField.Substring(0, SysModel.KeyField.Length - 2), SysModel.TableName);

            if (SysModel.strWhere.Trim() != "")
            {
                strSql += string.Format("Where {0} ", SysModel.strWhere);
                if (filter.Length > 0)
                    strSql += string.Format("and {0} ", filter);
            }
            else
            {
                if (filter.Length > 0)
                    strSql += string.Format("Where {0} ", filter);
            }
            strSql += string.Format("Order by {0}", SysModel.KeyField);


            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            return dt;
        }
        /// <summary>
        /// 綁定下拉編號名稱
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual DataTable GetIDNameList(string filter)
        {
            string strSql = string.Format("select {0} as ID,{0} + space(2) + {1}Name as IDName from {2} ", SysModel.KeyField, SysModel.KeyField.Substring(0, SysModel.KeyField.Length - 2), SysModel.TableName);

            if (SysModel.strWhere.Trim() != "")
            {
                strSql += string.Format("Where {0} ", SysModel.strWhere);
                if (filter.Length > 0)
                    strSql += string.Format("and {0} ", filter);
            }
            else
            {
                if (filter.Length > 0)
                    strSql += string.Format("Where {0} ", filter);
            }
            strSql += string.Format("Order by {0}", SysModel.KeyField);


            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            return dt;
        }
        /// <summary>
        /// 刪除子檔明細
        /// </summary>
        /// <param name="ID"></param>
        public virtual void DeleteDetail(string ID)
        {
            string[] subTableName = SysModel.MainSubTbl.Split(',');
            string strSql = "";
            for (int i = 0; i < subTableName.Length; i++)
            {
                strSql += string.Format("delete from {0} ", SysModel.TableName.Split('_')[0] + "_" + subTableName[i].Split('_')[1]);
                if (SysModel.strWhere.Trim() != "")
                    strSql += string.Format("Where {0} and {1}='{2}' ", SysModel.strWhere, SysModel.KeyField, ID);
                else
                    strSql += string.Format("Where {0}='{1}' ", SysModel.KeyField, ID);
            }
            ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 保存子檔明細，多明細
        /// </summary>
        /// <param name="dtSub"></param>
        /// <param name="oldID"></param>
        public void SaveDetail(DataTable[] dtSub, string oldID)
        {
            string strWhere = SysModel.strWhere;
            if (strWhere.Trim().Length > 0)
                strWhere += string.Format("And {0}='{1}'", SysModel.KeyField, oldID);
            else
                strWhere += string.Format("{0}='{1}'", SysModel.KeyField, oldID);
            SubBaseDao subDao = new SubBaseDao();
            for (int i = 0; i < dtSub.Length; i++)
                subDao.Save(dtSub[i], strWhere);
        }
        /// <summary>
        /// 保存子檔明細,單明細
        /// </summary>
        /// <param name="dtSub"></param>
        /// <param name="oldID"></param>
        public void SaveDetail(DataTable dtSub, string oldID)
        {
            string strWhere = SysModel.strWhere;
            if (strWhere.Trim().Length > 0)
                strWhere += string.Format("And {0}='{1}'", SysModel.KeyField, oldID);
            else
                strWhere += string.Format("{0}='{1}'", SysModel.KeyField, oldID);
            SubBaseDao subDao = new SubBaseDao(cnKey);
            subDao.Save(dtSub, strWhere);
        }
        /// <summary>
        /// 保存datatable到另一個庫
        /// </summary>
        /// <param name="dtSub"></param>
        /// <param name="oldID"></param>
        public void SaveDbTable(DataTable dtSub, string filter)
        {
            SubBaseDao subDao = new SubBaseDao(cnKey);
            subDao.Save(dtSub, filter);
        }
        /// <summary>
        /// 獲取企業未認證資料
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual DataSet GetEnterpriseDetail(string EnterpriseID)
        {
            string[] subTableName = SysModel.MainSubTbl.Split(',');
            string strSql = "";
            for (int i = 0; i < subTableName.Length; i++)
            {
                strSql += string.Format("select * from {0} ", subTableName[i]);
                if (SysModel.strWhere.Trim() != "")
                    strSql += string.Format("Where {0} and EnterpriseID='{1}' and Rows=1", SysModel.strWhere, EnterpriseID);
                else
                    strSql += string.Format("Where EnterpriseID='{0}' ", EnterpriseID);
            }
            
            strSql += string.Format(" Order by {0} ", SysModel.KeyField);
            DataSet ds = ia.ExecuteDataSetSql(strSql);
            for (int i = 0; i < ds.Tables.Count; i++)
                ds.Tables[i].TableName = subTableName[i];

            return ds;
        }
        /// <summary>
        /// 獲取企業未認證資料
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual DataSet GetEnterpriseDetail(string EnterpriseID,string filter)
        {
            string[] subTableName = SysModel.MainSubTbl.Split(',');
            string strSql = "";
            for (int i = 0; i < subTableName.Length; i++)
            {
                strSql += string.Format("select * from {0} ", subTableName[i]);
                if (SysModel.strWhere.Trim() != "")
                    strSql += string.Format("Where {0} and EnterpriseID='{1}'", SysModel.strWhere, EnterpriseID);
                else
                    strSql += string.Format("Where EnterpriseID='{0}' ",EnterpriseID);
            }
            if(filter.Trim().Length>0)
                strSql += " And " + filter;
            strSql += string.Format(" Order by {0} ", SysModel.KeyField);
            DataSet ds = ia.ExecuteDataSetSql(strSql);
            for (int i = 0; i < ds.Tables.Count; i++)
                ds.Tables[i].TableName = subTableName[i];

            return ds;
        }
        /// <summary>
        /// 獲取子檔記錄
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual DataSet GetSubDetail(string ID)
        {
            string[] subTableName = SysModel.MainSubTbl.Split(',');
            string strSql = "";
            for (int i = 0; i < subTableName.Length; i++)
            {
                strSql += string.Format("select * from {0} ", subTableName[i]);
                if (SysModel.strWhere.Trim() != "")
                    strSql += string.Format("Where {0} and {1}='{2}' ", SysModel.strWhere, SysModel.KeyField, ID);
                else
                    strSql += string.Format("Where {0}='{1}' ", SysModel.KeyField, ID);
            }
            strSql += string.Format(" Order by {0} ", SysModel.KeyField);
            DataSet ds = ia.ExecuteDataSetSql(strSql);
            for (int i = 0; i < ds.Tables.Count; i++)
                ds.Tables[i].TableName = subTableName[i];

            return ds;
        }
        /// <summary>
        /// 獲取子檔記錄
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual DataSet GetSubDetail(string ID, string filter)
        {
            string[] subTableName = SysModel.MainSubTbl.Split(',');
            string strSql = "";
            for (int i = 0; i < subTableName.Length; i++)
            {
                strSql += string.Format("select * from {0} Where {1}='{2}' and {3} ", subTableName[i], SysModel.KeyField, ID, filter);
            }
            DataSet ds = ia.ExecuteDataSetSql(strSql);
            for (int i = 0; i < ds.Tables.Count; i++)
                ds.Tables[i].TableName = subTableName[i];

            return ds;
        }
        /// <summary>
        /// 獲取最大編號
        /// </summary>
        /// <returns></returns>
        public virtual string GetMaxID()
        {
            string No = "";
            string strSql = string.Format("select max({0}) from {1} ", SysModel.KeyField, SysModel.TableName);
            if (SysModel.strWhere.Trim() != "")
                strSql += string.Format("Where {0} ", SysModel.strWhere);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            if (dt.Rows.Count > 0)
            {
                No = Common.NewID(dt.Rows[0][0].ToString());
            }
            return No;
        }
        /// <summary>
        /// 獲取最大編號，可自定義查詢條件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public virtual string GetMaxID(string strWhere)
        {
            string No = "";
            string strSql = string.Format("select max({0}) from {1} ", SysModel.KeyField, SysModel.TableName);
            //if (SysModel.strWhere.Trim() != "")
            //    strSql += string.Format("Where {0} and {1} ", SysModel.strWhere, strWhere);
            //else
            strSql += string.Format("Where {0} ", strWhere);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            if (dt.Rows.Count > 0)
            {
                No = Common.NewID(dt.Rows[0][0].ToString());
            }
            return No;
        }
        /// <summary>
        /// 根據編碼原則獲取單號
        /// </summary>
        /// <param name="Flag"></param>
        /// <param name="dTime"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public string GetAutoCode(DateTime dTime)
        {
            DBAccessLayer.IDBAccess ida = DBAccessLayer.DBFactory.GetDBAccess();
            string strSql = string.Format("Select {0} from Sys_Parameter ", SysModel.AutoCodeName);

            DataTable dt = ida.ExecuteDataSetSql(strSql).Tables[0];
            string CodeFormat = "YYYYMMDDXXXXXX 0";
            if (dt.Rows.Count > 0)
                CodeFormat = dt.Rows[0][0].ToString();

            string strCode = Common.GetCodeFormat(dTime, CodeFormat);
            return GetStrCode(strCode);
        }

        private string GetStrCode(string strCode)
        {
            string strSql = string.Format("select Max({0}) from {1} where {2} and {0} like '{3}'", SysModel.KeyField, SysModel.TableName, SysModel.strWhere, strCode);
            DataTable dt;
            string strReturn = "";

            dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() != "")
                    strReturn = Common.NewID(dt.Rows[0][0].ToString());
                else
                {
                    strCode = strCode.Replace("[0-9]", "0");
                    strReturn = strCode.Substring(0, strCode.Length - 1) + "1";
                }

            }
            else
            {
                strCode = strCode.Replace("[0-9]", "0");
                strReturn = strCode.Substring(0, strCode.Length - 1) + "1";
            }

            return strReturn;
        }
    }
}

