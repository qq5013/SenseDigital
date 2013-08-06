using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.OracleClient;
using System.Configuration;
using System.Web.SessionState;

namespace DBAccessLayer
{
    public enum DBAccessType
    {
        SQL,    // 以Sql形式访问数据库
        ODBC,   // 以Odbc形式访问数据库
        OLEDB,  // 以OleDb形式访问数据库
        ORACLE  // 以Oracle形式访问数据库
    }

    public class DBFactory
    {
        private static IDictionary<string, string> dicCnKey;
        #region 获取数据库访问对象的静态方法
        /// <summary>
        /// 获取数据库访问对象的静态方法
        /// </summary>
        /// <returns>返回数据库访问对象(dbAccess)</returns>
        public static IDBAccess GetDBAccess()
        {
            IDBAccess dbAccess = null;
            DBAccessType type = dbType();
            string connectionString = dbConnectionString("");
            switch (type)
            {
                case DBAccessType.SQL:
                    dbAccess = new DBAccess<SqlConnection, SqlCommand, SqlDataAdapter>(connectionString);
                    break;
                case DBAccessType.ODBC:
                    dbAccess = new DBAccess<OdbcConnection, OdbcCommand, OdbcDataAdapter>(connectionString);
                    break;
                case DBAccessType.OLEDB:
                    dbAccess = new DBAccess<OleDbConnection, OleDbCommand, OleDbDataAdapter>(connectionString);
                    break;
                case DBAccessType.ORACLE:
                    dbAccess = new DBAccess<OracleConnection, OracleCommand, OracleDataAdapter>(connectionString);
                    break;
            }

            return dbAccess;
        }
        /// <summary>
        /// 获取数据库访问对象的静态方法
        /// </summary>
        /// <returns>返回数据库访问对象(dbAccess)</returns>
        public static IDBAccess GetDBAccess(string connectionString)
        {
            IDBAccess dbAccess = null;
            DBAccessType type = dbType();
            switch (type)
            {
                case DBAccessType.SQL:
                    dbAccess = new DBAccess<SqlConnection, SqlCommand, SqlDataAdapter>(connectionString);
                    break;
                case DBAccessType.ODBC:
                    dbAccess = new DBAccess<OdbcConnection, OdbcCommand, OdbcDataAdapter>(connectionString);
                    break;
                case DBAccessType.OLEDB:
                    dbAccess = new DBAccess<OleDbConnection, OleDbCommand, OleDbDataAdapter>(connectionString);
                    break;
                case DBAccessType.ORACLE:
                    dbAccess = new DBAccess<OracleConnection, OracleCommand, OracleDataAdapter>(connectionString);
                    break;
            }

            return dbAccess;
        }
        /// <summary>
        /// 获取数据库访问对象的静态方法
        /// </summary>
        /// <param name="type">数据库访问的类型</param>
        /// <param name="connectionString">访问数据库的连接字符串</param>
        /// <returns>返回数据库访问对象(dbAccess)</returns>
        public static IDBAccess GetDBAccess(DBAccessType type, string cnKey)
        {
            IDBAccess dbAccess = null;
            string connectionString = dbConnectionString(cnKey);
            switch (type)
            {
                case DBAccessType.SQL:
                    dbAccess = new DBAccess<SqlConnection, SqlCommand, SqlDataAdapter>(connectionString);
                    break;
                case DBAccessType.ODBC:
                    dbAccess = new DBAccess<OdbcConnection, OdbcCommand, OdbcDataAdapter>(connectionString);
                    break;
                case DBAccessType.OLEDB:
                    dbAccess = new DBAccess<OleDbConnection, OleDbCommand, OleDbDataAdapter>(connectionString);
                    break;
                case DBAccessType.ORACLE:
                    dbAccess = new DBAccess<OracleConnection, OracleCommand, OracleDataAdapter>(connectionString);
                    break;
            }

            return dbAccess;
        }
        
        private static DBAccessType dbType()
        {
            return DBAccessType.SQL;
        }
        private static string dbConnectionString(string cnKey)
        {
            if (cnKey == "")
                cnKey = "BusinessUnit";
            string strCn = "";
            if (dicCnKey == null)
            {
                dicCnKey = new Dictionary<string, string>();
                strCn = GetCnString(cnKey);
                if (strCn.Length > 0)
                    dicCnKey.Add(cnKey, strCn);
                else
                {
                    strCn = GetKeyCnString(cnKey);
                    if (cnKey == "BusinessUnit")
                    {
                        strCn = "DE22AF18AF9393F4EAD85D59E9C6F068ADA7DD8449FAFD8BD150D83D5776086C2B2871232FF57A082DEC924E72F7A902";
                        SetCnString(cnKey, strCn);
                        dicCnKey.Add(cnKey, DESEncrypt.Decrypt(strCn));
                    }
                    else
                    {
                        SetCnString(cnKey, DESEncrypt.Encrypt(strCn));
                        dicCnKey.Add(cnKey, strCn);
                    }
                }
            }
            else
            {
                if (!dicCnKey.ContainsKey(cnKey))
                {
                    strCn = GetCnString(cnKey);
                    if (strCn.Length <= 0)
                    {
                        strCn = GetKeyCnString(cnKey);
                        SetCnString(cnKey, DESEncrypt.Encrypt(strCn));
                    }
                    dicCnKey.Add(cnKey, strCn);
                }
            }
            return dicCnKey[cnKey];
            //if(cnKey=="")
            //    return DESEncrypt.Decrypt(ConfigurationManager.AppSettings["BusinessUnit"]);
            //else
            //    return DESEncrypt.Decrypt(ConfigurationManager.AppSettings[cnKey]);
        }
       
        private static string GetKeyCnString(string EnterpriseID)
        {
            SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = DESEncrypt.Decrypt(ConfigurationManager.AppSettings["BusinessUnit"]);
            cn.Open();
            SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = string.Format("select * from Com_EnterpriseDb Where EnterpriseID='{0}'", EnterpriseID);
            cmd.Connection = cn;
            SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            string cnString = "";

            if(ds.Tables[0].Rows.Count>0)
                //cnString = string.Format("server={0};Initial Catalog={1};Integrated Security=SSPI",ds.Tables[0].Rows[0]["SQLServer"].ToString(),ds.Tables[0].Rows[0]["DbName"].ToString());
                cnString = string.Format("server={0};Initial Catalog={1};Uid=sa;Pwd={2}", ds.Tables[0].Rows[0]["SQLServer"].ToString(), ds.Tables[0].Rows[0]["DbName"].ToString(), ds.Tables[0].Rows[0]["Pwd"].ToString());

            return cnString;
        }
        private static string GetCnString(string Name)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "DB.xml");
            //获取Employees节点的所有子节点 
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("Connections").ChildNodes;

            string cnString = "";
            foreach (XmlNode xn in nodeList)//遍历所有子节点 
            {
                XmlElement xel = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                if (xel.GetAttribute("Name") == Name)//如果genre属性值为“张三” 
                {
                    cnString = xel.GetAttribute("Value");
                    break;
                }
            }
            return DESEncrypt.Decrypt(cnString);
        }
        private static void SetCnString(string Name,string Value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "DB.xml");
            XmlNode root = xmlDoc.SelectSingleNode("Connections");
            //查找<Connections>
            XmlElement xe1 = xmlDoc.CreateElement("Connection");
            //创建一个<Connection>节点
            xe1.SetAttribute("Name", Name);
            //设置该节点genre属性
            xe1.SetAttribute("Value", Value);
            //设置该节点ISBN属性            
            root.AppendChild(xe1);
            xmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory + "DB.xml");
        }
        #endregion
    }
 
}
