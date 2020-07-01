using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using MySql.Data.MySqlClient;
using System.Globalization;
using AForge.Imaging.Filters;
using Accord.Imaging;
using AForge.Math;
using AForge;
using Accord.Imaging.Filters;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.GPU;
using System.Diagnostics;
using System.Web;
using WebCam_Capture;
using Dynamsoft.DotNet.TWAIN.Enums;
using Dynamsoft.DotNet.TWAIN.WebCamera;

namespace Project
{
    
    public partial class fingerprints : Form
    {
        string Mysql = "server=localhost;Database=fingerprints;Uid=root;Pwd=302420;default command timeout=60";
        string fname1, fname2,fname3;
        Bitmap im33,siftimage,siftimage2;
        Bitmap newBitmap2, newBitmap1;
        StringBuilder stream = new StringBuilder();
        StringBuilder stream1 = new StringBuilder();
        StringBuilder stream2 = new StringBuilder();
        List<string> list = new List<string>();
        List<string> list1 = new List<string>();
        List<string> list2 = new List<string>();
        int harris_count1 = 0, harris_count2 = 0, harris_count3=0;
        bool FeaturebuttonWasClicked;
        bool Enterbuttonpressed=false;
        Signature sign = new Signature();
        int cropX;
        int cropY;
        int cropWidth;
        int cropHeight;
        public Pen cropPen;
        public DashStyle cropDashStyle = DashStyle.DashDot;
        WebCam webcam,webcam1;
        bool stopwebcambutton = false;

        public fingerprints()
        {
            InitializeComponent();
            
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = new Size(424, 295);
            timer1.Start();
            main_panel.Visible = false;
            fingerprint_panel.Visible = false;
            database_groupBox_fingr.Visible = false;
            database_panel_fingrprints.Visible = false;
            login_panel.Visible = true;
            error_label.Visible = false;
            usermanagemnt_panel.Visible = false;
            signature_panel.Visible = false;
            database_panel_signature_ver.Visible = false;
            dynamicDotNetTwain1.IfShowUI = true;
            dynamicDotNetTwain1.SupportedDeviceType = EnumSupportedDeviceType.SDT_WEBCAM;
            dynamicDotNetTwain1.IfThrowException = true;
            dynamicDotNetTwain1.Hide();
        }

        private void fingerprints_Load(object sender, EventArgs e)
        {
           webcam = new WebCam();
           webcam1 = new WebCam();
           webcam.InitializeWebCam(ref fingr_picbox1);
           webcam1.InitializeWebCam(ref pictureBox2_signature_panel);
        }

        private void fingerprint_btn_Click(object sender, EventArgs e)
        {
            main_panel.Visible = false;
            usermanagemnt_panel.Visible = false;
            fingerprint_panel.Visible = true;
            fingerprint_dataGridView.Visible = false;
            database_panel_fingrprints.Visible = false;
            this.ClientSize = new Size(1071, 552);
            fingrprint_groupBox1.Enabled = false;
            database_groupBox_fingr.Visible = true;
            start_webcam_btn_fingr_panel.Visible = false;
            stop_webcam_btn_fingr_panel.Visible = false;
            capture_image_btn_fingr_panel.Visible = false;
            crop_image_btn_fingr_panel.Visible = false;
            save_image_btn_finger_panel.Visible = false;
            signature_panel.Visible = false;
            database_panel_signature_ver.Visible = false;
        }

        private void back_btn_fingr_penel_Click(object sender, EventArgs e)
        {
            this.ClientSize = new Size(346, 266);
            main_panel.Visible = true;
            fingerprint_panel.Visible = false;
            fingerprint_dataGridView.Visible = false;
            if (fingr_picbox1.Image != null)
            {
                fingr_picbox1.Image.Dispose();
                fingr_picbox1.Image = null;
            }
            if (fingr_picbox2.Image != null)
            {
                fingr_picbox2.Image.Dispose();
                fingr_picbox2.Image = null;
            }
            stream1.Clear();
            stream.Clear();
            database_groupBox_fingr.Visible = false;
            
        }

        private void cancel_btn_fingr_panel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void import_btn_picbox1_fingr_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FileName = "";
                openFileDialog1.Title = "Images";
                openFileDialog1.Filter = "All Images|*.jpg; *.bmp; *.png";
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName.ToString() != "")
                {
                    fname1 = openFileDialog1.FileName.ToString();
                }
                fingr_picbox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Import the image properly");
            }
        }

        private void import_btn_picbox2_fingr_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FileName = "";
                openFileDialog1.Title = "Images";
                openFileDialog1.Filter = "All Images|*.jpg; *.bmp; *.png";
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName.ToString() != "")
                {
                    fname2 = openFileDialog1.FileName.ToString();
                }
                fingr_picbox2.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Import the image properly");
            }
        }

        private void comboBox1_inputs_finger_SelectedIndexChanged(object sender, EventArgs e)
        {
            fingrprint_groupBox1.Enabled = true;
            if (comboBox1_inputs_finger.Text == "From User")
            {
                if (fingr_picbox2.Image != null)
                {
                    fingr_picbox2.Image.Dispose();
                    fingr_picbox2.Image = null;
                }
                if (fingr_picbox1.Image != null)
                {
                    fingr_picbox1.Image.Dispose();
                    fingr_picbox1.Image = null;
                }
                import_btn_picbox1_fingr.Enabled = true;
                import_btn_picbox2_fingr.Enabled = true;
                fingerprint_dataGridView.Visible = false;
                extract_fechr_btn2.Enabled = true;
                extract_fechr_btn1.Enabled = true;
                Enterbuttonpressed = false;
                start_webcam_btn_fingr_panel.Visible = false;
                stop_webcam_btn_fingr_panel.Visible = false;
                capture_image_btn_fingr_panel.Visible = false;
                crop_image_btn_fingr_panel.Visible = false;
                save_image_btn_finger_panel.Visible = false;
            }
            if (comboBox1_inputs_finger.Text == "From Database")
            {
                if (fingr_picbox2.Image != null)
                {
                    fingr_picbox2.Image.Dispose();
                    fingr_picbox2.Image = null;
                }
                if (fingr_picbox1.Image != null)
                {
                    fingr_picbox1.Image.Dispose();
                    fingr_picbox1.Image = null;
                }
                Enterbuttonpressed = false;
                import_btn_picbox1_fingr.Enabled = true;
                import_btn_picbox2_fingr.Enabled = false;
                fingerprint_dataGridView.Visible = true;
                extract_fechr_btn2.Enabled = true;
                extract_fechr_btn1.Enabled = true;
                start_webcam_btn_fingr_panel.Visible = false;
                stop_webcam_btn_fingr_panel.Visible = false;
                capture_image_btn_fingr_panel.Visible = false;
                crop_image_btn_fingr_panel.Visible = false;
                save_image_btn_finger_panel.Visible = false;
                DataSet ds = Search();
                fingerprint_dataGridView.DataSource = ds.Tables[0].DefaultView;
                
            }
            if (comboBox1_inputs_finger.Text == "From Webcam")
            {
                Enterbuttonpressed = false;
                import_btn_picbox1_fingr.Enabled = false;
                import_btn_picbox2_fingr.Enabled = false;
                fingerprint_dataGridView.Visible = false;
                extract_fechr_btn2.Enabled = false;
                extract_fechr_btn1.Enabled = false;
                start_webcam_btn_fingr_panel.Visible = true;
                stop_webcam_btn_fingr_panel.Visible = true;
                capture_image_btn_fingr_panel.Visible = true;
                crop_image_btn_fingr_panel.Enabled = false;
                crop_image_btn_fingr_panel.Visible = true;
                save_image_btn_finger_panel.Visible = true;
                save_image_btn_finger_panel.Enabled = false;
                if (fingr_picbox1.Image != null)
                {
                    fingr_picbox1.Image.Dispose();
                    fingr_picbox1.Image = null;
                }
                if (fingr_picbox2.Image != null)
                {
                    fingr_picbox2.Image.Dispose();
                    fingr_picbox2.Image = null;
                }
            }
        }
        //start Webcam button in fingerprint main panel...
        private void start_webcam_btn_fingr_panel_Click(object sender, EventArgs e)
        {
            stopwebcambutton = false;
           // webcam.Start();
           // webcam.Continue();
            try
            {
                dynamicDotNetTwain1.SelectSource();
                dynamicDotNetTwain1.SetVideoContainer(fingr_picbox1);
                dynamicDotNetTwain1.OpenSource();
                //List the source name and resolutions
                int count = dynamicDotNetTwain1.ResolutionForCamList.Count;
                for (int j = 0; j < count; j++)
                {
                    string tempHeight = dynamicDotNetTwain1.ResolutionForCamList[j].Height.ToString();
                    string tempWidth = dynamicDotNetTwain1.ResolutionForCamList[j].Width.ToString();
                    string tempResolution = tempWidth + "X" + tempHeight;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            save_image_btn_finger_panel.Enabled = false;
            crop_image_btn_fingr_panel.Enabled = false;
        }
        //Capture image From picture box 1 in fingerprint panel...

        private void capture_image_btn_fingr_panel_Click(object sender, EventArgs e)
        {
           // fingr_picbox2.Image = fingr_picbox1.Image;
            try
            {
                dynamicDotNetTwain1.RemoveAllImages();
                dynamicDotNetTwain1.EnableSource();
                fingr_picbox2.Image = dynamicDotNetTwain1.GetImage(0);
                Bitmap img = new Bitmap(fingr_picbox2.Image);
                fingr_picbox2.Image = img;
                fingr_picbox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            save_image_btn_finger_panel.Enabled = false;
        }

        //Stop Webcam button in fingerprint panel..
        private void stop_webcam_btn_fingr_panel_Click(object sender, EventArgs e)
        {
           // webcam.Stop();
            dynamicDotNetTwain1.RemoveAllImages();
            dynamicDotNetTwain1.CloseSource();
            stopwebcambutton = true;
            save_image_btn_finger_panel.Enabled = true;
            if (fingr_picbox1.Image != null)
            {
                fingr_picbox1.Image.Dispose();
                fingr_picbox1.Image = null;
            }

        }
        //Mouse Down action listner on picture box 2 of fingerprint panel...

        private void fingr_picbox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Cursor = Cursors.Cross;
                cropX = e.X;
                cropY = e.Y;

                cropPen = new Pen(Color.Red, 1);
                cropPen.DashStyle = DashStyle.DashDotDot;


            }
            fingr_picbox2.Refresh();
            crop_image_btn_fingr_panel.Enabled = true;
        }
        //Mouse move action listner on picture box 2 of fingerprint panel...

        private void fingr_picbox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (fingr_picbox2.Image == null)
                return;


            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                fingr_picbox2.Refresh();
                cropWidth = e.X - cropX;
                cropHeight = e.Y - cropY;
                fingr_picbox2.CreateGraphics().DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight);
            }
        }
        //Crop image button in fingerprint panel...

        private void crop_image_btn_fingr_panel_Click(object sender, EventArgs e)
        {
            if (stopwebcambutton == false)
            {
                MessageBoxIcon iconType = MessageBoxIcon.Stop;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OK;
                iconType = MessageBoxIcon.Stop;
                DialogResult result =
                MessageBox.Show("First Stop the Webcam",
               "Error", buttonType, iconType, 0, 0);
            }
            else
            {
                Cursor = Cursors.Default;

                if (cropWidth < 1)
                {
                    return;
                }
                Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
                //First we define a rectangle with the help of already calculated points
                Bitmap OriginalImage = new Bitmap(fingr_picbox2.Image, fingr_picbox2.Width, fingr_picbox2.Height);
                //Original image
                Bitmap _img = new Bitmap(cropWidth, cropHeight, PixelFormat.Format24bppRgb);
                // for cropinf image
                Graphics g = Graphics.FromImage(_img);
                // create graphics
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //set image attributes
                g.DrawImage(OriginalImage, 0, 0, rect, GraphicsUnit.Pixel);

                fingr_picbox2.Image = _img;
                fingr_picbox2.SizeMode = PictureBoxSizeMode.StretchImage;
                crop_image_btn_fingr_panel.Enabled = false;
            }
        }
        //Save image button in fingerprint panel...
        private void save_image_btn_finger_panel_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(fingr_picbox2.Image);
            fingr_picbox1.Image = img;
            
            import_btn_picbox1_fingr.Enabled = false;
            import_btn_picbox2_fingr.Enabled = true;
            fingerprint_dataGridView.Visible = true;
            extract_fechr_btn2.Enabled = true;
            extract_fechr_btn1.Enabled = true;

            start_webcam_btn_fingr_panel.Visible = false;
            stop_webcam_btn_fingr_panel.Visible = false;
            capture_image_btn_fingr_panel.Visible = false;
            crop_image_btn_fingr_panel.Visible = false;
            save_image_btn_finger_panel.Visible = false;
            try
            {
                DataSet ds = Search();
                fingerprint_dataGridView.DataSource = ds.Tables[0].DefaultView;
                if (fingr_picbox2.Image != null)
                {
                    fingr_picbox2.Image.Dispose();
                    fingr_picbox2.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }
        }
        //Compare with whole database button.... in fingerprints panel

        private void comp_im_with_whole_db_btn_Click(object sender, EventArgs e)
        {
            import_btn_picbox2_fingr.Enabled = false;
            fingerprint_dataGridView.Visible = false;
            import_btn_picbox1_fingr.Enabled = true;
            extract_fechr_btn2.Enabled = false;
            fingrprint_groupBox1.Enabled = true;
            Enterbuttonpressed = true;
            if (fingr_picbox2.Image != null)
            {
                fingr_picbox2.Image.Dispose();
                fingr_picbox2.Image = null;
            }
            
        }
        //End of compare button...
        //Cell content Double click on Datagrid view which is on fingerprint panel...
        private void fingerprint_dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                   
                    DataGridViewRow row = fingerprint_dataGridView.Rows[e.RowIndex];
                    if (fingr_picbox2.Image != null)
                    {
                        fingr_picbox2.Image.Dispose();
                        fingr_picbox2.Image = null;
                    }
                    // set image from gridview to picture box.....
                    var data = (Byte[])(row.Cells["image"].Value);
                    var stream = new MemoryStream(data);
                    fingr_picbox2.Image = Image.FromStream(stream);
                    fingr_picbox2.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(""+ex);
            }
        }  
        //End of cell content Double click...
        public DataSet Search()
        {
            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            try
            {

                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * From storedata";
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;

            }
            catch (Exception ex)
            {
                Console.WriteLine("error while entering data." + ex);
            }
            return null;

            connection.Close();
        }
        // convert image into bytes....
        public static byte[] ImageToByteArray(System.Drawing.Image img, PictureBox pictureBox)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            if (pictureBox.Image != null)
            {

                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            return ms.ToArray();
        }//End of image conversion.......
        //*********************************************************************************************************************************************************
        //========================================== Feature Extraction Button on Picture Box 1 of Fingerprints Section ===========================================
        //******************************************                                                                    *******************************************
        //=========================================================================================================================================================

        private void extract_fechr_btn1_Click(object sender, EventArgs e)
        {
            try
            {
                if (fingr_picbox1.Image == null)
                {
                    MessageBoxIcon iconType = MessageBoxIcon.Stop;
                    MessageBoxButtons buttonType = MessageBoxButtons.OK;
                    buttonType = MessageBoxButtons.OK;
                    iconType = MessageBoxIcon.Stop;
                    DialogResult result =
                    MessageBox.Show("Import An Image First",
                   "Sorry", buttonType, iconType, 0, 0);
                }
                else
                {
                    stream1.Clear();
                    int count = 0;
                    Bitmap newbitmap;
                    Color newColor = Color.FromArgb(255, 255, 255);
                    Color backcolr = Color.FromArgb(0, 0, 0);
                    im33 = (Bitmap)fingr_picbox1.Image;
                    siftimage = im33;
                    newbitmap = new Bitmap(im33.Width, im33.Height);
                    HarrisCornersDetector hcd = new HarrisCornersDetector();
                    System.Collections.Generic.List<AForge.IntPoint> corners1 = hcd.ProcessImage(im33);
                    // process points
                    for (int i = 0; i < im33.Width; i++)
                    {
                        for (int j = 0; j < im33.Height; j++)
                        {
                            newbitmap.SetPixel(i, j, backcolr);
                        }
                    }
                    harris_count1 = 0;
                    foreach (IntPoint corner in corners1)
                    {
                        newbitmap.SetPixel(corner.X, corner.Y, newColor);
                        harris_count1++;
                    }
                    
                    //======================================Gray Scale=================================================//

                    Bitmap gsImage = Grayscale.CommonAlgorithms.BT709.Apply(im33);
                    fingr_picbox1.Image = (Bitmap)gsImage;
                    //======================================End=================================================//



                    //======================================Noise Removie=================================================//

                    Bitmap image;
                    Bitmap imge1 = (Bitmap)fingr_picbox1.Image;
                    Median filter = new Median();
                    image = filter.Apply(imge1);
                    if (fingr_picbox1.Image != null)
                    {
                        fingr_picbox1.Image.Dispose();
                        fingr_picbox1.Image = null;
                    }

                    fingr_picbox1.Image = image;
                    fingr_picbox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    //======================================End=================================================//



                    //======================================Canny Edge=================================================//

                    Bitmap image1 = (Bitmap)fingr_picbox1.Image;
                    CannyEdgeDetector filter2 = new CannyEdgeDetector();
                    Bitmap edge = filter2.Apply(image1);
                    fingr_picbox1.Image = (Bitmap)edge;
                    //======================================End=================================================//

                    //======================================Minutiae POints ============================================//

                    byte edge1, edge2, edge3, edge4, edge5, edge6, edge7, edge8;
                    Bitmap img1 = (Bitmap)fingr_picbox1.Image;
                    newBitmap2 = new Bitmap(img1.Width, img1.Height);

                    for (int i = 0; i < img1.Width; i++)
                    {
                        for (int j = 0; j < img1.Height; j++)
                        {

                            byte b = img1.GetPixel(i, j).G;
                            Color newClr = Color.FromArgb(0, 255, 0);
                            if (b != 0)
                            {
                                count = 0;
                                edge1 = img1.GetPixel(i, j - 1).G;
                                if (edge1 != 0)
                                {
                                    count++;
                                }
                                edge2 = img1.GetPixel(i + 1, j - 1).G;
                                if (edge2 != 0)
                                {
                                    count++;
                                }
                                edge3 = img1.GetPixel(i - 1, j - 1).G;
                                if (edge3 != 0)
                                {
                                    count++;
                                }
                                edge4 = img1.GetPixel(i - 1, j).G;
                                if (edge4 != 0)
                                {
                                    count++;
                                }
                                edge5 = img1.GetPixel(i - 1, j + 1).G;
                                if (edge4 != 0)
                                {
                                    count++;
                                }
                                edge6 = img1.GetPixel(i, j + 1).G;
                                if (edge6 != 0)
                                {
                                    count++;
                                }
                                edge7 = img1.GetPixel(i + 1, j + 1).G;
                                if (edge7 != 0)
                                {
                                    count++;
                                }
                                edge8 = img1.GetPixel(i + 1, j).G;
                                if (edge8 != 0)
                                {
                                    count++;
                                }

                                if (count == 1 || count == 3)
                                {
                                    newBitmap2.SetPixel(i, j, newClr);

                                }

                            }

                          else
                            {
                                newBitmap2.SetPixel(i, j, backcolr);
                            }
                        }
                    }
                    
                   foreach (IntPoint corner in corners1)
                    {
                        byte corner1, corner2, corner3, corner4, corner5, corner6, corner7, corner8;

                        int x = corner.X;
                        int y = corner.Y;
                        byte b = newBitmap2.GetPixel(x, y).G;

                        string digt = "w";
                        stream1.Append(digt);
                        if (b != 0)
                        {
                            string digits = "(x,y)";
                            stream1.Append(digits);
                        }
                        corner1 = newBitmap2.GetPixel(x, y - 1).G;
                        if (corner1 != 0)
                        {
                            string digits = "(x,y-1)";
                            stream1.Append(digits);
                        }
                        corner2 = newBitmap2.GetPixel(x + 1, y - 1).G;
                        if (corner1 != 0)
                        {
                            string digits = "(x+1,y-1)";
                            stream1.Append(digits);
                        }
                        corner3 = newBitmap2.GetPixel(x - 1, y - 1).G;
                        if (corner3 != 0)
                        {
                            string digits = "(x-1,y-1)";
                            stream1.Append(digits);
                        }
                        corner4 = newBitmap2.GetPixel(x - 1, y).G;
                        if (corner4 != 0)
                        {
                            string digits = "(x-1,y)";
                            stream1.Append(digits);
                        }
                        corner5 = newBitmap2.GetPixel(x - 1, y + 1).G;
                        if (corner5 != 0)
                        {
                            string digits = "(x-1,y+1)";
                            stream1.Append(digits);
                        }
                        corner6 = newBitmap2.GetPixel(x, y + 1).G;
                        if (corner6 != 0)
                        {
                            string digits = "(x,y+1)";
                            stream1.Append(digits);
                        }
                        corner7 = newBitmap2.GetPixel(x + 1, y + 1).G;
                        if (corner7 != 0)
                        {
                            string digits = "(x+1,y+1)";
                            stream1.Append(digits);
                        }
                        corner8 = newBitmap2.GetPixel(x + 1, y).G;
                        if (corner8 != 0)
                        {
                            string digits = "(x+1,y)";
                            stream1.Append(digits);
                        }

                    }
                    Concatenate concat = new Concatenate(newBitmap2);
                    Bitmap img3 = concat.Apply(newbitmap);
                    fingr_picbox1.Image = (Bitmap)img3;
                  //  Console.WriteLine(stream1);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(""+ex);
            }
        }

        //End

       //**********************************************************************************************************************************************************
      //==============================================     Feature Extraction Button on Picture Box 2 in Fingerprints Section. ====================================
      //==============================================                                                                         ====================================//
      //**************************************************************************************************************************************************************

        private void extract_fechr_btn2_Click(object sender, EventArgs e)
        {
            try
            {
                if (fingr_picbox2.Image == null)
                {
                    MessageBoxIcon iconType = MessageBoxIcon.Stop;
                    MessageBoxButtons buttonType = MessageBoxButtons.OK;
                    buttonType = MessageBoxButtons.OK;
                    iconType = MessageBoxIcon.Stop;
                    DialogResult result =
                    MessageBox.Show("Import An Image First",
                   "Sorry", buttonType, iconType, 0, 0);
                }
                else
                {
                    stream.Clear();
                    Bitmap newbitmap;
                    Color newColor = Color.FromArgb(255, 255, 255);
                    Color backcolr = Color.FromArgb(0, 0, 0);
                    im33 = (Bitmap)fingr_picbox2.Image;
                    siftimage2 = im33;
                    newbitmap = new Bitmap(im33.Width, im33.Height);
                    HarrisCornersDetector hcd = new HarrisCornersDetector();
                    System.Collections.Generic.List<AForge.IntPoint> corners1 = hcd.ProcessImage(im33);
                    // process points
                    for (int i = 0; i < im33.Width; i++)
                    {
                        for (int j = 0; j < im33.Height; j++)
                        {
                            newbitmap.SetPixel(i, j, backcolr);
                        }
                    }
                    harris_count2 = 0;
                    foreach (IntPoint corner in corners1)
                    {
                        newbitmap.SetPixel(corner.X, corner.Y, newColor);
                        harris_count2++;
                    }

                    //======================================Gray Scale=================================================//

                    Bitmap gsImage = Grayscale.CommonAlgorithms.BT709.Apply(im33);
                    fingr_picbox2.Image = (Bitmap)gsImage;
                    //======================================End=================================================//



                    //======================================Noise Removie=================================================//

                    Bitmap image;
                    Bitmap imge1 = (Bitmap)fingr_picbox2.Image;
                    Median filter = new Median();
                    image = filter.Apply(imge1);
                    if (fingr_picbox2.Image != null)
                    {
                        fingr_picbox2.Image.Dispose();
                        fingr_picbox2.Image = null;
                    }

                    fingr_picbox2.Image = image;
                    fingr_picbox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    //======================================End=================================================//



                    //======================================Canny Edge=================================================//

                    Bitmap image1 = (Bitmap)fingr_picbox2.Image;
                    CannyEdgeDetector filter2 = new CannyEdgeDetector();
                    Bitmap edge = filter2.Apply(image1);
                    fingr_picbox2.Image = (Bitmap)edge;
                    //======================================End=================================================//

                    //======================================Minutiae POints ============================================//

                    byte edge1, edge2, edge3, edge4, edge5, edge6, edge7, edge8;
                    Bitmap img1 = (Bitmap)fingr_picbox2.Image;
                    newBitmap1 = new Bitmap(img1.Width, img1.Height);
                    int count = 0;
                    for (int i = 0; i < img1.Width; i++)
                    {
                        for (int j = 0; j < img1.Height; j++)
                        {

                            byte b = img1.GetPixel(i, j).G;
                            Color newClr = Color.FromArgb(0, 255, 0);
                            if (b != 0)
                            {
                                count = 0;
                                edge1 = img1.GetPixel(i, j - 1).G;
                                if (edge1 != 0)
                                {
                                    count++;
                                }
                                edge2 = img1.GetPixel(i + 1, j - 1).G;
                                if (edge2 != 0)
                                {
                                    count++;
                                }
                                edge3 = img1.GetPixel(i - 1, j - 1).G;
                                if (edge3 != 0)
                                {
                                    count++;
                                }
                                edge4 = img1.GetPixel(i - 1, j).G;
                                if (edge4 != 0)
                                {
                                    count++;
                                }
                                edge5 = img1.GetPixel(i - 1, j + 1).G;
                                if (edge4 != 0)
                                {
                                    count++;
                                }
                                edge6 = img1.GetPixel(i, j + 1).G;
                                if (edge6 != 0)
                                {
                                    count++;
                                }
                                edge7 = img1.GetPixel(i + 1, j + 1).G;
                                if (edge7 != 0)
                                {
                                    count++;
                                }
                                edge8 = img1.GetPixel(i + 1, j).G;
                                if (edge8 != 0)
                                {
                                    count++;
                                }

                                if (count == 1 || count == 3)
                                {
                                    newBitmap1.SetPixel(i, j, newClr);

                                }

                            }

                            else
                            {
                                newBitmap1.SetPixel(i, j, backcolr);
                            }
                        }
                    }

                    foreach (IntPoint corner in corners1)
                    {
                        byte corner1, corner2, corner3, corner4, corner5, corner6, corner7, corner8;

                        int x = corner.X;
                        int y = corner.Y;
                        byte b = newBitmap1.GetPixel(x, y).G;

                        string digt = "w";
                        stream.Append(digt);
                        if (b != 0)
                        {
                            string digits = "(x,y)";
                            stream.Append(digits);
                        }
                        corner1 = newBitmap1.GetPixel(x, y - 1).G;
                        if (corner1 != 0)
                        {
                            string digits = "(x,y-1)";
                            stream.Append(digits);
                        }
                        corner2 = newBitmap1.GetPixel(x + 1, y - 1).G;
                        if (corner1 != 0)
                        {
                            string digits = "(x+1,y-1)";
                            stream.Append(digits);
                        }
                        corner3 = newBitmap1.GetPixel(x - 1, y - 1).G;
                        if (corner3 != 0)
                        {
                            string digits = "(x-1,y-1)";
                            stream.Append(digits);
                        }
                        corner4 = newBitmap1.GetPixel(x - 1, y).G;
                        if (corner4 != 0)
                        {
                            string digits = "(x-1,y)";
                            stream.Append(digits);
                        }
                        corner5 = newBitmap1.GetPixel(x - 1, y + 1).G;
                        if (corner5 != 0)
                        {
                            string digits = "(x-1,y+1)";
                            stream.Append(digits);
                        }
                        corner6 = newBitmap1.GetPixel(x, y + 1).G;
                        if (corner6 != 0)
                        {
                            string digits = "(x,y+1)";
                            stream.Append(digits);
                        }
                        corner7 = newBitmap1.GetPixel(x + 1, y + 1).G;
                        if (corner7 != 0)
                        {
                            string digits = "(x+1,y+1)";
                            stream.Append(digits);
                        }
                        corner8 = newBitmap1.GetPixel(x + 1, y).G;
                        if (corner8 != 0)
                        {
                            string digits = "(x+1,y)";
                            stream.Append(digits);
                        }

                    }
                    Concatenate concat = new Concatenate(newBitmap1);
                    Bitmap img3 = concat.Apply(newbitmap);
                    fingr_picbox2.Image = (Bitmap)img3;
                    //Console.WriteLine(stream);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(""+ex);
            }
        }
        //Function of Sift Algorithm....
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static Double Draw(Image<Gray, Byte> modelImage, Image<Gray, byte> observedImage)
        {
            Stopwatch watch;
            HomographyMatrix homography = null;
            double percent = 0;
            SURFDetector surfCPU = new SURFDetector(500, false);
            VectorOfKeyPoint modelKeyPoints;
            VectorOfKeyPoint observedKeyPoints;
            Matrix<int> indices;

            Matrix<byte> mask;
            int k = 2;
            double uniquenessThreshold = 0.8;
            if (GpuInvoke.HasCuda)
            {
                GpuSURFDetector surfGPU = new GpuSURFDetector(surfCPU.SURFParams, 0.01f);
                using (GpuImage<Gray, Byte> gpuModelImage = new GpuImage<Gray, byte>(modelImage))
                //extract features from the object image
                using (GpuMat<float> gpuModelKeyPoints = surfGPU.DetectKeyPointsRaw(gpuModelImage, null))
                using (GpuMat<float> gpuModelDescriptors = surfGPU.ComputeDescriptorsRaw(gpuModelImage, null, gpuModelKeyPoints))
                using (GpuBruteForceMatcher<float> matcher = new GpuBruteForceMatcher<float>(DistanceType.L2))
                {
                    modelKeyPoints = new VectorOfKeyPoint();
                    surfGPU.DownloadKeypoints(gpuModelKeyPoints, modelKeyPoints);
                    watch = Stopwatch.StartNew();

                    // extract features from the observed image
                    using (GpuImage<Gray, Byte> gpuObservedImage = new GpuImage<Gray, byte>(observedImage))
                    using (GpuMat<float> gpuObservedKeyPoints = surfGPU.DetectKeyPointsRaw(gpuObservedImage, null))
                    using (GpuMat<float> gpuObservedDescriptors = surfGPU.ComputeDescriptorsRaw(gpuObservedImage, null, gpuObservedKeyPoints))
                    using (GpuMat<int> gpuMatchIndices = new GpuMat<int>(gpuObservedDescriptors.Size.Height, k, 1, true))
                    using (GpuMat<float> gpuMatchDist = new GpuMat<float>(gpuObservedDescriptors.Size.Height, k, 1, true))
                    using (GpuMat<Byte> gpuMask = new GpuMat<byte>(gpuMatchIndices.Size.Height, 1, 1))
                    using (Emgu.CV.GPU.Stream stream = new Emgu.CV.GPU.Stream())
                    {
                        matcher.KnnMatchSingle(gpuObservedDescriptors, gpuModelDescriptors, gpuMatchIndices, gpuMatchDist, k, null, stream);
                        indices = new Matrix<int>(gpuMatchIndices.Size);
                        mask = new Matrix<byte>(gpuMask.Size);

                        //gpu implementation of voteForUniquess
                        using (GpuMat<float> col0 = gpuMatchDist.Col(0))
                        using (GpuMat<float> col1 = gpuMatchDist.Col(1))
                        {
                            GpuInvoke.Multiply(col1, new MCvScalar(uniquenessThreshold), col1, stream);
                            GpuInvoke.Compare(col0, col1, gpuMask, CMP_TYPE.CV_CMP_LE, stream);
                        }

                        observedKeyPoints = new VectorOfKeyPoint();
                        surfGPU.DownloadKeypoints(gpuObservedKeyPoints, observedKeyPoints);

                        //wait for the stream to complete its tasks
                        //We can perform some other CPU intesive stuffs here while we are waiting for the stream to complete.
                        stream.WaitForCompletion();

                        gpuMask.Download(mask);
                        gpuMatchIndices.Download(indices);

                        if (GpuInvoke.CountNonZero(gpuMask) >= 4)
                        {
                            int nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, indices, mask, 1.5, 20);
                            if (nonZeroCount >= 4)
                                homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints, indices, mask, 2);
                        }

                        watch.Stop();
                    }
                }
            }
            else
            {
                //extract features from the object image
                modelKeyPoints = surfCPU.DetectKeyPointsRaw(modelImage, null);
                Matrix<float> modelDescriptors = surfCPU.ComputeDescriptorsRaw(modelImage, null, modelKeyPoints);

                watch = Stopwatch.StartNew();

                // extract features from the observed image
                observedKeyPoints = surfCPU.DetectKeyPointsRaw(observedImage, null);
                Matrix<float> observedDescriptors = surfCPU.ComputeDescriptorsRaw(observedImage, null, observedKeyPoints);
                BruteForceMatcher<float> matcher = new BruteForceMatcher<float>(DistanceType.L2);
                matcher.Add(modelDescriptors);

                indices = new Matrix<int>(observedDescriptors.Rows, k);
                using (Matrix<float> dist = new Matrix<float>(observedDescriptors.Rows, k))
                {
                    matcher.KnnMatch(observedDescriptors, indices, dist, k, null);
                    mask = new Matrix<byte>(dist.Rows, 1);
                    mask.SetValue(255);
                    Features2DToolbox.VoteForUniqueness(dist, uniquenessThreshold, mask);
                }

                int nonZeroCount = CvInvoke.cvCountNonZero(mask);
                if (nonZeroCount >= 4)
                {
                    nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, indices, mask, 1.5, 20);
                    if (nonZeroCount >= 4)
                        homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints, indices, mask, 2);
                }

                watch.Stop();
            }

            //Draw the matched keypoints
            Image<Bgr, Byte> result = Features2DToolbox.DrawMatches(modelImage, modelKeyPoints, observedImage, observedKeyPoints,
               indices, new Bgr(255, 255, 255), new Bgr(255, 255, 255), mask, Features2DToolbox.KeypointDrawType.DEFAULT);

            if (homography != null)
            {  //draw a rectangle along the projected model
                Rectangle rect = modelImage.ROI;
                PointF[] pts = new PointF[] { 
               new PointF(rect.Left, rect.Bottom),
               new PointF(rect.Right, rect.Bottom),
               new PointF(rect.Right, rect.Top),
               new PointF(rect.Left, rect.Top)};
                homography.ProjectPoints(pts);

                result.DrawPolyline(Array.ConvertAll<PointF, System.Drawing.Point>(pts, System.Drawing.Point.Round), true, new Bgr(Color.Red), 5);
                if (modelKeyPoints.Size <= observedKeyPoints.Size)
                {
                    percent = (((double)modelKeyPoints.Size) / ((double)observedKeyPoints.Size)) * 100;
                }
                else
                {
                    percent = (((double)observedKeyPoints.Size) / ((double)modelKeyPoints.Size)) * 100;
                }
                
            }
            return percent;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        //End
        //***************************************************************************************************************************************************
       // Compare button in fingerprint section =================================

        private void compare_btn_fingerprint_Click(object sender, EventArgs e)
        {
            if (Enterbuttonpressed == false)
            {
                string words = stream.ToString();
                string[] arr = words.Split('w');
                string word = stream1.ToString();
                string[] arr2 = word.Split('w');
                double result3;

                if (words.Length == 0 || word.Length == 0)
                {
                    MessageBoxIcon iconType = MessageBoxIcon.Stop;
                    MessageBoxButtons buttonType = MessageBoxButtons.OK;
                    buttonType = MessageBoxButtons.OK;
                    iconType = MessageBoxIcon.Stop;
                    DialogResult result =
                    MessageBox.Show("Extract Features of Both Images First",
                   "Error", buttonType, iconType, 0, 0);
                }
                else
                {
                    int c = 0; int count3 = 0, count4 = 0;
                    list.Clear();
                    list1.Clear();
                    list2.Clear();
                    foreach (string wo in arr)
                    {
                        if (wo.Length > 2)
                        {
                            //Console.WriteLine(wo);
                            list1.Add(wo);
                            count3++;
                        }

                    }
                    //Console.WriteLine("End" + count3);
                    foreach (string woo in arr2)
                    {
                        if (woo.Length > 2)
                        {
                            //Console.WriteLine(woo);
                            list2.Add(woo);
                            count4++;
                        }
                    }
                    //Console.WriteLine("End " + count4);
                    for (int i = 0; i < list1.Count; i++)
                    {
                        for (int j = 0; j < list2.Count; j++)
                        {
                            if (list1[i] == list2[j])
                            {
                                c++;
                                list2[j] = null;
                                goto outer;
                            }
                        }
                    outer:
                        continue;
                    }
                    int lesser_value = 20000;
                    int corner_higher = 0, corner_lower = 0;

                    if (count3 < lesser_value)
                    {
                        lesser_value = count3;
                    }
                    if (count4 < lesser_value)
                    {
                        lesser_value = count4;
                    }
                    if (harris_count1 > corner_higher)
                    {
                        corner_higher = harris_count1;
                    }
                    if (harris_count2 > corner_higher)
                    {
                        corner_higher = harris_count2;
                    }
                    if (harris_count1 == corner_higher)
                    {
                        corner_lower = harris_count2;
                    }
                    if (harris_count2 == corner_higher)
                    {
                        corner_lower = harris_count1;
                    }

                    Image<Gray, Byte> mimage = new Image<Gray, byte>(siftimage2);
                    Image<Gray, Byte> oimage = new Image<Gray, byte>(siftimage);

                    result3 = Draw(mimage, oimage);
                    if (result3 >= 90)
                    {
                        if (harris_count1 == harris_count2 || (corner_higher / 100) * 95 <= corner_lower)
                        {
                            if ((lesser_value / 100) * 50 <= c)
                            {
                                MessageBox.Show("images Matched");
                            }
                            else
                            {
                                MessageBox.Show("Sorry Images are not same.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Images are different");
                    }
                    Console.WriteLine("images are "+result3+"% same");
                }
            }
            else
            {
                string word = stream1.ToString();
                string[] arr2 = word.Split('w');
                
                if (fingr_picbox1.Image==null)
                {
                    MessageBoxIcon iconType = MessageBoxIcon.Stop;
                    MessageBoxButtons buttonType = MessageBoxButtons.OK;
                    buttonType = MessageBoxButtons.OK;
                    iconType = MessageBoxIcon.Stop;
                    DialogResult result =
                    MessageBox.Show("Import an Images First",
                   "Error", buttonType, iconType, 0, 0);
                }
                else if (word.Length == 0)
                {
                    MessageBoxIcon iconType = MessageBoxIcon.Stop;
                    MessageBoxButtons buttonType = MessageBoxButtons.OK;
                    buttonType = MessageBoxButtons.OK;
                    iconType = MessageBoxIcon.Stop;
                    DialogResult result =
                    MessageBox.Show("Extract Features of Image First",
                   "Error", buttonType, iconType, 0, 0);
                }
                else
                {
                    bool result = false;
                    double result2;
                    MySqlConnection connection = new MySqlConnection(Mysql);
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "(select * from storedata)";
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var column1 = dataReader["window"];
                        var column2=dataReader["harris_count"];
                        var id = dataReader["ID"];
                        var name=dataReader["name"];
                        var fname = dataReader["fname"];
                        var cnic = dataReader["cnic"];
                        var gender = dataReader["gender"];
                        var address = dataReader["address"];
                        var email = dataReader["email"];
                        var ph = dataReader["ph"];
                        var image=(Byte[])dataReader["image"];
                        var person_image=(Byte[])dataReader["person_image"];
                        int harriscount2 = int.Parse(column2.ToString());
                        string words = column1.ToString();
                        string[] arr = word.Split('w');
                        int c = 0; int count3 = 0, count4 = 0;
                        list.Clear();
                        list1.Clear();
                        list2.Clear();
                        foreach (string wo in arr)
                        {
                            if (wo.Length > 2)
                            {
                                list1.Add(wo);
                                count3++;
                            }

                        }
                        foreach (string woo in arr2)
                        {
                            if (woo.Length > 2)
                            {
                                list2.Add(woo);
                                count4++;
                            }
                        }
                        for (int i = 0; i < list1.Count; i++)
                        {
                            for (int j = 0; j < list2.Count; j++)
                            {
                                if (list1[i] == list2[j])
                                {
                                    c++;
                                    list2[j] = null;
                                    goto outer;
                                }
                            }
                        outer:
                            continue;
                        }
                        int lesser_value = 20000;
                        int corner_higher = 0, corner_lower = 0;

                        if (count3 < lesser_value)
                        {
                            lesser_value = count3;
                        }
                        if (count4 < lesser_value)
                        {
                            lesser_value = count4;
                        }
                        if (harris_count1 > corner_higher)
                        {
                            corner_higher = harris_count1;
                        }
                        if (harriscount2 > corner_higher)
                        {
                            corner_higher = harriscount2;
                        }
                        if (harris_count1 == corner_higher)
                        {
                            corner_lower = harriscount2;
                        }
                        if (harriscount2 == corner_higher)
                        {
                            corner_lower = harris_count1;
                        }
                        var stream = new MemoryStream(image);
                        Bitmap img =(Bitmap) Image.FromStream(stream);
                        var stream2 = new MemoryStream(person_image);
                        Bitmap per_img = (Bitmap)Image.FromStream(stream2);
                        Image<Gray, Byte> mimage = new Image<Gray, byte>(img);
                        Image<Gray, Byte> oimage = new Image<Gray, byte>(siftimage);

                        result2 = Draw(mimage, oimage);
                        if (result2 >= 90)
                        {
                            if (harris_count1 == harriscount2 || (corner_higher / 100) * 95 <= corner_lower)
                            {
                                if ((lesser_value / 100) * 50 <= c)
                                {
                                    MessageBox.Show("Image Matched with Record having\n id=" + id.ToString()+"\nPerson Name="+name.ToString()+"\nFather Name="
                                        +fname.ToString()+"\nAddress="+address.ToString()+"\nCNIC="+cnic.ToString()+"\nEmail="+email.ToString()+"\nGender="+gender.ToString()+"\nPhone #="+ph.ToString());
                                    result = true;
                                    fingr_picbox2.Image = per_img;
                                    fingr_picbox2.SizeMode = PictureBoxSizeMode.StretchImage;
                                    goto outer2;
                                }

                            }

                        }
                    }
                outer2:
                    if (result != true)
                    {
                        MessageBox.Show("No match Found");
                    }
                    connection.Close();
                }
            }
        }
        //End of comparison button....
        //===========================================================================================================
        //ok button in database group box in fingerprints portion...

        private void ok_btn_database_grpbox_Click(object sender, EventArgs e)
        {
            
            if (database_comboBox_fingr.Text == "")
            {
                MessageBoxIcon iconType = MessageBoxIcon.Stop;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OK;
                iconType = MessageBoxIcon.Stop;
                DialogResult result =
                MessageBox.Show("Select An Option",
               "Error", buttonType, iconType, 0, 0);
            }
            else
            {
                db_panel_dataGridView.Visible = true;
                database_panel_fingrprints.Visible = true;
                fingerprint_panel.Visible = false;
                this.ClientSize = new Size(778, 478);
                id_textbox_db_panel.Enabled = false;
                FeaturebuttonWasClicked = false;
                if (database_comboBox_fingr.Text == "Add Record")
                {
                    delete_btn_db_panel.Visible = false;
                    see_btn_db_panel.Visible = false;
                    save_btn_db_panel.Visible = true;
                    import_btn_db_panel.Visible = true;
                    update_btn_db_panel.Visible = false;
                    extrct_fechr_btn_db_panel_fingr.Visible = true;
                    import_btn2_fingr_db_panel.Enabled = true;
                    string id_result = null;
                    DataSet ds = Autoid_for_SaveReocrd_fingerprints();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        id_result = (ds.Tables[0].Rows[i][0].ToString());

                    }
                    if (id_result == null)
                    {
                        id_textbox_db_panel.Text = "1";
                    }
                    else
                    {
                        int id = int.Parse(id_result);
                        id = id + 1;
                        id_textbox_db_panel.Text = id.ToString();
                    }
                    db_panel_dataGridView.DataSource = null;
                    db_panel_dataGridView.Rows.Clear();
                }
                if (database_comboBox_fingr.Text == "Delete Record")
                {
                    delete_btn_db_panel.Visible = true;
                    see_btn_db_panel.Visible = false;
                    save_btn_db_panel.Visible = false;
                    import_btn_db_panel.Visible = false;
                    update_btn_db_panel.Visible = false;
                    extrct_fechr_btn_db_panel_fingr.Visible = false;
                    import_btn2_fingr_db_panel.Enabled = false;
                    db_panel_dataGridView.DataSource = null;
                    db_panel_dataGridView.Rows.Clear();
                    DataSet ds = Search();
                    db_panel_dataGridView.DataSource = ds.Tables[0].DefaultView;
                }
                if (database_comboBox_fingr.Text == "Search Record")
                {
                    delete_btn_db_panel.Visible = false;
                    see_btn_db_panel.Visible = true;
                    save_btn_db_panel.Visible = false;
                    import_btn_db_panel.Visible = false;
                    update_btn_db_panel.Visible = true;
                    extrct_fechr_btn_db_panel_fingr.Visible = true;
                    import_btn2_fingr_db_panel.Enabled = true;
                    db_panel_dataGridView.DataSource = null;
                    db_panel_dataGridView.Rows.Clear();
                    DataSet ds = Search();
                    db_panel_dataGridView.DataSource = ds.Tables[0].DefaultView;
                }
                if (database_comboBox_fingr.Text == "Update Record")
                {
                    delete_btn_db_panel.Visible = false;
                    see_btn_db_panel.Visible = true;
                    save_btn_db_panel.Visible = false;
                    import_btn_db_panel.Visible = true;
                    update_btn_db_panel.Visible = true;
                    extrct_fechr_btn_db_panel_fingr.Visible = true;
                    import_btn2_fingr_db_panel.Enabled = true;
                    db_panel_dataGridView.DataSource = null;
                    db_panel_dataGridView.Rows.Clear();
                    DataSet ds = Search();
                    db_panel_dataGridView.DataSource = ds.Tables[0].DefaultView;
                }
            }
        }
        //BAck button on batabase panel....

        private void back_btn_db_panel_Click(object sender, EventArgs e)
        {
            database_panel_fingrprints.Visible = false;
            fingerprint_panel.Visible = true;
            this.ClientSize = new Size(1071, 552);
            id_textbox_db_panel.Clear();
            name_textbox_db_panel.Clear();
            fname_textbox_db_panel.Clear();
            cnic_textbox_db_panel.Clear();
            email_textbox_db_panel.Clear();
            add_textbox_db_panel.Clear();
            gender_comboBox_db_panel.Text = "";
            ph_textbox_db_panel.Clear();
            if (pictureBox_db_panel.Image != null)
            {
                pictureBox_db_panel.Image.Dispose();
                pictureBox_db_panel.Image = null;
            }
            if (finger_db_panel_pictureBox2.Image != null)
            {
                finger_db_panel_pictureBox2.Image.Dispose();
                finger_db_panel_pictureBox2.Image = null;
            }
        }
        //Import button on database Panel......

        private void import_btn_db_panel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FileName = "";
                openFileDialog1.Title = "Images";
                openFileDialog1.Filter = "All Images|*.jpg; *.bmp; *.png";
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName.ToString() != "")
                {
                    fname3 = openFileDialog1.FileName.ToString();
                }
                pictureBox_db_panel.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                pictureBox_db_panel.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Import the image properly");
            }
        }
        //Extract features button in database panel of fingerprints...

        private void extrct_fechr_btn_db_panel_fingr_Click(object sender, EventArgs e)
        {
            if (pictureBox_db_panel.Image == null)
            {
                MessageBoxIcon iconType = MessageBoxIcon.Stop;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OK;
                iconType = MessageBoxIcon.Stop;
                DialogResult result =
                MessageBox.Show("Import Image First",
               "Warning", buttonType, iconType, 0, 0);
            }
            else
            {
                FeaturebuttonWasClicked = true;
                stream1.Clear();
                Bitmap newbitmap;
                Color newColor = Color.FromArgb(255, 255, 255);
                Color backcolr = Color.FromArgb(0, 0, 0);
                im33 = (Bitmap)pictureBox_db_panel.Image;
                newbitmap = new Bitmap(im33.Width, im33.Height);
                HarrisCornersDetector hcd = new HarrisCornersDetector();
                System.Collections.Generic.List<AForge.IntPoint> corners1 = hcd.ProcessImage(im33);
                // process points
                for (int i = 0; i < im33.Width; i++)
                {
                    for (int j = 0; j < im33.Height; j++)
                    {
                        newbitmap.SetPixel(i, j, backcolr);
                    }
                }
                harris_count3 = 0;
                foreach (IntPoint corner in corners1)
                {
                    newbitmap.SetPixel(corner.X, corner.Y, newColor);
                    harris_count3++;
                }

                //======================================Gray Scale=================================================//

                Bitmap gsImage = Grayscale.CommonAlgorithms.BT709.Apply(im33);
                pictureBox_db_panel.Image = (Bitmap)gsImage;
                //======================================End=================================================//



                //======================================Noise Removie=================================================//

                Bitmap image;
                Bitmap imge1 = (Bitmap)pictureBox_db_panel.Image;
                Median filter = new Median();
                image = filter.Apply(imge1);
                if (pictureBox_db_panel.Image != null)
                {
                    pictureBox_db_panel.Image.Dispose();
                    pictureBox_db_panel.Image = null;
                }

                pictureBox_db_panel.Image = image;
                pictureBox_db_panel.SizeMode = PictureBoxSizeMode.StretchImage;
                //======================================End=================================================//



                //======================================Canny Edge=================================================//

                Bitmap image1 = (Bitmap)pictureBox_db_panel.Image;
                CannyEdgeDetector filter2 = new CannyEdgeDetector();
                Bitmap edge = filter2.Apply(image1);
                pictureBox_db_panel.Image = (Bitmap)edge;
                //======================================End=================================================//

                //======================================Minutiae POints ============================================//

                byte edge1, edge2, edge3, edge4, edge5, edge6, edge7, edge8;
                Bitmap img1 = (Bitmap)pictureBox_db_panel.Image;
                newBitmap1 = new Bitmap(img1.Width, img1.Height);
                int count = 0;
                for (int i = 0; i < img1.Width; i++)
                {
                    for (int j = 0; j < img1.Height; j++)
                    {

                        byte b = img1.GetPixel(i, j).G;
                        Color newClr = Color.FromArgb(0, 255, 0);
                        if (b != 0)
                        {
                            count = 0;
                            edge1 = img1.GetPixel(i, j - 1).G;
                            if (edge1 != 0)
                            {
                                count++;
                            }
                            edge2 = img1.GetPixel(i + 1, j - 1).G;
                            if (edge2 != 0)
                            {
                                count++;
                            }
                            edge3 = img1.GetPixel(i - 1, j - 1).G;
                            if (edge3 != 0)
                            {
                                count++;
                            }
                            edge4 = img1.GetPixel(i - 1, j).G;
                            if (edge4 != 0)
                            {
                                count++;
                            }
                            edge5 = img1.GetPixel(i - 1, j + 1).G;
                            if (edge4 != 0)
                            {
                                count++;
                            }
                            edge6 = img1.GetPixel(i, j + 1).G;
                            if (edge6 != 0)
                            {
                                count++;
                            }
                            edge7 = img1.GetPixel(i + 1, j + 1).G;
                            if (edge7 != 0)
                            {
                                count++;
                            }
                            edge8 = img1.GetPixel(i + 1, j).G;
                            if (edge8 != 0)
                            {
                                count++;
                            }

                            if (count == 1 || count == 3)
                            {
                                newBitmap1.SetPixel(i, j, newClr);

                            }

                        }

                        else
                        {
                            newBitmap1.SetPixel(i, j, backcolr);
                        }
                    }
                }

                foreach (IntPoint corner in corners1)
                {
                    byte corner1, corner2, corner3, corner4, corner5, corner6, corner7, corner8;

                    int x = corner.X;
                    int y = corner.Y;
                    byte b = newBitmap1.GetPixel(x, y).G;

                    string digt = "w";
                    stream2.Append(digt);
                    if (b != 0)
                    {
                        string digits = "(x,y)";
                        stream2.Append(digits);
                    }
                    corner1 = newBitmap1.GetPixel(x, y - 1).G;
                    if (corner1 != 0)
                    {
                        string digits = "(x,y-1)";
                        stream2.Append(digits);
                    }
                    corner2 = newBitmap1.GetPixel(x + 1, y - 1).G;
                    if (corner1 != 0)
                    {
                        string digits = "(x+1,y-1)";
                        stream2.Append(digits);
                    }
                    corner3 = newBitmap1.GetPixel(x - 1, y - 1).G;
                    if (corner3 != 0)
                    {
                        string digits = "(x-1,y-1)";
                        stream2.Append(digits);
                    }
                    corner4 = newBitmap1.GetPixel(x - 1, y).G;
                    if (corner4 != 0)
                    {
                        string digits = "(x-1,y)";
                        stream2.Append(digits);
                    }
                    corner5 = newBitmap1.GetPixel(x - 1, y + 1).G;
                    if (corner5 != 0)
                    {
                        string digits = "(x-1,y+1)";
                        stream2.Append(digits);
                    }
                    corner6 = newBitmap1.GetPixel(x, y + 1).G;
                    if (corner6 != 0)
                    {
                        string digits = "(x,y+1)";
                        stream2.Append(digits);
                    }
                    corner7 = newBitmap1.GetPixel(x + 1, y + 1).G;
                    if (corner7 != 0)
                    {
                        string digits = "(x+1,y+1)";
                        stream2.Append(digits);
                    }
                    corner8 = newBitmap1.GetPixel(x + 1, y).G;
                    if (corner8 != 0)
                    {
                        string digits = "(x+1,y)";
                        stream2.Append(digits);
                    }

                }
                Concatenate concat = new Concatenate(newBitmap1);
                Bitmap img3 = concat.Apply(newbitmap);
                pictureBox_db_panel.Image = (Bitmap)img3;
                //Console.WriteLine(stream);

            }
        }
        //End of button extract features...
        //Import button to import the image of person...
        private void import_btn2_fingr_db_panel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FileName = "";
                openFileDialog1.Title = "Images";
                openFileDialog1.Filter = "All Images|*.jpg; *.bmp; *.png";
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName.ToString() != "")
                {
                    fname3 = openFileDialog1.FileName.ToString();
                }
                finger_db_panel_pictureBox2.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                finger_db_panel_pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Import the Image Properly");
            }
        }
        //Save Button in Database Panel for FingerPrints........

        private void save_btn_db_panel_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox_db_panel.Image == null||finger_db_panel_pictureBox2.Image==null)
                {
                    MessageBoxIcon iconType = MessageBoxIcon.Stop;
                    MessageBoxButtons buttonType = MessageBoxButtons.OK;
                    buttonType = MessageBoxButtons.OK;
                    iconType = MessageBoxIcon.Stop;
                    DialogResult result =
                    MessageBox.Show("Import Images in both pictureboxes First",
                   "Warning", buttonType, iconType, 0, 0);
                }
                else if (FeaturebuttonWasClicked == false)
                {
                    MessageBoxIcon iconType = MessageBoxIcon.Stop;
                    MessageBoxButtons buttonType = MessageBoxButtons.OK;
                    buttonType = MessageBoxButtons.OK;
                    iconType = MessageBoxIcon.Stop;
                    DialogResult result =
                    MessageBox.Show("Extract Feature First",
                   "Warning", buttonType, iconType, 0, 0);
                }
                else
                {
                    byte[] byteImg = ImageToByteArray(im33, pictureBox_db_panel);
                    byte[] byteImg2 = ImageToByteArray(finger_db_panel_pictureBox2.Image, finger_db_panel_pictureBox2);
                    string ans = storedata(byteImg, id_textbox_db_panel.Text, name_textbox_db_panel.Text, fname_textbox_db_panel.Text, add_textbox_db_panel.Text,
                                           ph_textbox_db_panel.Text, gender_comboBox_db_panel.Text, cnic_textbox_db_panel.Text, email_textbox_db_panel.Text, stream2.ToString(), harris_count3.ToString(),byteImg2);
                    if (ans == "yes")
                    {
                        MessageBox.Show("Data Successfully Added");

                        id_textbox_db_panel.Clear();
                        name_textbox_db_panel.Clear();
                        fname_textbox_db_panel.Clear();
                        cnic_textbox_db_panel.Clear();
                        email_textbox_db_panel.Clear();
                        add_textbox_db_panel.Clear();
                        gender_comboBox_db_panel.Text = "";
                        ph_textbox_db_panel.Clear();
                        if (pictureBox_db_panel.Image != null)
                        {
                            pictureBox_db_panel.Image.Dispose();
                            pictureBox_db_panel.Image = null;
                        }
                        if (finger_db_panel_pictureBox2.Image != null)
                        {
                            finger_db_panel_pictureBox2.Image.Dispose();
                            finger_db_panel_pictureBox2.Image = null;
                        }
                        DataSet ds = Search();
                        db_panel_dataGridView.DataSource = ds.Tables[0].DefaultView;
                        string id_result = null;
                        DataSet dss = Autoid_for_SaveReocrd_fingerprints();
                        for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
                        {
                            id_result = (dss.Tables[0].Rows[i][0].ToString());

                        }
                        if (id_result == null)
                        {
                            id_textbox_db_panel.Text = "1";
                        }
                        else
                        {
                            int id = int.Parse(id_result);
                            id = id + 1;
                            id_textbox_db_panel.Text = id.ToString();
                        }
                    }
                    FeaturebuttonWasClicked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex+"");
            }
        }

        public String storedata(byte[] image,string id,string name,string fname,string address,string phone,string gender,string cnic,string email,string strm,string haris_count,byte[] image2)
        {
            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Insert into storedata(ID,image,name,fname,ph,address,cnic,email,gender,window,harris_count,person_image)values(@id,@image,@name,@fname,@ph,@add,@cnic,@email,@gender,@window,@harris_count,@image2)";
            cmd.Parameters.Add("@id", id);
            cmd.Parameters.Add("@image", image);
            cmd.Parameters.Add("@image2", image2);
            cmd.Parameters.Add("@name", name);
            cmd.Parameters.Add("@fname", fname);
            cmd.Parameters.Add("@ph", phone);
            cmd.Parameters.Add("@add", address);
            cmd.Parameters.Add("@cnic", cnic);
            cmd.Parameters.Add("@email", email);
            cmd.Parameters.Add("@gender",gender );
            cmd.Parameters.Add("@window",strm);
            cmd.Parameters.Add("@harris_count", haris_count);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return "yes";
        }
        //*****************************************************************************************************
        //*********** Auto id For Save Record Function...........

        public DataSet Autoid_for_SaveReocrd_fingerprints()
        {
            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select ID From storedata";
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;


            }

            catch (System.SystemException)
            {
                Console.WriteLine("error in Sql");
            }
            return null;

            connection.Close();
        }
        //end of auto id for Save Record........
        //Delete Button in Database Panel in fingerprints Section.....

        private void delete_btn_db_panel_Click(object sender, EventArgs e)
        {
            string ans = deleteRecord(id_textbox_db_panel.Text);
            if (ans == "yes")
            {
                MessageBox.Show("Data Deleted Successfully");
                id_textbox_db_panel.Clear();
                name_textbox_db_panel.Clear();
                fname_textbox_db_panel.Clear();
                cnic_textbox_db_panel.Clear();
                email_textbox_db_panel.Clear();
                add_textbox_db_panel.Clear();
                gender_comboBox_db_panel.Text = "";
                ph_textbox_db_panel.Clear();
                if (pictureBox_db_panel.Image != null)
                {
                    pictureBox_db_panel.Image.Dispose();
                    pictureBox_db_panel.Image = null;
                }
                if (finger_db_panel_pictureBox2.Image != null)
                {
                    finger_db_panel_pictureBox2.Image.Dispose();
                    finger_db_panel_pictureBox2.Image = null;
                }
                DataSet ds = Search();
                db_panel_dataGridView.DataSource = ds.Tables[0].DefaultView;
            }
        }
        //End of Delete Button in databse panel...
        //Double click on datagrid cell contents of database panel...

        private void db_panel_dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id_textbox_db_panel.Enabled = false;
                name_textbox_db_panel.Enabled = true;
                fname_textbox_db_panel.Enabled = true;
                cnic_textbox_db_panel.Enabled = true;
                ph_textbox_db_panel.Enabled = true;
                gender_comboBox_db_panel.Enabled = true;
                add_textbox_db_panel.Enabled = true;
                email_textbox_db_panel.Enabled = true;
                import_btn_db_panel.Visible = true;
                import_btn2_fingr_db_panel.Enabled = true;
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = db_panel_dataGridView.Rows[e.RowIndex];
                    id_textbox_db_panel.Text = row.Cells["ID"].Value.ToString();
                    name_textbox_db_panel.Text = row.Cells["name"].Value.ToString();
                    fname_textbox_db_panel.Text = row.Cells["fname"].Value.ToString();
                    add_textbox_db_panel.Text = row.Cells["address"].Value.ToString();
                    cnic_textbox_db_panel.Text = row.Cells["cnic"].Value.ToString();
                    gender_comboBox_db_panel.Text = row.Cells["gender"].Value.ToString();
                    ph_textbox_db_panel.Text = row.Cells["ph"].Value.ToString();
                    email_textbox_db_panel.Text = row.Cells["email"].Value.ToString();
                    // set image from gridview to picture box.....
                    string img = row.Cells["image"].Value.ToString();
                    string img2 = row.Cells["person_image"].Value.ToString();
                    if (pictureBox_db_panel.Image != null)
                    {
                        pictureBox_db_panel.Image.Dispose();
                        pictureBox_db_panel.Image = null;
                    }
                    if (finger_db_panel_pictureBox2.Image != null)
                    {
                        finger_db_panel_pictureBox2.Image.Dispose();
                        finger_db_panel_pictureBox2.Image = null;
                    }
                    if (img != null)
                    {
                        var data = (byte[])row.Cells["image"].Value;
                        var stream = new MemoryStream(data);
                        pictureBox_db_panel.Image = Image.FromStream(stream);
                        pictureBox_db_panel.SizeMode = PictureBoxSizeMode.StretchImage;

                    }
                    if (img2 != null)
                    {
                        var data = (byte[])row.Cells["person_image"].Value;
                        var stream = new MemoryStream(data);
                        finger_db_panel_pictureBox2.Image = Image.FromStream(stream);
                        finger_db_panel_pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(""+ex);
            }
        }
        //End of double click...
        //Function of Delete from database panel...
        public String deleteRecord(string id)
    {
        MySqlConnection connection = new MySqlConnection(Mysql);
        connection.Open();
        try
        {
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "delete FROm storedata where ID=@record";
            cmd.Parameters.Add("@record", id);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            return "yes";

        }
        catch (Exception ex)
        {
            Console.WriteLine("error while entering data." + ex);
        }
        return null;

        connection.Close();
    }
        //Function of Search Record By ID from database panel...
        public void searchrecord(string id)
        {
            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from storedata where id=@id";
                cmd.Parameters.Add("@id", id);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                db_panel_dataGridView.DataSource = ds.Tables[0].DefaultView;

            }
            catch (Exception ex)
            {
                Console.WriteLine("error while entering data." + ex);
            }

            connection.Close();
        }
        //========================================================================================================
        //Button of Search All Records From Database Panel...

        private void see_btn_db_panel_Click(object sender, EventArgs e)
        {
            name_textbox_db_panel.Enabled = false;
            fname_textbox_db_panel.Enabled = false;
            cnic_textbox_db_panel.Enabled = false;
            ph_textbox_db_panel.Enabled = false;
            gender_comboBox_db_panel.Enabled = false;
            add_textbox_db_panel.Enabled = false;
            email_textbox_db_panel.Enabled = false;
            id_textbox_db_panel.Enabled = true;
            
        }//End of search all records button.
        //=============================================================================================================
        //Update button in database panel..

        private void update_btn_db_panel_Click(object sender, EventArgs e)
        {
            if (FeaturebuttonWasClicked == false)
            {
                MessageBoxIcon iconType = MessageBoxIcon.Stop;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OK;
                iconType = MessageBoxIcon.Stop;
                DialogResult result =
                MessageBox.Show("Extract Feature First",
               "Warning", buttonType, iconType, 0, 0);
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(Mysql);
                connection.Open();
                try
                {

                    byte[] byteImg = ImageToByteArray(im33, pictureBox_db_panel);
                    byte[] byteImg2 = ImageToByteArray(finger_db_panel_pictureBox2.Image, finger_db_panel_pictureBox2);
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "update storedata set image=@image,name=@name,fname=@fname,ph=@ph,address=@address,cnic=@cnic,email=@email,gender=@gender,window=@window,harris_count=@harris_count,person_image=@image2 where ID=@id";
                    cmd.Parameters.Add("@id", id_textbox_db_panel.Text);
                    cmd.Parameters.Add("@image", byteImg);
                    cmd.Parameters.Add("@image2", byteImg2);
                    cmd.Parameters.Add("@name", name_textbox_db_panel.Text);
                    cmd.Parameters.Add("@fname", fname_textbox_db_panel.Text);
                    cmd.Parameters.Add("@ph", ph_textbox_db_panel.Text);
                    cmd.Parameters.Add("@address", add_textbox_db_panel.Text);
                    cmd.Parameters.Add("@cnic", cnic_textbox_db_panel.Text);
                    cmd.Parameters.Add("@email", email_textbox_db_panel.Text);
                    cmd.Parameters.Add("@gender", gender_comboBox_db_panel.Text);
                    cmd.Parameters.Add("@window",stream2.ToString());
                    cmd.Parameters.Add("@harris_count", harris_count3.ToString());
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    MessageBox.Show("Data Updated Successfully");
                    id_textbox_db_panel.Clear();
                    name_textbox_db_panel.Clear();
                    fname_textbox_db_panel.Clear();
                    cnic_textbox_db_panel.Clear();
                    email_textbox_db_panel.Clear();
                    add_textbox_db_panel.Clear();
                    gender_comboBox_db_panel.Text = "";
                    ph_textbox_db_panel.Clear();
                    if (pictureBox_db_panel.Image != null)
                    {
                        pictureBox_db_panel.Image.Dispose();
                        pictureBox_db_panel.Image = null;
                    }
                    if (finger_db_panel_pictureBox2.Image != null)
                    {
                        finger_db_panel_pictureBox2.Image.Dispose();
                        finger_db_panel_pictureBox2.Image = null;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("error while entering data." + ex);
                }
                DataSet dss = Search();
                db_panel_dataGridView.DataSource = dss.Tables[0].DefaultView;
                FeaturebuttonWasClicked = false;
            }
        }
        //End of Update update button..
        //Press Enter For Search...

       
        private void id_textbox_db_panel_KeyDown(object sender, KeyEventArgs e)
        {
          if (e.KeyCode == Keys.Enter)
            {
                searchrecord(id_textbox_db_panel.Text);
            }
        }
        //===========================================================================================================================
        //***************************************************************************************************************************
        //                LOGIN WINDOW


        private void timer1_Tick(object sender, EventArgs e)
        {
            welcom_label.Location = new System.Drawing.Point(welcom_label.Location.X + 5, welcom_label.Location.Y);

            if (welcom_label.Location.X > this.Width)
            {
                welcom_label.Location = new System.Drawing.Point(0 - welcom_label.Width, welcom_label.Location.Y);
            }
        }

        private void select_username_combobox_DropDown(object sender, EventArgs e)
        {
            login log = new login();
            try
            {
                select_username_combobox.Items.Clear();
                DataSet ds = log.GetUsernamesFromDatabase();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    select_username_combobox.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }


            }

            catch (Exception ex)
            {
                // custom error message box........
                MessageBoxIcon iconType = MessageBoxIcon.Error;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OKCancel;
                iconType = MessageBoxIcon.Error;
                DialogResult result =
                MessageBox.Show("Error Occur While Connecting DataBase",
               "Error", buttonType, iconType, 0, 0);
                MessageBox.Show("Exception is:" + ex);

            }
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if (select_username_combobox.Text == "")
            {
                MessageBoxIcon iconType = MessageBoxIcon.Error;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OKCancel;
                iconType = MessageBoxIcon.Error;
                DialogResult result =
                MessageBox.Show("You Didn't Select any Username",
               "Error", buttonType, iconType, 0, 0);
            }
            else
            {
                try
                {
                    login log = new login();
                    string paswrd = (pasword_textBox.Text);
                    string username = select_username_combobox.Text;

                    bool result = log.pasword_match(paswrd, username);

                    if (result == true)
                    {
                        if (username == "Administrator")
                        {
                            user_manage_btn.Visible = true;
                        }
                        else
                        {
                            user_manage_btn.Visible = false;
                        }
                        error_label.Visible = false;
                        main_panel.Visible = true;
                        login_panel.Visible = false;
                        this.ClientSize=new Size(349, 269);
                        pasword_textBox.Clear();
                    }

                    else
                    {
                        System.Drawing.Icon myIcon = new System.Drawing.Icon(System.Drawing.SystemIcons.Exclamation, 1, 1);
                        error_label.Image = myIcon.ToBitmap();
                        error_label.Visible = true;
                        pasword_textBox.Clear();
                    }



                }
                catch (Exception ex)
                {
                    MessageBoxIcon iconType = MessageBoxIcon.Error;
                    MessageBoxButtons buttonType = MessageBoxButtons.OK;
                    buttonType = MessageBoxButtons.OKCancel;
                    iconType = MessageBoxIcon.Error;
                    DialogResult result =
                    MessageBox.Show("Error Occur While Connecting DataBase",
                   "Error", buttonType, iconType, 0, 0);
                    MessageBox.Show("Exception is :" + ex);

                }
            }    //end of outer else body...
        }

        private void show_pas_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            pasword_textBox.PasswordChar = show_pas_checkBox.Checked ? '\0' : '*';
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            main_panel.Visible = false;
            login_panel.Visible = true;
            this.ClientSize = new Size(424, 295);
        }

        private void back_btn_user_managemnt_Click(object sender, EventArgs e)
        {
            main_panel.Visible = true;
            usermanagemnt_panel.Visible = false;
            this.ClientSize = new Size(349, 269);
        }
        //SAve Button in Add new user group box in user management panel...

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                login log = new login();
                if (user_name_ump_textBox.Text == "")
                {
                    MessageBox.Show("Enter User Name ");
                }
                else if (enter_new_pas_textBox.Text == "")
                {
                    MessageBox.Show("Enter Some Pasword");
                }
                else if (confirm_new_pas_textBox.Text == "")
                {
                    MessageBox.Show("Enter new Pasword again in confirm pasword textbox");
                }
                else if (confirm_new_pas_textBox.Text != enter_new_pas_textBox.Text)
                {
                    MessageBox.Show("Write the Same Pasword in both textfields");
                    confirm_new_pas_textBox.Clear();
                    enter_new_pas_textBox.Clear();
                }
                else
                {
                    String ans = log.CreateNewAccount(user_name_ump_textBox.Text, enter_new_pas_textBox.Text);
                    if (ans == "ok")
                    {
                        MessageBox.Show("Account Successfully Added");
                        user_name_ump_textBox.Clear();
                        confirm_new_pas_textBox.Clear();
                        enter_new_pas_textBox.Clear();
                    }
                    else
                    {
                        MessageBoxIcon iconType = MessageBoxIcon.Stop;
                        MessageBoxButtons buttonType = MessageBoxButtons.OK;
                        buttonType = MessageBoxButtons.OK;
                        iconType = MessageBoxIcon.Stop;
                        DialogResult result =
                        MessageBox.Show("Account Already Exist try Another Name",
                       "Sorry", buttonType, iconType, 0, 0);
                        user_name_ump_textBox.Clear();
                        confirm_new_pas_textBox.Clear();
                        enter_new_pas_textBox.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Account Already Exist");
            }
        }

        private void user_manage_btn_Click(object sender, EventArgs e)
        {
            main_panel.Visible = false;
            database_panel_fingrprints.Visible = false;
            database_panel_signature_ver.Visible = false;
            fingerprint_panel.Visible = false;
            signature_panel.Visible = false;
            usermanagemnt_panel.Visible = true;
            this.ClientSize = new Size(496, 421);
        }

        private void delete_user_comboBox_DropDown(object sender, EventArgs e)
        {
            login log = new login();
            try
            {
                delete_user_comboBox.Items.Clear();
                DataSet ds = log.GetUsernamesFromDatabase();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    delete_user_comboBox.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }


            }

            catch (Exception ex)
            {
                // custom error message box........
                MessageBoxIcon iconType = MessageBoxIcon.Error;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OKCancel;
                iconType = MessageBoxIcon.Error;
                DialogResult result =
                MessageBox.Show("Error Occur While Connecting DataBase",
               "Error", buttonType, iconType, 0, 0);
                MessageBox.Show("Exception is:" + ex);

            }
        }

        private void delete_user_btn_Click(object sender, EventArgs e)
        {
            login log = new login();
            if (delete_user_comboBox.Text == "")
            {
                // custom error message box........
                MessageBoxIcon iconType = MessageBoxIcon.Error;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OK;
                iconType = MessageBoxIcon.Error;
                DialogResult result =
                MessageBox.Show("Select A User Name",
               "Error", buttonType, iconType, 0, 0);
            }
            else
            {
                //Create message box which confirm that we realy want to remove all data...
                MessageBoxIcon iconType = MessageBoxIcon.Warning;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OKCancel;
                iconType = MessageBoxIcon.Warning;
                DialogResult result =
                MessageBox.Show("Do You Want to Delete Account",
               "Cofirm it", buttonType, iconType, 0, 0);
                switch (result)
                {
                    case DialogResult.OK:

                        string account = delete_user_comboBox.Text;
                        String ans = log.DeleteAccount(account);
                        if (ans == "yes")
                        {
                            MessageBox.Show("Account Successfully Deleted");
                            delete_user_comboBox.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Account Not Deleted");
                        }

                        break;
                    

                    default:
                        break;
                }

            }
        }
        //Combobox drop down in change pasword group box..
 
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            login log = new login();
            try
            {
                comboBox1.Items.Clear();
                DataSet ds = log.GetUsernamesFromDatabase();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comboBox1.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }


            }

            catch (Exception ex)
            {
                // custom error message box........
                MessageBoxIcon iconType = MessageBoxIcon.Error;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OKCancel;
                iconType = MessageBoxIcon.Error;
                DialogResult result =
                MessageBox.Show("Error Occur While Connecting DataBase",
               "Error", buttonType, iconType, 0, 0);
                MessageBox.Show("Exception is:" + ex);

            }
        }
       //SAve Button in Change pasword Group box....

        private void button2_Click(object sender, EventArgs e)
        {
            login log = new login();
            if (textBox1.Text != textBox2.Text)
            {
                MessageBox.Show("Write The same pasword in both textfields");
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                String ans = log.ChangePasword(comboBox1.Text,textBox1.Text);
                if (ans == "ok")
                {
                    MessageBox.Show("Record Successfully Updated");
                    textBox1.Clear();
                    textBox2.Clear();
                    comboBox1.ResetText();
                }
            }
        }

       

        //End of button save..
        
//===================================================================================================================================
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
///                                 SIGNATURE VERIFICATION SYSTEM  
/// 
//####################################################################################################################################
//************************************************************************************************************************************                
        private void signature_btn_Click(object sender, EventArgs e)
        {
            main_panel.Visible = false;
            usermanagemnt_panel.Visible = false;
            signature_panel.Visible = true;
            this.ClientSize = new Size(1071, 546);
            group_box1_signature.Enabled = false;
            groupBox2_signature.Enabled = true;
            dataGridView_signature_panel.Visible = false;
            fingerprint_panel.Visible = false;
            database_panel_signature_ver.Visible = false;
            database_panel_fingrprints.Visible = false;
            stop_webcam_btn_sign_panel.Visible = false;
            start_webcam_btn_sign_panel.Visible = false;
            crop_image_btn_signature.Visible = false;
            capture_image_btn_sign_panel.Visible = false;
            save_img_btn_sign_panel.Visible = false;
        }

        //Back Button on signature panel......
        private void back_btn_signature_panel_Click(object sender, EventArgs e)
        {
            main_panel.Visible = true;
            signature_panel.Visible = false;
            this.ClientSize = new Size(349, 269);
        }//End
        //Cancel button on Signature panel...

        private void cancel_btn_signature_panel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Selected index changed of combox 1 in group box 2 of signature panel...

        private void combox1_groupbox2_sign_panel_SelectedIndexChanged(object sender, EventArgs e)
        {
            group_box1_signature.Enabled = true;
            if (combox1_groupbox2_sign_panel.Text == "From User")
            {
                Enterbuttonpressed = false;
                import_btn_picbox1_signature.Enabled = true;
                extract_fechr_picbox1_sign_panel.Enabled = true;
                extract_fechr_btn_picbox2_sign.Enabled = true;
                crop_image_btn_signature.Enabled = false;
                import_btn_picbox2_sign.Enabled = true;
                stop_webcam_btn_sign_panel.Visible = false;
                start_webcam_btn_sign_panel.Visible = false;
                crop_image_btn_signature.Visible = false;
                capture_image_btn_sign_panel.Visible = false;
                save_img_btn_sign_panel.Visible = false;

                if (pictureBox2_signature_panel.Image != null)
                {
                    pictureBox2_signature_panel.Image.Dispose();
                    pictureBox2_signature_panel.Image = null;
                }
                dataGridView_signature_panel.Visible = false;
            }
            if (combox1_groupbox2_sign_panel.Text == "From Database")
            {
                Enterbuttonpressed = false;
                import_btn_picbox1_signature.Enabled = true;
                extract_fechr_picbox1_sign_panel.Enabled = true;
                import_btn_picbox2_sign.Enabled = false;
                extract_fechr_btn_picbox2_sign.Enabled = false;
                dataGridView_signature_panel.Visible = true;
                stop_webcam_btn_sign_panel.Visible = false;
                start_webcam_btn_sign_panel.Visible = false;
                crop_image_btn_signature.Visible = false;
                capture_image_btn_sign_panel.Visible = false;
                save_img_btn_sign_panel.Visible = false;

                if (pictureBox2_signature_panel.Image != null)
                {
                    pictureBox2_signature_panel.Image.Dispose();
                    pictureBox2_signature_panel.Image = null;
                }
                DataSet ds = sign.SearchAllRecord();
                dataGridView_signature_panel.DataSource = ds.Tables[0].DefaultView;
            }
            if (combox1_groupbox2_sign_panel.Text == "From Webcam")
            {
                Enterbuttonpressed = false;
                import_btn_picbox1_signature.Enabled = false;
                extract_fechr_picbox1_sign_panel.Enabled = false;
                import_btn_picbox2_sign.Enabled = false;
                extract_fechr_btn_picbox2_sign.Enabled = false;
                dataGridView_signature_panel.Visible = true;
                stop_webcam_btn_sign_panel.Visible = true;
                start_webcam_btn_sign_panel.Visible = true;
                crop_image_btn_signature.Visible = true;
                crop_image_btn_signature.Enabled = false;
                capture_image_btn_sign_panel.Visible = true;
                save_img_btn_sign_panel.Visible = true;
                save_img_btn_sign_panel.Enabled = false;
                if (pictureBox1_signature_panel.Image != null)
                {
                    pictureBox1_signature_panel.Image.Dispose();
                    pictureBox1_signature_panel.Image = null;
                }
                if (pictureBox2_signature_panel.Image != null)
                {
                    pictureBox2_signature_panel.Image.Dispose();
                    pictureBox2_signature_panel.Image = null;
                }
                DataSet ds = sign.SearchAllRecord();
                dataGridView_signature_panel.DataSource = ds.Tables[0].DefaultView;
            }
        }
        //Import Image button with picture box 1 in groupbox 1 of signature panel...

        private void import_btn_picbox1_signature_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FileName = "";
                openFileDialog1.Title = "Images";
                openFileDialog1.Filter = "All Images|*.jpg; *.bmp; *.png";
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName.ToString() != "")
                {
                    fname1 = openFileDialog1.FileName.ToString();
                }
                pictureBox1_signature_panel.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                pictureBox1_signature_panel.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Import Image Properly");
            }
        }
        //Import Image button with picture box2 in groupbox 1 of signature panel...
        private void import_btn_picbox2_sign_Click(object sender, EventArgs e)
        {

            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FileName = "";
                openFileDialog1.Title = "Images";
                openFileDialog1.Filter = "All Images|*.jpg; *.bmp; *.png";
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName.ToString() != "")
                {
                    fname1 = openFileDialog1.FileName.ToString();
                }
                pictureBox2_signature_panel.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                pictureBox2_signature_panel.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Import Image Properly");
            }
        }
        
        //Draw a rectangle arround the signature area...
        
        //for Picture box 1...
        private void pictureBox1_signature_panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Cursor = Cursors.Cross;
                cropX = e.X;
                cropY = e.Y;

                cropPen = new Pen(Color.Red, 1);
                cropPen.DashStyle = DashStyle.DashDotDot;


            }
            pictureBox1_signature_panel.Refresh();
            crop_image_btn_signature.Enabled = true;
        }

        private void pictureBox1_signature_panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1_signature_panel.Image == null)
                return;


            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pictureBox1_signature_panel.Refresh();
                cropWidth = e.X - cropX;
                cropHeight = e.Y - cropY;
                pictureBox1_signature_panel.CreateGraphics().DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight);
                crop_image_btn_signature.Enabled = true;
            }
           
        }
       
        //Crop button on picturebox1 in signature panel

        private void crop_btn_picbox1_signature_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;

            if (cropWidth < 1)
            {
                return;
            }
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            //First we define a rectangle with the help of already calculated points
            Bitmap OriginalImage = new Bitmap(pictureBox1_signature_panel.Image, pictureBox1_signature_panel.Width, pictureBox1_signature_panel.Height);
            //Original image
            Bitmap _img = new Bitmap(cropWidth, cropHeight, PixelFormat.Format24bppRgb);
            // for cropinf image
            Graphics g = Graphics.FromImage(_img);
            // create graphics
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //set image attributes
            g.DrawImage(OriginalImage, 0, 0, rect, GraphicsUnit.Pixel);

            pictureBox1_signature_panel.Image = _img;
            pictureBox1_signature_panel.SizeMode = PictureBoxSizeMode.StretchImage;
            crop_image_btn_signature.Enabled = false;
        }
        
        //Extract Feature button on picturebox1 in signature panel...
        bool extractfeaturebtnpressed1 = false;
        bool extractfeaturebtnpressed2 = false;
        private void extract_fechr_picbox1_sign_panel_Click(object sender, EventArgs e)
        {
            if (pictureBox1_signature_panel.Image == null)
            {
                MessageBoxIcon iconType = MessageBoxIcon.Stop;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OK;
                iconType = MessageBoxIcon.Stop;
                DialogResult result =
                MessageBox.Show("Import Image First",
               "Warning", buttonType, iconType, 0, 0);
            }
            else
            {
                extractfeaturebtnpressed1 = true;
                stream1.Clear();
                im33 = (Bitmap)pictureBox1_signature_panel.Image;
                siftimage = im33;
                HarrisCornersDetector hcd = new HarrisCornersDetector();
                System.Collections.Generic.List<AForge.IntPoint> corners1 = hcd.ProcessImage(im33);
                // process points

                harris_count3 = 0;
                foreach (IntPoint corner in corners1)
                {
                    harris_count3++;
                }
                extract_fechr_picbox1_sign_panel.Enabled = false;
            }
        }
        //Extract Feature button on picturebox2 in signature panel...

        private void extract_fechr_btn_picbox2_sign_Click(object sender, EventArgs e)
        {
            if (pictureBox2_signature_panel.Image == null)
            {
                MessageBoxIcon iconType = MessageBoxIcon.Stop;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OK;
                iconType = MessageBoxIcon.Stop;
                DialogResult result =
                MessageBox.Show("Import Image First",
               "Warning", buttonType, iconType, 0, 0);
            }
            else
            {
                extractfeaturebtnpressed2 = true;
                stream2.Clear();
                im33 = (Bitmap)pictureBox2_signature_panel.Image;
                siftimage2 = im33;
                HarrisCornersDetector hcd = new HarrisCornersDetector();
                System.Collections.Generic.List<AForge.IntPoint> corners1 = hcd.ProcessImage(im33);
                // process points

                harris_count2 = 0;
                foreach (IntPoint corner in corners1)
                {
                    harris_count2++;
                }
                extract_fechr_btn_picbox2_sign.Enabled = false;
            }
        }
        //Compare button in groupbox1 of signature verification panel...

        private void cmpare_btn_signature_panel_Click(object sender, EventArgs e)
        {
            bool result2 = false;
            if (Enterbuttonpressed != true)
            {
                if (combox1_groupbox2_sign_panel.Text == "From User")
                {
                    double result3 = 0;
                    if (extractfeaturebtnpressed1 == false || extractfeaturebtnpressed2 == false)
                    {
                        MessageBoxIcon iconType = MessageBoxIcon.Error;
                        MessageBoxButtons buttonType = MessageBoxButtons.OK;
                        buttonType = MessageBoxButtons.OKCancel;
                        iconType = MessageBoxIcon.Error;
                        DialogResult result =
                        MessageBox.Show("Extract Features of Both Images",
                       "Error", buttonType, iconType, 0, 0);
                    }
                    else
                    {
                        Image<Gray, Byte> mimage = new Image<Gray, byte>(siftimage2);
                        Image<Gray, Byte> oimage = new Image<Gray, byte>(siftimage);

                        result3 = Draw(mimage, oimage);
                        if (result3 >= 85)
                        {
                            MessageBox.Show("Images Matched");
                        }
                        else
                        {
                            MessageBox.Show("Images Are Not Same");
                        }
                        Console.WriteLine("Matching Percentage is=" + result3);
                        extractfeaturebtnpressed1 = false;
                        extractfeaturebtnpressed2 = false;
                        extract_fechr_btn_picbox2_sign.Enabled = true;
                        extract_fechr_picbox1_sign_panel.Enabled = true;

                    }
                }
                if (combox1_groupbox2_sign_panel.Text == "From Database")
                {
                    double result3 = 0;
                    if (extractfeaturebtnpressed1 == false)
                    {
                        MessageBoxIcon iconType = MessageBoxIcon.Error;
                        MessageBoxButtons buttonType = MessageBoxButtons.OK;
                        buttonType = MessageBoxButtons.OKCancel;
                        iconType = MessageBoxIcon.Error;
                        DialogResult result =
                        MessageBox.Show("Extract Features of Image",
                       "Error", buttonType, iconType, 0, 0);
                    }
                    else
                    {
                        if (pictureBox2_signature_panel.Image == null)
                        {
                            MessageBoxIcon iconType = MessageBoxIcon.Error;
                            MessageBoxButtons buttonType = MessageBoxButtons.OK;
                            buttonType = MessageBoxButtons.OKCancel;
                            iconType = MessageBoxIcon.Error;
                            DialogResult result =
                            MessageBox.Show("Set Some Picture in Picturebox 2",
                           "Error", buttonType, iconType, 0, 0);
                        }
                        else
                        {
                            Image<Gray, Byte> oimage = new Image<Gray, byte>(siftimage);
                            Image<Gray, Byte> mimage = new Image<Gray, byte>(siftimage2);
                            result3 = Draw(mimage, oimage);
                            if (result3 >= 85)
                            {
                                MessageBox.Show("Images Matched");
                            }
                            else
                            {
                                MessageBox.Show("Images Are Not Same");
                            }
                            Console.WriteLine("Matching Percentage is=" + result3);
                            extractfeaturebtnpressed1 = false;
                            extract_fechr_picbox1_sign_panel.Enabled = true;
                        }
                    }
                }
                if (combox1_groupbox2_sign_panel.Text == "From Webcam")
                {
                    double result3 = 0;
                    if (extractfeaturebtnpressed1 == false || extractfeaturebtnpressed2 == false)
                    {
                        MessageBoxIcon iconType = MessageBoxIcon.Error;
                        MessageBoxButtons buttonType = MessageBoxButtons.OK;
                        buttonType = MessageBoxButtons.OKCancel;
                        iconType = MessageBoxIcon.Error;
                        DialogResult result =
                        MessageBox.Show("Extract Features of Both Images",
                       "Error", buttonType, iconType, 0, 0);
                    }
                    else
                    {
                        Image<Gray, Byte> mimage = new Image<Gray, byte>(siftimage2);
                        Image<Gray, Byte> oimage = new Image<Gray, byte>(siftimage);

                        result3 = Draw(mimage, oimage);
                        if (result3 >= 85)
                        {
                            MessageBox.Show("Images Matched");
                        }
                        else
                        {
                            MessageBox.Show("Images Are Not Same");
                        }
                        Console.WriteLine("Matching Percentage is=" + result3);
                        extractfeaturebtnpressed1 = false;
                        extractfeaturebtnpressed2 = false;
                        extract_fechr_btn_picbox2_sign.Enabled = true;
                        extract_fechr_picbox1_sign_panel.Enabled = true;

                    }
                }
            }
            else
            {
                if (extractfeaturebtnpressed1 == false)
                {
                    MessageBoxIcon iconType = MessageBoxIcon.Error;
                    MessageBoxButtons buttonType = MessageBoxButtons.OK;
                    buttonType = MessageBoxButtons.OKCancel;
                    iconType = MessageBoxIcon.Error;
                    DialogResult result =
                    MessageBox.Show("Extract Features of Image",
                   "Error", buttonType, iconType, 0, 0);
                }
                else
                {
                    MySqlConnection connection = new MySqlConnection(Mysql);
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "(select * from signature)";
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    double result3 = 0;
                    
                    Image<Gray, Byte> oimage = new Image<Gray, byte>(siftimage);
                    Image<Gray, Byte> mimage;
                    while (dataReader.Read())
                    {
                        var id = dataReader["ID"];
                        var name = dataReader["name"];
                        var fname = dataReader["fname"];
                        var cnic = dataReader["cnic"];
                        var gender = dataReader["gender"];
                        var address = dataReader["address"];
                        var ph = dataReader["ph"];
                        var email = dataReader["email"];
                        
                        var person_image = (Byte[])dataReader["person_image"];
                        var strm2 = new MemoryStream(person_image);
                        Image per_image = Image.FromStream(strm2);
                        Bitmap bitmap2 = new Bitmap(per_image);
                        var image = (Byte[])dataReader["image"];
                        var strm = new MemoryStream(image);
                        Image image2 = Image.FromStream(strm);
                        Bitmap bitmap = new Bitmap(image2);
                        mimage = new Image<Gray, byte>(bitmap);
                        
                        result3 = Draw(mimage, oimage);
                        if (result3 >= 85)
                        {
                            MessageBox.Show("Image Matched with Image having\n Id="+id.ToString()+"\nPerson Name="+name.ToString()+"\nFather Name="+fname.ToString()
                                +"\nCNIC="+cnic.ToString()+"\nAddress="+address.ToString()+"\nPhone #="+ph.ToString()+"\nEmail="+email.ToString()+"\nGender="+gender.ToString());
                            Console.WriteLine("Matching Percentage is="+result3);
                            pictureBox2_signature_panel.Image = bitmap2;
                            result2 = true;
                            goto outer;
                            
                        }
                        
                    }//end of while loop..
                outer:

                    if (result2 != true)
                    {
                        MessageBox.Show("No Match Found");
                    }
                
                 }//end of else body..
            
            }//end of outer else...
        }
        //Cell content double click in datagridview of signature panel...
        private void dataGridView_signature_panel_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    
                    DataGridViewRow row = dataGridView_signature_panel.Rows[e.RowIndex];
                    if (pictureBox2_signature_panel.Image != null)
                    {
                        pictureBox2_signature_panel.Image.Dispose();
                        pictureBox2_signature_panel.Image = null;
                    }
                    // set image from gridview to picture box.....
                    var data = (Byte[])(row.Cells["image"].Value);
                    var stream = new MemoryStream(data);
                    pictureBox2_signature_panel.Image = Image.FromStream(stream);
                    pictureBox2_signature_panel.SizeMode = PictureBoxSizeMode.StretchImage;
                    siftimage2 = (Bitmap)pictureBox2_signature_panel.Image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }
        //Start webcam button in signature panel...
        private void start_webcam_btn_sign_panel_Click(object sender, EventArgs e)
        {
          // webcam1.Start();
          // webcam1.Continue();
            try
            {
                dynamicDotNetTwain1.SelectSource();
                dynamicDotNetTwain1.SetVideoContainer(pictureBox2_signature_panel);
                dynamicDotNetTwain1.OpenSource();
                //List the source name and resolutions
                int count = dynamicDotNetTwain1.ResolutionForCamList.Count;
                for (int j = 0; j < count; j++)
                {
                    string tempHeight = dynamicDotNetTwain1.ResolutionForCamList[j].Height.ToString();
                    string tempWidth = dynamicDotNetTwain1.ResolutionForCamList[j].Width.ToString();
                    string tempResolution = tempWidth + "X" + tempHeight;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
           crop_image_btn_signature.Enabled = false;
           save_img_btn_sign_panel.Enabled = false;
        }
        //Capture image button in signature panel  
        private void capture_image_btn_sign_panel_Click(object sender, EventArgs e)
        {
           // pictureBox1_signature_panel.Image = pictureBox2_signature_panel.Image;
            try
            {
                dynamicDotNetTwain1.RemoveAllImages();
                dynamicDotNetTwain1.EnableSource();
                pictureBox1_signature_panel.Image = dynamicDotNetTwain1.GetImage(0);
                Bitmap img = new Bitmap(pictureBox1_signature_panel.Image);
                pictureBox1_signature_panel.Image = img;
                pictureBox1_signature_panel.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        //Stop webcam button in signature panel...
        private void stop_webcam_btn_sign_panel_Click(object sender, EventArgs e)
        {
           // webcam1.Stop();
            dynamicDotNetTwain1.RemoveAllImages();
            dynamicDotNetTwain1.CloseSource();
            crop_image_btn_signature.Enabled = false;
            save_img_btn_sign_panel.Enabled = true;
            if (pictureBox2_signature_panel.Image != null)
            {
                pictureBox2_signature_panel.Image.Dispose();
                pictureBox2_signature_panel.Image = null;
            }
        }
        //Save image button in signature panel..
        private void save_img_btn_sign_panel_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox1_signature_panel.Image);
            pictureBox1_signature_panel.Image = img;

            import_btn_picbox1_signature.Enabled = false;
            import_btn_picbox2_sign.Enabled = true;
            dataGridView_signature_panel.Visible = true;
            extract_fechr_btn_picbox2_sign.Enabled = true;
            extract_fechr_picbox1_sign_panel.Enabled = true;

            stop_webcam_btn_sign_panel.Visible = false;
            start_webcam_btn_sign_panel.Visible = false;
            crop_image_btn_signature.Visible = false;
            capture_image_btn_sign_panel.Visible = false;
            save_img_btn_sign_panel.Visible = false;
            try
            {
                DataSet ds = sign.SearchAllRecord();
                dataGridView_signature_panel.DataSource = ds.Tables[0].DefaultView;
                if (pictureBox2_signature_panel.Image != null)
                {
                    pictureBox2_signature_panel.Image.Dispose();
                    pictureBox2_signature_panel.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }
        }
        //Enter button for match a picture with whole database in signature panel...
        private void enter_btn_grpbox2_sign_panel_Click(object sender, EventArgs e)
        {
            import_btn_picbox2_sign.Enabled = false;
            dataGridView_signature_panel.Visible = false;
            import_btn_picbox1_signature.Enabled = true; 
            extract_fechr_picbox1_sign_panel.Enabled = true;
            extract_fechr_btn_picbox2_sign.Enabled = false;
            group_box1_signature.Enabled = true;
            Enterbuttonpressed = true;
            if (pictureBox2_signature_panel.Image != null)
            {
                pictureBox2_signature_panel.Image.Dispose();
                pictureBox2_signature_panel.Image = null;
            }
        }
        //Enter button on signature panel for enter into database of signature..
        //*****************************************************************************************************************************
        //*****************************************************************************************************************************
        //                         
        //                                 DATABASE PANEL OF SIGNATURE
        private void forward_btn_grpbox2_sign_panel_Click(object sender, EventArgs e)
        {
            if (comboBox2_grpbox2_sign_panel.Text == "")
            {
                MessageBoxIcon iconType = MessageBoxIcon.Stop;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OK;
                iconType = MessageBoxIcon.Stop;
                DialogResult result =
                MessageBox.Show("Select An Option",
               "Error", buttonType, iconType, 0, 0);
            }
            else
            {
                dataGridView_sign_db_panel.Visible = true;
                database_panel_signature_ver.Visible = true;
                signature_panel.Visible = false;
                this.ClientSize = new Size(778, 478);
                id_txtbox_sign_db_panel.Enabled = false;
                FeaturebuttonWasClicked = false;
                dataGridView_signature_panel.Visible = false;
                if (pictureBox1_signature_panel.Image != null)
                {
                    pictureBox1_signature_panel.Image.Dispose();
                    pictureBox1_signature_panel.Image = null;
                }
                if (pictureBox2_signature_panel.Image != null)
                {
                    pictureBox2_signature_panel.Image.Dispose();
                    pictureBox2_signature_panel.Image = null;
                }
                if (comboBox2_grpbox2_sign_panel.Text == "Add Record")
                {
                    delete_btn_sign_db_panel.Visible = false;
                    see_record_btn_sign_db_panel.Visible = false;
                    save_btn_sign_db_panel.Visible = true;
                    import_btn_sign_db_panel.Visible = true;
                    update_btn_sign_db_panel.Visible = false;
                    extractfeature_btn_db_panel.Visible = true;
                    import_btn2_sign_db_panel.Visible = true;
                    string id_result = null;
                    DataSet ds = sign.Autoid_for_SaveReocrd_Signature();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        id_result = (ds.Tables[0].Rows[i][0].ToString());

                    }
                    if (id_result == null)
                    {
                       id_txtbox_sign_db_panel.Text = "1";
                    }
                    else
                    {
                        int id = int.Parse(id_result);
                        id = id + 1;
                        id_txtbox_sign_db_panel.Text = id.ToString();
                    }
                    dataGridView_sign_db_panel.DataSource = null;
                    dataGridView_sign_db_panel.Rows.Clear();
                }
                if (comboBox2_grpbox2_sign_panel.Text == "Delete Record")
                {
                    delete_btn_sign_db_panel.Visible = true;
                    see_record_btn_sign_db_panel.Visible = false;
                    save_btn_sign_db_panel.Visible = false;
                    import_btn_sign_db_panel.Visible = false;
                    update_btn_sign_db_panel.Visible = false;
                    extractfeature_btn_db_panel.Visible = false;
                    import_btn2_sign_db_panel.Visible = false;
                    dataGridView_sign_db_panel.DataSource = null;
                    dataGridView_sign_db_panel.Rows.Clear();
                    DataSet ds = sign.SearchAllRecord();
                    dataGridView_sign_db_panel.DataSource = ds.Tables[0].DefaultView;
                }
                if (comboBox2_grpbox2_sign_panel.Text == "Search Record")
                {
                    delete_btn_sign_db_panel.Visible = false;
                    see_record_btn_sign_db_panel.Visible = true;
                    save_btn_sign_db_panel.Visible = false;
                    import_btn_sign_db_panel.Visible = false;
                    update_btn_sign_db_panel.Visible = true;
                    extractfeature_btn_db_panel.Visible = true;
                    import_btn2_sign_db_panel.Visible = true;
                    dataGridView_sign_db_panel.DataSource = null;
                    dataGridView_sign_db_panel.Rows.Clear();
                    DataSet ds = sign.SearchAllRecord();
                    dataGridView_sign_db_panel.DataSource = ds.Tables[0].DefaultView;
                }
                if (comboBox2_grpbox2_sign_panel.Text == "Update Record")
                {
                    delete_btn_sign_db_panel.Visible = false;
                    see_record_btn_sign_db_panel.Visible = true;
                    save_btn_sign_db_panel.Visible = false;
                    import_btn_sign_db_panel.Visible = true;
                    update_btn_sign_db_panel.Visible = true;
                    import_btn2_sign_db_panel.Visible = true;
                    extractfeature_btn_db_panel.Visible = true;
                    dataGridView_sign_db_panel.DataSource = null;
                    dataGridView_sign_db_panel.Rows.Clear();
                    DataSet ds = sign.SearchAllRecord();
                    dataGridView_sign_db_panel.DataSource = ds.Tables[0].DefaultView;
                }
            }
        }
        //Bsck button on signature database panel...
        private void back_btn_sign_db_panel_Click(object sender, EventArgs e)
        {
            dataGridView_sign_db_panel.Visible = false;
            signature_panel.Visible = true;
            this.ClientSize = new Size(1071, 546);
            id_txtbox_sign_db_panel.Clear();
            name_txtbox_sign_db_panel.Clear();
            fname_txtbox_sign_db_panel.Clear();
            cnic_txtbox_sign_db_panel.Clear();
            email_txtbox_sign_db_panel.Clear();
            add_txtbox_sign_db_panel.Clear();
            gender_combobox_sign_db_panel.Text = "";
            ph_txtbox_sign_db_panel.Clear();
            if (picturebox_sign_db_panel.Image != null)
            {
                picturebox_sign_db_panel.Image.Dispose();
                picturebox_sign_db_panel.Image = null;
            }
            if (picturebox2_sign_db_panel.Image != null)
            {
                picturebox2_sign_db_panel.Image.Dispose();
                picturebox2_sign_db_panel.Image = null;
            }
        }
        //Import button in database panel of signature...
        private void import_btn_sign_db_panel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FileName = "";
                openFileDialog1.Title = "Images";
                openFileDialog1.Filter = "All Images|*.jpg; *.bmp; *.png";
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName.ToString() != "")
                {
                    fname1 = openFileDialog1.FileName.ToString();
                }
                picturebox_sign_db_panel.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                picturebox_sign_db_panel.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Import Image Properly");
            }
        }
        //Extract features button in signature database panel...
        private void extractfeature_btn_db_panel_Click(object sender, EventArgs e)
        {
            if (picturebox_sign_db_panel.Image == null)
            {
                MessageBoxIcon iconType = MessageBoxIcon.Stop;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OK;
                iconType = MessageBoxIcon.Stop;
                DialogResult result =
                MessageBox.Show("Import Image First",
               "Warning", buttonType, iconType, 0, 0);
            }
            else
            {
                FeaturebuttonWasClicked = true;
                stream1.Clear();
                im33 = (Bitmap)picturebox_sign_db_panel.Image;
                HarrisCornersDetector hcd = new HarrisCornersDetector();
                System.Collections.Generic.List<AForge.IntPoint> corners1 = hcd.ProcessImage(im33);
                // process points
                
                harris_count3 = 0;
                foreach (IntPoint corner in corners1)
                {
                    harris_count3++;
                }
                MessageBox.Show("Feature Extracted Successfully");
            }
        }
        //Save Record button in database Panel of Signature..
        private void save_btn_sign_db_panel_Click(object sender, EventArgs e)
        {
            try
            {
                if (picturebox_sign_db_panel.Image == null||picturebox2_sign_db_panel.Image==null)
                {
                    MessageBoxIcon iconType = MessageBoxIcon.Stop;
                    MessageBoxButtons buttonType = MessageBoxButtons.OK;
                    buttonType = MessageBoxButtons.OK;
                    iconType = MessageBoxIcon.Stop;
                    DialogResult result =
                    MessageBox.Show("Import both Images First",
                   "Warning", buttonType, iconType, 0, 0);
                }
                else if (FeaturebuttonWasClicked == false)
                {
                    MessageBoxIcon iconType = MessageBoxIcon.Stop;
                    MessageBoxButtons buttonType = MessageBoxButtons.OK;
                    buttonType = MessageBoxButtons.OK;
                    iconType = MessageBoxIcon.Stop;
                    DialogResult result =
                    MessageBox.Show("Extract Feature First",
                   "Warning", buttonType, iconType, 0, 0);
                }
                else
                {
                    byte[] byteImg = ImageToByteArray(im33, picturebox_sign_db_panel);
                    byte[] byteImg2 = ImageToByteArray(picturebox2_sign_db_panel.Image, picturebox2_sign_db_panel);
                    string ans = sign.storedata(byteImg, id_txtbox_sign_db_panel.Text, name_txtbox_sign_db_panel.Text, fname_txtbox_sign_db_panel.Text, add_txtbox_sign_db_panel.Text,
                                           ph_txtbox_sign_db_panel.Text, gender_combobox_sign_db_panel.Text, cnic_txtbox_sign_db_panel.Text, email_txtbox_sign_db_panel.Text, harris_count3.ToString(),byteImg2);
                    if (ans == "yes")
                    {
                        MessageBox.Show("Data Successfully Added");

                        id_txtbox_sign_db_panel.Clear();
                        name_txtbox_sign_db_panel.Clear();
                        fname_txtbox_sign_db_panel.Clear();
                        cnic_txtbox_sign_db_panel.Clear();
                        email_txtbox_sign_db_panel.Clear();
                        add_txtbox_sign_db_panel.Clear();
                        gender_combobox_sign_db_panel.Text = "";
                        ph_txtbox_sign_db_panel.Clear();
                        if (picturebox_sign_db_panel.Image != null)
                        {
                            picturebox_sign_db_panel.Image.Dispose();
                            picturebox_sign_db_panel.Image = null;
                        }
                        if (picturebox2_sign_db_panel.Image != null)
                        {
                            picturebox2_sign_db_panel.Image.Dispose();
                            picturebox2_sign_db_panel.Image = null;
                        }
                        DataSet ds = sign.SearchAllRecord();
                        dataGridView_sign_db_panel.DataSource = ds.Tables[0].DefaultView;
                        string id_result = null;
                        DataSet dss = sign.Autoid_for_SaveReocrd_Signature();
                        for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
                        {
                            id_result = (dss.Tables[0].Rows[i][0].ToString());

                        }
                        if (id_result == null)
                        {
                            id_txtbox_sign_db_panel.Text = "1";
                        }
                        else
                        {
                            int id = int.Parse(id_result);
                            id = id + 1;
                            id_txtbox_sign_db_panel.Text = id.ToString();
                        }
                    }
                    FeaturebuttonWasClicked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }
        }
        //Delete Record Button in Database Panel of Signature...
        private void delete_btn_sign_db_panel_Click(object sender, EventArgs e)
        {
            string ans = sign.deleteRecord(id_txtbox_sign_db_panel.Text);
            if (ans == "yes")
            {
                MessageBox.Show("Data Deleted Successfully");
                id_txtbox_sign_db_panel.Clear();
                name_txtbox_sign_db_panel.Clear();
                fname_txtbox_sign_db_panel.Clear();
                cnic_txtbox_sign_db_panel.Clear();
                email_txtbox_sign_db_panel.Clear();
                add_txtbox_sign_db_panel.Clear();
                gender_combobox_sign_db_panel.Text = "";
                ph_txtbox_sign_db_panel.Clear();
                if (picturebox_sign_db_panel.Image != null)
                {
                    picturebox_sign_db_panel.Image.Dispose();
                    picturebox_sign_db_panel.Image = null;
                }
                if (picturebox2_sign_db_panel.Image != null)
                {
                    picturebox2_sign_db_panel.Image.Dispose();
                    picturebox2_sign_db_panel.Image = null;
                }
                DataSet ds =sign.SearchAllRecord();
                dataGridView_sign_db_panel.DataSource = ds.Tables[0].DefaultView;
            }
        }
        //Cell Content Double click on datagridview on database panel in signature panel...
        private void dataGridView_sign_db_panel_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id_txtbox_sign_db_panel.Enabled = false;
                name_txtbox_sign_db_panel.Enabled = true;
                fname_txtbox_sign_db_panel.Enabled = true;
                cnic_txtbox_sign_db_panel.Enabled = true;
                ph_txtbox_sign_db_panel.Enabled = true;
                gender_combobox_sign_db_panel.Enabled = true;
                add_txtbox_sign_db_panel.Enabled = true;
                email_txtbox_sign_db_panel.Enabled = true;
                import_btn_sign_db_panel.Visible = true;
                import_btn2_sign_db_panel.Visible = true;
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView_sign_db_panel.Rows[e.RowIndex];
                    id_txtbox_sign_db_panel.Text = row.Cells["ID"].Value.ToString();
                    name_txtbox_sign_db_panel.Text = row.Cells["name"].Value.ToString();
                    fname_txtbox_sign_db_panel.Text = row.Cells["fname"].Value.ToString();
                    add_txtbox_sign_db_panel.Text = row.Cells["address"].Value.ToString();
                    cnic_txtbox_sign_db_panel.Text = row.Cells["cnic"].Value.ToString();
                    gender_combobox_sign_db_panel.Text = row.Cells["gender"].Value.ToString();
                    ph_txtbox_sign_db_panel.Text = row.Cells["ph"].Value.ToString();
                    email_txtbox_sign_db_panel.Text = row.Cells["email"].Value.ToString();
                    // set image from gridview to picture box.....
                    string img = row.Cells["image"].Value.ToString();
                    string img2 = row.Cells["person_image"].Value.ToString();
                    if (picturebox_sign_db_panel.Image != null)
                    {
                        picturebox_sign_db_panel.Image.Dispose();
                        picturebox_sign_db_panel.Image = null;
                    }
                    if (picturebox2_sign_db_panel.Image != null)
                    {
                        picturebox2_sign_db_panel.Image.Dispose();
                        picturebox2_sign_db_panel.Image = null;
                    }
                    if (img != null)
                    {
                        var data = (byte[])row.Cells["image"].Value;
                        var stream = new MemoryStream(data);
                        picturebox_sign_db_panel.Image = Image.FromStream(stream);
                        picturebox_sign_db_panel.SizeMode = PictureBoxSizeMode.StretchImage;

                    }
                    if (img2 != null)
                    {
                        var data = (byte[])row.Cells["person_image"].Value;
                        var stream = new MemoryStream(data);
                        picturebox2_sign_db_panel.Image = Image.FromStream(stream);
                        picturebox2_sign_db_panel.SizeMode = PictureBoxSizeMode.StretchImage;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }
        //Search Record By id button in database panel of signauture...
        private void see_record_btn_sign_db_panel_Click(object sender, EventArgs e)
        {
            name_txtbox_sign_db_panel.Enabled = false;
            fname_txtbox_sign_db_panel.Enabled = false;
            cnic_txtbox_sign_db_panel.Enabled = false;
            ph_txtbox_sign_db_panel.Enabled = false;
            gender_combobox_sign_db_panel.Enabled = false;
            add_txtbox_sign_db_panel.Enabled = false;
            email_txtbox_sign_db_panel.Enabled = false;
            id_txtbox_sign_db_panel.Enabled = true;
        }
        //key down action listener on id textbox of database panel of signature..
        private void id_txtbox_sign_db_panel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               DataSet ds= sign.searchrecord(id_txtbox_sign_db_panel.Text);
               dataGridView_sign_db_panel.DataSource = ds.Tables[0].DefaultView;
            }
        }
        //Update button in database panel of signature...
        private void update_btn_sign_db_panel_Click(object sender, EventArgs e)
        {
            if (FeaturebuttonWasClicked == false)
            {
                MessageBoxIcon iconType = MessageBoxIcon.Stop;
                MessageBoxButtons buttonType = MessageBoxButtons.OK;
                buttonType = MessageBoxButtons.OK;
                iconType = MessageBoxIcon.Stop;
                DialogResult result =
                MessageBox.Show("Extract Feature First",
               "Warning", buttonType, iconType, 0, 0);
            }
            else
            {
                
                try
                {

                    byte[] byteImg = ImageToByteArray(im33, picturebox_sign_db_panel);
                    byte[] byteImg2 = ImageToByteArray(picturebox2_sign_db_panel.Image, picturebox2_sign_db_panel);
                    string ans = sign.UpdateRecord(byteImg, id_txtbox_sign_db_panel.Text, name_txtbox_sign_db_panel.Text, fname_txtbox_sign_db_panel.Text, add_txtbox_sign_db_panel.Text,
                                           ph_txtbox_sign_db_panel.Text, gender_combobox_sign_db_panel.Text, cnic_txtbox_sign_db_panel.Text, email_txtbox_sign_db_panel.Text, harris_count3.ToString(),byteImg2);
                    if (ans == "yes")
                    {
                        MessageBox.Show("Data Updated Successfully");
                        id_txtbox_sign_db_panel.Clear();
                        name_txtbox_sign_db_panel.Clear();
                        fname_txtbox_sign_db_panel.Clear();
                        cnic_txtbox_sign_db_panel.Clear();
                        email_txtbox_sign_db_panel.Clear();
                        add_txtbox_sign_db_panel.Clear();
                        gender_combobox_sign_db_panel.Text = "";
                        ph_txtbox_sign_db_panel.Clear();
                        if (picturebox_sign_db_panel.Image != null)
                        {
                            picturebox_sign_db_panel.Image.Dispose();
                            picturebox_sign_db_panel.Image = null;
                        }
                        if (picturebox2_sign_db_panel.Image != null)
                        {
                            picturebox2_sign_db_panel.Image.Dispose();
                            picturebox2_sign_db_panel.Image = null;
                        }
                        DataSet dss = sign.SearchAllRecord();
                        dataGridView_sign_db_panel.DataSource = dss.Tables[0].DefaultView;
                        FeaturebuttonWasClicked = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error while entering data." + ex);
                }
               
            }
        }
        //Import Image button on picture box2 of signature database panel...
        private void import_btn2_sign_db_panel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FileName = "";
                openFileDialog1.Title = "Images";
                openFileDialog1.Filter = "All Images|*.jpg; *.bmp; *.png";
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName.ToString() != "")
                {
                    fname1 = openFileDialog1.FileName.ToString();
                }
                picturebox2_sign_db_panel.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                picturebox2_sign_db_panel.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Import Image Properly");
            }
        }

       

        //******************************************************************************************************************************
        //******************************************************************************************************************************

}//End of class fingerprints...

    //################################################################################################################################
    //   Signature verification class....
    public class Signature
    {
        string Mysql = "server=localhost;Database=fingerprints;Uid=root;Pwd=302420;default command timeout=60";
        //********************************************************************************************************************
        //Search All Records of Signature Database................
        public DataSet SearchAllRecord()
        {
            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from signature";
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;

            }

            catch (Exception ex)
            {
                Console.WriteLine("error while entering data." + ex);
            }
            return null;

            connection.Close();
        }//End Search All Records........
        //*********** Auto id For Save Record Function...........

        public DataSet Autoid_for_SaveReocrd_Signature()
        {
            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select ID From signature";
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;


            }

            catch (System.SystemException)
            {
                Console.WriteLine("error in Sql");
            }
            return null;

            connection.Close();
        }
        //end of auto id for Save Record........
        //Store Data in database of new signature record..
        public String storedata(byte[] image, string id, string name, string fname, string address, string phone, string gender, string cnic, string email, string haris_count,byte[]image2)
        {
            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Insert into signature(ID,name,fname,cnic,ph,gender,address,email,image,corner,person_image)values(@id,@name,@fname,@cnic,@ph,@gender,@address,@email,@image,@harris_count,@image2)";
            cmd.Parameters.Add("@id", id);
            cmd.Parameters.Add("@image", image);
            cmd.Parameters.Add("@image2", image2);
            cmd.Parameters.Add("@name", name);
            cmd.Parameters.Add("@fname", fname);
            cmd.Parameters.Add("@ph", phone);
            cmd.Parameters.Add("@address", address);
            cmd.Parameters.Add("@cnic", cnic);
            cmd.Parameters.Add("@email", email);
            cmd.Parameters.Add("@gender", gender);
            cmd.Parameters.Add("@harris_count", haris_count);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return "yes";
        }
        //*****************************************************************************************************
        //Function of Delete from database panel...
        public String deleteRecord(string id)
        {
            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "delete FROm signature where ID=@record";
                cmd.Parameters.Add("@record", id);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return "yes";

            }
            catch (Exception ex)
            {
                Console.WriteLine("error while entering data." + ex);
            }
            return null;

            connection.Close();
        }
        //Function of Search Record By ID from database panel...
        public DataSet searchrecord(string id)
        {
            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from signature where id=@id";
                cmd.Parameters.Add("@id", id);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;
            }
                
            catch (Exception ex)
            {
                Console.WriteLine("error while entering data." + ex);
            }
            return null;
            connection.Close();
        }
        //Function of Update Record in signature database..
        public String UpdateRecord(byte[] image, string id, string name, string fname, string address, string phone, string gender, string cnic, string email, string haris_count,byte[] image2)
        {
            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "update signature set name=@name,fname=@fname,cnic=@cnic,ph=@ph,gender=@gender,address=@address,email=@email,image=@image,corner=@harris_count,person_image=@image2 where ID=@id";
            cmd.Parameters.Add("@id", id);
            cmd.Parameters.Add("@image", image);
            cmd.Parameters.Add("@image2", image2);
            cmd.Parameters.Add("@name", name);
            cmd.Parameters.Add("@fname", fname);
            cmd.Parameters.Add("@ph", phone);
            cmd.Parameters.Add("@address", address);
            cmd.Parameters.Add("@cnic", cnic);
            cmd.Parameters.Add("@email", email);
            cmd.Parameters.Add("@gender", gender);
            cmd.Parameters.Add("@harris_count", haris_count);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return "yes";
        }
    }
    //End of Signature Class...

    //===========================================================================================================================
    //===========================================================================================================================
    //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
    //Log in class

    public class login
    {
        string Mysql = "server=localhost;Database=project;Uid=root;Pwd=302420";
        
        public DataSet GetUsernamesFromDatabase()
        {

            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select username From login";
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;


            }

            catch (System.SystemException)
            {
                Console.WriteLine("error in Sql");
            }
            return null;
        }

        public bool pasword_match(string pasword, string username)
        {
           // Console.WriteLine("welcome");
            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            try
            {
                bool result = false;
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select pasword From login where username=@title";
                cmd.Parameters.Add("@title", username);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                MySqlDataReader DR = cmd.ExecuteReader();
                while (DR.Read())
                {

                    if (pasword!=DR.GetString(0))
                    {
                        result = false;
                        return result;
                    }
                    else
                    {

                        return result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return false;

            connection.Close();
        } //end of function pasword match
        //Create a new Account in for login method....

        public String CreateNewAccount(string username, string pasword)
        {
            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            try
            {
                int pas = int.Parse(pasword);
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "insert into login(username,pasword)values(@user,@pas)";
                cmd.Parameters.Add("@pas", pas);
                cmd.Parameters.Add("@user", username);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return "ok";

            }
            catch (Exception ex)
            {
                Console.WriteLine("error while entering data." + ex);
            }
            return null;

            connection.Close();
        }//End of Create new account method.........
        //**********************************************************************************************************************
        //Delete Account From User Accounts
        public String DeleteAccount(string account)
        {
            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "delete FROm login where username=@account";
                cmd.Parameters.Add("@account", account);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return "yes";

            }
            catch (Exception ex)
            {
                Console.WriteLine("error while entering data." + ex);
            }
            return null;

            connection.Close();
        }//End of Delete Account........
        //************************************************************************************************************************
        //Change Pasword Method...

        public String ChangePasword(string username, string newpasword)
        {
            MySqlConnection connection = new MySqlConnection(Mysql);
            connection.Open();
            try
            {
                int pasword = int.Parse(newpasword);
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "update login set pasword=@pas where username=@user";
                cmd.Parameters.Add("@pas", pasword);
                cmd.Parameters.Add("@user", username);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return "ok";

            }
            catch (Exception ex)
            {
                Console.WriteLine("error while entering data." + ex);
            }
            return null;

            connection.Close();
        }//End of Change PAsword.........
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Class of Webcam...
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
