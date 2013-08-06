// =================================================================== 
// 項目說明
//====================================================================
// 捷銳科技(廈門)有限公司版權所有@Copy Right 2012-2015
// 文檔 Company.cs
// 項目名稱:Web化開發
// 創建時間2013/01/03
// 負責人:
// ===================================================================
using System;
namespace Js.Model.BusinessUnit
{
    /// <summary>
    ///Company數據實體
    /// </summary>
    [Serializable]
    public class CompanyInfo
    {
        #region 變量定義
        ///<summary>
        ///公司代號
        ///</summary>
        private string _companyNo;
        ///<summary>
        ///公司名稱
        ///</summary>
        private string _companyName = String.Empty;
        ///<summary>
        ///官方編號
        ///</summary>
        private string _unionID = String.Empty;
        ///<summary>
        ///負責人
        ///</summary>
        private string _president = String.Empty;
        ///<summary>
        ///公司電話
        ///</summary>
        private string _phone = String.Empty;
        ///<summary>
        ///公司傳真
        ///</summary>
        private string _fax = String.Empty;
        ///<summary>
        ///聯絡地址
        ///</summary>
        private string _registerAddress = String.Empty;
        ///<summary>
        ///公司網址
        ///</summary>
        private string _webUrl = String.Empty;
        ///<summary>
        ///英文名稱
        ///</summary>
        private string _englishName = String.Empty;
        ///<summary>
        ///英文地址
        ///</summary>
        private string _englishAddress = String.Empty;
        ///<summary>
        ///建檔日期
        ///</summary>
        private DateTime _createDate = new DateTime();
        ///<summary>
        ///建檔用戶
        ///</summary>
        private string _createUserName = String.Empty;
        ///<summary>
        ///異動日期
        ///</summary>
        private DateTime _lastModifyDate = new DateTime();
        ///<summary>
        ///異動用戶
        ///</summary>
        private string _lastModifyUserName = String.Empty;
        ///<summary>
        ///建檔日期
        ///</summary>
        private DateTime _checkDate = new DateTime();
        ///<summary>
        ///建檔用戶
        ///</summary>
        private string _checkUserName = String.Empty;
        #endregion

        #region 構造函數
        ///<summary>
        ///
        ///</summary>
        public CompanyInfo()
        {
        }
        #endregion

        #region 公共屬性

        ///<summary>
        ///公司代號
        ///</summary>
        public string CompanyNo
        {
            get { return _companyNo; }
            set { _companyNo = value; }
        }

        ///<summary>
        ///公司名稱
        ///</summary>
        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        ///<summary>
        ///官方編號
        ///</summary>
        public string UnionID
        {
            get { return _unionID; }
            set { _unionID = value; }
        }

        ///<summary>
        ///負責人
        ///</summary>
        public string President
        {
            get { return _president; }
            set { _president = value; }
        }

        ///<summary>
        ///公司電話
        ///</summary>
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        ///<summary>
        ///公司傳真
        ///</summary>
        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }

        ///<summary>
        ///聯絡地址
        ///</summary>
        public string RegisterAddress
        {
            get { return _registerAddress; }
            set { _registerAddress = value; }
        }

        ///<summary>
        ///公司網址
        ///</summary>
        public string WebUrl
        {
            get { return _webUrl; }
            set { _webUrl = value; }
        }

        ///<summary>
        ///英文名稱
        ///</summary>
        public string EnglishName
        {
            get { return _englishName; }
            set { _englishName = value; }
        }

        ///<summary>
        ///英文地址
        ///</summary>
        public string EnglishAddress
        {
            get { return _englishAddress; }
            set { _englishAddress = value; }
        }

        ///<summary>
        ///建檔日期
        ///</summary>
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        ///<summary>
        ///建檔用戶
        ///</summary>
        public string CreateUserName
        {
            get { return _createUserName; }
            set { _createUserName = value; }
        }

        ///<summary>
        ///異動日期
        ///</summary>
        public DateTime LastModifyDate
        {
            get { return _lastModifyDate; }
            set { _lastModifyDate = value; }
        }

        ///<summary>
        ///異動用戶
        ///</summary>
        public string LastModifyUserName
        {
            get { return _lastModifyUserName; }
            set { _lastModifyUserName = value; }
        }

        ///<summary>
        ///建檔日期
        ///</summary>
        public DateTime CheckDate
        {
            get { return _checkDate; }
            set { _checkDate = value; }
        }

        ///<summary>
        ///建檔用戶
        ///</summary>
        public string CheckUserName
        {
            get { return _checkUserName; }
            set { _checkUserName = value; }
        }

        #endregion

    }
}
