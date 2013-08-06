using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data ;

namespace Js.DAO.Rpt
{
    public class RptDao
    {
        DBAccessLayer.IDBAccess ia;
        string cnKey;
        DataTable dtHead;
        string _HostName;
        public RptDao()
        {
            ia =  DBAccessLayer.DBFactory.GetDBAccess();
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public RptDao(string FormID)
        {
            this.cnKey = "";
            ia = DBAccessLayer.DBFactory.GetDBAccess();
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public RptDao(string FormID, string cnKey)
        {
            this.cnKey = cnKey;
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
        }
        
        public DataTable GetRptData(string table, string filter)
        {
            string strSql = string.Format("select * from {0} ", table);
                
            if (filter.Trim().Length > 0)
                strSql += string.Format(" where {0} ", filter);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            return dt;
        }

        
        /// <summary>
        /// 給表頭賦值
        /// </summary>
        public void SetHeadParameter(string strColName, object obj)
        {
            if (dtHead == null || dtHead.Rows.Count == 0) return;
            dtHead.Rows[0][strColName] = obj;
        }
        /// <summary>
        /// 初始化表頭
        /// </summary>
        public void InitHead(DataTable dt, string HostName)
        {
            _HostName = HostName;
            dt = ia.ExecuteDataSetSql("select * from Rpt_Head where HostName='******' ").Tables[0];
            dtHead = dt;
            dtHead.TableName = "Rpt_Head";

            DataRow dr = dtHead.NewRow();

            dr["HostName"] = HostName;
            dr["Date1"] = new DateTime(1912, 1, 1, 0, 0, 1);
            dr["Date2"] = new DateTime(1912, 1, 1, 0, 0, 1);
            dr["Date3"] = new DateTime(1912, 1, 1, 0, 0, 1);
            dr["Date4"] = new DateTime(1912, 1, 1, 0, 0, 1);
            dr["Date5"] = new DateTime(1912, 1, 1, 0, 0, 1);
            dr["Date6"] = new DateTime(1912, 1, 1, 0, 0, 1);
            dr["Date7"] = new DateTime(1912, 1, 1, 0, 0, 1);
            dr["Date8"] = new DateTime(1912, 1, 1, 0, 0, 1);
            dr["BillNo1"] = String.Empty;
            dr["BillNo2"] = String.Empty;
            dr["BillNo3"] = String.Empty;
            dr["BillNo4"] = String.Empty;
            dr["BillNo5"] = String.Empty;
            dr["BillNo6"] = String.Empty;
            dr["BillNo7"] = String.Empty;
            dr["BillNo8"] = String.Empty;
            dr["BillNo9"] = String.Empty;
            dr["BillNo10"] = String.Empty;
            dr["BillNo11"] = String.Empty;
            dr["BillNo12"] = String.Empty;
            dr["BillNo13"] = String.Empty;
            dr["BillNo14"] = String.Empty;
            dr["BillNo15"] = String.Empty;
            dr["BillNo16"] = String.Empty;
            dr["BillNo17"] = String.Empty;
            dr["BillNo18"] = String.Empty;
            dr["BillNo19"] = String.Empty;
            dr["BillNo20"] = String.Empty;
            dr["BillNo21"] = String.Empty;
            dr["BillNo22"] = String.Empty;
            dr["BillNo23"] = String.Empty;
            dr["BillNo24"] = String.Empty;
            dr["BoolBit1"] = false;
            dr["BoolBit2"] = false;
            dr["BoolBit3"] = false;
            dr["BoolBit4"] = false;
            dr["BoolBit5"] = false;
            dr["BoolBit6"] = false;
            dr["BoolBit7"] = false;
            dr["BoolBit8"] = false;
            dr["BoolBit9"] = false;
            dr["BoolBit10"] = false;
            dr["BoolBit11"] = false;
            dr["BoolBit12"] = false;
            dr["IndexTinyint1"] = false;
            dr["IndexTinyint2"] = false;

            dr["CompanyName"] ="";
            dr["NumberDecimalDigits"] = 0;
            dr["PriceDecimalDigits"] = 0;
            dr["MoneyDecimalDigits"] =0;
            dr["ExchangeRateDecimalDigits"] = 0;

            //Kunchen.BLL.Companys.Company bllCompany = new Kunchen.BLL.Companys.Company();
            //bllCompany.DelNotExists(" CompanyID not in (select right([name],2) as CompanyID from master.dbo.sysdatabases where name like '" + Kunchen.Common.User.DataBasePrefix + "__')");
            //string CompanyID = Session["CompanyID"].ToString();
            //Kunchen.Model.Companys.CompanyInfo Model = bllCompany.GetModel(CompanyID);
            //dr["CompanyName"] = Model.CompanyName;
            //dr["DateType"] = Model.DateType;
            //dr["DateFormat"] = Model.DateFormat;
            //dr["ListSeparator"] = Model.ListSeparator;
            //dr["NumberDecimalDigits"] = Model.NumberDecimalDigits;
            //dr["PriceDecimalDigits"] = Model.PriceDecimalDigits;
            //dr["MoneyDecimalDigits"] = Model.MoneyDecimalDigits;
            //dr["ExchangeRateDecimalDigits"] = Model.ExchangeRateDecimalDigits;

            dr["txtTrail"] = String.Empty;

            //strSQL = "";
            //strSQL = "select IsShowISO, ISOBillNo from Sys_PreviewSetup where SysID=" + SysID + " and PermissionID=" + PermissionID + " ";
            //DataTable dt = Kunchen.DBUtility.SysHelperSQL.Query(strSQL).Tables[0];
            //if (dt.Rows.Count > 0 && dt.Rows[0]["IsShowISO"].ToString().ToLower() == "true")
            //{
            //    dr["ISOBillNo"] = dt.Rows[0]["ISOBillNo"].ToString();
            //    //strSQL = "";
            //    //strSQL = "update RptPHeadQuery set ISOBillNo = '" + dt.Rows[0]["ISOBillNo"].ToString() + "' where hostname='" + GetHostName() + "' and  PrefixName='" + RptPrefixName.Substring(0, 4) + "' ";
            //    //Kunchen.DBUtility.DbHelperSQL.ExecuteSql(strSQL);
            //}
            //else
                //dr["ISOBillNo"] = String.Empty;

            dtHead.Rows.Add(dr);            
        }
        /// <summary>
        /// 插入表頭
        /// </summary>
        public void SaveHead()
        {
            SubBaseDao subDao = new SubBaseDao(cnKey);
            subDao.Save(dtHead, "HostName='" + _HostName + "'");
        }

        public DataTable GetHead(string HostName)
        {
            string strSql = string.Format("select * from Rpt_Head where HostName = '{0}'", HostName);
 

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            return dt;
        }
    }
}


