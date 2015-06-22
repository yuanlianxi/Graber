using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Graber
{
    public partial class Form1 : Form
    {
        string[] areasName = "zhengzhou,xian".Split(',');
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            
            for (int ai = 0; ai < areasName.Length; ai++)
            {
                string fileName = string.Format("汽车之家二手车-{0}.txt", areasName[ai]);
                for (int i = 0; i < 5; i++)
                {
                    string urlSite = string.Format("http://www.che168.com/{0}/a0_0ms1dgscncgpilto8csp{1}ex/", areasName[ai], i+1);
                    string content = webClient.DownloadString(urlSite);
                    MatchCollection mc = Regex.Matches(content, @"<li[^<]+<div[^<]+<span[^<]+</span[^<]+<span[^<]+<span[^<]+<i[^<]+</i[^<]+</span[^<]+</span[^<]+<a[\s]+href=""([^""]+)""");
                    foreach (Match item in mc)
                    {
                        string targetUrl = item.Groups[1].Value;
                        string strUsedCarLink = string.Format("http://www.che168.com{0}\r\n", targetUrl);
                        File.AppendAllText(fileName, strUsedCarLink);
                    }

                }
            }
            MessageBox.Show("导出完成！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string carTypeRegexStr = @"<div class=""tit-name clearfix"">\s+<[^>]+>([^<]+)</h1>\s+</div>\s+<h2[^>]+>([^<]+)</h2>";
            string carUsedInfoRegexStr = @"<span class=""data-left"">\s+(\d+)[^\d]+(\d+)[^\s]+\s+<span[^<]+</span>\s+<span>([^.]+\.[\d]+)([^<]+)</span>";
            string carHolderNameRegexStr = @"<div class='part5' id='carOwnerInfo'>\s+<div><span class='font18'>([^<]+)</span>";
            string carHolderPhoneRegexStr = @"<a class='link999'  target=""_blank"" href=""/CarDetail/UcList_(\d+)";

            for (int ai = 0; ai < areasName.Length; ai++)
            {

            
            string fileName = string.Format("汽车之家二手车-{0}.txt", areasName[ai]);
            WebClient webClient = new WebClient();

            IEnumerable<string> links = File.ReadLines(fileName);
            string carInfoFileName = string.Format("汽车之家二手车车主联系信息-{0}.txt",areasName[ai]);
            foreach (var item in links)
            {
                string content = webClient.DownloadString(item);
                Match carTypeMatch = Regex.Match(content, carTypeRegexStr);
                Match carUsedInfoMatch = Regex.Match(content, carUsedInfoRegexStr);
                Match carHolderNameMatch = Regex.Match(content, carHolderNameRegexStr);
                Match carHolderPhoneMatch = Regex.Match(content, carHolderPhoneRegexStr);
                string result = string.Format("{0} {1}\t{2}/{3}\t{4}\t{5}\t{6}\t{7}\r\n"
                    , carTypeMatch.Groups[1].Value
                    , carTypeMatch.Groups[2].Value
                    , carUsedInfoMatch.Groups[1].Value
                    , carUsedInfoMatch.Groups[2].Value
                    , carUsedInfoMatch.Groups[3].Value
                    , carUsedInfoMatch.Groups[4].Value
                    , carHolderNameMatch.Groups[1].Value
                    , carHolderPhoneMatch.Groups[1].Value
                    );
                File.AppendAllText(carInfoFileName, result);
            }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Graber graber = new Graber();
            graber.LoadFromConfigFile("grabsite.xml");
            graber.Grab();
        }
    }
}
