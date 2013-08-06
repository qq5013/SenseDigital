using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Js.DAO.BusinessUnit
{
    public class CheckDao
    {
        string cnKey = "";
        DBAccessLayer.IDBAccess ia;
        DBAccessLayer.IDBAccess ea;

        public CheckDao()
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");
            ea = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");
            this.cnKey = "";
        }
        public CheckDao(string cnKey)
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");
            ea = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
            this.cnKey = cnKey;
        }
        //產品認證
        public void ProductCheck(string EnterpriseID, string CheckUserName)
        {
            string strSql = string.Format("delete from EP_Product where Flag=1 and EnterpriseID='{0}';update EP_Product set Flag=1,BU_CheckDate=getdate(),BU_CheckUserName='{1}' where Flag=2 and EnterpriseID='{0}'", EnterpriseID, CheckUserName);
            ia.ExecuteNonQuerySql(strSql);
            
            //轉到企業用戶庫
            strSql = string.Format("select * from EP_Product where Flag=1 and EnterpriseID='{0}'", EnterpriseID);
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            dt.TableName = "EP_Product";
            for (int i = 0; i < dt.Rows.Count; i++)
                dt.Rows[i]["Flag"] = 5;
            string filter = string.Format("Flag=5 and EnterpriseID='{0}'", EnterpriseID);

            //寫入企業資料庫
            SubBaseDao subDao = new SubBaseDao(EnterpriseID);
            subDao.Save(dt, filter);
            //刪除待驗證的產品
            strSql = string.Format("delete from EP_Product where (Flag=2 or Flag=3) and EnterpriseID='{0}'", EnterpriseID);
            ea.ExecuteNonQuerySql(strSql);

            //原認證產品插入記錄
            strSql = "Insert into EP_ProductRecord select * from EP_Product A where Flag=1 and exists(select * from EP_Product where Flag=5 and EnterpriseID=A.EnterpriseID and ProductID=A.ProductID)";
            ea.ExecuteNonQuerySql(strSql);
            //原認證產品放在Flag=3
            strSql = "update EP_Product set Flag=3 from EP_Product A where Flag=1 and exists(select * from EP_Product where Flag=5 and EnterpriseID=A.EnterpriseID and ProductID=A.ProductID)";
            ea.ExecuteNonQuerySql(strSql);
            
            //新認證的產品寫入
            strSql = string.Format("update EP_Product set Flag=1 where Flag=5 and EnterpriseID='{0}'", EnterpriseID);
            ea.ExecuteNonQuerySql(strSql);
        }
        //產品認證
        public void ProductLogisticsCheck(string EnterpriseID, string CheckUserName)
        {
            string strSql = string.Format("delete from EP_ProductLogistics where Flag=1 and EnterpriseID='{0}';update EP_ProductLogistics set Flag=1,BU_CheckDate=getdate(),BU_CheckUserName='{1}' where Flag=2 and EnterpriseID='{0}'", EnterpriseID, CheckUserName);
            ia.ExecuteNonQuerySql(strSql);
            //轉到企業用戶庫
            strSql = string.Format("select * from EP_ProductLogistics where Flag=1 and EnterpriseID='{0}'", EnterpriseID);
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            dt.TableName = "EP_ProductLogistics";
            for (int i = 0; i < dt.Rows.Count; i++)
                dt.Rows[i]["Flag"] = 5;
            string filter = string.Format("Flag=5 and EnterpriseID='{0}'", EnterpriseID);

            //寫入企業資料庫
            SubBaseDao subDao = new SubBaseDao(EnterpriseID);
            subDao.Save(dt, filter);
            //刪除待驗證的產品
            strSql = string.Format("delete from EP_ProductLogistics where (Flag=2 or Flag=3) and EnterpriseID='{0}'", EnterpriseID);
            ea.ExecuteNonQuerySql(strSql);
            //原認證產品插入記錄
            strSql = "Insert into EP_ProductLogisticsRecord select * from EP_ProductLogistics A where Flag=1 and exists(select * from EP_ProductLogistics where Flag=5 and EnterpriseID=A.EnterpriseID and LogisticsID=A.LogisticsID)";
            ea.ExecuteNonQuerySql(strSql);
            //原認證產品放在Flag=3
            strSql = "update EP_ProductLogistics set Flag=3 from EP_ProductLogistics A where Flag=1 and exists(select * from EP_ProductLogistics where Flag=5 and EnterpriseID=A.EnterpriseID and LogisticsID=A.LogisticsID)";
            ea.ExecuteNonQuerySql(strSql);
            //新認證的產品寫入
            strSql = string.Format("update EP_ProductLogistics set Flag=1 where Flag=5 and EnterpriseID='{0}'", EnterpriseID);
            ea.ExecuteNonQuerySql(strSql);
        }
        //產品認證
        public void ProductResumeCheck(string EnterpriseID, string CheckUserName)
        {
            string strSql = string.Format("delete from EP_ProductResume where Flag=1 and EnterpriseID='{0}';update EP_ProductResume set Flag=1,BU_CheckDate=getdate(),BU_CheckUserName='{1}' where Flag=2 and EnterpriseID='{0}'", EnterpriseID, CheckUserName);
            ia.ExecuteNonQuerySql(strSql);
            //轉到企業用戶庫
            strSql = string.Format("select * from EP_ProductResume where Flag=1 and EnterpriseID='{0}'", EnterpriseID);
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            dt.TableName = "EP_ProductResume";
            for (int i = 0; i < dt.Rows.Count; i++)
                dt.Rows[i]["Flag"] = 5;
            string filter = string.Format("Flag=5 and EnterpriseID='{0}'", EnterpriseID);

            //寫入企業資料庫
            SubBaseDao subDao = new SubBaseDao(EnterpriseID);
            subDao.Save(dt, filter);
            //刪除待驗證的產品
            strSql = string.Format("delete from EP_ProductResume where (Flag=2 or Flag=3) and EnterpriseID='{0}'", EnterpriseID);
            ea.ExecuteNonQuerySql(strSql);
            //原認證產品插入記錄
            strSql = "Insert into EP_ProductResumeRecord select * from EP_ProductResume A where Flag=1 and exists(select * from EP_ProductResume where Flag=5 and EnterpriseID=A.EnterpriseID and ResumeID=A.ResumeID)";
            ea.ExecuteNonQuerySql(strSql);
            //原認證產品放在Flag=3
            strSql = "update EP_ProductResume set Flag=3 from EP_ProductResume A where Flag=1 and exists(select * from EP_ProductResume where Flag=5 and EnterpriseID=A.EnterpriseID and ResumeID=A.ResumeID)";
            ea.ExecuteNonQuerySql(strSql);
            //新認證的產品寫入
            strSql = string.Format("update EP_ProductResume set Flag=1 where Flag=5 and EnterpriseID='{0}'", EnterpriseID);
            ea.ExecuteNonQuerySql(strSql);
        }
        //插入其他上傳記錄
        public void InsertIntoUploadRecord(string UserName, byte UseUnit, byte FileType, string FileName, string FileDesc, string Memo)
        {
            string strSql = string.Format("Insert into Sys_UploadRecord(UserName, UploadDate, UseUnit, FileType, FileName, FileDesc, Memo) select '{0}',getdate(),{1},{2},'{3}','{4}','{5}'", UserName, UseUnit, FileType, FileName, FileDesc, Memo);
            ia.ExecuteNonQuerySql(strSql);
        }
    }
}
