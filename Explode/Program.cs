using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explode
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PluginManager manager = new PluginManager(Directory.GetCurrentDirectory());
            // lists plugins
            foreach (ExplodePluginBase.IPluginBase plugin in manager.Plugins)
            {
                Console.WriteLine(plugin.FriendlyName);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
