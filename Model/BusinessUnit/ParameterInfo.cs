// =================================================================== 
// 項目說明
//====================================================================
// 捷銳科技(廈門)有限公司版權所有@Copy Right 2012-2015
// 文檔 Parameter.cs
// 項目名稱:Web化開發
// 創建時間2013/01/08
// 負責人:
// ===================================================================
using System;
namespace Js.Model.BusinessUnit
{
    /// <summary>
	///Parameter數據實體
	/// </summary>
	[Serializable]
	public class ParameterInfo
	{
		#region 變量定義
		///<summary>
		///標籤訂購單
		///</summary>
		private string _orderCode = String.Empty;
		///<summary>
		///標籤計劃單
		///</summary>
		private string _scheduleCode = String.Empty;
		///<summary>
		///標籤生產單
		///</summary>
		private string _productionCode = String.Empty;
		///<summary>
		///標籤入庫單
		///</summary>
		private string _inStockCode = String.Empty;
		///<summary>
		///生產作廢單
		///</summary>
		private string _invalidLabelCode = String.Empty;
		///<summary>
		///標籤出貨單
		///</summary>
        private string _deliverCode = String.Empty;
		///<summary>
		///退貨入庫單
		///</summary>
		private string _returnLabelCode = String.Empty;
		///<summary>
		///倉庫調撥單
		///</summary>
		private string _transferCode = String.Empty;
		///<summary>
		///標籤啟用單
		///</summary>
		private string _enableLabelNoCode = String.Empty;
		///<summary>
		///序號異動單
		///</summary>
		private string _labelNoActionCode = String.Empty;
		///<summary>
		///批次異動單
		///</summary>
		private string _batchActionCode = String.Empty;
		///<summary>
		///資訊登錄單
		///</summary>
		private string _infoRegisterCode = String.Empty;
		///<summary>
		///批次資訊登錄單
		///</summary>
		private string _batchInfoRegisterCode = String.Empty;
		///<summary>
		///比例小數位數
		///</summary>
		private byte _percentDecimalDigits = byte.Parse("0");
		///<summary>
		///預設使用年限
		///</summary>
		private byte _serviceYears = byte.Parse("0");
		///<summary>
		///預設啟用年月
		///</summary>
		private byte _enableMonths = byte.Parse("0");
		///<summary>
		///交易單結清日期
		///</summary>
		private DateTime _clearDate = new DateTime();
		///<summary>
		///關帳日期
		///</summary>
		private DateTime _closeDate = new DateTime();
		///<summary>
		///關帳日異動日期
		///</summary>
		private DateTime _lastModifyDate = new DateTime();
		///<summary>
		///關帳日異動用戶
		///</summary>
		private string _lastModifyUserName = String.Empty;
		#endregion
		
		#region 構造函數
		///<summary>
		///
		///</summary>
		public ParameterInfo()
		{
		}
		#endregion
		
		#region 公共屬性
		
		///<summary>
		///標籤訂購單
		///</summary>
		public string OrderCode
		{
			get {return _orderCode;}
			set {_orderCode = value;}
		}

		///<summary>
		///標籤計劃單
		///</summary>
		public string ScheduleCode
		{
			get {return _scheduleCode;}
			set {_scheduleCode = value;}
		}

		///<summary>
		///標籤生產單
		///</summary>
		public string ProductionCode
		{
			get {return _productionCode;}
			set {_productionCode = value;}
		}

		///<summary>
		///標籤入庫單
		///</summary>
		public string InStockCode
		{
			get {return _inStockCode;}
			set {_inStockCode = value;}
		}

		///<summary>
		///生產作廢單
		///</summary>
		public string InvalidLabelCode
		{
			get {return _invalidLabelCode;}
			set {_invalidLabelCode = value;}
		}

		///<summary>
		///標籤出貨單
		///</summary>
        public string DeliverCode
		{
            get { return _deliverCode; }
            set { _deliverCode = value; }
		}

		///<summary>
		///退貨入庫單
		///</summary>
		public string ReturnLabelCode
		{
			get {return _returnLabelCode;}
			set {_returnLabelCode = value;}
		}

		///<summary>
		///倉庫調撥單
		///</summary>
		public string TransferCode
		{
			get {return _transferCode;}
			set {_transferCode = value;}
		}

		///<summary>
		///標籤啟用單
		///</summary>
		public string EnableLabelNoCode
		{
			get {return _enableLabelNoCode;}
			set {_enableLabelNoCode = value;}
		}

		///<summary>
		///序號異動單
		///</summary>
		public string LabelNoActionCode
		{
			get {return _labelNoActionCode;}
			set {_labelNoActionCode = value;}
		}

		///<summary>
		///批次異動單
		///</summary>
		public string BatchActionCode
		{
			get {return _batchActionCode;}
			set {_batchActionCode = value;}
		}

		///<summary>
		///資訊登錄單
		///</summary>
		public string InfoRegisterCode
		{
			get {return _infoRegisterCode;}
			set {_infoRegisterCode = value;}
		}

		///<summary>
		///批次資訊登錄單
		///</summary>
		public string BatchInfoRegisterCode
		{
			get {return _batchInfoRegisterCode;}
			set {_batchInfoRegisterCode = value;}
		}

		///<summary>
		///比例小數位數
		///</summary>
		public byte PercentDecimalDigits
		{
			get {return _percentDecimalDigits;}
			set {_percentDecimalDigits = value;}
		}

		///<summary>
		///預設使用年限
		///</summary>
		public byte ServiceYears
		{
			get {return _serviceYears;}
			set {_serviceYears = value;}
		}

		///<summary>
		///預設啟用年月
		///</summary>
		public byte EnableMonths
		{
			get {return _enableMonths;}
			set {_enableMonths = value;}
		}

		///<summary>
		///交易單結清日期
		///</summary>
		public DateTime ClearDate
		{
			get {return _clearDate;}
			set {_clearDate = value;}
		}

		///<summary>
		///關帳日期
		///</summary>
		public DateTime CloseDate
		{
			get {return _closeDate;}
			set {_closeDate = value;}
		}

		///<summary>
		///關帳日異動日期
		///</summary>
		public DateTime LastModifyDate
		{
			get {return _lastModifyDate;}
			set {_lastModifyDate = value;}
		}

		///<summary>
		///關帳日異動用戶
		///</summary>
		public string LastModifyUserName
		{
			get {return _lastModifyUserName;}
			set {_lastModifyUserName = value;}
		}
	
		#endregion
		
	}
}
