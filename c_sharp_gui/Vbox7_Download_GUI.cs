using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Security.Authentication;
using Vbox7_Download_GUI;


namespace Vbox7_To_Kodi_GUI
{
    public partial class Form1 : Form
    {
        bool have_url = false;
        string api4 = "http://api.vbox7.com/v4/?action=";
        string token4 = "&app_token=imperia_android_0.1.0_3rG7jk";

        public WebClient Client { get; private set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void downloadFile() {
            string url_id = this.url_address.Text;
            string video_url = "";
            string video_title = "";
            string file_type = "";
            bool have_url = false;

            
            try
            {
                if (url_id[url_id.Length - 1] == '#')
                {
                    url_id = url_id.Substring(0, url_id.Length - 1);
                }
                string[] words = url_id.Split(':');
                url_id = words[2];
                string url = api4 + "r_video_play&video_md5=" + url_id + token4;



                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject json = JObject.Parse(result);
                    video_url = json["items"][0]["video_location"].ToString();
                    video_title = json["title"].ToString();
                    file_type = video_url.Substring(video_url.Length -4);
                    //string message = "Do you want to close this window?";
                    //string title = "Close Window";
                    //using (MessageBox_form f2 = new MessageBox_form())
                    //{
                    //    f2.set_videoLabel(video_title);
                    //    f2.ShowDialog(this);
                    //}
                    //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    //DialogResult btn_result = MessageBox.Show(video_url, video_title, buttons);
                    //if (btn_result == DialogResult.Yes)
                    //{
                    //    this.Close();
                    //}
                    //else
                    //{
                    //    // Do something  
                    //}


                    //MessageBox.Show(video_title + " - " + video_url);
                    //Clipboard.SetText(json["items"][0]["video_location"].ToString());

                    have_url = true;

                }
                
            }
            catch (Exception)
            {
                have_url = false;
                //throw;
            }
            if (have_url)
            {
                try
                {
                    //var sourceFile = @"d:\video.mp4";
                    var sourceFile = video_url;
                    Client = new WebClient();

                    var saveFileDialog = new SaveFileDialog();
                    //You can offer a default name
                    //saveFileDialog.FileName = "video-copy.avi";
                    saveFileDialog.FileName = video_title + file_type;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        
                        Client.DownloadProgressChanged += (s, p) =>
                        {
                            progressBar.Value = p.ProgressPercentage;
                        };
                        Client.DownloadFileCompleted += (s, p) =>
                        {
                            progressBar.Visible = false;
                            if (p.Cancelled)
                            {
                                MessageBox.Show("The download has been cancelled");
                                return;
                            }
                            if (p.Error != null) // We have an error! Retry a few times, then abort.
                            {
                                MessageBox.Show("An error ocurred while trying to download file");

                                return;
                            }

                            MessageBox.Show("File succesfully downloaded" + " - "+video_title);
                            // any other code to process the file
                        };
                        using (Client)
                        {
                            Client.DownloadFileAsync(new Uri(video_url), saveFileDialog.FileName);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            
        }
        private void downloadSubs() {
            string url_id = this.url_address.Text;
            string video_title = "";
            string subs_url = "";
            bool have_subs = false;


            try
            {
                if (url_id[url_id.Length - 1] == '#')
                {
                    url_id = url_id.Substring(0, url_id.Length - 1);
                }
                string[] words = url_id.Split(':');
                url_id = words[2];
                string url = api4 + "r_video_play&video_md5=" + url_id + token4;

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject json = JObject.Parse(result);
                    subs_url = json["items"][0]["video_subtitles_path"].ToString();
                    video_title = json["title"].ToString();
                    MessageBox.Show(video_title + " - " + subs_url);
                    //Clipboard.SetText(json["items"][0]["video_subtitles_path"].ToString());
                    have_subs = true;
                }
            }
            catch (Exception)
            {
                have_subs = false;
                //throw;
            }
            try
            {
                const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
                const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
                ServicePointManager.SecurityProtocol = Tls12;
                string html = string.Empty;
                //string urll = @"https://i49.vbox7.com/subtitles/715/117715_2.js?t=1655058810";
                string urll = subs_url;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urll);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
                {
                    html = reader.ReadToEnd();
                }
                MessageBox.Show(html);
            }
            catch (Exception)
            {

               // throw;
            }
           
            ////Encoding
            //byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(html);
            //string returntext = System.Convert.ToBase64String(mybyte);
            //Decoding
            //byte[] mybyte = System.Convert.FromBase64String(html);
            //string returntext = System.Text.Encoding.UTF8.GetString(mybyte);
            ////Encoding
            //mybyte = System.Text.Encoding.Unicode.GetBytes(returntext);
            //returntext = System.Convert.ToBase64String(mybyte);
            ////Decoding
            //mybyte = System.Convert.FromBase64String(returntext);
            //returntext = System.Text.Encoding.UTF8.GetString(mybyte);

            

            //if (have_url)
            //{
            //    try
            //    {
            //        //var sourceFile = @"d:\video.mp4";
            //        var sourceFile = video_url;
            //        Client = new WebClient();

            //        var saveFileDialog = new SaveFileDialog();
            //        //You can offer a default name
            //        //saveFileDialog.FileName = "video-copy.avi";
            //        saveFileDialog.FileName = video_title + ".mp4";
            //        if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //        {

            //            Client.DownloadProgressChanged += (s, p) =>
            //            {
            //                progressBar.Value = p.ProgressPercentage;
            //            };
            //            Client.DownloadFileCompleted += (s, p) =>
            //            {
            //                progressBar.Visible = false;
            //                if (p.Cancelled)
            //                {
            //                    MessageBox.Show("The download has been cancelled");
            //                    return;
            //                }
            //                if (p.Error != null) // We have an error! Retry a few times, then abort.
            //                {
            //                    MessageBox.Show("An error ocurred while trying to download file");

            //                    return;
            //                }

            //                MessageBox.Show("File succesfully downloaded" + " - " + video_title);
            //                // any other code to process the file
            //            };
            //            using (Client)
            //            {
            //                Client.DownloadFileAsync(new Uri(video_url), saveFileDialog.FileName);
            //            }
            //        }
            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
            //}
        }
        private void url_address_Click(object sender, EventArgs e)
        {
            if (!have_url)
            {
                have_url = true;
                this.url_address.Text = "";
            }
        }
        private void btn_download_Click(object sender, EventArgs e)
        {
            progressBar.Visible = true;
            progressBar.Value = 0;
            downloadFile();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Client.CancelAsync();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void btn_subtitles_Click(object sender, EventArgs e)
        {
            downloadSubs();
        }
    }
}
