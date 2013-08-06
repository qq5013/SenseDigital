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
namespace Js.Model.Sys
{
    /// <summary>
    ///TreeList數據實體
    /// </summary>
    [Serializable]
    public class TreeListInfo
    {
        #region 變量定義
		///<summary>
		///
		///</summary>
		private string _formID = String.Empty;
		///<summary>
		///
		///</summary>
		private byte _sysID;
		///<summary>
		///
		///</summary>
		private int _nodeID;
		///<summary>
		///
		///</summary>
		private string _text = String.Empty;
		///<summary>
		///
		///</summary>
		private string _text_cn = String.Empty;
		///<summary>
		///
		///</summary>
		private string _text_en = String.Empty;
		///<summary>
		///
		///</summary>
		private int _parentID = int.Parse("0");
		///<summary>
		///
		///</summary>
		private string _location = String.Empty;
		///<summary>
		///
		///</summary>
		private string _url = String.Empty;
		///<summary>
		///
		///</summary>
		private int _permissionID = int.Parse("0");
		///<summary>
		///add,modify,delete,print,stop,check,uncheck
		///</summary>
		private string _permission = String.Empty;
		///<summary>
		///
		///</summary>
		private string _imageUrl = String.Empty;
		///<summary>
		///
		///</summary>
		private string _precondition = String.Empty;
		///<summary>
		///列印前綴
		///</summary>
		private string _printPrefix = String.Empty;
		///<summary>
		///被用檢視表明
		///</summary>
		private string _isUsedView = String.Empty;
		///<summary>
		///隸屬版本
		///</summary>
		private string _showVersion = String.Empty;
		///<summary>
		///資料表名稱
		///</summary>
		private string _tableName = String.Empty;
		///<summary>
		///主鍵欄位
		///</summary>
		private string _keyField = String.Empty;
		///<summary>
		///資料表名稱
		///</summary>
		private string _viewName = String.Empty;
		///<summary>
		///以逗号分隔的排序字段列表,可以指定在字段后面指定DESC/ASC
		///</summary>
		private string _orderField = String.Empty;
		///<summary>
		///基本表名稱
		///</summary>
		private string _baseName = String.Empty;
		///<summary>
		///
		///</summary>
		private string _selectSQL = String.Empty;
		///<summary>
		///條件語句
		///</summary>
		private string _strWhere = String.Empty;
		///<summary>
		///用於主子檔匹配檢查(前主後子用逗號間隔)
		///</summary>
		private string _mainSubTbl = String.Empty;
		///<summary>
		///是否可見
		///</summary>
		private bool _visible = false;
		///<summary>
		///基本表名稱
		///</summary>
		private string _autoCodeName = String.Empty;
		#endregion
		
		#region 構造函數
		///<summary>
		///
		///</summary>
        public TreeListInfo()
		{
		}
		#endregion
		
		#region 公共屬性
		
		///<summary>
		///
		///</summary>
		public string FormID
		{
			get {return _formID;}
			set {_formID = value;}
		}

		///<summary>
		///
		///</summary>
		public byte SysID
		{
			get {return _sysID;}
			set {_sysID = value;}
		}

		///<summary>
		///
		///</summary>
		public int NodeID
		{
			get {return _nodeID;}
			set {_nodeID = value;}
		}

		///<summary>
		///
		///</summary>
		public string Text
		{
			get {return _text;}
			set {_text = value;}
		}

		///<summary>
		///
		///</summary>
		public string Text_cn
		{
			get {return _text_cn;}
			set {_text_cn = value;}
		}

		///<summary>
		///
		///</summary>
		public string Text_en
		{
			get {return _text_en;}
			set {_text_en = value;}
		}

		///<summary>
		///
		///</summary>
		public int ParentID
		{
			get {return _parentID;}
			set {_parentID = value;}
		}

		///<summary>
		///
		///</summary>
		public string Location
		{
			get {return _location;}
			set {_location = value;}
		}

		///<summary>
		///
		///</summary>
		public string Url
		{
			get {return _url;}
			set {_url = value;}
		}

		///<summary>
		///
		///</summary>
		public int PermissionID
		{
			get {return _permissionID;}
			set {_permissionID = value;}
		}

		///<summary>
		///add,modify,delete,print,stop,check,uncheck
		///</summary>
		public string Permission
		{
			get {return _permission;}
			set {_permission = value;}
		}

		///<summary>
		///
		///</summary>
		public string ImageUrl
		{
			get {return _imageUrl;}
			set {_imageUrl = value;}
		}

		///<summary>
		///
		///</summary>
		public string Precondition
		{
			get {return _precondition;}
			set {_precondition = value;}
		}

		///<summary>
		///列印前綴
		///</summary>
		public string PrintPrefix
		{
			get {return _printPrefix;}
			set {_printPrefix = value;}
		}

		///<summary>
		///被用檢視表明
		///</summary>
		public string IsUsedView
		{
			get {return _isUsedView;}
			set {_isUsedView = value;}
		}

		///<summary>
		///隸屬版本
		///</summary>
		public string ShowVersion
		{
			get {return _showVersion;}
			set {_showVersion = value;}
		}

		///<summary>
		///資料表名稱
		///</summary>
		public string TableName
		{
			get {return _tableName;}
			set {_tableName = value;}
		}

		///<summary>
		///主鍵欄位
		///</summary>
		public string KeyField
		{
			get {return _keyField;}
			set {_keyField = value;}
		}

		///<summary>
		///資料表名稱
		///</summary>
		public string ViewName
		{
			get {return _viewName;}
			set {_viewName = value;}
		}

		///<summary>
		///以逗号分隔的排序字段列表,可以指定在字段后面指定DESC/ASC
		///</summary>
		public string OrderField
		{
			get {return _orderField;}
			set {_orderField = value;}
		}

		///<summary>
		///基本表名稱
		///</summary>
		public string BaseName
		{
			get {return _baseName;}
			set {_baseName = value;}
		}

		///<summary>
		///
		///</summary>
		public string SelectSQL
		{
			get {return _selectSQL;}
			set {_selectSQL = value;}
		}

		///<summary>
		///條件語句
		///</summary>
		public string strWhere
		{
			get {return _strWhere;}
			set {_strWhere = value;}
		}

		///<summary>
		///用於主子檔匹配檢查(前主後子用逗號間隔)
		///</summary>
		public string MainSubTbl
		{
			get {return _mainSubTbl;}
			set {_mainSubTbl = value;}
		}

		///<summary>
		///是否可見
		///</summary>
		public bool Visible
		{
			get {return _visible;}
			set {_visible = value;}
		}

		///<summary>
		///基本表名稱
		///</summary>
		public string AutoCodeName
		{
			get {return _autoCodeName;}
			set {_autoCodeName = value;}
		}
	
		#endregion
    }
}
