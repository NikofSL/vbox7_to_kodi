using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace JsonToKodi
{
    public partial class Form1 : Form
    {

        string username = "osmcLG";
        string password = "parolalg";
        string ipAddr = "192.168.1.11";
        public Form1()
        {
            InitializeComponent();
        }

        private JObject jsonQuery(JObject json) 
        {
            username = this.textBox2.Text;
            password = this.textBox3.Text;

            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            
            var url = "http://" + this.textBox1.Text + "/jsonrpc";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Headers.Add("Authorization", "Basic " + encoded);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JObject.Parse(result);
                //JObject respons = JObject.Parse(result);
                //MessageBox.Show(respons["result"]["time"].ToString());
                //MessageBox.Show(respons["result"]["totaltime"].ToString());
            }
        }

        private void btn_clear_pls_Click(object sender, EventArgs e)
        {
            string jsonStringClear_pl = @"{
                            ""jsonrpc"":""2.0"",
                            ""id"":""1"",
                            ""method"":""Playlist.Clear"",
                            ""params"":{""playlistid"": 1}
                            }";
            JObject jsonClear_pl = JObject.Parse(jsonStringClear_pl);
            jsonQuery(jsonClear_pl);
        }

        private void btn_create_pls_Click(object sender, EventArgs e)
        {
            string jsonStringCreate_pl = @"{
                            ""jsonrpc"":""2.0"",
                            ""id"":""1"",
                            ""method"":""Playlist.Insert"",
                            ""params"":[1,0,{ ""file"":""plugin://plugin.video.vbox7/?url=VOD&mode=14&name=%D0%97%D0%B0%D1%80%D0%B5%D0%B4%D0%B8+%D0%B2%D0%B8%D0%B4%D0%B5%D0%BE+%D0%BF%D0%BE+%D0%BD%D0%B5%D0%B3%D0%BE%D0%B2%D0%BE%D1%82%D0%BE+ID&video_id=";
            jsonStringCreate_pl += this.vbox7_url.Text;
            jsonStringCreate_pl += @"""}],""id"":55
                            }"; ;


            JObject jsonCreate_pl = JObject.Parse(jsonStringCreate_pl);
            jsonQuery(jsonCreate_pl);
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            string jsonStringPlay = @"{
                            ""jsonrpc"":""2.0"",
                            ""id"":""1"",
                            ""method"":""Player.Open"",
                            ""params"":{ ""item"":{ ""position"":0,""playlistid"":1},""options"":{ } },""id"":56
                            }";
            JObject jsonPlay = JObject.Parse(jsonStringPlay);
            jsonQuery(jsonPlay);
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            string jsonStringStop = @"{
                            ""jsonrpc"":""2.0"",
                            ""id"":""1"",
                            ""method"":""Player.Stop"",
                            ""params"":{""playerid"":1}
                            }";
            JObject jsonStop = JObject.Parse(jsonStringStop);
            jsonQuery(jsonStop);
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            string jsonStringPlayPause = @"{
                            ""jsonrpc"":""2.0"",
                            ""id"":""1"",
                            ""method"":""Player.PlayPause"",
                            ""params"":{""playerid"":1}
                            }";
            JObject jsonPlayPause = JObject.Parse(jsonStringPlayPause);
            jsonQuery(jsonPlayPause);
        }

        private void btn_get_time_Click(object sender, EventArgs e)
        {
            string jsonStringPlayPause = @"{
                            ""jsonrpc"":""2.0"",
                            ""id"":""1"",
                            ""method"":""Player.GetProperties"",
                            ""params"":{""playerid"":1, ""properties"":[""time"", ""totaltime""]},""id"":1
                            }";
            JObject jsonPlayPause = JObject.Parse(jsonStringPlayPause);
            //jsonQuery(jsonPlayPause);

            JObject jsonGetTime = jsonQuery(jsonPlayPause);
            string hours = jsonGetTime["result"]["time"]["hours"].ToString();
            string minutes = jsonGetTime["result"]["time"]["minutes"].ToString();
            string seconds = jsonGetTime["result"]["time"]["seconds"].ToString();

            string hoursT = jsonGetTime["result"]["totaltime"]["hours"].ToString();
            string minutesT = jsonGetTime["result"]["totaltime"]["minutes"].ToString();
            string secondsT = jsonGetTime["result"]["totaltime"]["seconds"].ToString();

            label1.Text = hours + ":" + minutes + ":" + seconds + "\n" + hoursT + ":" + minutesT + ":" + secondsT;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            btn_stop.PerformClick();
            btn_clear_pls.PerformClick();
            btn_create_pls.PerformClick();
            btn_play.PerformClick();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = ipAddr;
            this.textBox2.Text = username;
            this.textBox3.Text = password;
        }


    }
}