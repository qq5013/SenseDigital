using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Js.DAO.BusinessUnit
{
    public class ParameterDao
    {
        DBAccessLayer.IDBAccess ia;

        public ParameterDao()
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");
        }
        public ParameterDao(string cnKey)
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
        }

        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.BusinessUnit.ParameterInfo GetModel()
        {
            string strSql = string.Format("select * from Sys_Parameter");

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            Js.Model.BusinessUnit.ParameterInfo model = new Js.Model.BusinessUnit.ParameterInfo();
            if (dt.Rows.Count != 0)
            {
                model.OrderCode = dt.Rows[0]["OrderCode"].ToString();
                model.ScheduleCode = dt.Rows[0]["ScheduleCode"].ToString();
                model.ProductionCode = dt.Rows[0]["ProductionCode"].ToString();
                model.InStockCode = dt.Rows[0]["InStockCode"].ToString();
                model.InvalidLabelCode = dt.Rows[0]["InvalidLabelCode"].ToString();
                model.DeliverCode = dt.Rows[0]["DeliverCode"].ToString();
                model.ReturnLabelCode = dt.Rows[0]["ReturnLabelCode"].ToString();
                model.TransferCode = dt.Rows[0]["TransferCode"].ToString();
                model.EnableLabelNoCode = dt.Rows[0]["EnableLabelNoCode"].ToString();
                model.LabelNoActionCode = dt.Rows[0]["LabelNoActionCode"].ToString();
                model.BatchActionCode = dt.Rows[0]["BatchActionCode"].ToString();
                model.InfoRegisterCode = dt.Rows[0]["InfoRegisterCode"].ToString();
                model.BatchInfoRegisterCode = dt.Rows[0]["BatchInfoRegisterCode"].ToString();
                model.ServiceYears = byte.Parse(dt.Rows[0]["ServiceYears"].ToString());
                model.PercentDecimalDigits = byte.Parse(dt.Rows[0]["PercentDecimalDigits"].ToString());
                model.ClearDate = DateTime.Parse(dt.Rows[0]["ClearDate"].ToString());
                model.CloseDate = DateTime.Parse(dt.Rows[0]["CloseDate"].ToString());
                model.LastModifyDate = DateTime.Parse(dt.Rows[0]["LastModifyDate"].ToString());
                model.LastModifyUserName = dt.Rows[0]["LastModifyUserName"].ToString();
            }
            return model;
        }
        /// <summary>
        /// 更新關賬日期
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public void UpdateCloseDate(Js.Model.BusinessUnit.ParameterInfo model)
        {
            string strSql = string.Format("Update Sys_Parameter set CloseDate='{0}',LastModifyDate='{1}',LastModifyUserName='{2}'",
                            model.CloseDate.ToString("yyyy/MM/dd"), model.LastModifyDate.ToString("yyyy/MM/dd HH:mm"), model.LastModifyUserName);
            
            ia.ExecuteNonQuerySql(strSql);
        }
    }
}

