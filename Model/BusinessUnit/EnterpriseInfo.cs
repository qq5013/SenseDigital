// =================================================================== 
// 項目說明
//====================================================================
// 捷銳科技(廈門)有限公司版權所有@Copy Right 2006-2008
// 文檔 TreeList.cs
// 項目名稱:Web化開發
// 創建時間2012/11/30
// 負責人:
// ===================================================================
using System;
namespace Js.Model.BusinessUnit
{
    /// <summary>
    ///Enterprise數據實體
    /// </summary>
    [Serializable]
    public class EnterpriseInfo
    {
        #region 變量定義
        ///<summary>
        ///企業編號
        ///</summary>
        private string _enterpriseID;
        ///<summary>
        ///企業名稱
        ///</summary>
        private string _enterpriseName = String.Empty;
        ///<summary>
        ///企業英文名稱
        ///</summary>
        private string _enterpriseEName = String.Empty;
        ///<summary>
        ///企業簡稱
        ///</summary>
        private string _enterpriseSName = String.Empty;
        ///<summary>
        ///企業類別
        ///</summary>
        private string _categoryID = String.Empty;
        ///<summary>
        ///官方編號
        ///</summary>
        private string _unionID = String.Empty;
        ///<summary>
        ///標籤生產
        ///</summary>
        private bool _labelFrom = false;
        ///<summary>
        ///負責人員
        ///</summary>
        private string _president = String.Empty;
        ///<summary>
        ///負責人員職稱
        ///</summary>
        private string _presidentPost = String.Empty;
        ///<summary>
        ///公司電話
        ///</summary>
        private string _phone = String.Empty;
        ///<summary>
        ///公司傳真
        ///</summary>
        private string _fax = String.Empty;
        ///<summary>
        ///公司地址
        ///</summary>
        private string _address = String.Empty;
        ///<summary>
        ///
        ///</summary>
        private string _zipNo = String.Empty;
        ///<summary>
        ///公司網址
        ///</summary>
        private string _webUrl = String.Empty;
        ///<summary>
        ///連絡人員
        ///</summary>
        private string _contact = String.Empty;
        ///<summary>
        ///連絡人員職稱
        ///</summary>
        private string _contactPost = String.Empty;
        ///<summary>
        ///聯絡人行動電話
        ///</summary>
        private string _cellPhone = String.Empty;
        ///<summary>
        ///聯絡電話
        ///</summary>
        private string _contactPhone = String.Empty;
        ///<summary>
        ///電子郵件
        ///</summary>
        private string _email = String.Empty;
        ///<summary>
        ///預設使用年限
        ///</summary>
        private byte _serviceYears = byte.Parse("0");
        ///<summary>
        ///預設啟用年月
        ///</summary>
        private byte _enableMonths = byte.Parse("0");
        ///<summary>
        ///備註
        ///</summary>
        private string _memo = String.Empty;
        ///<summary>
        ///建檔日期
        ///</summary>
        private DateTime _createDate = new DateTime();
        ///<summary>
        ///建檔人員
        ///</summary>
        private string _createUserName = String.Empty;
        ///<summary>
        ///異動日期
        ///</summary>
        private DateTime _lastModifyDate = new DateTime();
        ///<summary>
        ///異動人員
        ///</summary>
        private string _lastModifyUserName = String.Empty;
        ///<summary>
        ///覆核日期
        ///</summary>
        private DateTime _checkDate = new DateTime();
        ///<summary>
        ///覆核人員
        ///</summary>
        private string _checkUserName = String.Empty;
        #endregion

        #region 構造函數
        ///<summary>
        ///
        ///</summary>
        public EnterpriseInfo()
        {
        }
        #endregion

        #region 公共屬性

        ///<summary>
        ///企業編號
        ///</summary>
        public string EnterpriseID
        {
            get { return _enterpriseID; }
            set { _enterpriseID = value; }
        }

        ///<summary>
        ///企業名稱
        ///</summary>
        public string EnterpriseName
        {
            get { return _enterpriseName; }
            set { _enterpriseName = value; }
        }

        ///<summary>
        ///企業英文名稱
        ///</summary>
        public string EnterpriseEName
        {
            get { return _enterpriseEName; }
            set { _enterpriseEName = value; }
        }

        ///<summary>
        ///企業簡稱
        ///</summary>
        public string EnterpriseSName
        {
            get { return _enterpriseSName; }
            set { _enterpriseSName = value; }
        }

        ///<summary>
        ///企業類別
        ///</summary>
        public string CategoryID
        {
            get { return _categoryID; }
            set { _categoryID = value; }
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
        ///標籤生產
        ///</summary>
        public bool LabelFrom
        {
            get { return _labelFrom; }
            set { _labelFrom = value; }
        }

        ///<summary>
        ///負責人員
        ///</summary>
        public string President
        {
            get { return _president; }
            set { _president = value; }
        }

        ///<summary>
        ///負責人員職稱
        ///</summary>
        public string PresidentPost
        {
            get { return _presidentPost; }
            set { _presidentPost = value; }
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
        ///公司地址
        ///</summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        ///<summary>
        ///
        ///</summary>
        public string ZipNo
        {
            get { return _zipNo; }
            set { _zipNo = value; }
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
        ///連絡人員
        ///</summary>
        public string Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }

        ///<summary>
        ///連絡人員職稱
        ///</summary>
        public string ContactPost
        {
            get { return _contactPost; }
            set { _contactPost = value; }
        }

        ///<summary>
        ///聯絡人行動電話
        ///</summary>
        public string CellPhone
        {
            get { return _cellPhone; }
            set { _cellPhone = value; }
        }

        ///<summary>
        ///聯絡電話
        ///</summary>
        public string ContactPhone
        {
            get { return _contactPhone; }
            set { _contactPhone = value; }
        }

        ///<summary>
        ///電子郵件
        ///</summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        ///<summary>
        ///預設使用年限
        ///</summary>
        public byte ServiceYears
        {
            get { return _serviceYears; }
            set { _serviceYears = value; }
        }

        ///<summary>
        ///預設啟用年月
        ///</summary>
        public byte EnableMonths
        {
            get { return _enableMonths; }
            set { _enableMonths = value; }
        }

        ///<summary>
        ///備註
        ///</summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
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
        ///建檔人員
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
        ///異動人員
        ///</summary>
        public string LastModifyUserName
        {
            get { return _lastModifyUserName; }
            set { _lastModifyUserName = value; }
        }

        ///<summary>
        ///覆核日期
        ///</summary>
        public DateTime CheckDate
        {
            get { return _checkDate; }
            set { _checkDate = value; }
        }

        ///<summary>
        ///覆核人員
        ///</summary>
        public string CheckUserName
        {
            get { return _checkUserName; }
            set { _checkUserName = value; }
        }

        #endregion

    }
}