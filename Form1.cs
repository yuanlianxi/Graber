using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tesseract;

namespace Graber
{
    public partial class Form1 : Form
    {
        private string[] areasName = "zhengzhou,xian".Split(',');

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 512;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Graber graber = new Graber();
            graber.LoadFromConfigFile("grabsitecaihong.xml");
            graber.Grab();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Graber graber = new Graber();
            graber.LoadFromConfigFile("grabsitedongbei.xml");
            graber.Grab();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Graber graber = new Graber();
            graber.LoadFromConfigFile("grabsitehuaxizhongnan.xml");
            graber.Grab();
        }


        private void button9_Click_1(object sender, EventArgs e)
        {
            Graber graber = new Graber();
            graber.LoadFromConfigFile("grabsitehuabei.xml");
            graber.Grab();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Graber graber = new Graber();
            graber.LoadFromConfigFile("yichecaihong.xml");
            graber.Grab();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graber graber = new Graber();
            graber.LoadFromConfigFile("yichedongbei.xml");
            graber.Grab();
        }
    }
}