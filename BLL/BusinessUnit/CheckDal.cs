using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Js.DAO.BusinessUnit;

namespace Js.BLL.BusinessUnit
{
    public class CheckDal
    {
        CheckDao dao;
        public CheckDal()
        {
            dao = new CheckDao();
        }
        public CheckDal(string cnKey)
        {
            dao = new CheckDao(cnKey);
        }
        //產品認證
        public void ProductCheck(string EnterpriseID, string CheckUserName)
        {
            dao.ProductCheck(EnterpriseID, CheckUserName);
        }
        //產品認證
        public void ProductLogisticsCheck(string EnterpriseID, string CheckUserName)
        {
            dao.ProductLogisticsCheck(EnterpriseID, CheckUserName);
        }
        //產品認證
        public void ProductResumeCheck(string EnterpriseID, string CheckUserName)
        {
            dao.ProductResumeCheck(EnterpriseID, CheckUserName);
        }
        //插入其他上傳記錄
        public void InsertIntoUploadRecord(string UserName, byte UseUnit, byte FileType, string FileName, string FileDesc, string Memo)
        {
            dao.InsertIntoUploadRecord(UserName, UseUnit, FileType, FileName, FileDesc, Memo);
        }
    }
}
