using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Js.DAO;
namespace Js.BLL
{
    public class BaseDal
    {
        BaseDao dao;
        public BaseDal()
        {
            dao = new BaseDao();
        }
        public BaseDal(string FormID)
        {
            dao = new BaseDao(FormID);
        }
        public BaseDal(string FormID,string cnKey)
        {
            dao = new BaseDao(FormID,cnKey);            
        }
        public BaseDal(int SId, int PId, string cnKey)
        {
            dao = new BaseDao(SId, PId, cnKey);
        }
        public bool Exists(string ID)
        {
            return dao.Exists(ID);
        }
        public virtual bool Exists(string ID, string filter)
        {
            return dao.Exists(ID, filter);
        }
        public void Delete(string ID)
        {
            dao.Delete(ID);
        }
        public virtual void Delete(string ID, string filter)
        {
            dao.Delete(ID,filter);
        }
        public void Add(DataRow dr)
        {
            dao.Add(dr);
        }
        public void Update(DataRow dr, string oldID)
        {
            dao.Update(dr, oldID);
        }
        public virtual void Update(DataRow dr, string oldID, string filter)
        {
            dao.Update(dr, oldID,filter);
        }
        public DataTable GetRecord(string filter)
        {
            return dao.GetRecord(filter);
        }
        public DataTable GetOrderByRecord(string filter, string orderby)
        {
            return dao.GetOrderByRecord(filter, orderby);
        }
        /// <summary>
        /// 獲取記錄，用原表名
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual DataTable GetRecord(string ID, string fieldName,string filter)
        {
            return dao.GetRecord(ID, fieldName,filter);
        }
        public DataTable GetViewRecord(string filter)
        {
            return dao.GetViewRecord(filter);
        }
        public virtual DataTable GetRecord(string move, string ID)
        {
            return dao.GetRecord(move, ID);
        }
        /// <summary>
        /// 上下筆移動時獲取記錄
        /// </summary>
        /// <param name="move"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual DataTable GetEnterpriseRecord(string move, string EnterpriseID, string ID)
        {
            return dao.GetEnterpriseRecord(move, EnterpriseID, ID);
        }
        public virtual DataTable GetRecordNoWhere(string filter)
        {
            return dao.GetRecordNoWhere(filter);
        }
        public virtual DataTable GetDistinctRecord(string fieldName)
        {
            return dao.GetDistinctRecord(fieldName);
        }
        public virtual DataTable GetDistinctRecord(string fieldName,string filter)
        {
            return dao.GetDistinctRecord(fieldName, filter);
        }
        public DataTable GetNameList(string filter)
        {
            return dao.GetNameList(filter);
        }
        public DataTable GetIDNameList(string filter)
        {
            return dao.GetIDNameList(filter);
        }
        public void DeleteDetail(string ID)
        {
            dao.DeleteDetail(ID);
        }
        public void SaveDetail(DataTable[] dtSub, string oldID)
        {
            dao.SaveDetail(dtSub, oldID);
        }
        public void SaveDetail(DataTable dtSub, string oldID)
        {
            dao.SaveDetail(dtSub, oldID);
        }
        public void SaveDbTable(DataTable dtSub, string filter)
        {
            dao.SaveDbTable(dtSub,filter);
        }
        /// <summary>
        /// 獲取企業未認證資料
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual DataSet GetEnterpriseDetail(string EnterpriseID)
        {
            return dao.GetEnterpriseDetail(EnterpriseID);
        }
        /// <summary>
        /// 獲取企業未認證資料
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual DataSet GetEnterpriseDetail(string EnterpriseID,string filter)
        {
            return dao.GetEnterpriseDetail(EnterpriseID, filter);
        }
        public DataSet GetSubDetail(string ID)
        {
            return dao.GetSubDetail(ID);
        }
        public virtual DataSet GetSubDetail(string ID, string filter)
        {
            return dao.GetSubDetail(ID, filter);
        }
        public string GetMaxID()
        {
            return dao.GetMaxID();
        }
        public string GetMaxID(string strWhere)
        {
            return dao.GetMaxID(strWhere);
        }
        public string GetAutoCode(DateTime dTime)
        {
            return dao.GetAutoCode( dTime);
        }
        
    }
}
