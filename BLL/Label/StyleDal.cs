using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Js.DAO.Label;
namespace Js.BLL.Label
{
    public class StyleDal
    {
        StyleDao dao;
        public StyleDal()
        {
            dao = new StyleDao();
        }
        public StyleDal(string cnKey)
        {
            dao = new StyleDao(cnKey);            
        }
        /// <summary>
        /// 獲取記錄，用原表名
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataTable GetRecord(string filter)
        {
            return dao.GetRecord(filter);
        }
        /// <summary>
        /// 更新圖檔路徑
        /// </summary>
        public void UpdateImagePath(string StyleID, string FieldName, string ImagePath)
        {
            dao.UpdateImagePath(StyleID, FieldName, ImagePath);
        }
        /// <summary>
        /// 獲取倉庫樣式數量
        /// </summary>
        public DataTable GetWarehousePages(string StyleID)
        {
            return dao.GetWarehousePages(StyleID);
        }
        /// <summary>
        /// 插入倉庫樣式數量
        /// </summary>
        public void InsertWarehousePagesByWID(string WarehouseID, string UserName)
        {
            dao.InsertWarehousePagesByWID(WarehouseID,UserName);
        }
        /// <summary>
        /// 插入倉庫樣式數量
        /// </summary>
        public void InsertWarehousePagesByStyleID(string StyleID, string UserName)
        {
            dao.InsertWarehousePagesByStyleID(StyleID, UserName);
        }
        /// <summary>
        /// 刪除倉庫樣式數量
        /// </summary>
        public void DeleteWarehousePagesByWID(string WarehouseID)
        {
            dao.DeleteWarehousePagesByWID(WarehouseID);
        }
        /// <summary>
        /// 刪除倉庫樣式數量
        /// </summary>
        public void DeleteWarehousePagesByStyleID(string StyleID)
        {
            dao.DeleteWarehousePagesByStyleID(StyleID);
        }
    }
}
