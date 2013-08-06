using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Js.DAO
{
    public class TableFieldInfo
    {

            public DataTable dtFields = null;
            string strTableName = "";

            /// <summary>
            /// strSqlTableName 資料表名稱 
            /// </summary>
            public TableFieldInfo(string strTblName)
            {
                strTableName = strTblName;
                Open();
            }
            /// <summary>
            /// strSqlTableName 資料表名稱 
            /// </summary>
            public TableFieldInfo(string strTblName,string cnKey)
            {
                strTableName = strTblName;
                Open(cnKey);
            }
            /// <summary>
            /// 開啟
            /// </summary>
            private void Open()
            {
                using (DBAccessLayer.IDBAccess ia = DBAccessLayer.DBFactory.GetDBAccess())
                {
                    try
                    {
                        string strSql = string.Format("SELECT name,xtype, (case xtype when 239 then length/2 when 231 then length/2  else length end) as Length, isnullable as IsNotNull,colstat," +
                                    "(SELECT column_default FROM INFORMATION_SCHEMA.COLUMNS where column_name= name and TABLE_NAME='{0}') as cdefault " +
                                    "FROM sys.syscolumns WHERE id = OBJECT_ID('{0}')", strTableName);

                        dtFields = ia.ExecuteDataSetSql(strSql).Tables[0];
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            /// <summary>
            /// 開啟
            /// </summary>
            private void Open(string cnKey)
            {
                using (DBAccessLayer.IDBAccess ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL,cnKey))
                {
                    try
                    {
                        string strSql = string.Format("SELECT name,xtype, (case xtype when 239 then length/2 when 231 then length/2  else length end) as Length, isnullable as IsNotNull,colstat," +
                                    "(SELECT column_default FROM INFORMATION_SCHEMA.COLUMNS where column_name= name and TABLE_NAME='{0}') as cdefault " +
                                    "FROM sys.syscolumns WHERE id = OBJECT_ID('{0}')", strTableName);

                        dtFields = ia.ExecuteDataSetSql(strSql).Tables[0];
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            } 
            /// <summary>
            /// 取得是否允許 null
            /// </summary>
            public bool GetColIsNotNull(string colname)
            {
                if (dtFields != null && dtFields.Rows.Count > 0)
                {
                    DataRow[] row = dtFields.Select("name = '" + colname + "'");
                    if (row != null && row.Length > 0)
                    {
                        return int.Parse(row[0]["IsNotNull"].ToString()) == 1;
                    }
                }
                return false;
            }
            /// <summary>
            /// 取得類型長度
            /// </summary>
            public int GetTypeLength(string colname)
            {
                if (dtFields != null && dtFields.Rows.Count > 0)
                {
                    DataRow[] row = dtFields.Select("name = '" + colname + "'");
                    if (row != null && row.Length > 0)
                    {
                        return int.Parse(row[0]["Length"].ToString());
                    }
                }
                return -1;
            }
            /// <summary>
            /// 取得欄位類型 
            /// </summary>
            public SqlDbType GetColType(string colname)
            {
                SqlDbType sdType = SqlDbType.NVarChar;
                if (dtFields != null && dtFields.Rows.Count > 0)
                {
                    DataRow[] row = dtFields.Select("name = '" + colname + "'");
                    if (row != null && row.Length > 0)
                    {
                        switch (int.Parse(row[0]["xtype"].ToString()))
                        {
                            case 36:
                                sdType = SqlDbType.UniqueIdentifier;
                                break;
                            case 48:
                                sdType = SqlDbType.TinyInt;
                                break;
                            case 52:
                                sdType = SqlDbType.SmallInt;
                                break;
                            case 56:
                                sdType = SqlDbType.Int;
                                break;
                            case 61:
                                sdType = SqlDbType.DateTime;
                                break;
                            case 104:
                                sdType = SqlDbType.Bit;
                                break;
                            case 106:
                            case 108:
                                sdType = SqlDbType.Decimal;
                                break;
                            case 231:
                                sdType = SqlDbType.NVarChar;
                                break;
                            default:
                                sdType = SqlDbType.NVarChar;
                                break;
                        }
                    }
                }
                return sdType;
            }
            
            private object GetDFValue(SqlDbType type, string strDefault)
            {
                try
                {
                    switch (type)
                    {
                        case SqlDbType.TinyInt:
                            return strDefault.Length > 0 ? byte.Parse(strDefault) : byte.Parse("0");
                        case SqlDbType.Int:
                            return strDefault.Length > 0 ? int.Parse(strDefault) : 0;
                        case SqlDbType.Bit:
                            return strDefault.Length > 0 ? int.Parse(strDefault) > 0 : false;
                        case SqlDbType.Decimal:
                            return strDefault.Length > 0 ? decimal.Parse(strDefault) : 0;
                        case SqlDbType.DateTime:
                            return null;
                        case SqlDbType.NVarChar:
                            return strDefault;
                    }
                }
                catch { }
                return null;
            }
        }
    }

