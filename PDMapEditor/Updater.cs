using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDMapEditor
{
    public partial class Updater : Form
    {
        public Updater()
        {
            InitializeComponent();
        }

        static WebClient client = new WebClient();

        public void CheckForUpdates()
        {
            string answer = "";
            try
            {
                answer = client.DownloadString(@"https://bitbucket.org/PayDay_/daenerys/downloads/newestBuild.txt");
            }
            catch
            {
                MessageBox.Show("Failed to reach Bitbucket to check for updates.");
                Program.UpdateFinished();
            }

            int newestBuild = -1;
            bool success = int.TryParse(answer, out newestBuild);

            if (success)
            {
                if (newestBuild > Program.BUILD)
                {
                    DialogResult dialogResult = MessageBox.Show("Update available!\nCurrent build: " + Program.BUILD + "\nNewest build: " + newestBuild + "\n\nWould you like to download and install the newest version?", "Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //Download newest ZIP
                        string file = "DAEnerys_b" + newestBuild + ".zip";
                        string url = @"https://bitbucket.org/PayDay_/daenerys/downloads/" + file;

                        client.DownloadFileCompleted += new AsyncCompletedEventHandler(UpdateDownloadComplete);
                        client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(UpdateDownloadProgessChanged);
                        client.DownloadFileAsync(new Uri(url), "update.zip");

                        labelDownloading.Text = "Downloading " + file + "\nfrom " + url + ".";
                    }
                }
            }
        }

        private void UpdateDownloadProgessChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadProgress.Value = e.ProgressPercentage;
        }

        private void UpdateDownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            labelDownloading.Text = "Download finished, extracting ZIP...";
            //Program.UpdateFinished();
        }
    }
}
