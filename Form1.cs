using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyTabs;
using Microsoft.Win32;

namespace MiNavegador
{
    public partial class Form1 : Form
    {
        public string historial = "", html = "";

        protected TitleBarTabs ParentTabs
        {
            get
            {
                return (ParentForm as TitleBarTabs);
            }
        }

        public Form1()
        {
            InitializeComponent();

            //Cambiar el navegador por defecto del webview
            var appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe";
            using (var Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true))
                Key.SetValue(appName, 99999, RegistryValueKind.DWord);

            webBrowser1.Navigate("https://www.bing.com/");
            customizeDesign();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward) webBrowser1.GoForward();
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) 
                webBrowser1.Navigate("https://" + guna2TextBox1.Text  + "/");  
            
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {        
            if (guna2TextBox1.Text.Trim() != "")
                webBrowser1.Navigate("https://" + guna2TextBox1.Text + "/");
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.bing.com/");
            guna2TextBox1.Text = "";
        }

        private void customizeDesign()
        {
            panel3.Visible = false;
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            panel3.Visible = !panel3.Visible;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            using (Historial h = new Historial(html))
            {
                h.ShowDialog();
               
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack) webBrowser1.GoBack();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            using (Creador c = new Creador())
            {
                c.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            using (Ayuda a = new Ayuda())
            {
                a.ShowDialog();
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            historial += webBrowser1.Url.ToString() + "<br>";
            html = "<html>" +
            "<div " +
            "style=\"width:450px; text-overflow: ellipsis; " +
            "word-wrap: break-word; overflow: scroll; max-height: 150px;>" +
            historial +
            "</div>" +
            "</html>";
        }
    }
}
