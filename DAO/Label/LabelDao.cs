using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Js.DAO.Label
{
    public class LabelDao
    {
        DBAccessLayer.IDBAccess ia;
        string cnKey;

        public LabelDao()
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess();
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public LabelDao(string cnKey)
        {
            this.cnKey = cnKey;
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
        }

        /// <summary>
        /// 獲取標籤序號資訊
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual DataTable GetLabelNoInfo(string labelNo)
        {
            string StyleID = labelNo.Substring(2, 2);
            int LabelNo = int.Parse(labelNo.Substring(4));
            int i = LabelNo / 1000000;
            string TableNo = i.ToString().PadLeft(4,'0');

            string tableName = "LB_" + StyleID + "_" + TableNo;
            string strSql = string.Format("select * from {0} where LabelNo='{1}'", tableName, labelNo);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            return dt;
        }

    }
}

