using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Js.Com
{
    public class User
    {
        /// <summary>
        /// 連接字串
        /// </summary>
        public static string ConnectStringBU;
       /// <summary>
       /// 服務器名稱
       /// </summary>
        public static string ServerBU;
        /// <summary>
        /// 服務器用戶名
        /// </summary>
        public static string UidBU;
        /// <summary>
        /// 服務器密碼
        /// </summary>
        public static string PwdBU;
        /// <summary>
        /// Windows驗證 
        /// </summary>
        public static bool AuthenticationBU;
        /// <summary>
        /// 服務器名稱
        /// </summary>
        public static string ServerLB;
        /// <summary>
        /// 服務器用戶名
        /// </summary>
        public static string UidLB;
        /// <summary>
        /// 服務器密碼
        /// </summary>
        public static string PwdLB;
        /// <summary>
        /// Windows驗證 
        /// </summary>
        public static bool AuthenticationLB;

        public static string UserID;

        public static string strDateFormat;
        /// <summary>
        /// Web日期格式
        /// </summary>
        public static string strWebDateFormat;
        public static string ReportPath = "";
        
        /// <summary>
        /// 程序名稱
        /// </summary>
        public static string AppNamePrefix;

        /// <summary>
        /// 電腦名稱
        /// </summary>
        public static string HostName;

        /// <summary>
        /// 文字框背景顏色
        /// </summary>
        public static Color txtBackColor;
        /// <summary>
        /// 臨時子串
        /// </summary>
        public static string tmpString1;
        public static string tmpString2;
        /// <summary>
        /// 公司名称
        /// </summary>
        public static string CompanyName;

        /// <summary>
        /// 交易区间：0，交易起始日期 1，交易结束日期。
        /// </summary>
        public static DateTime[] TradeDate;

        /// <summary>
        /// 關账日期
        /// </summary>
        public static DateTime CloseDate;

        /// <summary>
        /// 常用部門
        /// </summary>
        public static string DepartmentID;
        /// <summary>
        /// 常用幣別
        /// </summary>
        public static string CurrencyID;
        /// <summary>
        /// 常用倉庫
        /// </summary>
        public static string WareHouseID;

        /// <summary>
        /// 使用立冲功能
        /// </summary>
        public static bool StrikeBalance;
        /// <summary>
        /// 系統使用記錄
        /// </summary>
        public static int UserRecordID;
        /// <summary>
        /// 子表單數量，不包括報表，選單。只記錄資料表單
        ///如（部門資料，銷貨單等）
        /// </summary>
        public static short ChildCount = 0;

        public static float FontSize = 9.00f; 

        
       
    }

    public enum SysIndex
    {
        /// <summary>
        /// 公司參數
        /// </summary>
        cpy = -1,
        com = 0,
        stk = 1,
        ord = 3,
        chk = 4,
        acc = 5,
    }
}
