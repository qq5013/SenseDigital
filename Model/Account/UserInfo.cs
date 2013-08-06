// =================================================================== 
// 項目說明
//====================================================================
// 捷銳科技(廈門)有限公司版權所有@Copy Right 2012-2015
// 文檔 Users.cs
// 項目名稱:Web化開發
// 創建時間2013/02/01
// 負責人:
// ===================================================================
using System;
namespace Js.Model.Account
{
    /// <summary>
    ///unts_Users數據實體
    /// </summary>
    [Serializable]
    public class UsersInfo
    {
    #region 變量定義
		///<summary>
		///
		///</summary>
		private int _userID;
		///<summary>
		///
		///</summary>
		private string _userName = String.Empty;
		///<summary>
		///真實姓名
		///</summary>
		private string _trueName = String.Empty;
		///<summary>
		///用戶等級
		///</summary>
		private int _userLevel = int.Parse("0");
		///<summary>
		///上級用戶
		///</summary>
		private int _parentLevel = int.Parse("0");
		///<summary>
		///
		///</summary>
		private byte[] _password;
		///<summary>
		///人員編號
		///</summary>
		private string _personID = String.Empty;
		///<summary>
		///部門編號
		///</summary>
		private string _departmentID = String.Empty;
		///<summary>
		///人員姓名
		///</summary>
		private string _personName = String.Empty;
		///<summary>
		///性別
		///</summary>
		private bool _sex = false;
		///<summary>
		///連絡電話
		///</summary>
		private string _phone = String.Empty;
		///<summary>
		///
		///</summary>
		private string _cellPhone = String.Empty;
		///<summary>
		///
		///</summary>
		private string _email = String.Empty;
		///<summary>
		///狀態 0:未啟用 1:啟用  2:停用
		///</summary>
		private byte _state = byte.Parse("0");
		///<summary>
		///啟用日期
		///</summary>
		private DateTime? _enableDate = new DateTime();
		///<summary>
		///停用日期
		///</summary>
		private DateTime? _stopDate = new DateTime();
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
		#endregion
		
		#region 構造函數
		///<summary>
		///
		///</summary>
        public UsersInfo()
		{
		}
		#endregion
		
		#region 公共屬性
		
		///<summary>
		///
		///</summary>
		public int UserID
		{
			get {return _userID;}
			set {_userID = value;}
		}

		///<summary>
		///
		///</summary>
		public string UserName
		{
			get {return _userName;}
			set {_userName = value;}
		}

		///<summary>
		///真實姓名
		///</summary>
		public string TrueName
		{
			get {return _trueName;}
			set {_trueName = value;}
		}

		///<summary>
		///用戶等級
		///</summary>
		public int UserLevel
		{
			get {return _userLevel;}
			set {_userLevel = value;}
		}

		///<summary>
		///上級用戶
		///</summary>
		public int ParentLevel
		{
			get {return _parentLevel;}
			set {_parentLevel = value;}
		}

		///<summary>
		///
		///</summary>
		public byte[] Password
		{
			get {return _password;}
			set {_password = value;}
		}

		///<summary>
		///人員編號
		///</summary>
		public string PersonID
		{
			get {return _personID;}
			set {_personID = value;}
		}

		///<summary>
		///部門編號
		///</summary>
		public string DepartmentID
		{
			get {return _departmentID;}
			set {_departmentID = value;}
		}

		///<summary>
		///人員姓名
		///</summary>
		public string PersonName
		{
			get {return _personName;}
			set {_personName = value;}
		}

		///<summary>
		///性別
		///</summary>
		public bool Sex
		{
			get {return _sex;}
			set {_sex = value;}
		}

		///<summary>
		///連絡電話
		///</summary>
		public string Phone
		{
			get {return _phone;}
			set {_phone = value;}
		}

		///<summary>
		///
		///</summary>
		public string CellPhone
		{
			get {return _cellPhone;}
			set {_cellPhone = value;}
		}

		///<summary>
		///
		///</summary>
		public string Email
		{
			get {return _email;}
			set {_email = value;}
		}

		///<summary>
		///狀態 0:未啟用 1:啟用  2:停用
		///</summary>
		public byte State
		{
			get {return _state;}
			set {_state = value;}
		}

		///<summary>
		///啟用日期
		///</summary>
		public DateTime? EnableDate
		{
			get {return _enableDate;}
			set {_enableDate = value;}
		}

		///<summary>
		///停用日期
		///</summary>
		public DateTime? StopDate
		{
			get {return _stopDate;}
			set {_stopDate = value;}
		}

		///<summary>
		///建檔日期
		///</summary>
		public DateTime CreateDate
		{
			get {return _createDate;}
			set {_createDate = value;}
		}

		///<summary>
		///建檔用戶
		///</summary>
		public string CreateUserName
		{
			get {return _createUserName;}
			set {_createUserName = value;}
		}

		///<summary>
		///異動日期
		///</summary>
		public DateTime LastModifyDate
		{
			get {return _lastModifyDate;}
			set {_lastModifyDate = value;}
		}

		///<summary>
		///異動用戶
		///</summary>
		public string LastModifyUserName
		{
			get {return _lastModifyUserName;}
			set {_lastModifyUserName = value;}
		}
	
		#endregion
		
	}
}
