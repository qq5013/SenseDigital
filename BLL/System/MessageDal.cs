using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Js.DAO.System;
namespace Js.BLL.System
{
    public class MessageDal
    {
        MessageDao dao;
        public MessageDal()
        {
            dao = new MessageDao();
        }
        public MessageDal(string FormID)
        {
            dao = new MessageDao(FormID);
        }
        public MessageDal(string FormID,string cnKey)
        {
            dao = new MessageDao(FormID, cnKey);            
        }
        /// <summary>
        /// 獲取公告主檔資料
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataTable GetRecord(string ID)
        {
            return dao.GetRecord(ID);
        }
        /// <summary>
        /// 獲取公告子檔資料
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataTable GetSubRecord(string ID, int UserFlag, string UserName)
        {
            return dao.GetSubRecord(ID, UserFlag, UserName);
        }
        /// <summary>
        /// 更新公告已讀
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public void UpdateMessageRead(string ID, int UserFlag, string UserName)
        {
            dao.UpdateMessageRead(ID,UserFlag,UserName);
        }
        /// <summary>
        /// 更新回覆公告已讀
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public void UpdateMessageRead1(string ID, int UserFlag, string UserName)
        {
            dao.UpdateMessageRead1(ID, UserFlag, UserName);
        }
        /// <summary>
        /// 回覆公告
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public void ReplyMessage(string ID, int UserFlag, string UserName, string Title, string Content)
        {
            dao.ReplyMessage(ID, UserFlag, UserName, Title, Content);
        }
    }
}
