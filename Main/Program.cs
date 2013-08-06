using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace Main
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static MainForm MdiForm = new MainForm();
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(AppThreadException);

            Login.frmConnect frmTest = new Login.frmConnect();
            Login.frmLogin frmLog = new Login.frmLogin();

            if (frmTest.ShowDialog() == DialogResult.OK)
                if (frmLog.ShowDialog() == DialogResult.OK)
                    AppSingleton.Run(MdiForm);
        }
        static void AppThreadException(object source, System.Threading.ThreadExceptionEventArgs e)
        {
            string errorMsg = string.Format("未處理異常: \n{0}\n", e.Exception.Message);
            errorMsg += Environment.NewLine;

            DialogResult result = MessageBox.Show(errorMsg, "Application Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);

            //如果點"終止"則退出程式
            if (result == DialogResult.Abort)
            {
                Application.Exit();
            }
        }
    }
    public class AppSingleton
    {
        static Mutex m_Mutex;

        public static void Run()
        {
            if (IsFirstInstance())
            {
                Application.ApplicationExit += new EventHandler(OnExit);
                Application.Run();
            }

        }
        public static void Run(ApplicationContext context)
        {
            if (IsFirstInstance())
            {
                Application.ApplicationExit += new EventHandler(OnExit);
                Application.Run(context);
            }
        }
        public static void Run(Form mainForm)
        {
            if (IsFirstInstance())
            {

                Application.ApplicationExit += new EventHandler(OnExit);
                Application.EnableVisualStyles();
                Application.Run(mainForm);
            }
            else
            {
                MessageBox.Show("此程式已在執行中...!", "系統提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        static bool IsFirstInstance()
        {
            m_Mutex = new Mutex(false, "Main Mutext");
            bool owned = false;
            owned = m_Mutex.WaitOne(TimeSpan.Zero, false);
            return owned;
        }
        static void OnExit(object sender, EventArgs args)
        {
            m_Mutex.ReleaseMutex();
            m_Mutex.Close();
        }
    }
}

                               
