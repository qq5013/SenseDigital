// =================================================================== 
// 項目說明
//====================================================================
// 捷銳科技(廈門)有限公司版權所有@Copy Right 2012-2015
// 文檔 RoleInfo.cs
// 項目名稱:Web化開發
// 創建時間2013/03/06
// 負責人:
// ===================================================================
using System;

namespace Js.Model.Account
{
    /// <summary>
    ///unts_Roles數據實體
    /// </summary>
    [Serializable]
    public class RoleInfo
    {
        #region 變量定義
		///<summary>
		///
		///</summary>
		private int _roleID;
		///<summary>
		///
		///</summary>
		private string _roleName = String.Empty;
        ///<summary>
        ///用戶等級
        ///</summary>
        private int _userLevel = int.Parse("0");
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
        public RoleInfo()
		{
		}
		#endregion
		
		#region 公共屬性
		
		///<summary>
		///
		///</summary>
		public int RoleID
		{
			get {return _roleID;}
			set {_roleID = value;}
		}

		///<summary>
		///
		///</summary>
		public string RoleName
		{
            get { return _roleName; }
            set { _roleName = value; }
		}
        ///<summary>
        ///用戶等級
        ///</summary>
        public int UserLevel
        {
            get { return _userLevel; }
            set { _userLevel = value; }
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
