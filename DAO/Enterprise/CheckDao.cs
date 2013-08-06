using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace Js.DAO.Enterprise
{
    public class CheckDao
    {
        DBAccessLayer.IDBAccess ia;
        DBAccessLayer.IDBAccess ea;
        Js.Model.Sys.TreeListInfo SysModel;
        string cnKey = "";

        public CheckDao()
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");
            ea = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");            
        }
        public CheckDao(string FormID,string cnKey)
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");
            ea = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);

            this.cnKey = cnKey;
            Sys.TreeListDao dao = new Sys.TreeListDao();
            SysModel = dao.GetModel(FormID);
        }
        //未認證企業覆核
        public void Check(string EnterpriseID, string CheckUserName)
        {
            string strSql = string.Format("Update {0} set EP_CheckDate=getdate(),EP_CheckUserName='{1}',State=2 where Flag=2 and EnterpriseID='{2}' and State=1", SysModel.TableName, CheckUserName, EnterpriseID);
            ea.ExecuteNonQuerySql(strSql);
        }
        //未認證取消企業覆核
        public void UnCheck(string EnterpriseID)
        {
            //上傳的營運的資料刪除
            string strSql = string.Format("delete from {0} where Flag=2 and EnterpriseID='{1}' and State=3", SysModel.TableName, EnterpriseID);
            ia.ExecuteNonQuerySql(strSql);

            strSql = string.Format("Update {0} set EP_CheckDate=null,EP_CheckUserName='',State=1,UploadUserName='',UploadDate=null where Flag=2 and EnterpriseID='{1}' and (State=2 or State=3)", SysModel.TableName, EnterpriseID);
            ea.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 修改記錄
        /// </summary>
        /// <returns></returns>
        public DataTable GetModifyRecord(string FormID,string filter)
        {
            string TableName = FormID + "Record";

            string strSql = string.Format("Select * from {0} Where Flag=1 and {1} Order by  BU_CheckDate Desc", TableName, filter);
            return ea.ExecuteDataSetSql(strSql).Tables[0];
        }
        /// <summary>
        /// 企業用戶上傳記錄
        /// </summary>
        /// <returns></returns>
        public DataTable GetUploadEmptyRecord()
        {
            string strSql = "Select * from EP_UploadRecord Where 1=2";
            return ea.ExecuteDataSetSql(strSql).Tables[0];
        }
        /// <summary>
        /// 插入企業用戶上傳記錄
        /// </summary>
        /// <param name="Record"></param>
        /// <param name="HostProcess"></param>
        public void InsertUploadRrecord(DataRow dr)
        {
            string strSql = string.Format("insert into dbo.EP_UploadRecord(EnterpriseID, EnterpriseName, UserName, UploadPath, UploadDate) " +
                            "values ('{0}','{1}','{2}','{3}',getdate())",
                            dr["EnterpriseID"].ToString(), dr["EnterpriseName"].ToString(), dr["UserName"].ToString(), dr["UploadPath"].ToString());


            ea.ExecuteNonQuerySql(strSql.ToString());
        }
        //企業產品已比對檢查完更新狀態
        public void UpdateCheckState(string EnterpriseID)
        {
            string strSql = string.Format("Update {0} set State=1 Where Flag=2 and EnterpriseID='{1}' and State=0", SysModel.TableName, EnterpriseID);
            ea.ExecuteNonQuerySql(strSql);            
        }
        //上傳到營運庫的時候進行判斷
        //營運里是否還有未認證的資料存在
        public bool IsExiststNoCheck(string EnterpriseID)
        {
            string strSql = string.Format("select top 1 * from {0} Where Flag=2 and EnterpriseID='{1}'", SysModel.TableName, EnterpriseID);
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        //企業未認證的是否已經覆核
        public bool IsEnterpriseChecked(string EnterpriseID)
        {
            string strSql = string.Format("select top 1 * from {0} Where Flag=2 and EnterpriseID='{1}' and EP_CheckUserName<>''", SysModel.TableName, EnterpriseID);
            DataTable dt = ea.ExecuteDataSetSql(strSql).Tables[0];
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        //轉入未認證產品資料
        public DataTable test(string EnterpriseID, string UserName)
        {

            string strFileName = ConfigurationManager.AppSettings["ImportPath"] + EnterpriseID + @"\Product\Product.xls";
            //產生臨時用戶表
            string strSql = string.Format("SELECT * FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0', 'Excel 12.0;HDR=Yes;IMEX=1;Database=" + strFileName + "', 'select * from [Product$]')");
            DataTable dt = ea.ExecuteDataSetSql(strSql).Tables[0];
            return dt;
        }
        //轉入未認證產品資料
        public void ImportProduct(string EnterpriseID, string UserName)
        {
            string strSql = string.Format("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[{0}_Product]') and type in (N'U')) drop table {0}_Product", UserName);
            ea.ExecuteNonQuerySql(strSql);

            string strFileName = ConfigurationManager.AppSettings["ImportPath"] + EnterpriseID + @"\Product\Product.xls";
            //產生臨時用戶表
            strSql = string.Format("SELECT * into {0}_Product FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0', 'Excel 12.0;HDR=Yes;IMEX=1;Database=" + strFileName + "', 'select * from [Product$]')",UserName);
            strSql = string.Format("SELECT * FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0', 'Excel 12.0;HDR=Yes;IMEX=1;Database=" + strFileName + "', 'select * from [Product$]')");
            DataTable dt = ea.ExecuteDataSetSql(strSql).Tables[0];
            ea.ExecuteNonQuerySql(strSql);
            
            //先轉入到flag=10資料
            strSql = string.Format("delete from Ep_Product where flag=10;insert into EP_Product(Flag, EnterpriseID, EnterpriseName,ProductID, ProductName, ProductEName, GroupName, ClassName, " + 
                            "Barcode1, Barcode2, Description, OfficalUrl, ServicePhone, Memo,CreateDate, CreateUserName, LastModifyDate, LastModifyUserName) " +
                            "SELECT 10, isnull(EnterpriseID,''), isnull(EnterpriseName,''),isnull(ProductID,''), isnull(ProductName,''), isnull(ProductEName,''), isnull(GroupName,''), " +
                            "isnull(ClassName,''), isnull(Barcode1,''), isnull(Barcode2,''), isnull(Description,''), isnull(OfficalUrl,''), isnull(ServicePhone,''), isnull(Memo,''), getdate(),'{0}',getdate(),'{0}' " + 
                            "FROM {0}_Product",UserName);

            ea.ExecuteNonQuerySql(strSql);

            strSql = string.Format("delete from Ep_Product where Flag=2 and EnterpriseID='{0}' and ProductID in (select ProductID from Ep_Product where Flag=10 and EnterpriseID='{0}');Update EP_Product set Flag=2 where Flag=10 and EnterpriseID='{0}'", EnterpriseID);
            ea.ExecuteNonQuerySql(strSql);
        }
        //轉入未認證產品資料
        public DataTable ImportProduct1(string EnterpriseID, string UserName)
        {
            string strFileName = ConfigurationManager.AppSettings["UploadPath"] + EnterpriseID + @"\Product\Product.xls";
            string OleConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";Extended Properties='Excel 12.0 Xml;HDR=Yes;'";
            OleDbConnection OleCn = new OleDbConnection(OleConStr);
            OleCn.Open();
            OleDbCommand OleCmd = new OleDbCommand();
            OleCmd.Connection = OleCn;
            string strSQL = @"SELECT * FROM [Product$]";
            OleCmd.CommandText = strSQL;
            OleDbDataAdapter oda = new OleDbDataAdapter(OleCmd);
            DataSet ds = new DataSet();
            oda.Fill(ds);

            OleCn.Close();
            return ds.Tables[0];     
        }
        //獲取未認證產品臨時表
        public DataTable GetProductTemp(string filter)
        {
            string strSql = string.Format("select * from EP_ProductTemp where {0}", filter);
            DataTable dt = ea.ExecuteDataSetSql(strSql).Tables[0];
            dt.TableName = "EP_ProductTemp";
            return dt;
        }
        //寫入EP_Product
        public void SaveProduct(DataTable dt, string UserName)
        {            
            string filter = string.Format("UserName='{0}'", UserName);
            SubBaseDao subDao = new SubBaseDao(cnKey);
            subDao.Save(dt, filter);
            //更新創建用戶、日期
            string strSql = "update EP_ProductTemp set CreateDate=EP_Product.CreateDate,CreateUserName=EP_Product.CreateUserName from EP_ProductTemp inner join EP_Product On EP_ProductTemp.EnterpriseID=EP_Product.EnterpriseID and EP_ProductTemp.ProductID=EP_Product.ProductID and EP_Product.Flag=1";
            ea.ExecuteNonQuerySql(strSql);
            
            strSql = string.Format("delete from EP_Product where Flag=2 and exists(select * from EP_ProductTemp where EnterpriseID = EP_Product.EnterpriseID and ProductID=EP_Product.ProductID and UserName='{0}')", UserName);
            ea.ExecuteNonQuerySql(strSql);

            strSql = string.Format("insert into EP_Product(Flag, EnterpriseID, EnterpriseName, ProductID, ProductName, ProductEName, GroupName, ClassName, " +
                                   "Barcode1, Barcode2, Description, OfficalUrl, ServicePhone, Memo, CreateDate, CreateUserName, LastModifyDate, LastModifyUserName) " +
                                   "select 2,EnterpriseID, EnterpriseName, ProductID, ProductName, ProductEName, GroupName, ClassName, " +
                                   "Barcode1, Barcode2, Description, OfficalUrl, ServicePhone, Memo, CreateDate, CreateUserName, getdate(), '{0}' " +
                                   "from Ep_ProductTemp where UserName='{0}' and EnterpriseID<>''", UserName);
            ea.ExecuteNonQuerySql(strSql);

            //刪除臨時資料
            strSql = string.Format("delete from Ep_ProductTemp where UserName='{0}'", UserName);
            ea.ExecuteNonQuerySql(strSql);
        }
        //轉入未認證產品資料
        public void ImportProductResume(string EnterpriseID, string UserName)
        {
            string strSql = string.Format("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[{0}_ProductResume]') and type in (N'U')) drop table {0}_ProductResume", UserName);
            ea.ExecuteNonQuerySql(strSql);

            string strFileName = ConfigurationManager.AppSettings["ImportPath"] + EnterpriseID + @"\Resume\Prod_resume.xls";

            //產生臨時用戶表
            strSql = string.Format("SELECT * into {0}_ProductResume FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0', 'Excel 12.0;HDR=Yes;IMEX=1;Database=" + strFileName + "', 'select * from [Resume$]')", UserName);
            ea.ExecuteNonQuerySql(strSql);

            //先轉入到flag=10資料
            strSql = string.Format("delete from EP_ProductResume where flag=10;insert into EP_ProductResume(Flag, EnterpriseID, ResumeID,ProductID, ProductName, ProductEName, GroupName, ClassName, " +
                            "Barcode1, Barcode2, Description, OfficalUrl, ServicePhone, Memo,CreateDate, CreateUserName, LastModifyDate, LastModifyUserName) " +
                            "SELECT 10, isnull(EnterpriseID,''), isnull(ProductID,''), isnull(ProductName,''), isnull(ProductEName,''), isnull(GroupName,''), isnull(ClassName,''), isnull(Barcode1,''), " +
                            "isnull(Barcode2,''), isnull(Description,''), isnull(OfficalUrl,''), isnull(ServicePhone,''), isnull(Memo,''), getdate(), '{0}', getdate(), '{0}' " +
                            "FROM {0}_ProductResume", UserName);

            ea.ExecuteNonQuerySql(strSql);
            strSql = string.Format("delete from Ep_ProductResume where Flag=2 and EnterpriseID='{0}' and ResumeID in (select ResumeID from Ep_ProductResume where Flag=10 and EnterpriseID='{0}');Update EP_Product set Flag=2 where Flag=10 and EnterpriseID='{0}'", EnterpriseID);
            ea.ExecuteNonQuerySql(strSql);
        }
        //轉入未認證產品資料
        public DataTable ImportProductResume1(string EnterpriseID, string UserName)
        {
            string strFileName = ConfigurationManager.AppSettings["UploadPath"] + EnterpriseID + @"\Resume\Prod_resume.xls";
            string OleConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";Extended Properties='Excel 12.0 Xml;HDR=Yes;'";
            OleDbConnection OleCn = new OleDbConnection(OleConStr);
            OleCn.Open();
            OleDbCommand OleCmd = new OleDbCommand();
            OleCmd.Connection = OleCn;
            string strSQL = @"SELECT * FROM [ProductResume$]";
            OleCmd.CommandText = strSQL;
            OleDbDataAdapter oda = new OleDbDataAdapter(OleCmd);
            DataSet ds = new DataSet();
            oda.Fill(ds);
            OleCn.Close();

            return ds.Tables[0];
        }
        //獲取未認證產品臨時表
        public DataTable GetProductResumeTemp(string filter)
        {
            string strSql = string.Format("select * from EP_ProductResumeTemp where {0}", filter);
            DataTable dt = ea.ExecuteDataSetSql(strSql).Tables[0];
            dt.TableName = "EP_ProductResumeTemp";
            return dt;
        }
        //寫入EP_Product
        public void SaveProductResume(DataTable dt, string UserName)
        {
            string filter = string.Format("UserName='{0}'", UserName);
            SubBaseDao subDao = new SubBaseDao(cnKey);
            subDao.Save(dt, filter);

            //更新創建用戶、日期
            string strSql = "update EP_ProductResumeTemp set CreateDate=EP_ProductResume.CreateDate,CreateUserName=EP_ProductResume.CreateUserName from EP_ProductResumeTemp inner join EP_ProductResume On EP_ProductResumeTemp.EnterpriseID=EP_ProductResume.EnterpriseID and EP_ProductResumeTemp.ResumeID=EP_ProductResume.ResumeID and EP_ProductResume.Flag=1";
            ea.ExecuteNonQuerySql(strSql);

            strSql = string.Format("delete from EP_ProductResume where Flag=2 and exists(select * from EP_ProductResumeTemp where EnterpriseID = EP_ProductResume.EnterpriseID and ResumeID=EP_ProductResume.ResumeID and UserName='{0}')", UserName);
            ea.ExecuteNonQuerySql(strSql);

            //過濾存在的產品
            strSql = string.Format("insert into EP_ProductResume(Flag, EnterpriseID, EnterpriseName, ResumeID, ProductID, ProductName, ProduceDate, HasGarantee, GaranteeDate, " +
                                   "OfficalUrl, ServicePhone, Description, ResumeOfficalUrl, EPResumeID, CreateDate, CreateUserName, LastModifyDate, LastModifyUserName) " +
                                   "select 2,A.EnterpriseID, A.EnterpriseName, A.ResumeID, A.ProductID, B.ProductName, A.ProduceDate, A.HasGarantee, A.GaranteeDate, " +
                                   "A.OfficalUrl, A.ServicePhone, A.Description, A.ResumeOfficalUrl, A.EPResumeID, A.CreateDate, A.CreateUserName, getdate(), '{0}' " +
                                   "from Ep_ProductResumeTemp A " + 
                                   "inner join EP_Product B On A.EnterpriseID=B.EnterpriseID and A.ProductID=B.ProductID and B.Flag=1 " +
                                   "where A.UserName='{0}' and A.EnterpriseID<>''", UserName);
            ea.ExecuteNonQuerySql(strSql);

            //刪除臨時資料
            strSql = string.Format("delete from Ep_ProductResumeTemp where UserName='{0}'", UserName);
            ea.ExecuteNonQuerySql(strSql);
        }
        //轉入未認證產品物流資訊資料
        public void ImportProductLogistics(string EnterpriseID, string UserName)
        {
            string strSql = string.Format("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[{0}_ProductLogistics]') and type in (N'U')) drop table {0}_ProductLogistics", UserName);
            ea.ExecuteNonQuerySql(strSql);

            string strFileName = ConfigurationManager.AppSettings["ImportPath"] + EnterpriseID + @"\Logistics\Prod_Logistics.xls";

            //產生臨時用戶表
            strSql = string.Format("SELECT * into {0}_ProductLogistics FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0', 'Excel 12.0;HDR=Yes;IMEX=1;Database=" + strFileName + "', 'select * from [EP_Product$]')", UserName);
            ea.ExecuteNonQuerySql(strSql);
                        
            //先轉入到flag=10資料
            strSql = string.Format("delete from Ep_ProductLogistics where flag=10;insert into Ep_ProductLogistics(Flag, EnterpriseID, ProductID, ProductName, ProductEName, GroupName, ClassName, " +
                            "Barcode1, Barcode2, Description, OfficalUrl, ServicePhone, Memo,CreateDate, CreateUserName, LastModifyDate, LastModifyUserName) " +
                            "SELECT 10, isnull(EnterpriseID,''), isnull(ProductID,''), isnull(ProductName,''), isnull(ProductEName,''), isnull(GroupName,''), isnull(ClassName,''), isnull(Barcode1,''), " +
                            "isnull(Barcode2,''), isnull(Description,''), isnull(OfficalUrl,''), isnull(ServicePhone,''), isnull(Memo,''), getdate(), '{0}', getdate(), '{0}' " +
                            "FROM {0}_ProductLogistics", UserName);

            ea.ExecuteNonQuerySql(strSql);
            strSql = string.Format("delete from Ep_ProductLogistics where Flag=2 and EnterpriseID='{0}' and LogisticsID in (select LogisticsID from Ep_ProductLogistics where Flag=10 and EnterpriseID='{0}');Update EP_Product set Flag=2 where Flag=10 and EnterpriseID='{0}'", EnterpriseID);
            ea.ExecuteNonQuerySql(strSql);
        }
        //轉入未認證產品資料
        public DataTable ImportProductLogistics1(string EnterpriseID, string UserName)
        {
            string strFileName = ConfigurationManager.AppSettings["UploadPath"] + EnterpriseID + @"\Logistics\Prod_Logistics.xls";
            string OleConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";Extended Properties='Excel 12.0 Xml;HDR=Yes;'";
            OleDbConnection OleCn = new OleDbConnection(OleConStr);
            OleCn.Open();
            OleDbCommand OleCmd = new OleDbCommand();
            OleCmd.Connection = OleCn;
            string strSQL = @"SELECT * FROM [ProductLogistics$]";
            OleCmd.CommandText = strSQL;
            OleDbDataAdapter oda = new OleDbDataAdapter(OleCmd);
            DataSet ds = new DataSet();
            oda.Fill(ds);

            OleCn.Close();
            return ds.Tables[0];
        }
        //獲取未認證產品臨時表
        public DataTable GetProductLogisticsTemp(string filter)
        {
            string strSql = string.Format("select * from EP_ProductLogisticsTemp where {0}", filter);
            DataTable dt = ea.ExecuteDataSetSql(strSql).Tables[0];
            dt.TableName = "EP_ProductLogisticsTemp";
            return dt;
        }
        //寫入EP_Product
        public void SaveProductLogistics(DataTable dt, string UserName)
        {
            string filter = string.Format("UserName='{0}'", UserName);
            SubBaseDao subDao = new SubBaseDao(cnKey);
            subDao.Save(dt, filter);
            
            //更新創建用戶、日期
            string strSql = "update EP_ProductLogisticsTemp set CreateDate=EP_ProductLogistics.CreateDate,CreateUserName=EP_ProductLogistics.CreateUserName from EP_ProductLogisticsTemp inner join EP_ProductLogistics On EP_ProductLogisticsTemp.EnterpriseID=EP_ProductLogistics.EnterpriseID and EP_ProductLogisticsTemp.LogisticsID=EP_ProductLogistics.LogisticsID and EP_ProductLogisticsTemp.Flag=1";
            ea.ExecuteNonQuerySql(strSql);
            
            strSql = string.Format("delete from EP_ProductLogistics where Flag=2 and exists(select * from EP_ProductLogisticsTemp where EnterpriseID = EP_ProductLogistics.EnterpriseID and LogisticsID=EP_ProductLogistics.LogisticsID and UserName='{0}')", UserName);
            ea.ExecuteNonQuerySql(strSql);

            strSql = string.Format("insert into EP_ProductLogistics( Flag, EnterpriseID, EnterpriseName, LogisticsID, ProductID, ProductName, Area, Description, " + 
                                   "EPLogisticsID, LogisticsOfficalUrl, CreateDate, CreateUserName, LastModifyDate, LastModifyUserName) " +
                                   "select 2,A.EnterpriseID, A.EnterpriseName, A.LogisticsID, A.ProductID, B.ProductName, A.Area, A.Description, " +
                                   "A.EPLogisticsID, A.LogisticsOfficalUrl, A.CreateDate, A.CreateUserName, getdate(), '{0}' " +
                                   "from EP_ProductLogisticsTemp A " +
                                   "inner join EP_Product B On A.EnterpriseID=B.EnterpriseID and A.ProductID=B.ProductID and B.Flag=1 " +
                                   "where A.UserName='{0}' and A.EnterpriseID<>''", UserName);
            ea.ExecuteNonQuerySql(strSql);

            //刪除臨時資料
            strSql = string.Format("delete from EP_ProductLogisticsTemp where UserName='{0}'", UserName);
            ea.ExecuteNonQuerySql(strSql);
        }
        //上傳到營運庫
        public void UpoladBusinessUnit(string EnterpriseID, string UserName)
        {
            string strSql = string.Format("select * from {0} where Flag=2 and EnterpriseID='{1}'", SysModel.TableName, EnterpriseID);
            DataTable dt = ea.ExecuteDataSetSql(strSql).Tables[0];
            dt.TableName = SysModel.TableName;

            string filter = string.Format("Flag=2 and EnterpriseID='{0}'", EnterpriseID);

            //寫入營運資料庫
            SubBaseDao subDao = new SubBaseDao();
            subDao.Save(dt, filter);

            //更新上傳人員上傳日期
            strSql = string.Format("update {0} set UploadUserName='{1}',UploadDate=getdate(),State=3 where Flag=2 and EnterpriseID='{2}' and State=2", SysModel.TableName, UserName,EnterpriseID);
            ea.ExecuteNonQuerySql(strSql);
            ia.ExecuteNonQuerySql(strSql);
        }
        ///獲取企業用戶管理用戶
        ///
        public DataTable GetManagerUser(string EnterpriseID)
        {
            string strSql = string.Format("select distinct Accounts_UserManagerSub.UserName,Accounts_Users.PersonName from Accounts_UserManagerSub Left join Accounts_Users On Accounts_UserManagerSub.UserName=Accounts_Users.UserName where Accounts_UserManagerSub.EnterpriseID='{0}'", EnterpriseID);
            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];

            return dt;
            
        }
    }
}
