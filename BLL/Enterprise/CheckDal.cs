using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using Js.DAO.Enterprise;

namespace Js.BLL.Enterprise
{
    public class CheckDal
    {
        CheckDao dao;
        public CheckDal()
        {
            dao = new CheckDao();
        }
        public CheckDal(string FormID,string cnKey)
        {
            dao = new CheckDao(FormID,cnKey);
        }

        public void Check(string EnterpriseID, string CheckUserName)
        {
            dao.Check(EnterpriseID, CheckUserName);
        }
        public void UnCheck(string EnterpriseID)
        {
            dao.UnCheck(EnterpriseID);
        }
        /// <summary>
        /// 修改記錄
        /// </summary>
        /// <returns></returns>
        public DataTable GetModifyRecord(string FormID,string filter)
        {
            return dao.GetModifyRecord(FormID,filter);
        }
        /// <summary>
        /// 企業用戶上傳記錄
        /// </summary>
        /// <returns></returns>
        public DataTable GetUploadEmptyRecord()
        {
            return dao.GetUploadEmptyRecord();
        }
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="Record"></param>
        /// <param name="HostProcess"></param>
        public void InsertUploadRrecord(DataRow dr)
        {
            dao.InsertUploadRrecord(dr);
        }
        //企業產品已比對檢查完更新狀態
        public void UpdateCheckState(string EnterpriseID)
        {
            dao.UpdateCheckState(EnterpriseID);
        }
        //上傳到營運庫的時候進行判斷
        //營運里是否還有未認證的資料存在
        public bool IsExiststNoCheck(string EnterpriseID)
        {
            return dao.IsExiststNoCheck(EnterpriseID);
        }
        //企業未認證的是否已經覆核
        public bool IsEnterpriseChecked(string EnterpriseID)
        {
            return dao.IsEnterpriseChecked(EnterpriseID);
        }
        
        //轉入Excel資料到未認證產品
        public void ImportProduct(string EnterpriseID, string UserName)
        {
            dao.ImportProduct(EnterpriseID, UserName);
        }
        //轉入未認證產品資料
        public DataTable ImportProduct1(string EnterpriseID, string UserName)
        {
            return dao.ImportProduct1(EnterpriseID, UserName);
        }
        //獲取未認證產品臨時表
        public DataTable GetProductTemp(string filter)
        {
            return dao.GetProductTemp(filter);
        }
        public void SaveProduct(DataTable dt, string UserName)
        {
            dao.SaveProduct(dt, UserName);
        }
        //轉入Excel資料到未認證產品履歷
        public void ImportProductResume(string EnterpriseID, string UserName)
        {
            dao.ImportProductResume(EnterpriseID, UserName);
        }
        //轉入未認證產品資料
        public DataTable ImportProductResume1(string EnterpriseID, string UserName)
        {
            return dao.ImportProductResume1(EnterpriseID, UserName);
        }
        //獲取未認證產品臨時表
        public DataTable GetProductResumeTemp(string filter)
        {
            return dao.GetProductResumeTemp(filter);
        }
        public void SaveProductResume(DataTable dt, string UserName)
        {
            dao.SaveProductResume(dt, UserName);
        }
        //轉入Excel資料到未認證產品物流資訊
        public void ImportProductLogistics(string EnterpriseID, string UserName)
        {
            dao.ImportProductLogistics(EnterpriseID, UserName);
        }
        //轉入未認證產品資料
        public DataTable ImportProductLogistics1(string EnterpriseID, string UserName)
        {
            return dao.ImportProductLogistics1(EnterpriseID, UserName);
        }
        //獲取未認證產品臨時表
        public DataTable GetProductLogisticsTemp(string filter)
        {
            return dao.GetProductLogisticsTemp(filter);
        }
        public void SaveProductLogistics(DataTable dt, string UserName)
        {
            dao.SaveProductLogistics(dt, UserName);
        }
        //上傳到營運庫
        public void UpoladBusinessUnit(string EnterpriseID, string UserName)
        {
            dao.UpoladBusinessUnit(EnterpriseID, UserName);

        }
        ///獲取企業用戶管理用戶
        ///
        public DataTable GetManagerUser(string EnterpriseID)
        {
            return dao.GetManagerUser(EnterpriseID);
        }
    }
}
