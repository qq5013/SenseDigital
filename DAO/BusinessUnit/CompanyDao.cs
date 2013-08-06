using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Js.DAO.BusinessUnit
{
    public class CompanyDao
    {
        DBAccessLayer.IDBAccess ia;

        public CompanyDao()
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");
        }
        public CompanyDao(string cnKey)
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
        }

        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.BusinessUnit.CompanyInfo GetModel()
        {
            string strSql = string.Format("select * from Sys_Company");

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            Js.Model.BusinessUnit.CompanyInfo model = new Js.Model.BusinessUnit.CompanyInfo();
            if (dt.Rows.Count != 0)
            {
                model.CompanyNo = dt.Rows[0]["CompanyNo"].ToString();
                model.CompanyName = dt.Rows[0]["CompanyName"].ToString();
                model.UnionID = dt.Rows[0]["UnionID"].ToString();
                model.President = dt.Rows[0]["President"].ToString();
                model.Phone = dt.Rows[0]["Phone"].ToString();
                model.Fax = dt.Rows[0]["Fax"].ToString();
                model.RegisterAddress = dt.Rows[0]["RegisterAddress"].ToString();
                model.WebUrl = dt.Rows[0]["WebUrl"].ToString();
                model.EnglishName = dt.Rows[0]["EnglishName"].ToString();
                model.EnglishAddress = dt.Rows[0]["EnglishAddress"].ToString();
                //model.CreateDate = dt.Rows[0]["CreateDate"].ToString();
                //model.CreateUserName = dt.Rows[0]["CreateUserName"].ToString();
                //model.LastModifyDate = dt.Rows[0]["LastModifyDate"].ToString();
                //model.LastModifyUserName = dt.Rows[0]["LastModifyUserName"].ToString();
                //model.CheckDate = dt.Rows[0]["CheckDate"].ToString();
                //model.CheckUserName = dt.Rows[0]["CheckUserName"].ToString();

                if (dt.Rows[0]["CreateDate"].ToString().Length > 0)
                    model.CreateDate = DateTime.Parse(dt.Rows[0]["CreateDate"].ToString());
                model.CreateUserName = dt.Rows[0]["CreateUserName"].ToString();
                if (dt.Rows[0]["LastModifyDate"].ToString().Length > 0)
                    model.LastModifyDate = DateTime.Parse(dt.Rows[0]["LastModifyDate"].ToString());
                model.LastModifyUserName = dt.Rows[0]["LastModifyUserName"].ToString();
                //if (dt.Rows[0]["CheckDate"].ToString().Length > 0)
                //    model.CheckDate = DateTime.Parse(dt.Rows[0]["CheckDate"].ToString());
                //model.CheckUserName = dt.Rows[0]["CheckUserName"].ToString();
            }
            return model;
        }
        /// <summary>
        /// 更新公司資料
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public void Update(Js.Model.BusinessUnit.CompanyInfo model,string No)
        {
            string strSql = "select * from Sys_Company";
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {

                strSql = string.Format("Update Sys_Company set CompanyNo='{0}', CompanyName='{1}', UnionID='{2}', President='{3}', Phone='{4}', Fax='{5}', " +
                            "RegisterAddress='{6}', WebUrl='{7}', EnglishName='{8}', EnglishAddress='{9}', CreateDate={10}, CreateUserName='{11}'," +
                            "LastModifyDate={12},LastModifyUserName='{13}'",
                            model.CompanyNo, model.CompanyName, model.UnionID, model.President, model.Phone, model.Fax, model.RegisterAddress,
                            model.WebUrl, model.EnglishName, model.EnglishAddress,
                            model.CreateDate, model.CreateUserName, model.LastModifyDate, model.LastModifyUserName);
            }
            else
            {

                strSql = string.Format("insert into Sys_Company( CompanyNo, CompanyName, UnionID, President, Phone, Fax, " +
                                "RegisterAddress, WebUrl, EnglishName, EnglishAddress, CreateDate, CreateUserName,LastModifyDate, " +
                                "LastModifyUserName, CheckDate, CheckUserName) values " +
                                "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},'{11}',{12},'{13}'",
                                model.CompanyNo, model.CompanyName, model.UnionID, model.President, model.Phone, model.Fax, model.RegisterAddress,
                                model.WebUrl, model.EnglishName, model.EnglishAddress,
                                model.CreateDate, model.CreateUserName, model.LastModifyDate, model.LastModifyUserName);
            }
            ia.ExecuteNonQuerySql(strSql);            
        }
    }
}
