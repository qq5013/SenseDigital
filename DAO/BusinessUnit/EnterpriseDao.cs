using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Js.DAO.BusinessUnit
{
    public class EnterpriseDao
    {
        DBAccessLayer.IDBAccess ia;

        public EnterpriseDao()
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");
        }
        public EnterpriseDao(string cnKey)
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
        }        
        
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.BusinessUnit.EnterpriseInfo GetModel(string EnterpriseID)
        {
            string strSql = string.Format("select * from Com_Enterprise Where EnterpriseID='{0}'", EnterpriseID);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            Js.Model.BusinessUnit.EnterpriseInfo model = new Js.Model.BusinessUnit.EnterpriseInfo();
            if (dt.Rows.Count != 0)
            {
                model.EnterpriseID = dt.Rows[0]["EnterpriseID"].ToString();
                model.EnterpriseName = dt.Rows[0]["EnterpriseName"].ToString();
                model.EnterpriseEName = dt.Rows[0]["EnterpriseEName"].ToString();
                model.CategoryID = dt.Rows[0]["CategoryID"].ToString();
                model.President = dt.Rows[0]["President"].ToString();
                model.PresidentPost = dt.Rows[0]["PresidentPost"].ToString();
                model.WebUrl = dt.Rows[0]["WebUrl"].ToString();
                model.Phone = dt.Rows[0]["Phone"].ToString();
                model.Memo = dt.Rows[0]["Memo"].ToString();
                model.Email = dt.Rows[0]["Email"].ToString();
                model.CellPhone = dt.Rows[0]["CellPhone"].ToString();
                model.Contact = dt.Rows[0]["Contact"].ToString();
                model.ContactPhone = dt.Rows[0]["ContactPhone"].ToString();
                model.ContactPost = dt.Rows[0]["ContactPost"].ToString();
                model.ServiceYears = byte.Parse(dt.Rows[0]["ServiceYears"].ToString());
                model.EnableMonths = byte.Parse(dt.Rows[0]["EnableMonths"].ToString());
                model.Address = dt.Rows[0]["Address"].ToString();
                model.ZipNo = dt.Rows[0]["ZipNo"].ToString();
                if(dt.Rows[0]["CreateDate"].ToString().Length>0)
                    model.CreateDate = DateTime.Parse(dt.Rows[0]["CreateDate"].ToString());
                model.CreateUserName = dt.Rows[0]["CreateUserName"].ToString();
                if (dt.Rows[0]["LastModifyDate"].ToString().Length > 0)
                    model.LastModifyDate = DateTime.Parse(dt.Rows[0]["LastModifyDate"].ToString());
                model.LastModifyUserName = dt.Rows[0]["LastModifyUserName"].ToString();
                if (dt.Rows[0]["CheckDate"].ToString().Length > 0)
                    model.CheckDate = DateTime.Parse(dt.Rows[0]["CheckDate"].ToString());                
                model.CheckUserName = dt.Rows[0]["CheckUserName"].ToString();
            }
            return model;
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public void InsertModifyRecord(string EnterpriseID,byte ServiceYears,byte EnableMonths)
        {
            string strSql = string.Format("Insert into Com_EnterpriseModifyRecord(EnterpriseID, ModifyDate, ServiceYears, EnableMonths) select '{0}',getdate(),{1},{2}", EnterpriseID, ServiceYears, EnableMonths);
            ia.ExecuteNonQuerySql(strSql);            
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public DataTable GetModifyRecord(string EnterpriseID)
        {
            string strSql = string.Format("select * from Com_EnterpriseModifyRecord where EnterpriseID='{0}' Order by ModifyDate desc", EnterpriseID);
            return ia.ExecuteDataSetSql(strSql).Tables[0];
        }
    }
}

