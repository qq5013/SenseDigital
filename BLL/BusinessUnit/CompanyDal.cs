using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Js.Model.BusinessUnit;
using Js.DAO.BusinessUnit;
namespace Js.BLL.BusinessUnit
{
    public class CompanyDal
    {
        CompanyDao dao;
        public CompanyDal()
        {
            dao = new CompanyDao();
        }
        public CompanyDal(string cnKey)
        {
            dao = new CompanyDao(cnKey);
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.BusinessUnit.CompanyInfo GetModel()
        {
            return dao.GetModel();
        }
        /// <summary>
        /// 更新公司資料
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public void Update(Js.Model.BusinessUnit.CompanyInfo model, string No)
        {
            dao.Update(model,No);

        }
    }
}

