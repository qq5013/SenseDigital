using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
namespace Js.Com
{
    /// <summary>
    /// string rt=UGConvert.GetAllByRegex(@"\u6b63\u5728\u767b\u5f55\u8bba\u575b");
    /// </summary>
    public class UGConvert
    {
        public static string GetAllByRegex(string s)
        {
            string text1 = string.Empty;
            Regex regex1 = new Regex(@"[\][u][0-9a-fA-F]{4}");
            IEnumerator enumerator1 = regex1.Matches(s.ToLower()).GetEnumerator();
            while (enumerator1.MoveNext())
            {
                Match match1 = (Match)enumerator1.Current;
                text1 = regex1.Replace(s, new MatchEvaluator(UGConvert.MatchEval));
            }
            return text1.Replace(@"\", "");
        }
        public static string GetG(string s)
        {
            byte[] buffer1 = new byte[2];
            string text1 = s.Substring(4, 2);
            string text2 = s.Substring(2, 2);
            short num1 = Convert.ToInt16(text1, 0x10);
            short num2 = Convert.ToInt16(text2, 0x10);
            buffer1[0] = (byte)num1;
            buffer1[1] = (byte)num2;
            return Encoding.Unicode.GetString(buffer1);
        }
        private static string MatchEval(Match m)
        {
            return UGConvert.GetG(@"\" + m.Value);
        }
    }
}
