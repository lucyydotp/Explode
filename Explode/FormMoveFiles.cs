using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic.FileIO; //System.IO does not support cross-volume file transfers, VB.FileIO does.


namespace Explode {
    public partial class FormMoveFiles : Form {

        private bool done = false;

        public FormMoveFiles(FormMain frm, string[] sources, string destination, bool cut) {
            InitializeComponent();
            Show();
            Task.Factory.StartNew(() => {
                MoveFiles(sources, destination, cut);
                done = true;
                Invoke(new Action(() => {
                    frm.Refresh();
                    Close();
                }));
            });
        }

        private void MoveFiles(string[] sources, string destination, bool cut) {
            List<string> files = new List<string>();
            foreach (string k in sources) {
                if (Directory.Exists(k)) {
                    files.AddRange(EnumerateFiles(k));
                } else if (File.Exists(k)) {
                    files.Add(k);
                }
            }

            string sourceDirectory = new FileInfo(sources.Last()).DirectoryName;

            Invoke(new Action(() => {
                barPrimary.Minimum = 0;
                barPrimary.Value = 0;
                barPrimary.Maximum = files.Count;
            }));

            for(int i = 0; i < files.Count; i++) {
                string k = files[i];
                FileInfo file = new FileInfo(k);
                if (file.Exists) {
                    Invoke(new Action(() => {
                        barPrimary.Value = i + 1;
                        barSecondary.Value = 0;
                        if (cut) lblPrimary.Text = "Moving files (" + (i + 1) + " / " + files.Count + ")";
                        else lblPrimary.Text = "Copying files (" + (i + 1) + " / " + files.Count + ")";
                    }));
                    FileSystem.CreateDirectory(new FileInfo(destination + k.Replace(sourceDirectory, "")).DirectoryName);
                    if (cut) {
                        if (file.Directory.Root == new DirectoryInfo(destination).Root) {
                            FileSystem.MoveFile(k, destination + k.Replace(sourceDirectory, "")); //this should happen instantly, therefore tracking progress is pointless.
                        } else {
                            //cross volume will be copied anyways, so we should track it's progress. Just delete it afterwards.
                            ProgressCopy(k, destination + k.Replace(sourceDirectory, ""), barSecondary, lblSecondary);
                            FileSystem.DeleteFile(k);
                        }
                    } else ProgressCopy(k, destination + k.Replace(sourceDirectory, ""), barSecondary, lblSecondary); //If it wasn't cut, copy it and track progress.
                }
            }
            if (cut) {
                foreach (string k in sources) {
                    if (Directory.Exists(k)) FileSystem.DeleteDirectory(k, DeleteDirectoryOption.DeleteAllContents);
                }
            }
        }

        private string[] EnumerateFiles(string directory) {
            List<string> files = new List<string>();
            foreach (string k in Directory.EnumerateDirectories(directory)) {
                files.AddRange(EnumerateFiles(k));
            }
            files.AddRange(Directory.GetFiles(directory));
            return files.ToArray();
        }

        private bool ProgressCopy(string source, string dest, ProgressBar bar, Label lbl) {

            if (File.Exists(source) && !File.Exists(dest)) {
                FileStream streamSource = File.OpenRead(source);
                FileStream streamDest = File.OpenWrite(dest);

                try {

                    int blockSize = 1000000;
                    bar.Invoke(new Action(() => {
                        bar.Minimum = 0;
                        bar.Value = 0;
                        bar.Maximum = (int)Math.Ceiling(new FileInfo(source).Length / (double)blockSize);
                    }));

                    for (int i = 0; i < bar.Maximum; i++) {
                        byte[] block = new byte[blockSize];
                        streamSource.Seek((long)i * (long)blockSize, SeekOrigin.Begin);
                        int realLength = streamSource.Read(block, 0, blockSize);
                        streamDest.Seek((long)i * (long)blockSize, SeekOrigin.Begin);
                        streamDest.Write(block, 0, realLength);
                        bar.Invoke(new Action(() => {
                            lbl.Text = "Copying file " + new FileInfo(source).Name + " (" + (bar.Value * (blockSize / 1000000)) + "MB / " + (bar.Maximum * (blockSize / 1000000)) + "MB)";
                            bar.Value++;
                        }));
                    }

                    streamSource.Close();
                    streamDest.Close();

                    return true;
                
                } catch (Exception e) {
                    streamSource.Close();
                    streamDest.Close();
                    if (File.Exists(dest)) File.Delete(dest);
                    Debug.WriteLine(e.Message);
                    return false;
                }
            } else return false;
        }

        private void FormMoveFiles_FormClosing(object sender, FormClosingEventArgs e) {
            if (!done) e.Cancel = true;
        }
    }
}
