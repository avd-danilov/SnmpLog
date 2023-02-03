using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using SnmpSharpNet;
using System.Net.Sockets;
using System.Net;
namespace SnmpLog
{
    internal static class Program
    {
        /// <summary>
        /// 
        
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            Console.ReadLine();
        }




    }
}
