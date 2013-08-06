using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Js.Model.Sys;
using Js.DAO.Sys;
namespace Js.BLL.Sys
{
    public class TreeListDal
    {
        TreeListDao dao;
        public TreeListDal()
        {
            dao = new TreeListDao();
        }
        public TreeListDal(string cnKey)
        {
            dao = new TreeListDao(cnKey);
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.Sys.TreeListInfo GetModel(string FormID)
        {
            return dao.GetModel(FormID);
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.Sys.TreeListInfo GetModel(int SId, int PId)
        {
            return dao.GetModel(SId, PId);
        }
        /// <summary>
        /// 得到LeftBar數據
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public DataTable GetLeftBar()
        {
            return dao.GetLeftBar();
        }
        /// <summary>
        /// 得到LeftBar數據
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public DataTable GetLeftBar(string filter)
        {
            return dao.GetLeftBar(filter);
        }
        /// <summary>
        /// 得到LeftBar數據
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public DataTable GetLeftBar(string filter, string culture)
        {
            return dao.GetLeftBar(filter, culture);
        }
    }
}
