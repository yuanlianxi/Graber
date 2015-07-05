using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using tessnet2;
using System.Reflection;
using System.IO;

namespace Graber
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(CommonTool.GetTextFromImage("gettel.jpg"));
            MessageBox.Show(CommonTool.GetTextFromImage("gettel1.jpg"));
            //tessnet2.Tesseract tesseract = new tessnet2.Tesseract();
            //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");
            ////tesseract.SetVariable("num","0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
            //tesseract.Init(null, "eng", false);
            //List<Word> lstWord = tesseract.DoOCR(Bitmap.FromFile("gettel.jpg") as Bitmap, Rectangle.Empty);
            //MessageBox.Show(lstWord[0].Text);
        }
    }
}