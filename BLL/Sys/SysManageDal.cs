using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Js.DAO.Sys;
using Js.Model;
namespace Js.BLL.Sys
{
    public class SysManageDal
    {
        SysManageDao dao;
        public SysManageDal()
        {
            dao = new SysManageDao();
        }
        public SysManageDal(string cnKey)
        {
            dao = new SysManageDao(cnKey);
        }
        public int GetMaxID()
        {
            return dao.GetMaxID();
        }
        public int AddTreeNode(SysNode node)
        {
            return dao.AddTreeNode(node);
        }

        public void UpdateNode(SysNode node)
        {
            dao.UpdateNode(node);
        }

        public void DelTreeNode(int SysID, int NodeID)
        {
            dao.DelTreeNode(SysID, NodeID);
        }

        /// <summary>
        /// 得到菜單節點
        /// </summary>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public SysNode GetNode(int SysID, int NodeID)
        {
            return dao.GetNode(SysID, NodeID);
        }

        public DataSet dtSysIdName()
        {
            return dao.dtSysIdName();
        }
        public DataSet GetTreeList(string strWhere)
        {
            return dao.GetTreeList(strWhere);
        }

        /// <summary>
        /// 得到分頁數據
        /// </summary>
        /// <param name="PageSize">頁尺寸</param>
        /// <param name="PageIndex">頁碼</param>
        /// <param name="strWhere">查詢條件 (注意: 不要加 where)</param>
        /// <returns></returns>
        public DataSet GetListByPage(int PageSize, int PageIndex, string strWhere)
        {
            return dao.GetListByPage(PageSize, PageIndex, strWhere);
        }

        #region 日志
        /// <summary>
        /// 增加日志
        /// </summary>
        /// <param name="time"></param>
        /// <param name="loginfo"></param>
        public void AddLog(string loginfo)
        {
            dao.AddLog(loginfo);
        }

        public void DeleteLog(int ID)
        {
            dao.DeleteLog(ID);
        }
        public void DelOverdueLog(int days)
        {
            dao.DelOverdueLog(days);
        }
        public void DeleteLog(string strWhere)
        {
            dao.DeleteLog(strWhere);
        }
        public DataSet GetLogs(string strWhere)
        {
            return dao.GetLogs(strWhere);
        }
        public DataRow GetLog(string ID)
        {
            return dao.GetLog(ID);
        }
        public string HostName()
        {
            return dao.HostName;
        }



        /// <summary>
        /// 獲得最大RecordID
        /// </summary>
        /// <returns></returns>
        public int GetMaxRecordID()
        {
            return dao.GetMaxRecordID();
        }
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="Record"></param>
        /// <param name="HostProcess"></param>
        public void InsertSysRrecord(DataRow dr)
        {
            dao.InsertSysRrecord(dr);
        }
        

        /// <summary>
        /// 使用系統記錄
        /// </summary>
        /// <returns></returns>
        public DataTable GetSysRecord()
        {
            return dao.GetSysRecord();
        }
        /// <summary>
        /// 使用系統記錄
        /// </summary>
        /// <returns></returns>
        public DataTable GetSysEmptyRecord()
        {
            return dao.GetSysEmptyRecord();
        }
        /// <summary>
        /// 清除使用記錄
        /// </summary>
        public void ClearSysRecord()
        {
            dao.ClearSysRecord();
        }

        #endregion

    }
}
