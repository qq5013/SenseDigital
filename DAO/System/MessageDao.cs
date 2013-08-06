using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Js.DAO.System
{
    public class MessageDao
    {
        DBAccessLayer.IDBAccess ia;
        string cnKey;
        string FormID;

        public MessageDao()
        {
            ia =  DBAccessLayer.DBFactory.GetDBAccess();
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public MessageDao(string FormID)
        {
            this.cnKey = "";
            this.FormID = FormID;
            ia = DBAccessLayer.DBFactory.GetDBAccess();
        }
        //string strTbName
        /// <summary>
        /// 初始化
        /// </summary>
        public MessageDao(string FormID, string cnKey)
        {
            this.cnKey = "";
            this.FormID = FormID;
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
        }
        /// <summary>
        /// 獲取公告主檔資料
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataTable GetRecord(string ID)
        {
            string strSql = string.Format("select * from Sys_AnnounceMain Where AnnounceID='{0}'", ID);
            return ia.ExecuteDataSetSql(strSql).Tables[0];
        }
        /// <summary>
        /// 獲取公告子檔資料
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataTable GetSubRecord(string ID, int UserFlag, string UserName)
        {
            string strSql = string.Format("select * from Sys_AnnounceSub Where AnnounceID='{0}' and ReceiverFlag={1} and ReceiverUserName ='{2}'", ID, UserFlag, UserName);
            return ia.ExecuteDataSetSql(strSql).Tables[0];
        }
        /// <summary>
        /// 更新公告已讀
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public void UpdateMessageRead(string ID,int UserFlag,string UserName)
        {
            string strSql = string.Format("Update Sys_AnnounceSub set IsRead=1,ReadDate=getdate() Where AnnounceID='{0}' and ReceiverFlag={1} and ReceiverUserName ='{2}'",ID,UserFlag,UserName);
            ia.ExecuteNonQuerySql(strSql);            
        }
        /// <summary>
        /// 更新回覆公告已讀
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public void UpdateMessageRead1(string ID, int UserFlag, string UserName)
        {
            string strSql = string.Format("Update Sys_AnnounceSub set IsRead1=1,ReadDate1=getdate() Where AnnounceID='{0}' and ReceiverFlag={1} and ReceiverUserName ='{2}'", ID, UserFlag, UserName);
            ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 回覆公告
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public void ReplyMessage(string ID, int UserFlag, string UserName, string Title, string Content)
        {
            string strSql = string.Format("Update Sys_AnnounceSub set ReplyDate=getdate(),ReplyTitle='{0}',ReplyContent='{1}',IsRead1=0,ReadDate1=null Where AnnounceID='{2}' and ReceiverFlag={3} and ReceiverUserName ='{4}'", Title, Content, ID, UserFlag, UserName);
            ia.ExecuteNonQuerySql(strSql);
        }
        /// <summary>
        /// 自動發送公告
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public void SystemMessage(DataRow dr,DataTable dtSub)
        {
            Js.DAO.BaseDao dao = new BaseDao(FormID);
            dao.Add(dr);
            dao.SaveDetail(dtSub, "");
        }
    }
}

