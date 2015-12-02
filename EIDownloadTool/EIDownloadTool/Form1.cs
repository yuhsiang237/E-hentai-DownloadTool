using System;
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
        String donwloadTile="";
        String folder;

        public   Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }




        public int loadPagesCount(String url,Object senser)
        {

            ((Button)senser).Text = "加載頁面中...";
            ((Button)senser).Enabled = false;
            int pagesCount = 1;
            byte[] byteArray;
            String data;
            try
            {
                //取得下載標題

                HttpWebClient title = new HttpWebClient(new CookieContainer());
                title.Encoding = Encoding.UTF8;
                byteArray = title.DownloadData(url);
                 data = Encoding.UTF8.GetString(byteArray);

                int first = data.IndexOf("<title>");
                int last = data.IndexOf("</title>");

                donwloadTile = data.Substring(first + 7, last - first - 7);
            }catch(Exception e)
            {

            }
           
            //取得下載頁數
            Pages.Clear();
            Pages.Enqueue(url);


            try
            {
              
                HttpWebClient download = new HttpWebClient(new CookieContainer());
                download.Encoding = Encoding.UTF8;
                byteArray = download.DownloadData(url);
                data = Encoding.UTF8.GetString(byteArray);

                for (int i = 1; ; i++)
                {
                    if (data.IndexOf(url + "?p=" + i) > -1)
                    {
                        pagesCount++;
                        Pages.Enqueue(url + "?p=" + i);
                    }
                    else
                    {
                        break;
                    }
                }

                StatusNow.Text = "成功獲取資料，頁數:" + pagesCount.ToString()+"，正在Trace中...";
             
                
                
            }
            catch(Exception e)
            {

            }
            finally
            {
                      System.Threading.Thread newThread = new System.Threading.Thread(this.ThreadProcLoad);
                       newThread.Start();
               
             }
         
           return pagesCount;
        }

        public  void DownloadFunc(String url, int fileCount, String file)
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
            Pages.Clear();
            LinkURL.Clear();
            loadPagesCount(textbox_MainURL.Text,sender);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            folder= System.Environment.CurrentDirectory+"\\DownloadFile\\";
    
                    
        }

        private void ThreadProcLoad()
        {


            while (Pages.Count > 0)
            {
                //LoagPage

                HttpWebClient download = new HttpWebClient(new CookieContainer());
                download.Encoding = Encoding.UTF8;
                byte[] byteArray = download.DownloadData(Pages.Dequeue());
                String data = Encoding.UTF8.GetString(byteArray);

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
                           // this.HackListView.Items.Add(new ListViewItem(new String[] { (HackListView.Items.Count + 1).ToString(), m.Groups[1].ToString() }));
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
     
                //建立該檔案目錄
                System.IO.Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\DownloadFile\\" + donwloadTile);

                for (int i = 0; i < HackListView.Items.Count; i++)
                {
                    ProcessBarState.Value++;
                    Console.WriteLine(HackListView.Items[i].SubItems[1].Text);
                    DownloadFunc(HackListView.Items[i].SubItems[1].Text, folder + donwloadTile + "\\" + (HackListView.Items[i].SubItems[0].Text + (HackListView.Items[i].SubItems[2].Text)));
                    StatusNow.Text = "Download(" + (i + 1) + "/" + HackListView.Items.Count +")";
                }

                MessageBox.Show("FINISH!");



                btnGetPages.Enabled = true;
                btnGetPages.Text = "開始下載";
     
        }

        private void ThreadDownload()
        {
            
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
            catch(Exception e)
            {

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://never-nop.com");
        }
    }


}


