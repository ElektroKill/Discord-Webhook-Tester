using System;
using System.Threading;
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
            progressBar1.Maximum = Convert.ToInt32(numericUpDown1.Value);
            progressBar1.Value = 0;
            for (int i = 0; i <= numericUpDown1.Value - 1; i++)
            {
                DiscordWebhook dw = new DiscordWebhook(textBox1.Text);
                dw.TTS = checkBox1.Checked;
                dw.UserName = textBox2.Text;
                dw.Message = richTextBox1.Text;
                dw.AvatarURL = textBox3.Text;
                dw.Send();
                progressBar1.Value++;
                Thread.Sleep(250);
            }
        }
    }
    public class DiscordWebhook
    {
        private static NameValueCollection WebhookValues = new NameValueCollection();
        private WebClient discordClient;
        private string WebhookAdress { get; set; }
        public bool TTS { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public string AvatarURL { get; set; }     
        public DiscordWebhook(string WebhookURL)
        {
            discordClient = new WebClient();
            WebhookAdress = WebhookURL;
        }
        public void Send()
        {
            try
            {
                WebhookValues.Set("tts", TTS.ToString());
                WebhookValues.Set("username", UserName);
                WebhookValues.Set("avatar_url", AvatarURL);
                WebhookValues.Set("content", Message);
                discordClient.UploadValues(WebhookAdress, WebhookValues);
                discordClient.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :  " + e.ToString());
            }
        }
    }
}
