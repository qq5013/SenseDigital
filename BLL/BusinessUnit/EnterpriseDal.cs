using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Js.Model.BusinessUnit;
using Js.DAO.BusinessUnit;
namespace Js.BLL.BusinessUnit
{
    public class EnterpriseDal
    {
        EnterpriseDao dao;
        public EnterpriseDal()
        {
            dao = new EnterpriseDao();
        }
        public EnterpriseDal(string cnKey)
        {
            dao = new EnterpriseDao(cnKey);
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.BusinessUnit.EnterpriseInfo GetModel(string EnterpriseID)
        {
            return dao.GetModel(EnterpriseID);
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public void InsertModifyRecord(string EnterpriseID, byte ServiceYears, byte EnableMonths)
        {
            dao.InsertModifyRecord(EnterpriseID, ServiceYears, EnableMonths);
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public DataTable GetModifyRecord(string EnterpriseID)
        {
            return dao.GetModifyRecord(EnterpriseID);
        }
    }
}

