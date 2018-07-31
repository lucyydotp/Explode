using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Explode.Utilities {
    public static class IconCacher {
        public static bool cachedOnLaunch = false;
        public static void CacheAllIcons(ref Dictionary<string, System.Drawing.Icon> dict) {
            string[] filetypes = Registry.ClassesRoot.GetSubKeyNames().Where((x) => x.StartsWith(".")).ToArray();

            dict["|"] = Etier.IconHelper.IconReader.GetFolderIcon(Etier.IconHelper.IconReader.IconSize.Small, Etier.IconHelper.IconReader.FolderType.Closed);
            dict[".|"] = Etier.IconHelper.IconReader.GetFileIcon(".|", Etier.IconHelper.IconReader.IconSize.Small, false);

            foreach (string k in filetypes) {
                dict[k] = Etier.IconHelper.IconReader.GetFileIcon(k, Etier.IconHelper.IconReader.IconSize.Small, false);
            }

            cachedOnLaunch = true;
        }
    }
}
