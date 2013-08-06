using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Js.DAO.Label
{
    public class ProductionDao
    {
        DBAccessLayer.IDBAccess ia;
        string cnKey;

        public ProductionDao()
        {
            ia =  DBAccessLayer.DBFactory.GetDBAccess();
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public ProductionDao(string cnKey)
        {
            this.cnKey = cnKey;
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
        }

         //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public ProductionDao(string FormID, string cnKey)
        {
            this.cnKey = cnKey;
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
        }
        public bool GetBetweenData(string strValue)
        {
            string[] str = strValue.Split(new string[] { "$##$" }, StringSplitOptions.RemoveEmptyEntries);
            string strYear = str[0].Substring(0, 4);
            string strStyleID = str[1];
            int starNO = int.Parse(str[2].Substring(4));
            int EndNO = int.Parse(str[3].Substring(4));


            string strSql = string.Format("select StartLabelNo,EndLabelNo from  lb_productionmain " +
                            " where convert(VARCHAR(10),BillDate,120) like '{0}%' and styleid='{1}'", strYear, strStyleID);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            bool blnvalue = true;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int GroupStartNo = Convert.ToInt32(dt.Rows[i][0].ToString().Substring(4));
                int GroupEndNo = Convert.ToInt32(dt.Rows[i][1].ToString().Substring(4));
                if (starNO > GroupStartNo && EndNO < GroupEndNo)
                {
                    blnvalue = false;
                    break;
                }
                if (starNO > GroupStartNo && EndNO < GroupEndNo)
                {
                    blnvalue = false;
                    break;
                }
            }


            return blnvalue;
        }

       
    }
}

