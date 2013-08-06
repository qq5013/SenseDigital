using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Js.Model.BusinessUnit;
using Js.DAO.BusinessUnit;

namespace Js.BLL.BusinessUnit
{
    public class ParameterDal
    {
        ParameterDao dao;
        public ParameterDal()
        {
            dao = new ParameterDao();
        }
        public ParameterDal(string cnKey)
        {
            dao = new ParameterDao(cnKey);
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.BusinessUnit.ParameterInfo GetModel()
        {
            return dao.GetModel();
        }
        /// <summary>
        /// 更新關賬日期
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public void UpdateCloseDate(Js.Model.BusinessUnit.ParameterInfo model)
        {
            dao.UpdateCloseDate(model);
        }
    }
}
