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
        public static FormMain app;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Utilities.IconCacher.CacheAllIcons(ref FileUpdateHandler.iconCache);

            // declaring like this helps with debugging
            app = new FormMain();
            Application.Run(app);
        }
    }
}
