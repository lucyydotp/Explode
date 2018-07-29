using System;
using System.IO;
using System.Windows.Forms;

namespace Explode
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static Form1 app;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // declaring like this helps with debugging
            app = new Form1();
            Application.Run(app);
        }
    }
}
