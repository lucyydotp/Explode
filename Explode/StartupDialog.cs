using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explode
{
    public partial class StartupDialog : Form
    {
        public StartupDialog()
        {
            InitializeComponent();
        }

        // actually happens on the Shown event
        private void StartupDialog_Load(object sender, EventArgs e)
        {
            Utilities.IconCacher.CacheAllIcons(ref FileUpdateHandler.iconCache, this);
        }
    }
}
