using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Js.Com
{
    public class PageValidate
    {
        // Fields
        private static Regex RegCHZN;
        private static Regex RegDecimal;
        private static Regex RegDecimalSign;
        private static Regex RegEmail;
        private static Regex RegNumber;
        private static Regex RegNumberSign;
        private static string CHAR1;//只在轉變金額為漢字金額的方法中使用這個變量。

        // Methods
        static PageValidate()
        {
            PageValidate.RegNumber = new Regex("^[0-9]+$");
            PageValidate.RegNumberSign = new Regex("^[+-]?[0-9]+$");
            PageValidate.RegDecimal = new Regex(@"^\d+(\.\d*)?$");
            PageValidate.RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$");
            PageValidate.RegEmail = new Regex(@"^[\w-]+@[\w-]+\.(com|net|org|edu|mil|tv|biz|info)$");
            PageValidate.RegCHZN = new Regex("[\u4e00-\u9fa5]");
        }

        public PageValidate()
        {

        }
        public static string Decode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }
        public static string Encode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }

        public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
        {
            string text1 = string.Empty;
            if ((inputKey != null) && (inputKey != string.Empty))
            {
                text1 = req.QueryString[inputKey];
                if (text1 == null)
                {
                    text1 = req.Form[inputKey];
                }
                if (text1 != null)
                {
                    text1 = PageValidate.SqlText(text1, maxLen);
                    if (!PageValidate.IsNumber(text1))
                    {
                        text1 = string.Empty;
                    }
                }
            }
            if (text1 == null)
            {
                text1 = string.Empty;
            }
            return text1;
        }

        public static string HtmlEncode(string inputData)
        {
            return HttpUtility.HtmlEncode(inputData);
        }

        public static string InputText(string inputString, int maxLength)
        {
            StringBuilder builder1 = new StringBuilder();
            if ((inputString != null) && (inputString != string.Empty))
            {
                inputString = inputString.Trim();
                if (inputString.Length > maxLength)
                {
                    inputString = inputString.Substring(0, maxLength);
                }
                for (int num1 = 0; num1 < inputString.Length; num1++)
                {
                    switch (inputString[num1])
                    {
                        case '<':
                            builder1.Append("&lt;");
                            break;

                        case '>':
                            builder1.Append("&gt;");
                            break;

                        case '"':
                            builder1.Append("&quot;");
                            break;

                        default:
                            builder1.Append(inputString[num1]);
                            break;
                    }
                }
                builder1.Replace("'", " ");
            }
            return builder1.ToString();
        }

        public static bool IsDecimal(string inputData)
        {
            Match match1 = PageValidate.RegDecimal.Match(inputData.Replace(",",""));
            return match1.Success;
        }

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

        public static bool IsDecimalSign(string inputData)
        {
            Match match1 = PageValidate.RegDecimalSign.Match(inputData);
            return match1.Success;
        }

        public static bool IsEmail(string inputData)
        {
            Match match1 = PageValidate.RegEmail.Match(inputData);
            return match1.Success;
        }


        public static bool IsHasCHZN(string inputData)
        {
            Match match1 = PageValidate.RegCHZN.Match(inputData);
            return match1.Success;
        }
        public static bool IsNumber(string inputData)
        {
            Match match1 = PageValidate.RegNumber.Match(inputData);
            return match1.Success;
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
        public static string ParseDate(string strDate)
        {
            if (strDate.Trim().Length > 0)
                return DateTime.Parse(strDate).ToString("yyyy/MM/dd");
            else
                return "";
        }
        /// <summary>
        /// 字符串转化成日期(西元日期格式)，DateFormat指定日期格式(西元yyyy/MM/dd,中元gyyyy/MM/dd)。
        /// </summary>
        /// <param name="inputDate"></param>
        /// <param name="DateFormat"></param>
        /// <returns></returns>
        public static string ParseDateTime(string strDate)
        {
            if (strDate.Trim().Length > 0)
            {
                strDate = DateTime.Parse(strDate).ToString("yyyy/MM/dd HH:mm");
                if (strDate == "0001/01/01 00:00")
                    return "";
                else
                    return strDate;
            }
            else
                return "";
        }
        /// <summary>
        /// 字符串转化成日期(西元日期格式)，DateFormat指定日期格式(西元yyyy/MM/dd,中元gyyyy/MM/dd)。
        /// </summary>
        /// <param name="inputDate"></param>
        /// <param name="DateFormat"></param>
        /// <returns></returns>
        public static DateTime ParseDateTime(string inputDate, string DateFormat)
        {
            DateTime dtime = new DateTime(1912, 1, 1, 0, 0,1);
            if (!string.IsNullOrEmpty(inputDate))
            {
                try
                {
                    int y = 1, m = 1, d = 1;
                    char cSpliter = '/';
                    if (DateFormat.Replace("gyyyy", "").Replace("yyyy", "").Replace("MM", "").Replace("dd", "").Trim().Length > 0)
                        cSpliter = char.Parse(DateFormat.Replace("gyyyy", "").Replace("yyyy", "").Replace("MM", "").Replace("dd", "").Trim().Substring(0, 1));
                
                    char[] Spliter ={ cSpliter, cSpliter };
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
                    string[] c = strTime.Split(new char[]{':'});
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
        public static bool IsNumberSign(string inputData)
        {
            Match match1 = PageValidate.RegNumberSign.Match(inputData);
            return match1.Success;
        }

        public static void SetLabel(Label lbl, object inputObj)
        {
            PageValidate.SetLabel(lbl, inputObj.ToString());
        }

        public static void SetLabel(Label lbl, string txtInput)
        {
            lbl.Text = PageValidate.HtmlEncode(txtInput);
        }

        public static string SqlText(string sqlInput, int maxLength)
        {
            if ((sqlInput != null) && (sqlInput != string.Empty))
            {
                sqlInput = sqlInput.Trim();
                if (sqlInput.Length > maxLength)
                {
                    sqlInput = sqlInput.Substring(0, maxLength);
                }
            }
            return sqlInput;
        }

        public static int GetColIndex(string[] strFieldName, string ColName)
        {
            for (int i = 0; i < strFieldName.Length; i++)
            {
                if (strFieldName[i].ToLower() == ColName.ToLower())
                {
                    return i;
                }
            }
            return -1;
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
            string tmpReplicate="";
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

            //char c;
            //int i;
            //string TempNo = "";
            //switch (No.Substring(No.Length - 1, 1))
            //{
            //    case "9":
            //        for (i = 1; i <= No.Length; i++)
            //        {
            //            if (No.Substring(No.Length - i, 1) != "9")
            //            {
            //                c = Convert.ToChar(No.Substring(No.Length - i, 1));
            //                TempNo = No.Substring(0, No.Length - i) + Convert.ToString((char)(c + 1)) + "".PadLeft(i - 1, '0');
            //                break;
            //            }
            //            else
            //                TempNo = "1" + "".PadLeft(i, '0');

            //        }
            //        No = TempNo;
            //        break;

            //    case "Z":
            //        if (No.Length == 1)
            //            No = "A0";
            //        else
            //        {
            //            c = Convert.ToChar(No.Substring(No.Length - 2, 1));
            //            No = No.Substring(0, No.Length - 2) + Convert.ToString((char)(c + 1)) + "A";
            //        }
            //        break;
            //    default:
            //        c = Convert.ToChar(No.Substring(No.Length - 1, 1));
            //        No = No.Substring(0, No.Length - 1) + Convert.ToString((char)(c + 1));
            //        break;
            //}
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
        #endregion

        #region 轉變金額為漢字金額,調用方法TransChinese1
        //最大能轉變999999999999.99(9千多億)
        public static string TransChinese1(double i)
        {
            string returnChinese;
            string string1, string2;
            Int32 LenNum;

            string1 = i.ToString().Trim();
            if (string1.IndexOf('.') == -1)
            {
                TransChinese(string1.Length, string1);
                CHAR1 = CHAR1 + "元整";
                returnChinese = CHAR1;
                CHAR1 = "";
            }
            else
            {
                TransChinese(string1.IndexOf('.') + 1, string1.Substring(0, string1.IndexOf('.')));
                CHAR1 = CHAR1 + "元";
                returnChinese = CHAR1;
                CHAR1 = "";
                LenNum = string1.IndexOf('.');
                for (int k = 1; k <= 2; k++)
                {
                    try
                    {
                        string2 = string1.Substring(LenNum + k, 1);
                        returnChinese = returnChinese + ChineseChar1(string2, k);
                    }
                    catch { }
                }
                returnChinese = returnChinese + "整";
            }
            return returnChinese.Replace(" ", "");
        }


        private static string TransChinese(int numbit, string Number)
        {
            string lnumber, returnString = "";
            int bit;
            if (numbit >= 9)
            {
                lnumber = Number.Substring(0, Number.Length - 8);
                bit = Number.Length - 8;
                CHAR1 = CHAR1 + TransNum(lnumber, bit, 2);
                returnString = TransChinese(8, Number.Substring(Number.Length - 8, 8));
            }
            else if (numbit >= 5)
            {
                lnumber = Number.Substring(0, Number.Length - 4);
                bit = Number.Length - 4;
                CHAR1 = CHAR1 + TransNum(lnumber, bit, 1);
                returnString = TransChinese(4, Number.Substring(Number.Length - 4, 4));
            }
            else
            {
                lnumber = Number.Substring(0, Number.Length);
                bit = Number.Length;
                CHAR1 = CHAR1 + TransNum(lnumber, bit, 0);
            }
            return returnString;
        }

        private static string TransNum(string char1, int bit, int labelbit)
        {
            string returnString = "";
            switch (bit)
            {
                case 4:
                    if (char1.Substring(0, 1) != "0")
                        returnString = returnString + "壹貳參肆伍陸柒捌玖".Substring(short.Parse(char1.Substring(0, 1)) - 1, 1) + "仟";
                    else if (char1.Substring(0, 1) == "0" && char1.Substring(1, 1) == "0")//防止出現兩個零
                    { }
                    else if (char1.Substring(1, 1) != "0" || char1.Substring(2, 1) != "0" || char1.Substring(3, 1) != "0")
                        returnString = returnString + "零壹貳參肆伍陸柒捌玖".Substring(short.Parse(char1.Substring(0, 1)), 1);// Choose(char1.Substring(1, 1) + 1, "零", "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖");

                    if (char1.Substring(1, 1) != "0")
                        returnString = returnString + "壹貳參肆伍陸柒捌玖".Substring(short.Parse(char1.Substring(1, 1)) - 1, 1) + "佰";
                    else
                    {
                        if (char1.Substring(0, 1) != "0" && char1.Substring(2, 1) != "0")
                            returnString = returnString + "零壹貳參肆伍陸柒捌玖".Substring(short.Parse(char1.Substring(1, 1)), 1);// Choose(char1.Substring(2, 1) + 1, "零", "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖");
                    }
                    if (char1.Substring(2, 1) != "0")
                        returnString = returnString + "壹貳參肆伍陸柒捌玖".Substring(short.Parse(char1.Substring(2, 1)) - 1, 1) + "拾";
                    else
                    {
                        if (char1.Substring(3, 1) != "0")
                            returnString = returnString + "零壹貳參肆伍陸柒捌玖".Substring(short.Parse(char1.Substring(2, 1)), 1);// Choose(char1.Substring( 3, 1) + 1, "零", "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖");
                    }
                    if (char1.Substring(3, 1) != "0")
                        returnString = returnString + "壹貳參肆伍陸柒捌玖".Substring(short.Parse(char1.Substring(3, 1)) - 1, 1);// Choose(char1.Substring( 4, 1), "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖");
                    if (char1.Substring(0, 1) != "0" || char1.Substring(1, 1) != "0" || char1.Substring(2, 1) != "0" || char1.Substring(3, 1) != "0")
                        returnString = returnString + " 萬億".Substring(labelbit, 1);// Choose(labelbit + 1, "", "萬", "億");
                    else
                        returnString = "";
                    break;
                case 3:
                    returnString = returnString + "壹貳參肆伍陸柒捌玖".Substring(short.Parse(char1.Substring(0, 1)) - 1, 1) + "佰";
                    if (char1.Substring(1, 1) != "0")
                        returnString = returnString + "壹貳參肆伍陸柒捌玖".Substring(short.Parse(char1.Substring(1, 1)) - 1, 1) + "拾";
                    else
                    {
                        if (char1.Substring(2, 1) != "0")
                            returnString = returnString + "零壹貳參肆伍陸柒捌玖".Substring(short.Parse(char1.Substring(1, 1)), 1);
                    }
                    if (char1.Substring(2, 1) != "0")
                        returnString = returnString + "壹貳參肆伍陸柒捌玖".Substring(short.Parse(char1.Substring(2, 1)) - 1, 1);
                    returnString = returnString + " 萬億".Substring(labelbit, 1);// Choose(labelbit + 1, "", "萬", "億");
                    break;
                case 2:
                    returnString = returnString + "壹貳參肆伍陸柒捌玖".Substring(short.Parse(char1.Substring(0, 1)) - 1, 1) + "拾";
                    if (char1.Substring(1, 1) != "0")
                        returnString = returnString + "壹貳參肆伍陸柒捌玖".Substring(short.Parse(char1.Substring(1, 1)) - 1, 1);
                    returnString = returnString + " 萬億".Substring(labelbit, 1);// Choose(labelbit + 1, "", "萬", "億");
                    break;
                case 1:
                    if (char1 != "0")
                        returnString = returnString + "壹貳參肆伍陸柒捌玖".Substring(short.Parse(char1.Substring(0, 1)) - 1, 1);
                    switch (labelbit)
                    {
                        case 2:
                            if (char1 == "0") { }
                            else
                                returnString = returnString + "億";
                            break;
                        case 1:
                            if (char1 == "0") { }
                            else
                                returnString = returnString + "萬";
                            break;
                        case 0:
                            if (char1 == "0")
                                returnString = "零";
                            break;
                    }
                    break;
            }
            return returnString;
        }

        private static string ChineseChar1(string numstring, int numlable)
        {
            string string5, string6;
            string returnString = "";

            if (numstring == "0") return returnString;
            if (numstring == "") return returnString;
            string5 = "壹貳參肆伍陸柒捌玖".Substring(short.Parse(numstring) - 1, 1);// Choose(short.Parse(numstring), "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖");
            string6 = "角分".Substring(numlable - 1, 1);// Choose(numlable, "角", "分");
            returnString = string5 + string6;
            return returnString;
        }
        #endregion

        /// <summary>
        /// 标准年月字符串 转化成格式化字符串。
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string GetYearMonthString(string Value)
        {

            string strValue = "";
            if (Value == null || Value.ToString() == "")
                strValue = "";
            else
            {
                string strText = Value.ToString().Replace("/", "").Trim();
                if (strText.Length >= 3)
                {
                    string dateformat = Js.Com.User.strDateFormat;
                    int intMonth = int.Parse(strText.Substring(strText.Length - 2, 2));
                    int intYear = int.Parse(strText.Substring(0, strText.Length - 2));
                    if (intYear > 1911)
                    {
                        string stry = intYear.ToString();
                        if (dateformat.IndexOf("g") >= 0)
                        {
                            intYear = intYear - 1911;
                            stry = intYear.ToString();
                        }
                        string strm = intMonth.ToString().PadLeft(2, '0');

                        string Datespliter = "/";

                        string strFormat = dateformat.Replace("gyyyy", "").Replace("yyyy", "").Replace("MM", "").Replace("dd", "").Trim();
                        if (strFormat.Length > 0)
                            Datespliter = strFormat.Substring(0, 1);

                        if (dateformat.Replace("g", "").Replace("dd", "").Replace(Datespliter, "").Trim().IndexOf("M") == 0)
                            strValue = strm + Datespliter + stry;
                        else
                            strValue = stry + Datespliter + strm;
                    }
                    else
                    {
                        strValue = "";
                    }
                }
                else
                {
                    strValue = "";
                }
            }

            return strValue;
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
