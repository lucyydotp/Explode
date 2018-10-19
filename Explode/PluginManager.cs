using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ExplodePluginBase;

/*
    This plugin system is based on code written by Christoph Gattnar, 
    licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

	   http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
 */

namespace Explode
{
    public class PluginManager
    {
        // contains instances of plugins
        // there isn't one for column plugins because they're added straight into the ListView
        private List<IFileTypeBase> _fileTypes = new List<IFileTypeBase>();
        private List<IMenuItemBase> _menuTypes = new List<IMenuItemBase>();

        public PluginManager(string directory, ListView listView, ContextMenuStrip menuStrip)
        {
            // plugin file names in here
            string[] pluginFileNames = null;
            if (Directory.Exists(directory))
            {
                // kind of speaks for itself here


                pluginFileNames = Directory.GetFiles(directory, "*.dll");
            }

            // contains all the assemblies
            ICollection<Assembly> assemblies = new List<Assembly>(pluginFileNames.Length);
            foreach (string dllFile in pluginFileNames)
            {
                // loads all of the assemblies
                AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                Assembly assembly = Assembly.Load(an);
                assemblies.Add(assembly);
            }

            #region Column plugins
            // contains Type objects of all the plugins
            ICollection<Type> pluginTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly != null)
                {
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types)
                    {
                        if (type.IsInterface || type.IsAbstract)
                        {
                            continue;
                        }
                        else
                        {
                            Type _interface = type.GetInterface("IPluginBase");
                            if (_interface != null)
                            {
                                if (_interface.Name == "IPluginBase")
                                {
                                    pluginTypes.Add(type);
                                }
                            }
                        }
                    }
                }
            }

            // inserts builtin plugins
            listView.Columns.Add(new ExplodeColumn(new BuiltinName()));
            listView.Columns.Add(new ExplodeColumn(new BuiltinSize()));
            listView.Columns.Add(new ExplodeColumn(new BuiltinFormat()));
            listView.Columns.Add(new ExplodeColumn(new BuiltinExtension()));

            // creates an instance of each type and inserts it into the ListView
            foreach (Type type in pluginTypes)
            {
                listView.Columns.Add(new ExplodeColumn((IPluginBase)Activator.CreateInstance(type)));
            }

            #endregion

            #region File type plugins

            // contains Type objects of all the plugins
            ICollection<Type> typePluginTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly != null)
                {
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types)
                    {
                        if (type.IsInterface || type.IsAbstract)
                        {
                            continue;
                        }
                        else
                        {
                            Type _interface = type.GetInterface("IFileTypeBase");
                            if (_interface != null)
                            {
                                if (_interface.Name == "IFileTypeBase")
                                {
                                    typePluginTypes.Add(type);
                                }
                            }
                        }
                    }
                }
            }

            // creates an instance of each type
            foreach (Type type in typePluginTypes)
            {
                IFileTypeBase plugin = (IFileTypeBase)Activator.CreateInstance(type);
                _fileTypes.Add(plugin);
            }


            // adds builtin types
            _fileTypes.Add(new BuiltinTxt());
            _fileTypes.Add(new BuiltinLnk());
            _fileTypes.Add(new BuiltinExe());

            #endregion

            #region Right-click menu plugins

            // contains Type objects of all the plugins
            ICollection<Type> menuPluginTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly != null)
                {
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types)
                    {
                        if (type.IsInterface || type.IsAbstract)
                        {
                            continue;
                        }
                        else
                        {
                            Type _interface = type.GetInterface("IMenuItemBase");
                            if (_interface != null)
                            {
                                if (_interface.Name == "IMenuItemBase")
                                {
                                    menuPluginTypes.Add(type);
                                }
                            }
                        }
                    }
                }
            }

            //TODO: remove this line in the build, this is for testing
            menuPluginTypes.Add(typeof(BuiltinTestItem));

            // creates an instance of each type
            foreach (Type type in menuPluginTypes)
            {
                ExplodeMenuStripItem plugin = new ExplodeMenuStripItem((IMenuItemBase)Activator.CreateInstance(type), listView);
                plugin.Text = plugin.handler.ActionName;
                // add to menu
                menuStrip.Items.Add(plugin);
                menuStrip.Update();
            }

            #endregion
        }

        // this makes sure that the plugin list can't be edited

        public List<IFileTypeBase> FileTypes
        {
            get { return _fileTypes; }
        }
    }

    // this class is used to pair plugins with column headers 
    public class ExplodeColumn : ColumnHeader
    {
        public IPluginBase handler;
        public ExplodeColumn(IPluginBase handler)
        {
            this.handler = handler;
            Text = handler.FriendlyName;
        }

        public string GetInfo(FileStream s)
        {
            return handler.ColumnData(s);
        }
    }

    public class ExplodeMenuStripItem : ToolStripItem
    {
        public IMenuItemBase handler;
        private ListView listview;
        public ExplodeMenuStripItem(IMenuItemBase handler, ListView listView)
        {
            this.handler = handler;
            Text = handler.ActionName;
            Size = new System.Drawing.Size(117, 22);
            Click += ClickHandler;
            listview = listView;
        }

        public void ClickHandler(object sender, EventArgs e)
        {
            handler.ProcessFile(File.Open(((FormMain)listview.Parent.Parent.Parent).CurrentDirectory + listview.SelectedItems[0].Text, FileMode.Open));
        }
    }
}
