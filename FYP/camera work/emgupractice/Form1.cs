using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using Dynamsoft.DotNet.TWAIN.Enums;
using Dynamsoft.DotNet.TWAIN.WebCamera;
using System.Web;
using WebCam_Capture;

namespace emgupractice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dynamicDotNetTwain1.IfShowUI = true;
            dynamicDotNetTwain1.SupportedDeviceType = EnumSupportedDeviceType.SDT_WEBCAM;
            dynamicDotNetTwain1.IfThrowException = true;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                dynamicDotNetTwain1.SelectSource();
                dynamicDotNetTwain1.SetVideoContainer(pictureBox1);
                dynamicDotNetTwain1.OpenSource();
                //List the source name and resolutions
                textBox1.Text = dynamicDotNetTwain1.CurrentSourceName;
                int count = dynamicDotNetTwain1.ResolutionForCamList.Count;
                for (int j = 0; j < count; j++)
                {
                    string tempHeight = dynamicDotNetTwain1.ResolutionForCamList[j].Height.ToString();
                    string tempWidth = dynamicDotNetTwain1.ResolutionForCamList[j].Width.ToString();
                    string tempResolution = tempWidth + "X" + tempHeight;
                    comboBox1.Items.Insert(j, tempResolution);
                    comboBox1.SelectedIndex = 0;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        WebCam webcam;
        private void Form1_Load(object sender, EventArgs e)
        {
            webcam = new WebCam();
            webcam.InitializeWebCam(ref pictureBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string serverName = "localhost"; //please update the server name accordingly
            string actionPagePath = "/UseWebcamInCSharp/SaveToFile.aspx";
            dynamicDotNetTwain1.HTTPUploadAllThroughPostAsPDF(serverName, actionPagePath, "test.pdf");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
               dynamicDotNetTwain1.RemoveAllImages();
               dynamicDotNetTwain1.EnableSource();
               pictureBox2.Image = dynamicDotNetTwain1.GetImage(0);
               pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "test.pdf";
            saveFileDialog.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dynamicDotNetTwain1.SaveAllAsPDF(saveFileDialog.FileName);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamicDotNetTwain1.ResolutionForCam = dynamicDotNetTwain1.ResolutionForCamList[comboBox1.SelectedIndex];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            webcam.Start();
            webcam.Continue();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox1.Image;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }



    }

    class WebCam
    {
        private WebCamCapture webcam;
        private System.Windows.Forms.PictureBox _FrameImage;
        private int FrameNumber = 50;
        public void InitializeWebCam(ref System.Windows.Forms.PictureBox ImageControl)
        {
            webcam = new WebCamCapture();
            webcam.FrameNumber = ((ulong)(0ul));
            webcam.TimeToCapture_milliseconds = FrameNumber;
            webcam.ImageCaptured += new WebCamCapture.WebCamEventHandler(webcam_ImageCaptured);
            _FrameImage = ImageControl;
        }

        void webcam_ImageCaptured(object source, WebcamEventArgs e)
        {
            _FrameImage.Image = e.WebCamImage;
        }

        public void Start()
        {
            webcam.TimeToCapture_milliseconds = FrameNumber;
            webcam.Start(0);
        }

        public void Stop()
        {
            webcam.Stop();
        }

        public void Continue()
        {
            // change the capture time frame
            webcam.TimeToCapture_milliseconds = FrameNumber;

            // resume the video capture from the stop
            webcam.Start(this.webcam.FrameNumber);
        }

        public void ResolutionSetting()
        {
            webcam.Config();
        }

        public void AdvanceSetting()
        {
            webcam.Config2();
        }

    }
}
