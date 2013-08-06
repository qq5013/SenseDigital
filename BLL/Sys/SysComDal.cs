using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Js.DAO.Sys;
namespace Js.BLL.Sys
{
    public class SysComDal
    {
        SysComDao dao;
        public SysComDal()
        {
            dao = new SysComDao();
        }

        public SysComDal(string cnKey)
        {
            dao = new SysComDao(cnKey);
        }
        /// <summary>
        ///  测试连接是否成功
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="DatabasePrefix"></param>
        /// <param name="Uid"></param>
        /// <param name="Pwd"></param>
        /// <param name="DataWind"></param>
        /// <returns></returns>
        public bool OpenConnection(string connectionString)
        {
            return dao.OpenConnection(connectionString);
        }
        /// <summary>
        ///  测试连接是否成功
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="DatabasePrefix"></param>
        /// <param name="Uid"></param>
        /// <param name="Pwd"></param>
        /// <param name="DataWind"></param>
        /// <returns></returns>
        public bool OpenConnection(string Server, string Database, string Uid, string Pwd, bool Authentication)
        {
            return dao.OpenConnection(Server, Database, Uid, Pwd, Authentication);
        }
        /// <summary>
        ///  测试连接是否成功
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="DatabasePrefix"></param>
        /// <param name="Uid"></param>
        /// <param name="Pwd"></param>
        /// <param name="DataWind"></param>
        /// <returns></returns>
        public string GetConnection(string Server, string Database, string Uid, string Pwd, bool Authentication)
        {
            return dao.GetConnection(Server, Database, Uid, Pwd, Authentication);
        }
        /// <summary>
        /// 传回PageIndex页的资料。
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhere"></param>
        /// <param name="SelectField"></param>
        /// <returns></returns>
        public DataSet SelectTable(string FormID, int PageIndex, string strWhere, int PageSize, string OrderField, out int PageCount, out int RecrodCount)
        {
            return dao.SelectTable(FormID, PageIndex, strWhere, PageSize, OrderField,out PageCount, out RecrodCount);
        }
        /// <summary>
        /// 传回PageIndex页的资料。
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhere"></param>
        /// <param name="SelectField"></param>
        /// <returns></returns>
        public DataSet SelectTable(int SysID, int PermissionID, int PageIndex, string strWhere, int PageSize, out int PageCount, out int RecrodCount)
        {
            return dao.SelectTable(SysID,PermissionID,PageIndex,strWhere,PageSize,out PageCount,out RecrodCount);
        }
        /// <summary>
        /// 传回PageIndex页的资料,可自行傳入排序字段。
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhere"></param>
        /// <param name="SelectField"></param>
        /// <returns></returns>
        public DataSet SelectTable(int SysID, int PermissionID, int PageIndex, string strWhere, int PageSize, string OrderField,out int PageCount, out int RecrodCount)
        {
            return dao.SelectTable(SysID, PermissionID, PageIndex, strWhere, PageSize, OrderField,out PageCount, out RecrodCount);
        }
        /// <summary>
        /// 传回PageIndex页的资料，并对资料中的日期欄位转化为字符串形式。
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhere"></param>
        /// <param name="SelectField"></param>
        /// <returns></returns>
        public DataSet SelectTable(int SysID, int PermissionID, int PageIndex, string strWhere, int PageSize, out int PageCount, out int RecrodCount,string DateFormat)
        {
            return dao.SelectTable(SysID,PermissionID,PageIndex,strWhere,PageSize,out PageCount,out RecrodCount,DateFormat);
        }
        
        /// <summary>
        /// 多條件查詢欄位,blnSelectAll=true,返回所有栏位，false返回SingleSearch=true的欄位
        /// </true>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <returns></returns>
        public DataSet SearchTable(string FormID,bool blnSelectAll)
        {
            return dao.SearchTable(FormID, blnSelectAll);
        }
        /// <summary>
        /// 资料表中，改栏位的所有值(重复只出现一次)。
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public DataSet FieldTable(int SysID, int PermissionID, string FieldName)
        {
            return dao.FieldTable(SysID,PermissionID,FieldName);
        }

        /// <summary>
        /// 獲得欄位長度
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <returns></returns>
        public DataSet GetFieldLength(int SysID, int PermissionID)
        {
            return dao.GetFieldLength(SysID,PermissionID);
        }


        #region 笔数
        /// <summary>
        /// 單[多]選 筆數  
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int RecordCount(int SysID, int PermissionID, string strWhere)
        {
            return dao.RecordCount(SysID, PermissionID, strWhere);
        }
       
        /// <summary>
        /// 當前編號的筆數
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="strWhere"></param>
        /// <param name="CurrentValue"></param>
        /// <returns></returns>
        public int CurrentRecord(int SysID, int PermissionID, string strWhere,string CurrentValue,out ArrayList FieldOrderValue) //單[多]選 筆數  
        {
            return dao.CurrentRecord(SysID, PermissionID, strWhere, CurrentValue, out FieldOrderValue);
        }
        
      
        /// <summary>
        /// 返回沒有沒有資料的表單名稱
        /// </summary>
        /// <returns></returns>
        public string GetHasNotRecrodName(int SysID, int PermissionID)
        {
            return dao.GetHasNotRecrodName(SysID, PermissionID);
        }
        #endregion 

        #region 地址输入法，常用词库设定
        /// <summary>
        /// 獲取資料;
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetAddress(string TableName, int strNo)
        {
            return dao.GetAddress(TableName, strNo);
        }

        /// <summary>
        /// 刪除資料表(TableName)的內容;
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="strWhere"></param>
        public void DelAddress(string TableName, int strNo)
        {
            dao.DelAddress(TableName, strNo);
        }
        /// <summary>
        /// 插入資料表(tableName);
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="array"></param>
        public void InsertAddress(string TableName, ArrayList array)
        {
            dao.InsertAddress(TableName, array);
        }
        /// <summary>
        /// 獲得地址最大編號
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public int GetAddressID(string TableName)
        {
            return dao.GetAddressID(TableName);
        }
        #endregion 
        
        #region 获得单笔资料
        /// <summary>
        /// 首笔，上笔，下笔，末笔资料
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="ListType"></param>
        /// <param name="strKeyValue"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public string GetBaseModel(int SysID, int PermissionID, int ListType, ArrayList strKeyValue, string strWhere)
        {
            return dao.GetBaseModel(SysID, PermissionID, ListType, strKeyValue, strWhere);
        }

        #endregion

        #region 当前值被用情况
        /// <summary>
        /// 判斷當前值是否被用
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="CurrentValue"></param>
        /// <returns></returns>
        public bool GetCurrentIsUsed(string FormID, string CurrentValue) 
        {
            return dao.GetCurrentIsUsed(FormID, CurrentValue);
        }
        /// <summary>
        /// 當前值被使用記錄
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <param name="CurrentValue"></param>
        /// <returns></returns>
        public DataSet GetCurrentUsed(string FormID, string CurrentValue)
        {
            return dao.GetCurrentUsed(FormID, CurrentValue);
        }
        #endregion

        #region Sys_ColFozen 凍結
        /// <summary>
        /// 保存凍結
        /// </summary>
        public void SaveColFozen(string User, string frmCaption, string DgvName, int Fozen)
        {
            dao.SaveColFozen(User, frmCaption, DgvName, Fozen);
        }
        /// <summary>
        /// 刪除凍結
        /// </summary>
        public void DeleteColFozen(string User, string frmCaption, string DgvName)
        {
            dao.DeleteColFozen(User, frmCaption, DgvName);
        }

        /// <summary>
        /// 取得凍結
        /// </summary>
        public DataSet GetColFozen(string User, string frmCaption)
        {
            return dao.GetColFozen(User, frmCaption);
        }
        #endregion


        ///
        /// <summary>
        /// 多项查询--- 插入 欄
        /// </summary>
        public void InsertResults(int SysID, int PermissionID, string Flag, string strKey)
        {
            dao.InsertResults(SysID, PermissionID, Flag, strKey);
        }
        /// <summary>
        /// 刪除多選記錄
        /// </summary>
        public void DeleteAllResults(int SysID, int PermissionID, string Flag)
        {
            dao.DeleteAllResults(SysID, PermissionID, Flag);
        }

        public void AddParamentSub(string CodeName,string strUser)
        {
            dao.AddParamentSub(CodeName, strUser);
        }
        /// <summary>
        /// 根據sysid,permissionid，获得欄位，组合成语句，并返回所查询的DataSet
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <returns></returns>
        public DataSet GetSearchSelectSQL(string FormID, int PageIndex, string strWhere, int PageSize, out int PageCount, out int RecrodCount)
        {
            return dao.GetSearchSelectSQL(FormID, PageIndex, strWhere, PageSize, out PageCount, out RecrodCount);
        }
        /// <summary>
        /// 根據sysid,permissionid，获得欄位，组合成语句，并返回所查询的DataSet
        /// </summary>
        /// <param name="SysID"></param>
        /// <param name="PermissionID"></param>
        /// <returns></returns>
        public DataSet GetSearchSelectSQL(string FormID, int PageIndex, string strWhere, int PageSize, out int PageCount, out int RecrodCount, out string SelectField)
        {
            return dao.GetSearchSelectSQL(FormID, PageIndex, strWhere, PageSize, out PageCount, out RecrodCount, out SelectField);
        }

        /// <summary>
        /// 設定當前 SysID PermissionID
        /// </summary>
        /// <param name="strFormclass">Form.tostring()</param>
        /// <param name="SysID">回寫</param>
        /// <param name="PermissionID">回寫</param>
        public bool SetCurrentSysIDPermissionID(string strFormclass, ref int SysID, ref int PermissionID)
        {
            return dao.SetCurrentSysIDPermissionID(strFormclass, ref SysID, ref PermissionID);
        }
        /// <summary>
        /// 單據可否被編輯
        /// 返回
        /// 0：可做任何操作
        /// 1：單據時間不在交易時間內
        /// 2：單據時間＜＝關帳日期
        /// 3：已複核
        /// 4：被用
        /// </summary>
        public int GetBillCanBeEdit(string FromID, string strID)
        {
            return dao.GetBillCanBeEdit(FromID, strID);
        }
        /// <summary>
        /// 單據可否被編輯
        /// 返回
        /// 0：可做任何操作
        /// 1：單據時間不在交易時間內
        /// 2：單據時間＜＝關帳日期
        /// 3：已複核
        /// 4：被用
        /// </summary>
        public int GetBillCanBeEdit(string FromID, string strID,string filter)
        {
            return dao.GetBillCanBeEdit(FromID, strID, filter);
        }
        /// <summary>
        /// 單據可否被編輯
        /// 返回
        /// 0：可做任何操作
        /// 1：單據時間不在交易時間內
        /// 2：單據時間＜＝關帳日期
        /// 3：已複核
        /// 4：被用
        /// </summary>
        public int GetBillCanBeEdit(string FromID, string strID, object date, object beginDate, object enddate, object closedate)
        {
            return dao.GetBillCanBeEdit(FromID, strID, date, beginDate, enddate, closedate);
        }
        /// <summary>
        /// 開立資料庫
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="strSQL"></param>
        /// <param name="strPath"></param>
        public bool CreateDataBase(string IP, string strSQL, string dbPath, string strPath)
        {
            return dao.CreateDataBase(IP, strSQL, dbPath,strPath);
        }

        public void DeleteDataBase(string IP, string strDataBase)
        {
            dao.DeleteDataBase(IP, strDataBase);
        }
    }
}