﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using EHdownloadTool;

using System.IO;
namespace EIDownloadTool
{

    public unsafe partial class Form1 : Form
    {
        Queue<String> Pages = new Queue<string>();
        Queue<String> LinkURL = new Queue<string>();
        String donwloadTile = "";
        String folder;
        private CookieContainer cc;
        private SpWebClient spwc;
        private string webtoken;
        private System.Threading.Thread newThread;
        string ret = "";
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.cc = new CookieContainer();
            this.spwc = new SpWebClient(cc);
            this.spwc.Headers.Add("User-Agent", "User-Agent: Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.97 Safari/537.36");
        }




        public int loadPagesCount(String url, Object senser)
        {

            ((Button)senser).Text = "加載頁面中...";
            ((Button)senser).Enabled = false;
            int pagesCount = 1;
            byte[] byteArray;
            String data;

            try
            {
               
                //=====
                HttpWebClient title = new HttpWebClient(new CookieContainer());
                title.Encoding = Encoding.UTF8;


                byteArray = title.DownloadData(url);
                data = Encoding.UTF8.GetString(byteArray);

                int first = data.IndexOf("<title>");
                int last = data.IndexOf("</title>");

                donwloadTile = data.Substring(first + 7, last - first - 7);
            }
            catch (Exception e)
            {

            }

            //取得下載頁數
            Pages.Clear();
            Pages.Enqueue(url);

            try
            {

                //check final / char
                if ((url[url.Length - 1]) != '/')
                {
                    url += '/';
                }

                //GET NEW SESSION FIX 20160222
                ret = spwc.DownloadString(url + "?nw=session", Encoding.UTF8);
                CookieCollection cookies = cc.GetCookies(new Uri(url));
                foreach (Cookie cookie in cookies)
                {
                    if (cookie.Name == "uconfig")
                        this.webtoken = cookie.Value;
                }
                data = spwc.DownloadString(url, Encoding.UTF8);

                //取得所有p值找最大
                GC.Collect();
                int maxPages = 0;
                for (int i = 1000;i >= 0 ; i--)
                {
                    if (data.IndexOf(url + "?p=" + i) != -1)
                    {
                        maxPages = i;
                        break;
                    }
                }
                GC.Collect();
                for (int i = 1; i <= maxPages; i++)
                {
                    pagesCount++;
                    Pages.Enqueue(url + "?p=" + i);
                }
                GC.Collect();
                StatusNow.Text = "成功獲取資料，頁數:" + pagesCount.ToString() + "，正在Trace中...";
            }
            catch (Exception e)
            {

            }
            finally
            {
                newThread = new System.Threading.Thread(this.ThreadProcLoad);
                newThread.Start();

            }

            return pagesCount;
        }

        public void DownloadFunc(String url, int fileCount, String file)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            System.IO.Stream dataStream = httpResponse.GetResponseStream();
            byte[] buffer = new byte[8192];

            FileStream fs = new FileStream(folder + Convert.ToString(fileCount) + "." + file,
            FileMode.Create, FileAccess.Write);
            int size = 0;
            do
            {
                size = dataStream.Read(buffer, 0, buffer.Length);
                if (size > 0)
                    fs.Write(buffer, 0, size);
            } while (size > 0);
            fs.Close();

            httpResponse.Close();

        }

        private void btnGetPages_Click(object sender, EventArgs e)
        {
            ProcessBarState.Value = 0;
            HackListView.Items.Clear();
            Pages.Clear();
            LinkURL.Clear();
            loadPagesCount(textbox_MainURL.Text, sender);
            //Avoid Memory Leak
            GC.Collect();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            folder = System.Environment.CurrentDirectory + "\\DownloadFile\\";


        }

        private void ThreadProcLoad()
        {

            //Avoid Memory Leak
            GC.Collect();
            while (Pages.Count > 0)
            {
                //LoagPage

                String data = spwc.DownloadString(Pages.Dequeue(), Encoding.UTF8);

                //切割圖片網址
                Match m;
                string HRefPattern = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))";

                try
                {
                    m = Regex.Match(data, HRefPattern,
                                    RegexOptions.IgnoreCase | RegexOptions.Compiled,
                                    TimeSpan.FromSeconds(1));
                    while (m.Success)
                    {
                        if (m.Groups[1].ToString().Contains("http://g.e-hentai.org/s/"))
                        {
                              LinkURL.Enqueue(m.Groups[1].ToString());
                        }
                        m = m.NextMatch();
                    }
                }
                catch (RegexMatchTimeoutException)
                {
                    Console.WriteLine("The matching operation timed out.");
                }
                finally
                {


                    ThreadDownload();
                }
            }

            ProcessBarState.Value = 0;
            ProcessBarState.Maximum = HackListView.Items.Count;

            try
            {
                //建立該檔案目錄,若整體長度>260會錯誤! 
                String folder = System.Environment.CurrentDirectory + "\\DownloadFile\\" + donwloadTile;
                if (folder.Length >250)
                {
                    donwloadTile = DateTime.Now.ToString("yyyy-MM-dd-hh-mm");
                }
                donwloadTile = donwloadTile.Replace("\\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace("<", "").Replace(">", "").Replace("|", "").Replace("E-Hentai Galleries", "");
               
                System.IO.Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\DownloadFile\\" + donwloadTile);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please change your dir\n請修正你的檔案目錄\n因目錄總長度大於260字元");
                btnGetPages.Enabled = true;
                btnGetPages.Text = "開始下載";
                return;
            }

            for (int i = 0; i < HackListView.Items.Count; i++)
            {
                ProcessBarState.Value++;
                Console.WriteLine(HackListView.Items[i].SubItems[1].Text);
                DownloadFunc(HackListView.Items[i].SubItems[1].Text, folder + donwloadTile + "\\" + (HackListView.Items[i].SubItems[0].Text + (HackListView.Items[i].SubItems[2].Text)));
                StatusNow.Text = "Download(" + (i + 1) + "/" + HackListView.Items.Count + ")";
            }

            MessageBox.Show("FINISH!");


            btnGetPages.Enabled = true;
            btnGetPages.Text = "開始下載";


        }

        private void ThreadDownload()
        {
            //Avoid Memory Leak
            GC.Collect();
            while (LinkURL.Count > 0)
            {
                //LoagPage

                HttpWebClient download = new HttpWebClient(new CookieContainer());
                download.Encoding = Encoding.UTF8;
                byte[] byteArray = download.DownloadData(LinkURL.Dequeue());
                String data = Encoding.UTF8.GetString(byteArray);

                //切割圖片網址
                Match m;
                string HRefPattern = "<img id.+?src=[\"'](.+?)[\"'].*?>";

                try
                {
                    m = Regex.Match(data, HRefPattern,
                                    RegexOptions.IgnoreCase | RegexOptions.Compiled,
                                    TimeSpan.FromSeconds(1));
                    while (m.Success)
                    {
                        if (m.Groups[1].ToString().Contains("jpg"))
                        {
                            this.HackListView.Items.Add(new ListViewItem(new String[] { (HackListView.Items.Count + 1).ToString(), m.Groups[1].ToString(), ".jpg" }));

                        }
                        if (m.Groups[1].ToString().Contains("png"))
                        {
                            this.HackListView.Items.Add(new ListViewItem(new String[] { (HackListView.Items.Count + 1).ToString(), m.Groups[1].ToString(), ".png" }));
                        }
                        if (m.Groups[1].ToString().Contains("gif"))
                        {
                            this.HackListView.Items.Add(new ListViewItem(new String[] { (HackListView.Items.Count + 1).ToString(), m.Groups[1].ToString(), ".gif" }));
                        }
                        m = m.NextMatch();
                    }
                }
                catch (RegexMatchTimeoutException)
                {
                    Console.WriteLine("The matching operation timed out.");
                }


            }



        }


        public static void DownloadFunc(String url, String downloadPath)
        {
            //Avoid Memory Leak
            GC.Collect();
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                System.IO.Stream dataStream = httpResponse.GetResponseStream();
                byte[] buffer = new byte[8192];

                FileStream fs = new FileStream(downloadPath,
                FileMode.Create, FileAccess.Write);
                int size = 0;
                do
                {
                    size = dataStream.Read(buffer, 0, buffer.Length);
                    if (size > 0)
                        fs.Write(buffer, 0, size);
                } while (size > 0);
                fs.Close();

                httpResponse.Close();
            }
            catch (Exception e)
            {

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://never-nop.com");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            //Close Thread
            try
            {
                newThread.Abort();
            }
            catch (Exception ex) { }
        }
    }


}


