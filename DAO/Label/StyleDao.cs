using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Js.DAO.Label
{
    public class StyleDao
    {
        DBAccessLayer.IDBAccess ia;
        string cnKey;

        public StyleDao()
        {
            ia =  DBAccessLayer.DBFactory.GetDBAccess();
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public StyleDao(string cnKey)
        {
            this.cnKey = cnKey;
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public StyleDao(string FormID, string cnKey)
        {
            this.cnKey = cnKey;
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
        }
        /// <summary>
        /// 獲取記錄，用原表名
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual DataTable GetRecord(string filter)
        {
            string strSql = "select * from LB_DefaultData ";
            if(filter.Trim().Length>0)
                strSql += string.Format(" where {0}", filter);

            strSql += " Order by Flag";
            
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];           

            return dt;
        }
        /// <summary>
        /// 取得 限用產品
        /// </summary>
        public virtual DataTable GetLimitedProduct(string filter)
        {
            string strSql = "select * from LB_StyleSub ";
            if (filter.Trim().Length > 0)
                strSql += string.Format(" where ID='{0}'", filter);

            strSql += " Order by RowID";

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            return dt;
        }
        /// <summary>
        /// 更新圖檔路徑
        /// </summary>
        public virtual void UpdateImagePath(string StyleID,string FieldName,string ImagePath)
        {
            string strSql = string.Format("Update LB_Style set {0}='{1}' where StyleID='{2}'", FieldName, ImagePath, StyleID);

            ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 獲取倉庫樣式數量
        /// </summary>
        public virtual DataTable GetWarehousePages(string StyleID)
        {
            string strSql = string.Format("select * from VLB_WarehousePages where StyleID='{0}'", StyleID);

            return ia.ExecuteDataSetSql(strSql).Tables[0];
        }
        /// <summary>
        /// 插入倉庫樣式數量
        /// </summary>
        public virtual void InsertWarehousePagesByWID(string WarehouseID,string UserName)
        {
            string strSql = string.Format("delete from LB_WarehousePages where WarehouseID='{0}';" +
                            "Insert into LB_WarehousePages(WarehouseID,StyleID, dbo.LB_WarehousePages.LastModifyDate,LastModifyUserName) " +
                            "select '{0}',StyleID,getdate(),'{1}' From LB_Style", WarehouseID, UserName);

            ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 插入倉庫樣式數量
        /// </summary>
        public virtual void InsertWarehousePagesByStyleID(string StyleID, string UserName)
        {
            string strSql = string.Format("delete from LB_WarehousePages where StyleID='{0}';" +
                            "Insert into LB_WarehousePages(WarehouseID,StyleID, dbo.LB_WarehousePages.LastModifyDate,LastModifyUserName) " +
                            "select WarehouseID,'{0}',getdate(),'{1}' from LB_Warehouse", StyleID, UserName);

            ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 刪除倉庫樣式數量
        /// </summary>
        public virtual void DeleteWarehousePagesByWID(string WarehouseID)
        {
            string strSql = string.Format("delete from LB_WarehousePages where WarehouseID='{0}'", WarehouseID);

            ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 刪除倉庫樣式數量
        /// </summary>
        public virtual void DeleteWarehousePagesByStyleID(string StyleID)
        {
            string strSql = string.Format("delete from LB_WarehousePages where StyleID='{0}'", StyleID);

            ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 獲取最大位置
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataTable GetLoadData(string filter)
        {
            string[] str = filter.Split(new string[] { "$##$" }, StringSplitOptions.RemoveEmptyEntries);
            string strYear = str[0].Substring(0, 4);
            string strStyleID = str[1];
            int Pages = int.Parse(str[2]);
            int StdPages = int.Parse(str[3]);
            int Volumns = int.Parse(str[4]);

            string strSql = string.Format("select max(volumeNO) as volumeNO,max(lb_productionsub.endLabelNo) as endLabelNo from  lb_productionsub " +
                            "inner join lb_productionmain on lb_productionsub.billid=lb_productionmain.billid " +
                            " where convert(VARCHAR(10),lb_productionmain.BillDate,120) like '{0}%' and styleid='{1}'", strYear, strStyleID);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0].BeginEdit();
                if (dt.Rows[0]["volumeNO"].ToString() == "")
                {
                    dt.Rows[0]["volumeNO"] = strYear.Substring(2) + strStyleID + "0".PadLeft(9, '0');
                }
                if (dt.Rows[0]["endLabelNo"].ToString() == "")
                {
                    dt.Rows[0]["endLabelNo"] = strYear.Substring(2) + strStyleID + "0".PadLeft(12, '0');
                }
                dt.Rows[0].EndEdit();
                dt.AcceptChanges();
            }
            int intVolumeNO = int.Parse(dt.Rows[0]["volumeNO"].ToString().Substring(4));
            int intendLabelNo = int.Parse(dt.Rows[0]["endLabelNo"].ToString().Substring(4));

            DataTable dtNew = new DataTable();
            DataColumn dc1 = new DataColumn("RowID");
            DataColumn dc2 = new DataColumn("VolumeNo");
            DataColumn dc3 = new DataColumn("StartLabelNo");
            DataColumn dc4 = new DataColumn("EndLabelNo");
            DataColumn dc5 = new DataColumn("Pages");
            DataColumn dc6 = new DataColumn("StarNo");
            DataColumn dc7 = new DataColumn("EndNo");

            dtNew.Columns.Add(dc1);
            dtNew.Columns.Add(dc2);
            dtNew.Columns.Add(dc3);
            dtNew.Columns.Add(dc4);
            dtNew.Columns.Add(dc5);
            dtNew.Columns.Add(dc6);
            dtNew.Columns.Add(dc7);

            for (int i = 1; i <= Volumns; i++)
            {
                DataRow dr = dtNew.NewRow();
                dr["RowID"] = i;
                dr["VolumeNo"] = strYear.Substring(2) + strStyleID + (intVolumeNO + i).ToString().PadLeft(9, '0');
                dr["StartLabelNo"] = strYear.Substring(2) + strStyleID + (intendLabelNo + StdPages * (i - 1) + 1).ToString().PadLeft(12, '0');
                dr["EndLabelNo"] = strYear.Substring(2) + strStyleID + (intendLabelNo + StdPages * i).ToString().PadLeft(12, '0');
                dr["Pages"] = StdPages;
                dr["StarNo"] = strYear.Substring(2) + strStyleID + (intendLabelNo + 1).ToString().PadLeft(12, '0');
                dr["EndNo"] = strYear.Substring(2) + strStyleID + (intendLabelNo + Pages).ToString().PadLeft(12, '0');
                dtNew.Rows.Add(dr);
            }
            return dtNew;
        }
    }
}

