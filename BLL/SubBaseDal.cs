using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Js.DAO;
namespace Js.BLL
{
    public class SubBaseDal
    {
        SubBaseDao dao;
        public SubBaseDal()
        {
            dao = new SubBaseDao();
        }
        public SubBaseDal(string cnKey)
        {
            dao = new SubBaseDao(cnKey);
        }
        public void Save(DataTable dt, string strWhere)
        {
            dao.Save(dt, strWhere);
        }
        /// <summary>
        /// 傳入where条件时，顺序为：欄位名称，欄位值。如strwhere[0]欗位名称，strwhere[1]欗位值。
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strWhere"></param>
        public void Save(DataTable dt, object[] strWhere)
        {
            dao.Save(dt, strWhere);
        }
    }
}


