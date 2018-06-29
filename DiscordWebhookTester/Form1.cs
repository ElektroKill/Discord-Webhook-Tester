using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Net;

namespace DiscordWebhookTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (DiscordWebhook Webhook = new DiscordWebhook())
            {
                Webhook.ProfilePicture = textBox3.Text;
                Webhook.UserName = textBox2.Text;
                Webhook.WebHook = textBox1.Text;
                Webhook.Send(richTextBox1.Text);
            }
        }
    }
    public class DiscordWebhook : IDisposable
    {
        private static NameValueCollection Values = new NameValueCollection();
        private readonly WebClient webClient1;
        public string WebHook { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
        public DiscordWebhook()
        {
            webClient1 = new WebClient();
        }
        public void Send(string msg)
        {
            try
            {
                Values.Set("username", UserName);
                Values.Set("avatar_url", ProfilePicture);
                Values.Set("content", msg);
                webClient1.UploadValues(WebHook, Values);
            }
            catch { }
        }
        public void Dispose()
        {
            webClient1.Dispose();
        }
    }
}
