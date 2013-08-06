using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Js.DAO.Label;
namespace Js.BLL.Label
{
    public class LabelDal
    {
        LabelDao dao;
        public LabelDal()
        {
            dao = new LabelDao();
        }
        public LabelDal(string cnKey)
        {
            dao = new LabelDao(cnKey);            
        }
        /// <summary>
        /// 獲取標籤序號資訊
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataTable GetLabelNoInfo(string labelNo)
        {
            return dao.GetLabelNoInfo(labelNo);
        }
    }
}
