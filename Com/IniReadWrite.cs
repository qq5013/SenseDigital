using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace Js.Com
{
    /// <summary>
    /// 讀寫 ini檔
    /// </summary>
    public class IniReadWrite
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public static string ReadInivalue(string Section, string Key, string localPath)
        {
            StringBuilder temp = new StringBuilder(1000);
            int i = GetPrivateProfileString(Section, Key, "", temp, 1000, localPath);
            return temp.ToString();
        }
        public static void WriteInivalue(string Section, string Key, string value, string localPath)
        {
            WritePrivateProfileString(Section, Key, value, localPath);
        }
    }
}
