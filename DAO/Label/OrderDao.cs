using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Js.DAO.Label
{
    public class OrderDao
    {
        DBAccessLayer.IDBAccess ia;
        string cnKey;

        public OrderDao()
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess();
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public OrderDao(string FormID)
        {
            this.cnKey = "";
            ia = DBAccessLayer.DBFactory.GetDBAccess();
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public OrderDao(string FormID, string cnKey)
        {
            this.cnKey = cnKey;
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
        }

        /// <summary>
        /// 獲取歷史地址
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual DataTable historyAddress(string filter)
        {
            string strSql = "select distinct DeliverAddress from LB_OrderMain ";
            if (filter.Trim().Length > 0)
                strSql += string.Format(" where flag =1 and DeliverAddress <> '' and EnterpriseID= '{0}'", filter);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            return dt;
        }

        public virtual DataTable historyCountry(string filter)
        {
            string strSql = "select distinct DeliverCountry from LB_OrderMain where  flag =1 and DeliverCountry <> ''";
            //if (filter.Trim().Length > 0)
            //    strSql += string.Format(" where DeliverCountry <> '' and EnterpriseID= '{0}'", filter);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            return dt;
        }
        //historyMehtod
        public virtual DataTable historyMehtod(string filter)
        {
            string strSql = "select distinct DeliverMehtod from LB_OrderMain  where flag =1 and DeliverMehtod <> ''";
            //if (filter.Trim().Length > 0)
            //    strSql += string.Format(" where DeliverMehtod <> '' and EnterpriseID= '{0}'", filter);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            return dt;
        }

        public void ChangeEstimate(string id, string rowid)
        {
            string strSql = string.Format("update LB_Ordersub set IsEstimate = case when IsEstimate =1 then 0 else 1 end  where flag =1 and IsClose = 0 and  BillID = '{0}' and rowid = {1}", id, rowid);
            ia.ExecuteDataSetSql(strSql);
        }

        public void updateBillState(string id)
        {
            string strSql = string.Format("update LB_OrderMain set BillState = 2  where flag =1 and  BillID = '{0}' and not exists(select * from LB_Ordersub where flag =1 and IsClose = 0 and BillID = LB_OrderMain.BillID) ", id);
            ia.ExecuteDataSetSql(strSql);
        }

        public string GetStartNo(string oldid, string EnterpriseID, string StyleID, DateTime date)
        {
            string strSql = "select top 1 StartNo from LB_OrderSub ";
            strSql += string.Format(" where BillID <> '{0}' and EnterpriseID= '{1}' and StyleID= '{2}' order by BillDate desc,billid desc,SubID desc", oldid, EnterpriseID, StyleID);
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            string strNewOne = "";
            if (dt.Rows.Count == 0 || ("" + dt.Rows[0][0]).Length == 0)
            {
                strNewOne += (EnterpriseID.Trim() + "000000").Substring(0, 6);
                strNewOne += (StyleID.Trim() + "000000").Substring(0, 3);
                strNewOne += date.Year.ToString().Substring(2);
                //            123456789
                strNewOne += "000000001";
            }
            else
            {
                strNewOne = Common.NewID(dt.Rows[0][0].ToString());
            }
            return strNewOne;
        }

        public string GetEndNo(string StartNo, int length)
        {
            return Common.NewID(StartNo, length);
        }

        public DataTable GetLoadState()
        {
            string strSql = "select MIN(billid) as billid1,MAX(billid) as billid2,MIN(enterpriseid) as  enterpriseid1,max(enterpriseid) as enterpriseid2 ,MIN(BillDate) as BillDate1,MAX(BillDate) as BillDate2,MIN(PreDeliverDate) as PreDeliverDate1,MAX(PreDeliverDate) as PreDeliverDate2 from LB_Ordersub where flag = 1 ";

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            return dt;
        }

        public DataTable GetLoadData(string filter)
        {
            string strSql = "select  RowID,Flag  as OrderFlag,BillID as OrderBillID,SubID as OrderSubID,EnterpriseID,EnterpriseName,StyleID,LabelMode,RealQty,SparesPercent,SparesQty,RemainQty,StartNo,EndNo,PreDeliverDate,Memo,InStockBillID,InStockQty from VLB_Ordersub where flag =1 and State = 0 and exists(SELECT * FROM LB_OrderMain WHERE flag = 1 and billid=VLB_Ordersub.billid and (LabelFrom = 0)) ";
            if (filter.Trim().Length > 0)
                strSql += filter;

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            return dt;
        }
        public void ChangeScheduleEstimate(string id) //shj
        {
            string strSql = string.Format("update LB_Ordersub set IsEstimate = case when IsEstimate =1 then 0 else 1 end  where flag =2 and  BillID = '{0}'", id);
            ia.ExecuteDataSetSql(strSql);
            strSql = string.Format("update lb_ordermain set BillState = 2 where flag =2 and  BillID = '{0}'", id);
            ia.ExecuteDataSetSql(strSql);
        }
    }
}
