using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Js.DAO
{
    public class SubBaseDao
    {
        DBAccessLayer.IDBAccess ia;

        Js.Model.Sys.TreeListInfo SysModel;
        string cnKey = "";
        TableFieldInfo tf;
        
        public SubBaseDao()
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");
        }
        public SubBaseDao(string cnKey)
        {
            this.cnKey = cnKey;
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
        }

        #region Sub Members
        public void Save(DataTable dt, string strWhere)
        {
            try
            {
                string TableName = dt.TableName;
                string strSql = "";

                if (!string.IsNullOrEmpty(strWhere))
                {
                    strSql = string.Format("delete from {0} where {1}", TableName, strWhere);
                    ia.ExecuteNonQuerySql(strSql);
                }
                TableFieldInfo tf = new TableFieldInfo(TableName, cnKey);
                DataTable dtCol = tf.dtFields;

                StringBuilder strInsert = new StringBuilder();
                strInsert.AppendFormat("insert into {0} (",TableName);
                string strFiledName = "";
                int count = 0;
                for (int i = 0; i < dtCol.Rows.Count; i++)
                {
                    if (dtCol.Rows[i]["colstat"].ToString() != "1")
                        strFiledName += dtCol.Rows[i]["Name"].ToString() + ",";
                    else
                        count = 1;
                }
                if (strFiledName.Length > 0)
                    strFiledName = strFiledName.Substring(0, strFiledName.LastIndexOf(",")) + ")";
                strInsert.Append(strFiledName);
                strInsert.Append(" values( ");

                strFiledName = "";
                for (int i = 0; i < dtCol.Rows.Count; i++)
                {
                    if (dtCol.Rows[i]["colstat"].ToString() != "1")
                        strFiledName += "@" + dtCol.Rows[i]["Name"].ToString() + ",";
                }
                if (strFiledName.Length > 0)
                    strFiledName = strFiledName.Substring(0, strFiledName.LastIndexOf(",")) + ")";
                strInsert.Append(strFiledName);

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j].RowState == DataRowState.Deleted)  //shj
                        continue;
                    SqlParameter[] parameters = new SqlParameter[dtCol.Rows.Count - count];
                    int k = 0;
                    for (int i = 0; i < dtCol.Rows.Count; i++)
                    {
                        if (dtCol.Rows[i]["colstat"].ToString() != "1")
                        {
                            parameters[k] = new SqlParameter("@" + dtCol.Rows[i]["Name"].ToString(), tf.GetColType(dtCol.Rows[i]["Name"].ToString()), tf.GetTypeLength(dtCol.Rows[i]["Name"].ToString()));
                            if (dt.Rows[j][dtCol.Rows[i]["Name"].ToString()] != null && dt.Rows[j][dtCol.Rows[i]["Name"].ToString()].ToString() != "")
                            {
                                if (dt.Rows[j][dtCol.Rows[i]["Name"].ToString()].GetType().Name == "DateTime")
                                {
                                    parameters[k].Value = Common.ReDateTime((DateTime)dt.Rows[j][dtCol.Rows[i]["Name"].ToString()]);
                                    if (parameters[k].Value == null) parameters[k].Value = DBNull.Value;
                                }
                                else
                                    parameters[k].Value = dt.Rows[j][dtCol.Rows[i]["Name"].ToString()];

                            }
                            else
                            {
                                switch (tf.GetColType(dtCol.Rows[i]["Name"].ToString()))
                                {
                                    case SqlDbType.TinyInt:
                                        parameters[k].Value = 0;
                                        break;
                                    case SqlDbType.Int:
                                        
                                        parameters[k].Value = 0;
                                        break;
                                    case SqlDbType.DateTime:
                                        parameters[k].IsNullable = true;
                                        parameters[k].Value = DBNull.Value;
                                        break;
                                    case SqlDbType.Bit:
                                        parameters[k].Value = false;
                                        break;
                                    case SqlDbType.Decimal:
                                        parameters[k].Value = 0;
                                        break;
                                    case SqlDbType.SmallInt:
                                        parameters[k].Value = 0;
                                        break;
                                    case SqlDbType.NVarChar:
                                        parameters[k].Value = "";
                                        break;
                                    default:
                                        parameters[k].Value = DBNull.Value;
                                        break;

                                }
                            }
                            k++;
                        }
                    }
                    ia.ExecuteNonQuerySql(strInsert.ToString(), parameters);                   
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 傳入where条件时，顺序为：欄位名称，欄位值。如strwhere[0]欗位名称，strwhere[1]欗位值。
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strWhere"></param>
        public void Save(DataTable dt, object[] strWhere)
        {
            try
            {
                string TableName = dt.TableName;
                string strSql = "";
                if (!(strWhere == null || strWhere.Length == 0))
                {
                    string []str =new string[strWhere.Length/2];
                    for (int i = 0; i < strWhere.Length / 2; i++)
                    {
                        if (Common.GetObjectType(strWhere[i * 2 + 1]) == SqlDbType.Int || 
                            Common.GetObjectType(strWhere[i * 2 + 1]) == SqlDbType.Decimal || 
                            Common.GetObjectType(strWhere[i * 2 + 1]) == SqlDbType.Bit)
                        {
                            str[i] = strWhere[i * 2] + "=" + strWhere[i * 2 + 1];
                        }
                        else
                        {
                            str[i] = strWhere[i * 2] + "='" + strWhere[i * 2 + 1].ToString().Replace("'", "''") + "'";
                        } 
                    }
                    string where = "";
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (i == 0)
                            where += str[i];
                        else
                            where += " and " + str[i];

                    }
                    strSql = string.Format("delete from {0} where {1}", TableName, strWhere);
                    ia.ExecuteNonQuerySql(strSql);
                }
                TableFieldInfo tf = new TableFieldInfo(TableName, cnKey);
                DataTable dtCol = tf.dtFields;

                StringBuilder strInsert = new StringBuilder();
                strInsert.Append("insert into " + TableName + "(");
                string strFiledName = "";
                int count = 0;
                for (int i = 0; i < dtCol.Rows.Count; i++)
                {
                    if (dtCol.Rows[i]["colstat"].ToString() != "1")
                        strFiledName += dtCol.Rows[i]["Name"].ToString() + ",";
                    else
                        count = 1;
                }
                if (strFiledName.Length > 0)
                    strFiledName = strFiledName.Substring(0, strFiledName.LastIndexOf(",")) + ")";
                strInsert.Append(strFiledName);
                strInsert.Append(" values( ");


                strFiledName = "";
                for (int i = 0; i < dtCol.Rows.Count; i++)
                {
                    if (dtCol.Rows[i]["colstat"].ToString() != "1")
                        strFiledName += "@" + dtCol.Rows[i]["Name"].ToString() + ",";
                }
                if (strFiledName.Length > 0)
                    strFiledName = strFiledName.Substring(0, strFiledName.LastIndexOf(",")) + ")";
                strInsert.Append(strFiledName);             
               
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    SqlParameter[] parameters = new SqlParameter[dtCol.Rows.Count-count];
                    int k = 0;
                    for (int i = 0; i < dtCol.Rows.Count; i++)
                    {
                        if (dtCol.Rows[i]["colstat"].ToString() != "1")
                        {
                            parameters[k] = new SqlParameter("@" + dtCol.Rows[i]["Name"].ToString(), tf.GetColType(dtCol.Rows[i]["Name"].ToString()), tf.GetTypeLength(dtCol.Rows[i]["Name"].ToString()));
                            if (dt.Rows[j][dtCol.Rows[i]["Name"].ToString()] != null && dt.Rows[j][dtCol.Rows[i]["Name"].ToString()].ToString() != "")
                            {
                                if (dt.Rows[j][dtCol.Rows[i]["Name"].ToString()].GetType().Name == "DateTime")
                                    parameters[k].Value = Common.ReDateTime((DateTime)dt.Rows[j][dtCol.Rows[i]["Name"].ToString()]);
                                else
                                    parameters[k].Value = dt.Rows[j][dtCol.Rows[i]["Name"].ToString()];
                                
                            }
                            else
                            {
                                switch (tf.GetColType(dtCol.Rows[i]["Name"].ToString()))
                                {
                                    case SqlDbType.TinyInt:
                                        parameters[k].Value = 0;
                                        break;
                                    case SqlDbType.Int:
                                        parameters[k].Value = 0;
                                        break;
                                    case SqlDbType.DateTime:
                                        parameters[k].Value = DBNull.Value;
                                        break;
                                    case SqlDbType.Bit:
                                        parameters[k].Value = false;
                                        break;
                                    case SqlDbType.Decimal:
                                        parameters[k].Value = 0;
                                        break;
                                    case SqlDbType.SmallInt:
                                        parameters[k].Value = 0;
                                        break;
                                    case SqlDbType.NVarChar:
                                        parameters[k].Value = "";
                                        break;
                                    default:
                                        parameters[k].Value = DBNull.Value;
                                        break;

                                }
                            }
                            k++;
                        }
                    }
                    ia.ExecuteNonQuerySql(strInsert.ToString(), parameters);
                }


            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
    }
        #endregion
}

