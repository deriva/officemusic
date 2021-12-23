using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OfficeMusic
{
    public partial class Form1 : Form
    {
        private static int OpenState = 0;
        private static string OpenTime = "";//开启播放时间
        private static string StopTime = "";//停止时间
        private static Dictionary<string,string> dicMusic;

        public Form1()
        {
            InitializeComponent();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var st = DateTime.Now.ToString("HH-mm-ss");
            var dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (OpenState == 1)
            {
                var timemusic = 0;
                if (StopTime == st) { Stop(); }
            }
            else
            {
                var musiDanCc = dicMusic.Where(x => x.Key.StartsWith(st)).Count();
                if (musiDanCc>0)
                {
                  var dd=  dicMusic.Where(x => x.Key.StartsWith(st)).First();
                    var url = dd.Value;
                    OpenTime = st;
                    var sce = int.Parse(dd.Key.Split('-')[3]);//.ToInt();
                    StopTime = DateTime.Now.AddSeconds(sce).ToString("HH-mm-ss");
                    Play(url);
                }

            }

            labtime.Text = dt;


        }

        private string GetFileInfo()
        {
            var fileinfo = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "/res/" + OpenTime);
            if (fileinfo.Length > 0)
                return fileinfo[0];
            return "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labtime.Text = "等待";
            InitMusic();
          
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;  //定时器时间间隔
            timer1.Start();   //定时器开始工作
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
        }

        private void Play(string url)
        {
         //   var url = AppDomain.CurrentDomain.BaseDirectory + "/res/李丽芬 - 爱江山更爱美人.mp3";
            axWindowsMediaPlayer1.URL = url;
            OpenState = 1;
            // axWindowsMediaPlayer1.play
        }

        private void Stop()
        {
            var url = AppDomain.CurrentDomain.BaseDirectory + "/res/李丽芬 - 爱江山更爱美人.mp3";
            axWindowsMediaPlayer1.URL = "";
            OpenState = 0;
            // axWindowsMediaPlayer1.play
        }

        private void InitMusic()
        {
            dicMusic = new Dictionary<string, string>();
            dicMusic.Clear();
            lstMusicItem.Items.Clear();
            var root = AppDomain.CurrentDomain.BaseDirectory + "/res/";
            var fileinfo = Directory.GetFiles(root);
            if (fileinfo.Length > 0)
            {
                foreach(var it in fileinfo)
                {
                    if (it.Contains(".mp3"))
                    {
                        var time = it.Replace(root, "");
                        dicMusic.Add(time, it);
                        lstMusicItem.Items.Add(time);
                    }
                }
             
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            InitMusic();
        }
    }
}
