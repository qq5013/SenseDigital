using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
namespace Js.DAO
{
    public class Common
    {
         // Fields
        private static Regex RegNumber;
        // Methods
        static Common()
        {
            RegNumber = new Regex("^[0-9]+$");
        }
        #region 獲得新編號
        /// <summary>
        /// 紡號＋1
        /// </summary>
        public static string NewID(string No)
        {
            if (No == "")
                return No;
            int id = No.Length - 1;
            char[] arr = No.ToCharArray();
            string strReturn = "";

            while (id > -1)
            {
                switch (arr[id].ToString())
                {
                    case "9":
                        strReturn = "0" + strReturn;
                        id--;
                        break;

                    case "Z":
                        strReturn = "A" + strReturn;
                        id--;
                        break;
                    default:
                        if (IsNumber(arr[id].ToString()))
                            No = No.Substring(0, id) + (int.Parse(arr[id].ToString()) + 1) + strReturn;
                        else
                            No = No.Substring(0, id) + (char)(arr[id] + 1) + strReturn;

                        id = -1;
                        break;
                }
            }
            return No;
        }
        /// <summary>
        /// 流水號　累加
        /// A001 + 20 = A021 
        /// </summary>
        public static string NewID(string No, int add)
        {
            if (No == "" || add <= 0)
                return No;
            string str1 = No.Substring(0, No.Length - ("" + add).Length);
            string str2 = No.Substring(No.Length - ("" + add).Length, ("" + add).Length);

            str2 = "" + (int.Parse(str2) + add);
            if (str2.Length > ("" + add).Length)
                return NewID(str1) + str2.Substring(str2.Length - ("" + add).Length, ("" + add).Length);
            else
                return str1 + str2;
        }
        public static bool IsNumber(string inputData)
        {
            Match match1 = RegNumber.Match(inputData);
            return match1.Success;
        }
        #endregion

        #region 获取单号
        public static string GetCodeFormat(DateTime dtDate, string CodeFormat)
        {
            if (CodeFormat.Length <= 0)
                CodeFormat = "YYYYMMDDXXXXXXXX 0";
            string[] arr = CodeFormat.Split(' ');
            string strK = CodeFormat.Substring(0, int.Parse(arr[1])).Trim();
            arr[0] = arr[0].Substring(int.Parse(arr[1])).Trim();
            arr[0] = arr[0].Replace("YYYY", "" + dtDate.Year);
            arr[0] = arr[0].Replace("MM", dtDate.Month.ToString("00"));
            arr[0] = arr[0].Replace("DD", dtDate.Day.ToString("00"));
            arr[0] = arr[0].Replace("X", "[0-9]");

            return strK + arr[0];
        }
        
        /// <summary>
        /// get object type
        /// </summary>
        public static SqlDbType GetObjectType(object obj)
        {
            switch (obj.GetType().Name.ToLower())
            {
                case "guid":
                    return SqlDbType.UniqueIdentifier;
                case "byte":
                case "int32":
                    return SqlDbType.Int;
                case "decimal":
                case "double":
                    return SqlDbType.Decimal;
                case "string":
                    return SqlDbType.NVarChar;
                case "boolean":
                    return SqlDbType.Bit;
                case "datetime":
                    return SqlDbType.DateTime;
            }
            return SqlDbType.NVarChar;
        }
        #endregion

        public static bool ValidateNumberLength(string inputData, int[] absoluteLength, out string ErrorMessage)
        {
            //數據庫對應位數長度
            int digitNumber = absoluteLength[0];
            //數據庫對應小數位數
            int decimalLength = absoluteLength[1];
            string[] strInput = (inputData.Replace(",", "")).Split(new char[] { '.' });
            if ((strInput[0].Length + decimalLength) <= digitNumber)
            {
                if ((strInput[0].Length + strInput[1].Length) <= digitNumber)
                {
                    ErrorMessage = "";
                    return true;
                }
                else
                {
                    ErrorMessage = "對不起,輸入數值超出了范圍!";
                    return false;
                }
            }
            else
            {
                ErrorMessage = "對不起,整數位超出范圍!";
                return false;
            }
        }

        public static bool IsDateTime(string Datetime)
        {
            bool blnDateTime;
            try
            {
                DateTime dtime = DateTime.Parse(Datetime);
                blnDateTime = true;
            }
            catch
            {
                blnDateTime = false;
            }
            return blnDateTime;
        }
        /// <summary>
        /// 字符串转化成日期(西元日期格式)，DateFormat指定日期格式(西元yyyy/MM/dd,中元gyyyy/MM/dd)。
        /// </summary>
        /// <param name="inputDate"></param>
        /// <param name="DateFormat"></param>
        /// <returns></returns>
        public static DateTime ParseDateTime(string inputDate, string DateFormat)
        {
            DateTime dtime = new DateTime(1912, 1, 1, 0, 0, 1);
            if (!string.IsNullOrEmpty(inputDate))
            {
                try
                {
                    int y = 1, m = 1, d = 1;
                    char cSpliter = '/';
                    if (DateFormat.Replace("gyyyy", "").Replace("yyyy", "").Replace("MM", "").Replace("dd", "").Trim().Length > 0)
                        cSpliter = char.Parse(DateFormat.Replace("gyyyy", "").Replace("yyyy", "").Replace("MM", "").Replace("dd", "").Trim().Substring(0, 1));

                    char[] Spliter = { cSpliter, cSpliter };
                    string[] a = inputDate.Split(Spliter);
                    string[] b = DateFormat.Split(Spliter);
                    for (int index = 0; index < b.Length; index++)
                    {
                        if (b[index] == "gyyyy")
                        {
                            y = int.Parse(a[index]) + 1911;
                        }
                        if (b[index] == "yyyy")
                        {
                            y = int.Parse(a[index]);
                        }
                        if (b[index] == "MM")
                        {
                            m = int.Parse(a[index]);
                        }
                        if (b[index] == "dd")
                        {
                            d = int.Parse(a[index]);
                        }
                    }
                    dtime = new DateTime(y, m, d);
                }
                catch
                {

                }
            }
            return dtime;

        }

        /// <summary>
        /// 字符串转化成日期(西元日期格式)，DateFormat指定日期格式(西元yyyy/MM/dd,中元gyyyy/MM/dd)。
        /// </summary>
        /// <param name="inputDate">輸入日期字串(含時間)</param>
        /// <param name="DateFormat">日期格式</param>
        /// <returns></returns>
        public static DateTime ParseDateTimeWithTime(string inputDate, string DateFormat)
        {
            DateTime dtime = new DateTime(1912, 1, 1, 0, 0, 1);
            string strDate = inputDate.Substring(0, inputDate.IndexOf(" ")).Trim();
            string strTime = inputDate.Substring(inputDate.IndexOf(" ")).Trim();
            if (!string.IsNullOrEmpty(strDate) && !string.IsNullOrEmpty(strTime))
            {
                try
                {
                    int y = 1, m = 1, d = 1, H = 0, M = 0, S = 0;
                    char cSpliter = '/';
                    if (DateFormat.Replace("gyyyy", "").Replace("yyyy", "").Replace("MM", "").Replace("dd", "").Trim().Length > 0)
                        cSpliter = char.Parse(DateFormat.Replace("gyyyy", "").Replace("yyyy", "").Replace("MM", "").Replace("dd", "").Trim().Substring(0, 1));

                    char[] Spliter = { cSpliter, cSpliter };
                    string[] a = strDate.Split(Spliter);
                    string[] b = DateFormat.Split(Spliter);
                    string[] c = strTime.Split(new char[] { ':' });
                    for (int index = 0; index < b.Length; index++)
                    {
                        if (b[index] == "gyyyy")
                        {
                            y = int.Parse(a[index]) + 1911;
                        }
                        if (b[index] == "yyyy")
                        {
                            y = int.Parse(a[index]);
                        }
                        if (b[index] == "MM")
                        {
                            m = int.Parse(a[index]);
                        }
                        if (b[index] == "dd")
                        {
                            d = int.Parse(a[index]);
                        }
                    }
                    H = int.Parse(c[0]);
                    M = int.Parse(c[1]);
                    S = int.Parse(c[2]);
                    dtime = new DateTime(y, m, d, H, M, S);
                }
                catch
                {

                }
            }
            return dtime;

        }
        public static ValueType ReDateTime(DateTime Datetime)
        {
            if (Datetime <= new DateTime(1912, 1, 1, 0, 0, 1))
                return null;
            else
            {
                return Datetime;
            }

        }
        public static string GetDateTimeString(DateTime dttime, string strFormat)
        {
            if (string.IsNullOrEmpty(strFormat))
                strFormat = "yyyy/MM/dd";
            string strDateValue = "";
            if (dttime <= new DateTime(1912, 1, 1, 0, 0, 1))
                return strDateValue;
            string spliter = "/";

            string strNewFormat = strFormat.Replace("gyyyy", "").Replace("yyyy", "").Replace("MM", "").Replace("dd", "").Trim();
            if (strNewFormat.Length > 0)
                spliter = strNewFormat.Substring(0, 1);
            string[] s = new string[3];
            int y = dttime.Year;
            if (strFormat.IndexOf("g") >= 0)
            {
                y = y - 1911;
            }
            s[0] = y.ToString();
            s[1] = dttime.Month.ToString().PadLeft(2, '0');
            s[2] = dttime.Day.ToString().PadLeft(2, '0');

            if (strFormat.Replace("g", "") == "yyyy" + spliter + "MM" + spliter + "dd")
            {
                strDateValue = s[0] + spliter + s[1] + spliter + s[2];
            }
            if (strFormat.Replace("g", "") == "MM" + spliter + "dd" + spliter + "yyyy")
            {
                strDateValue = s[1] + spliter + s[2] + spliter + s[0];
            }
            if (strFormat.Replace("g", "") == "dd" + spliter + "MM" + spliter + "yyyy")
            {
                strDateValue = s[2] + spliter + s[1] + spliter + s[0];
            }
            //strDateValue = newDate.ToString(strFormat.Replace("g",""), new System.Globalization.CultureInfo("zh-TW", true));

            return strDateValue;
        }
        public static string GetDateTimeString(DateTime dttime, string strFormat, bool ShowTime)
        {
            if (string.IsNullOrEmpty(strFormat))
                strFormat = "yyyy/MM/dd";
            string strDateValue = "";
            if (dttime <= new DateTime(1912, 1, 1, 0, 0, 1))
                return strDateValue;

            string spliter = "/";
            string strNewFormat = strFormat.Replace("gyyyy", "").Replace("yyyy", "").Replace("MM", "").Replace("dd", "").Trim();
            if (strNewFormat.Length > 0)
                spliter = strNewFormat.Substring(0, 1);
            string[] s = new string[6];
            int y = dttime.Year;
            if (strFormat.IndexOf("g") >= 0)
            {
                y = y - 1911;
            }
            s[0] = y.ToString();
            s[1] = dttime.Month.ToString().PadLeft(2, '0');
            s[2] = dttime.Day.ToString().PadLeft(2, '0');
            s[3] = dttime.Hour.ToString().PadLeft(2, '0');
            s[4] = dttime.Minute.ToString().PadLeft(2, '0');
            s[5] = dttime.Second.ToString().PadLeft(2, '0');


            if (strFormat.Replace("g", "") == "yyyy" + spliter + "MM" + spliter + "dd")
            {
                strDateValue = s[0] + spliter + s[1] + spliter + s[2];
            }
            if (strFormat.Replace("g", "") == "MM" + spliter + "dd" + spliter + "yyyy")
            {
                strDateValue = s[1] + spliter + s[2] + spliter + s[0];
            }
            if (strFormat.Replace("g", "") == "dd" + spliter + "MM" + spliter + "yyyy")
            {
                strDateValue = s[2] + spliter + s[1] + spliter + s[0];
            }
            if (ShowTime)
                strDateValue = strDateValue + " " + s[3] + ":" + s[4] + ":" + s[5];

            return strDateValue;
        }
        

        public static string std_Format(string strFormat, byte tmpValue)
        {
            string[] strItem = strFormat.Split(',');
            if (tmpValue < strItem.Length)
                return strItem[tmpValue].Trim();
            else
                return strItem[0].Trim();
        }

        public static string std_Un_Format(string strFormat, string tmpValue)
        {
            string[] strItem = strFormat.Split(',');

            for (int i = 0; i < strItem.Length; i++)
            {
                if (strItem[i].Trim() == tmpValue.Trim())
                    return i.ToString();
            }
            return "0";
        }
        //字元運算式重複指定的次數
        public static string Replicate(string character_expression, int integer_expression)
        {
            string tmpReplicate = "";
            for (int i = 0; i < integer_expression; i++)
            {
                tmpReplicate = tmpReplicate + character_expression;
            }
            return tmpReplicate;
        }

        /// <summary>
        ///两个日期之间相差的天数
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static int GetDateTimeDiff(DateTime dt1, DateTime dt2)
        {
            TimeSpan ts = dt2 - dt1;
            return ts.Days;
        }

        
        /// <summary>
        /// 
        /// </summary>
        public static string GetYearMonthString(string Value, string dateformat)
        {

            string strValue = "";
            if (Value == null || Value.ToString() == "")
                strValue = "";
            else
            {
                int intYear = 0;
                string Datespliter = "-";

                if (dateformat.IndexOf("g") >= 0)
                {
                    intYear = 1911;
                }
                string strFormat = dateformat.Replace("gyyyy", "").Replace("yyyy", "").Replace("MM", "").Replace("dd", "").Trim();
                if (strFormat.Length > 0)
                    Datespliter = strFormat.Substring(0, 1);

                strValue = "" + (int.Parse(Value.Substring(0, 4)) - intYear) + Datespliter + Value.Substring(Value.Length - 2, 2);
            }

            return strValue;
        }

        /// <summary>
        /// Format 數字
        /// </summary>
        public static decimal GetNumberFormat(object celValue, string strNumFormat)
        {
            try
            {
                return decimal.Parse(decimal.Parse("" + celValue).ToString(strNumFormat));
            }
            catch { }
            return 0;
        }

        //把西元年轉化成 格式化年;
        public static string GetYearFormat(string inpuYear, string DateFormat)
        {
            int y = 0;

            if (DateFormat.Replace("gyyyy", "").Replace("MM", "").Replace("dd", "").Replace("/", "").Trim().Length == 0)
                y = int.Parse(inpuYear) - 1911;
            else
                y = int.Parse(inpuYear);

            return y.ToString();
        }

        //把格式年 轉化成 西元年
        public static string GetYearString(string inpuYear, string DateFormat)
        {
            int y = 0;

            if (DateFormat.Replace("gyyyy", "").Replace("MM", "").Replace("dd", "").Replace("/", "").Trim().Length == 0)
                y = int.Parse(inpuYear) + 1911;
            else
                y = int.Parse(inpuYear);

            return y.ToString();
        }

        #region 全角半角转换
        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.Trim().ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }


        /// <summary> 转半角的函数(DBC case) </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(string input)
        {
            char[] c = input.Trim().ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion
    }
}
