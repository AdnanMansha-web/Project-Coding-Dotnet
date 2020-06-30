using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace GUI_Hotel_Reservation
{
    public partial class Client : Form
    {
        public Client()//////////////////////////////////////Start of Constructor///////////////////////////////////////////////
        {
            InitializeComponent();
            
            get_server_object();/////////////////////Calling a get_server_object function////////////////////////////////////////////////

            //==========================================================
            // File: Client.cs
            // This code used to set a date and time on login_panel. 
            //==========================================================
            string date = DateTime.Now.ToShortDateString();
            string time = DateTime.Now.ToShortTimeString();
            login_date_label.Text = date;
            login_time_label.Text = time;
            //==========================================================
            // End 
            //==========================================================
 //****************************************************************** Start of Reservation Panel For Constructor*****************************************************************************************//

            Room_Reserved();/////////////////////Calling a Room_Reserved function for Add Reservation tab on Reservation Panel////////////////////////////////////////////////
            
            int id = remoteObject.id_set();///////////////////////Through  remoteObject  get id_NO for Add Reservation tab on Reservation Panel//////////////////  

            id_number_textBox.Text = id.ToString();///////////////Set id_NO in textbox  for Add Reservation tab on Reservation Panel ////////////////////

            //==========================================================
            // File: Client.cs
            // This code used to Enable All the textBoxes for Add Reservation tab on Reservation Panel. 
            //==========================================================

            id_number_textBox.Enabled = false;
            Check_in_date_textBox.Enabled = false;
            Check_out_date_textBox.Enabled = false;
            Rate_textBox.Enabled = false;
            //==========================================================
            // End 
            //==========================================================
 //***************************************************************************************************************************************//
           
            //==========================================================
            // 
            // File: Client.cs
            // This code hide All the combobox used for room_number in Add Reservation tab on Reservation Panel.
            //
            //==========================================================
            middle_comboBox.Hide();
            Normal_comboBox.Hide();
            vip_comboBox.Hide();
            //==========================================================
            // End 
            //==========================================================
//*****************************************************************************************************************************************//
            Search_textBox.Hide();/////////////////////Hide  Search textBox  for Search tab on Reservation Panel//////////////////////////////////////////
            Delete_textBox.Enabled = false;/////////////////////Enable Delete textBox  for Delete tab on Reservation Panel//////////////////////////////////////////

            //==========================================================
            // File: Client.cs
            // This code hide All the combobox used for room_number in Update tab  on Reservation Panel .
            //==========================================================
            update_Normal_comboBox.Hide();
            update_Middle_comboBox.Hide();
            update_Vip_comboBox.Hide();

            //==========================================================
            // End 
            //==========================================================

            //==========================================================
            // File: Client.cs
            // This code used to Enable All the textBoxes for update tab on Reservation Panel. 
            //==========================================================
            update_id_no_textBox.Enabled = false;
            update_room_type_textBox.Enabled = false;
            update_room_number_textBox.Enabled = false;
            update_name_textBox.Enabled = false;
            update_fname_textBox.Enabled = false;
            update_cnic_textBox.Enabled = false;
            update_gender_textBox.Enabled = false;
            update_phone_textBox.Enabled = false;
            update_number_of_days_textBox.Enabled = false;
            update_check_in_date_textBox.Enabled = false;
            update_check_out_date_textBox1.Enabled = false;
            update_address_textBox.Enabled = false;
            update_rate_textBox.Enabled = false;
            //==========================================================
            // End 
            //==========================================================
            
//**************************************************************End of Reservation Panel For Constructor**************************************************************************************//

//*****************************************************************Start of Administration Panel For Constructor***************************************************************************************//
            
            int s_id = remoteObject.Staff_id();///////////////////////Through  remoteObject  get id_NO for Add Staff tab on Adminstration Panel//////////////////////// 

            Staff_id_textBox.Text = s_id.ToString();///////////////Set id_NO in textbox   for Add Staff tab on Adminstration Panel///////////////////////////////////////////////////

            Staff_privileges_textBox.Text = "user";/////////////////////Set privileges for Add Staff tab on Adminstration Panel//////////////////////////////////////////

            

 //*************************************************************************************************************************//
            
            Staff_search_textBox.Hide();/////////////////////Hide Staff Search textBox  for Search tab on Adminstration Panel//////////////////////////////////////////

            Staff_Delete_textBox.Enabled = false;/////////////////////Enable Staff Delete textBox for Delete tab on Adminstration Panel//////////////////////////////////////////
            //==========================================================
            // File: Client.cs
            // This code used to Enable Three textBoxes for Add Staff tab on Adminstration Panel . 
            //==========================================================
            Staff_id_textBox.Enabled = false;
            Staff_joining_textBox.Enabled = false;
            Staff_privileges_textBox.Enabled = false;
            //==========================================================
            // End 
            //==========================================================
 //**************************************************************************************************************************//
            //==========================================================
            // File: Client.cs
            // This code used to Enable All the textBoxes for update tab on Adminstration Panel. 
            //==========================================================
              Staff_update_joiningdate_textBox.Enabled = false;
              Staff_update_id_textBox.Enabled=false;
              Staff_update_name_textBox.Enabled=false;
              Staff_update_password_textBox.Enabled=false;
              Staff_update_privileges_textBox.Enabled=false;
              Staff_update_cnic_textBox.Enabled=false;
              Staff_update_fname_textBox.Enabled=false;
              Staff_update_phone_textBox.Enabled=false;
              Staff_update_address_textBox.Enabled=false;
              Staff_update_salary_textBox.Enabled=false;
              Staff_update_privileges_textBox.Enabled =false;
              //==========================================================
              // End 
              //==========================================================
 //*************************************************************************End of Administration Panel For Constructor*************************************************************************//
             
             
             
            
        }///////////////////////////////////////End of Constructor///////////////////////////////////////////////

        //==========================================================
        // 
        // File: Client.cs
        // These are the globle variables .
        //
        //==========================================================


        product remoteObject;///////////////////for remoteObject////////////////////////
        string room;//////////////////////for room_number of Add Reservation tab on Reservation Panel /////////////////////////////
        string Staff_gender;/////////////for Staff_gender  of Add Staff tab on Adminstration Panel //////////////////////////////
        string updated_gender;///////////////////for gender of update tab on staff Panel

        //==========================================================
        // End 
        //==========================================================

//**************************************************************************************************************************//
        //==========================================================
        // 
        // File: Client.cs
        // This code make a remote object .
        //
        //==========================================================


        void get_server_object()
        {
            try
            {
                remoteObject = (product)Activator.GetObject(typeof(product), "tcp://localhost:6/RemoteObjects");

            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //==========================================================
        // End 
        //==========================================================

//**************************************************************************************************************************//

//*************************************************************************Start of  Login Panel********************************************************************************//

        //==========================================================
        // 
        // File: Client.cs
        // This code used for login .
        //Takes user_name and Password from user and then login.
        //If Anyone False then it will show error through label. 
        //==========================================================

        //////Change Text to Password 
        private void password_textBox_TextChanged(object sender, EventArgs e)
        {
            password_textBox.PasswordChar = '*';
        }

        ///////Login button
        private void Login_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Staff_radioButton.Checked)
                {
                    string username = username_textBox.Text;
                    string password = password_textBox.Text;
                    string privilege = "user";
                    bool d = remoteObject.Login(username, password, privilege);
                    if (d == true)
                    {
                        Login_panel.Hide();
                        Administration_panel.Hide();
                        Administration_change_password_panel.Hide();
                        Staff_password_change_panel.Hide();
                        Reservation_panel.Show();
                    }
                    else
                    {
                        Error_label.Text = "Invalid id or password";
                        Error_label.Show();
                    }


                }


                if (Admin_radioButton.Checked)
                {
                    string username = username_textBox.Text;
                    string password = password_textBox.Text;
                    string privilege = "Administration";
                    bool d = remoteObject.Login(username, password, privilege);
                    if (d == true)
                    {
                        Login_panel.Hide();
                        Reservation_panel.Hide();
                        Administration_panel.Show();
                        Administration_change_password_panel.Hide();
                        Staff_password_change_panel.Hide();
                        
                    }
                    else
                    {
                        Error_label.Text = "Invalid id or password";
                        Error_label.Show();
                    }


                }
                if (!(Staff_radioButton.Checked) && !(Admin_radioButton.Checked))
                {

                    MessageBoxIcon icontype = MessageBoxIcon.Error;
                    MessageBoxButtons b = MessageBoxButtons.OK;
                    DialogResult result =
               MessageBox.Show("First Checked the Staff or Adminstration RadioButton",
              "Error MessageBox", b, icontype, 0, 0);
                }
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }////End of login Button


        /////Checkbox Show hide password to MessageBox.
        private void Password_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            string es;
            try
            {

                if (Password_checkBox.Checked)
                {
                    es = password_textBox.Text;
                    MessageBox.Show(es);

                }
                if ((!Password_checkBox.Checked))
                {
                    password_textBox.PasswordChar = '*';
                }
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }////End of Checkbox
         
        //==========================================================
        // End 
        //==========================================================
//**********************************************************************End of  Login Panel*************************************************************************************//

        
//**********************************************************************Start of  Reservation Panel****************************************************************************//
        /////Show Staff  Password Change Panel and hide All other panels
        private void changePasswordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Administration_panel.Hide();
            Login_panel.Hide();
            Reservation_panel.Hide();
            Administration_change_password_panel.Hide();
            Staff_password_change_panel.Show();
        }
//**********************************************************************Start of  Staff  Password Change Panel**************************************//

        //==========================================================
        // 
        // File: Client.cs
        // This code used for All those textfields which use in the Staff Password Chane panel 
        //Change Text to Password 
        //==========================================================
        private void Staff_old_password_textBox_TextChanged(object sender, EventArgs e)
        {
            Staff_old_password_textBox.PasswordChar = '*';
        }

        private void Staff_new_password_textBox_TextChanged(object sender, EventArgs e)
        {
            Staff_new_password_textBox.PasswordChar = '*';
        }

        private void Staff_conform_new_password_textBox_TextChanged(object sender, EventArgs e)
        {
            Staff_conform_new_password_textBox.PasswordChar = '*';
        }
        //==========================================================
        // End 
        //==========================================================

        //checkBox used on Staff Password Change Panel for Showing New Password on MessageBox 
        private void Staff_view_new_password_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            string es;
            try
            {
                if (Staff_view_new_password_checkBox.Checked)
                {
                    es = Staff_new_password_textBox.Text;
                    MessageBox.Show(es);

                }
                if ((!Staff_view_new_password_checkBox.Checked))
                {
                    Staff_new_password_textBox.PasswordChar = '*';
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }////End of CheckBox


        //==========================================================
        // 
        // File: Client.cs
        // This code hide All the Panels which used in Form.
        //This code used for change password of Staff. 
        //==========================================================

        ////Cancel Button on Staff Password Change.
        private void Staff_password_change_canel_button_Click(object sender, EventArgs e)
        {
            Staff_password_change_panel.Hide();
            Login_panel.Hide();
            Administration_change_password_panel.Hide();
            Administration_panel.Hide();
            Reservation_panel.Show();
        }

        ////Save Button on Staff Password Change.
        private void Staff_save_password_change_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Staff_old_password_textBox.Text != "")//"1" if.
                {
                    if (Staff_new_password_textBox.Text != "")//"2" if.
                    {
                        if (Staff_conform_new_password_textBox.Text != "")//"3" if.
                        {
                            string password_change = password_textBox.Text;
                            string user_name = username_textBox.Text;
                            string staff_old_password = Staff_old_password_textBox.Text;
                            string staff_new_password = Staff_new_password_textBox.Text;
                            if (staff_old_password == password_change)//"4" if.
                            {
                                if (Staff_new_password_textBox.Text == Staff_conform_new_password_textBox.Text)//"5" if.
                                {
                                    string result = remoteObject.Staff_change_password(user_name, staff_new_password);
                                    MessageBox.Show(result);

                                    string res = remoteObject.login_change_password(user_name, staff_new_password);
                                    MessageBox.Show(res);
                                }//End of "5" if.
                                else
                                {

                                    MessageBox.Show("Mismatch Confrom Password with New Password");

                                }
                            }//End of "4" if.
                            else
                            {
                                MessageBox.Show("Incorrect Old Password");
                            }
                        }//End of "3" if.
                        else
                        {
                            MessageBox.Show("Fillup Conform New Password Textfield");
                        }
                    }//End of "2" if.
                    else
                    {
                        MessageBox.Show("Fillup New Password Textfield");
                    }
                }// End of "1" if.
                else
                {
                    MessageBox.Show("Fillup old Password Textfield");
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        //==========================================================
        // End 
        //==========================================================

//**********************************************************************end of  Staff  Password Change Panel****************************************//

        //==========================================================
        // 
        // File: Client.cs
        //This code used for Logout and exit .
        // This code also used for showing the tabpages of Add_Reservation through ToolStripMenuItem.
        //==========================================================


        ////Logout
        private void logoutToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Login_panel.Show();
            Reservation_panel.Hide();
            Administration_panel.Hide();
            Administration_change_password_panel.Hide();
            Staff_password_change_panel.Hide();
            username_textBox.Clear();
            password_textBox.Clear();
            Staff_radioButton.Checked = false;
            Admin_radioButton.Checked = false;
            Error_label.Hide();
        }

        ////Exit
        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addReservationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Add_Reservation.SelectTab(0);
        }

        private void searchToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Add_Reservation.SelectTab(1);
        }

        private void deleteToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Add_Reservation.SelectTab(2);
        }

        private void viewReportToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Add_Reservation.SelectTab(3);
        }

        private void updateToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Add_Reservation.SelectTab(4);
        }
        //==========================================================
        // End of ToolStripMenuItem code used for Reservation Panel.
        //==========================================================

//*******************************************************************************************************************************//
        
        
        //==========================================================
        // 
        // File: Client.cs
        // This code remove a Reserved Room .
        //Calling From Constructor
        //==========================================================

        void Room_Reserved()
        {
            try
            {
                
                List<string> Reserved_room = remoteObject.Reserved_Room();
               
                List<int> intList = new List<int>();
                for (int j = 0; j < Reserved_room.Count; j++)
                {
                    intList.Add(int.Parse(Reserved_room[j]));
                }
                foreach (int i in intList) // Loop through List with foreach.
                {

                  if (i >= 1 && i <= 20)
                   {
                       Normal_comboBox.Items.Remove(i.ToString());
                      update_Normal_comboBox.Items.Remove(i.ToString());
                    }
                    if (i >= 21 && i <= 30)
                    {
                        middle_comboBox.Items.Remove(i.ToString());
                        update_Middle_comboBox.Items.Remove(i.ToString());
                    }
                    if (i >= 31 && i <= 40)
                    {
                        vip_comboBox.Items.Remove(i.ToString());
                       update_Vip_comboBox.Items.Remove(i.ToString());
                    }

                }
 

            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //==========================================================
        // End 
        //==========================================================
        void Room_add(String Add_room)
        {

            try
            {
                int Room_add = int.Parse(Add_room);
                if (Room_add >= 1 && Room_add <= 20)
                {
                    Normal_comboBox.Items.Add(Room_add.ToString());
                    update_Normal_comboBox.Items.Add(Room_add.ToString());
                }
                else if (Room_add >= 21 && Room_add <= 30)
                {

                    middle_comboBox.Items.Remove(Room_add.ToString());
                    update_Middle_comboBox.Items.Remove(Room_add.ToString());
                }
                else if (Room_add >= 31 && Room_add <= 40)
                {
                    vip_comboBox.Items.Remove(Room_add.ToString());
                    update_Vip_comboBox.Items.Remove(Room_add.ToString());
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

//*******************************************************************************************************************************//
        
        //==========================================================
        // 
        // File: Client.cs
        // This code used for showing Room_Number for Different Type of Room .
       //==========================================================

        private void Room_type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Room_type_comboBox.SelectedItem.ToString() == "Normal")
                {
                    Room_number_comboBox.Hide();
                    vip_comboBox.Hide();
                    middle_comboBox.Hide();
                    Normal_comboBox.Show();

                }
                if (Room_type_comboBox.SelectedItem.ToString() == "Middle")
                {
                    Room_number_comboBox.Hide();
                    Normal_comboBox.Hide();
                    vip_comboBox.Hide();
                    middle_comboBox.Show();
                }
                if (Room_type_comboBox.SelectedItem.ToString() == "Vip")
                {
                    Room_number_comboBox.Hide();
                    middle_comboBox.Hide();
                    Normal_comboBox.Hide();
                    vip_comboBox.Show();
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        //==========================================================
        // End 
        //==========================================================

 //*******************************************************************************************************************************//

        //==========================================================
        // Start of number of days comboBox which calculate rate then show rate in the rate textbox.  
        //==========================================================

        private void NO_of_days_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Room_type_comboBox.Text != "")
                {
                    if (Room_type_comboBox.SelectedItem.ToString() == "Normal")
                    {

                        String d = NO_of_days_comboBox.SelectedItem.ToString();
                        int no_of_days = int.Parse(d);

                        Rate_textBox.Text = ((300) * no_of_days).ToString();

                    }


                    if (Room_type_comboBox.SelectedItem.ToString() == "Middle")
                    {
                        String d = NO_of_days_comboBox.SelectedItem.ToString();
                        int no_of_days = int.Parse(d);

                        Rate_textBox.Text = ((500) * no_of_days).ToString();
                    }

                    if (Room_type_comboBox.SelectedItem.ToString() == "Vip")
                    {
                        String d = NO_of_days_comboBox.SelectedItem.ToString();
                        int no_of_days = int.Parse(d);

                        Rate_textBox.Text = ((300) * no_of_days).ToString();
                    }
                }
                else
                {
                    MessageBox.Show("First select Room type");
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }//End of Number of days combobox.




        //==========================================================
        // End of number of days comboBox which calculate rate then show rate in the rate textbox.  
        //==========================================================

//*******************************************************************************************************************************//
      

        //==========================================================
        // 
        // File: Client.cs
        // This code used for taking start date of Reservation through datetime Picker
        //==========================================================

        private void Start_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            string date = Start_dateTimePicker.Value.ToString("yyyy-MM-dd");
            Check_in_date_textBox.Text = date;
        }


        //==========================================================
        // 
        // File: Client.cs
        // This code used for taking end date of Reservation through datetime Picker
        //==========================================================

        private void End_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            string date = End_dateTimePicker.Value.ToString("yyyy-MM-dd");
            Check_out_date_textBox.Text = date;

        }
//*******************************************************************************************************************************//

        //==========================================================
        // 
        // File: Client.cs
        // This code used for taking All the Information of Added Reservation through textboxes by using button name "Done" 
        //Then store in database through remoteObject After that it will show u a Message for Success or Failure.
        //==========================================================

        ////////Done button used on Add Reservation tab on Reservation Panel.
        private void Done_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (id_number_textBox.Text != "" && Room_type_comboBox.Text != "" && Name_textBox.Text != "" && Father_name_textBox.Text != "" && Cnic_textBox.Text != "" && gender_comboBox.Text != "" && Phone_textBox.Text != "" && NO_of_days_comboBox.Text != "" && Check_in_date_textBox.Text != "" && Check_out_date_textBox.Text != "" && Address_textBox.Text != "" && Rate_textBox.Text != "")
                {
                    string id = id_number_textBox.Text;
                    string room_type = Room_type_comboBox.SelectedItem.ToString();

                    if (Room_type_comboBox.SelectedItem.ToString() == "Normal")
                    {
                        room = Normal_comboBox.SelectedItem.ToString();
                        Normal_comboBox.Items.Remove(room);
                    }
                    if (Room_type_comboBox.SelectedItem.ToString() == "Middle")
                    {
                        room = middle_comboBox.SelectedItem.ToString();
                        middle_comboBox.Items.Remove(room);
                    }
                    if (Room_type_comboBox.SelectedItem.ToString() == "Vip")
                    {
                        room = vip_comboBox.SelectedItem.ToString();
                        vip_comboBox.Items.Remove(room);
                    }

                    string name = Name_textBox.Text;
                    string father_name = Father_name_textBox.Text;
                    string cnic = Cnic_textBox.Text;
                    string gender = gender_comboBox.SelectedItem.ToString();
                    string phone = Phone_textBox.Text;
                    string number_of_days = NO_of_days_comboBox.Text;
                    string start_date = Check_in_date_textBox.Text;
                    string end_date = Check_out_date_textBox.Text;
                    string address = Address_textBox.Text;
                    string rate = Rate_textBox.Text;
                    MessageBox.Show(start_date);
                    string result = remoteObject.Add_Reservation(id, room_type, room, name, father_name, cnic, gender, phone, number_of_days, start_date, end_date, address, rate);
                    MessageBox.Show(result);
                    int id_number = remoteObject.id_set();///////////////////////Through  remoteObject  get id_NO for Add Client////////////////////////  
                    id_number_textBox.Text = id_number.ToString();///////////////Set id_NO in textbox///////////////////////////////////////////////////

                    Name_textBox.Clear();
                    Father_name_textBox.Clear();
                    Cnic_textBox.Clear();
                    Phone_textBox.Clear();

                    Check_in_date_textBox.Clear();
                    Check_out_date_textBox.Clear();
                    Address_textBox.Clear();


                    Rate_textBox.Clear();
                }
                else
                {
                    MessageBox.Show("Fillup All the Textboxes");
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //==========================================================
        // End   
        //==========================================================
//************************************************************************************************************************//
        
        //==========================================================
        // 
        // File: Client.cs
        // This code used for clear All the Textboxes that involve in Add Reservation.
        //==========================================================

        private void Reset_button_Click(object sender, EventArgs e)
        {
            try
            {
                Name_textBox.Clear();
                Father_name_textBox.Clear();
                Cnic_textBox.Clear();
                Phone_textBox.Clear();

                Check_in_date_textBox.Clear();
                Check_out_date_textBox.Clear();
                Address_textBox.Clear();
                Rate_textBox.Clear();
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //==========================================================
        //End   
        //==========================================================

//***************************************************************************************************************************//
        
        //==========================================================
        // 
        // File: Client.cs
        //This code used for Searching Any Record of Reservation from database 
        //Through Id,Name,Room by calling method from remoteobject.
        //
        //==========================================================

        private void Search_id_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            Search_textBox.Show();
        }

        private void Search_name_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            Search_textBox.Show();
        }

        private void Searchroom_radioButton_CheckedChanged(object sender, EventArgs e)
        {

            Search_textBox.Show();
        }

        //Search button on Search tab of Reservation Panel.
        private void Search_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Search_id_radioButton.Checked != false || Search_name_radioButton.Checked != false || Searchroom_radioButton.Checked != false)
                {
                    if (Search_textBox.Text != "")
                    {
                        if (Search_id_radioButton.Checked)
                        {

                            String Search_id_number = Search_textBox.Text;
                            DataSet ds = remoteObject.Search_by_id(Search_id_number);
                            Search_dataGridView.DataSource = ds.Tables[0].DefaultView;
                        }


                        if (Search_name_radioButton.Checked)
                        {

                            String name = Search_textBox.Text;
                            DataSet ds = remoteObject.Search_by_Name(name);
                            Search_dataGridView.DataSource = ds.Tables[0].DefaultView;
                        }


                        if (Searchroom_radioButton.Checked)
                        {
                            String room_number = Search_textBox.Text;
                            DataSet sss = remoteObject.Search_by_Room(room_number);
                            Search_dataGridView.DataSource = sss.Tables[0].DefaultView;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Fillup the required Textfield");
                    }
                }
                else
                {
                    MessageBox.Show("First chech Any One RadioButton ");
                }
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }//End of Search Button

        //==========================================================
        //End   
        //==========================================================

//***************************************************************************************************************************//



        //==========================================================
        // 
        // File: Client.cs
        //This code used for Deleting Any Record of Reservation from database 
        //Through Id by calling method from remoteobject.
        //After Click the Show Data button on Delete tab on Reservation Panel. 
        //==========================================================

        //Show data Button that Shows data into DataGridView on Delete Tab on Reservation Panel.
        private void Delete_Show_data_button_Click(object sender, EventArgs e)
        {
            DataSet ds = remoteObject.Delete();
            Delete_dataGridView.DataSource = ds.Tables[0].DefaultView;
        }//End of Show Data button For Delete tab.


        //DataGridView When click on the Content of a sell Then it set the id_number into Delete_testBox.
        private void Delete_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = Delete_dataGridView.Rows[e.RowIndex];
                    Delete_textBox.Text = row.Cells["id_number"].Value.ToString();
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }//End of DataGridView for Delete tab.


        //Delete by one button Delete one Record of Reservation from Database on Delete tab on Reservation Panel.
        private void Delete_by_one_button_Click(object sender, EventArgs e)
        {


            try
            {
                if (Delete_textBox.Text != "")
                {
                    string Delete_id = Delete_textBox.Text;
                    List<string> add_room = remoteObject.Add_Room(Delete_id);
                    string result = remoteObject.Delete_by_id(Delete_id);
                    MessageBox.Show(result);
                    DataSet ds = remoteObject.Delete();
                    Delete_dataGridView.DataSource = ds.Tables[0].DefaultView;
                    string room_add=add_room.ElementAt(0);
                    Room_add(room_add);

                    

                }
                else
                {
                    MessageBox.Show("First Click to Show data button and click to content cell then Click on That Button  ");
                }



            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }//End of Delete by One Button 

        //All Delete Button Delete the All Record of Reservation from Database on Delete tab on Reservation Panel.
        private void All_Delete_button_Click(object sender, EventArgs e)
        {
            try
            {
                string result = remoteObject.All_Delete();
                MessageBox.Show(result);
                DataSet ds = remoteObject.Delete();
                Delete_dataGridView.DataSource = ds.Tables[0].DefaultView;
               
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }//End of All Delete Button.

        //==========================================================
        //End   
        //==========================================================

//***************************************************************************************************************************//


        //==========================================================
        // 
        // File: Client.cs
        //This code used for Showing Reports of Reservations on View Report tab on Reservation panel.
        //Which are four Daily Report,Weekly Report ,Monthly and Full Report.
        //Through  method Calling  from remoteobject.
        //==========================================================
 
        //Daily Report Button on View Report tab on Reservation Panel. 
        private void Daily_report_button_Click(object sender, EventArgs e)
        {

            
            string todaydate = DateTime.UtcNow.ToString("yyyy-MM-dd");
           
            DataSet res = remoteObject.Daily_report(todaydate);
            view_report_dataGridView.DataSource = res.Tables[0].DefaultView;

        }//End of Daily Report Button.


         //Weekly Report Button on View Report tab on Reservation Panel.
        private void Weekly_report_button_Click(object sender, EventArgs e)
        {

            DataSet res = remoteObject.Wee_report();
            view_report_dataGridView.DataSource = res.Tables[0].DefaultView;
           
        }//End of Weekly Report button.


        //Full Report Button on View Report tab on Reservation Panel.
        private void Full_report_button_Click(object sender, EventArgs e)
        {
            DataSet result = remoteObject.Full_report();
            view_report_dataGridView.DataSource = result.Tables[0].DefaultView;

        }//End of Full Report button.

        //Monthly Report Button on View Report tab on Reservation Panel.
        private void Monthly_Report_button_Click(object sender, EventArgs e)
        {
            DataSet result = remoteObject.Monthly_report();
            view_report_dataGridView.DataSource = result.Tables[0].DefaultView;
        }//End of Monthly Report button.

        //Print_Report_button will print the report which show in View_report_dataGridView on View Report tab on Reservation_panel.
        private void Print_Report_button_Click(object sender, EventArgs e)
        {
            try
            {

                Document doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 42, 35);
                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("C://PDFs//Test.pdf", FileMode.Create));
                doc.Open();

                PdfPTable table = new PdfPTable(view_report_dataGridView.Columns.Count);
                table.DefaultCell.Padding = 3;
                table.WidthPercentage = 100;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.DefaultCell.BorderWidth = 2;


                for (int j = 0; j < view_report_dataGridView.Columns.Count; j++)
                {
                    DataGridViewColumn column = view_report_dataGridView.Columns[j];
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(150, 150, 150);
                    table.AddCell(cell);
                }
                table.HeaderRows = 1;

                for (int i = 0; i < view_report_dataGridView.Rows.Count; i++)
                {
                    for (int k = 0; k < view_report_dataGridView.Columns.Count; k++)
                    {
                        if (view_report_dataGridView[k, i].Value != null)
                        {
                            table.AddCell(new Phrase(view_report_dataGridView[k, i].Value.ToString()));

                        }
                    }
                }

                doc.Add(table);


                doc.Close();
                System.Diagnostics.Process.Start(@"C:\\PDFs\\Test.pdf");
               
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }//End of Print Report Button.


        //==========================================================
        //End   
        //==========================================================

//***************************************************************************************************************************//

        //==========================================================
        // 
        // File: Client.cs
        //This code used for Update the Record of Reservation in DataBase.
        //First Click on Show Data Button then You Can Update the Record After Click on Done Button.
        //Through  method Calling  from remoteobject.
        //==========================================================

        //Change the textBox of update_room_type then you can get All the Avaliable Rooms in Room Number ComboBox on update tab on Reservation Panel . 
        private void update_room_type_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (update_room_type_textBox.Text == "Normal")
                {
                    update_Normal_comboBox.Show();
                    update_Middle_comboBox.Hide();
                    update_Vip_comboBox.Hide();
                    update_room_no_comboBox.Hide();
                }

                if (update_room_type_textBox.Text == "Middle")
                {
                    update_Middle_comboBox.Show();
                    update_Vip_comboBox.Hide();
                    update_room_no_comboBox.Hide();
                    update_Normal_comboBox.Hide();

                }


                if (update_room_type_textBox.Text == "Vip")
                {
                    update_Vip_comboBox.Show();
                    update_room_no_comboBox.Hide();
                    update_Normal_comboBox.Hide();
                    update_Middle_comboBox.Hide();
                }
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }//End of Change the textBox of update_room_type then you can get All the Avaliable Rooms in Room Number ComboBox on update tab on Reservation Panel .


        //Select room Type from update_room_type_comobox and set into update_room_type_textBox on update tab on Reservation Panel. 
        private void update_room_type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_room_type_textBox.Text = update_room_type_comboBox.SelectedItem.ToString();
        
        }// End of Select room Type from update_room_type_comobox and set into update_room_type_textBox on update tab on Reservation Panel.

        //Select Room number from update__Normal_comobox and set into update_room_Number_textBox on update tab on Reservation Panel.
        private void update_Normal_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_room_number_textBox.Text = update_Normal_comboBox.SelectedItem.ToString();
        }//End

        //Select Room number from update__Middle_comobox and set into update_room_Number_textBox on update tab on Reservation Panel.
        private void update_Middle_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_room_number_textBox.Text = update_Middle_comboBox.SelectedItem.ToString();
        }//End

        //Select Room number from update__Vip_comobox and set into update_room_Number_textBox on update tab on Reservation Panel.
        private void update_Vip_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_room_number_textBox.Text = update_Vip_comboBox.SelectedItem.ToString();
        
        }//End

        //Select Gender from update__Gender_comobox and set into update_gender_textBox on update tab on Reservation Panel.
        private void update_gender_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_gender_textBox.Text = update_gender_comboBox.SelectedItem.ToString();
        }//End

        //Select Format of Date from update__Check_in_date_datetimePicker and set into update_check_in_date_textBox on update tab on Reservation Panel.
        private void update_check_in_date_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            string Start_date = update_check_in_date_dateTimePicker.Value.ToString("yyyy-MM-dd");
            update_check_in_date_textBox.Text = Start_date;
        }//End

        //Select Format of Date from update__Check_out_date_datetimePicker and set into update_check_out_date_textBox1 on update tab on Reservation Panel.
        private void update_check_out_date_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            string end_date = update_check_out_date_dateTimePicker.Value.ToString("yyyy-MM-dd");
            update_check_out_date_textBox1.Text = end_date;
        }//End

        //Select Number of days from update__no_of_days_comboBox and set into update_Number_of_days_textBox on update tab on Reservation Panel.
        private void update_no_of_days_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_number_of_days_textBox.Text = update_no_of_days_comboBox.SelectedItem.ToString();
        }//End


        //Calculate the Rate According to Room Type with the number of days  on update tab on Reservation Panel.
        private void update_number_of_days_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (update_room_type_textBox.Text == "Normal")
                {

                    String d = update_number_of_days_textBox.Text;
                    int no_of_days = int.Parse(d);
                    update_rate_textBox.Text = ((300) * no_of_days).ToString();

                }


                if (update_room_type_textBox.Text == "Middle")
                {
                    String d = update_number_of_days_textBox.Text;
                    int no_of_days = int.Parse(d);
                    update_rate_textBox.Text = ((500) * no_of_days).ToString();
                }

                if (update_room_type_textBox.Text == "Vip")
                {
                    String d = update_number_of_days_textBox.Text;
                    int no_of_days = int.Parse(d);
                    update_rate_textBox.Text = ((300) * no_of_days).ToString();
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }//End

        //Show Data Button that Enable all the textBoxes and Show data in DataGridView on Update tab on Reservation Panel.
        private void update_Show_data_button_Click(object sender, EventArgs e)
        {


            try
            {
                update_name_textBox.Enabled = true;
                update_fname_textBox.Enabled = true;
                update_cnic_textBox.Enabled = true;

                update_phone_textBox.Enabled = true;
                update_address_textBox.Enabled = true;

                DataSet result = remoteObject.update();
                update_dataGridView.DataSource = result.Tables[0].DefaultView;
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }//End of Show Data Button.

        // DataGridView When we click on the Content of cell then it set data into respective textboxes  on Update tab on Reservation Panel.
        private void update_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = update_dataGridView.Rows[e.RowIndex];
                    update_id_no_textBox.Text = row.Cells["id_number"].Value.ToString();
                    update_room_type_textBox.Text = row.Cells["room_type"].Value.ToString();
                    update_room_number_textBox.Text = row.Cells["room_number"].Value.ToString();
                    update_name_textBox.Text = row.Cells["name"].Value.ToString();
                    update_fname_textBox.Text = row.Cells["father_name"].Value.ToString();
                    update_cnic_textBox.Text = row.Cells["cnic"].Value.ToString();
                    update_gender_textBox.Text = row.Cells["gender"].Value.ToString();
                    update_phone_textBox.Text = row.Cells["phone"].Value.ToString();
                    update_number_of_days_textBox.Text = row.Cells["number_of_days"].Value.ToString();

                    string Start_date = row.Cells["check_in_date"].Value.ToString();
                    update_check_in_date_textBox.Text = DateTime.Parse(Start_date).ToString("yyyy-MM-dd");

                    string end_date = row.Cells["check_out_date"].Value.ToString();
                    update_check_out_date_textBox1.Text = DateTime.Parse(end_date).ToString("yyyy-MM-dd");

                    update_address_textBox.Text = row.Cells["address"].Value.ToString();
                    update_rate_textBox.Text = row.Cells["rate"].Value.ToString();



                }
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }//End of DataGridView.




        //Done Button that Saves  all the Updated Data into DataBase and Show data in DataGridView on Update tab on Reservation Panel.
        private void update_done_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (update_id_no_textBox.Text != "" && update_room_type_textBox.Text != "" && update_name_textBox.Text != "" && update_fname_textBox.Text != "" && update_cnic_textBox.Text != "" && update_gender_textBox.Text != "" && update_phone_textBox.Text != "" && update_number_of_days_textBox.Text != "" && update_check_in_date_textBox.Text != "" && update_check_out_date_textBox1.Text != "" && update_address_textBox.Text != "" && update_rate_textBox.Text != "")
                {
                    string update_id = update_id_no_textBox.Text;
                    string update_room_type = update_room_type_textBox.Text;
                    string update_room_number = update_room_number_textBox.Text;
                    string update_name = update_name_textBox.Text;
                    string update_fname = update_fname_textBox.Text;
                    string update_cnic = update_cnic_textBox.Text;
                    string update_gender = update_gender_textBox.Text;
                    string update_phone = update_phone_textBox.Text;
                    string update_no_days = update_number_of_days_textBox.Text;
                    string update_check_in_date = update_check_in_date_textBox.Text;
                    string update_check_out_date = update_check_out_date_textBox1.Text;
                    string update_address = update_address_textBox.Text;
                    string update_rate = update_rate_textBox.Text;

                    string result = remoteObject.Update_Reservation(update_id, update_room_type, update_room_number, update_name, update_fname, update_cnic, update_gender, update_phone, update_no_days, update_check_in_date, update_check_out_date, update_address, update_rate);
                    MessageBox.Show(result);

                    DataSet ds = remoteObject.update();
                    update_dataGridView.DataSource = ds.Tables[0].DefaultView;
                }
                else
                {
                    MessageBox.Show("First click the show data Button then click on cell Content in dataGridView");
                }
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }//End of Done Button.

        //==========================================================
        //End   
        //==========================================================

//***************************************************************************************************************************//

//**********************************************************************************End of Reservation Panel*******************************************************************//

     
//***************************************************************Start of Administration Panel*********************************************************************************//    


        //==========================================================
        // 
        // File: Client.cs
        // This code hide All the Panels which used in Form.
        //This code used for change password of Adminstration. 
        //==========================================================


        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administration_panel.Hide();
            Login_panel.Hide();
            Reservation_panel.Hide();
            Administration_change_password_panel.Show();
            Staff_password_change_panel.Hide();
        }
        //==========================================================
        //End   
        //==========================================================
//*************************************************************Start of Administration change password panel*****************************//


        //==========================================================
        // 
        // File: Client.cs
        // This code used for All those textfields which use in the Administration Change Password  panel 
        //Change Text to Password 
        //==========================================================
        private void Admin_old_password_textBox_TextChanged(object sender, EventArgs e)
        {
            Admin_old_password_textBox.PasswordChar = '*';
        }

        private void Admin_New_password_textBox_TextChanged(object sender, EventArgs e)
        {
            Admin_New_password_textBox.PasswordChar = '*';
        }

        private void Admin_conform_new_password_textBox_TextChanged(object sender, EventArgs e)
        {
            Admin_conform_new_password_textBox.PasswordChar = '*';
        }

        //==========================================================
        //End   
        //==========================================================

        //CheckBox used for showing the new password on Administration password change Panel
        private void Admin_change_view_password_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            string es;
            try
            {
                if (Admin_change_view_password_checkBox.Checked)
                {
                    es = Admin_New_password_textBox.Text;
                    MessageBox.Show(es);

                }
                if ((!Admin_change_view_password_checkBox.Checked))
                {
                    Admin_New_password_textBox.PasswordChar = '*';
                }
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }//End of CheckBox on Administration password change Panel.


        //Save Button code which used in Administration_change_password_panel;
        private void Change_password_save_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Admin_old_password_textBox.Text != "")//"1" if.
                {
                    if (Admin_New_password_textBox.Text != "")//"2" if.
                    {
                        if (Admin_conform_new_password_textBox.Text != "")//"3" if.
                        {
                            string admin_old_password = Admin_old_password_textBox.Text;
                            string admin_new_password = Admin_New_password_textBox.Text;
                            string password_change = password_textBox.Text;
                            string user_name = username_textBox.Text;
                            if (admin_old_password == password_change)//"4" if.
                            {
                                if (Admin_New_password_textBox.Text == Admin_conform_new_password_textBox.Text)//"5" if.
                                {
                                    MessageBox.Show(password_change);
                                    string result = remoteObject.Admin_change_password(user_name, admin_new_password);
                                    MessageBox.Show(result);
                                }//End of "5" if.
                                else
                                {

                                    MessageBox.Show("Mismatch Confrom Password with New Password");

                                }

                            }//End of "4" if.
                            else
                            {
                                MessageBox.Show("Incorrect Old Password");
                            }
                        }//End of "3" if.
                        else
                        {
                            MessageBox.Show("Fillup Conform New Password Textfield");
                        }
                    }//End of "2" if.
                    else
                    {
                        MessageBox.Show("Fillup New Password Textfield");
                    }
                }// End of "1" if.
                else
                {
                    MessageBox.Show("Fillup old Password Textfield");
                }

            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }//End of Save Button on Administration password change Panel.



        //Cancel Button code which used in Administration_change_password_panel;
        private void Change_password_Cancel_button_Click(object sender, EventArgs e)
        {
            Administration_change_password_panel.Hide();
            Administration_panel.Show();
            Reservation_panel.Hide();
            Login_panel.Hide();
            Staff_password_change_panel.Hide();
        }//End of Cancel Button on Administration password change Panel.




//*************************************************************End of Administration change password panel*****************************//



        //==========================================================
        // 
        // File: Client.cs
        // This code used for showing the tabpages of Staff_tabcontrol through ToolStripMenuItem.
        //==========================================================

       private void addStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Staff_tabControl.SelectTab(0);
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Staff_tabControl.SelectTab(1);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Staff_tabControl.SelectTab(2);
        }

        private void viewReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Staff_tabControl.SelectTab(3);
        }

        private void updateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Staff_tabControl.SelectTab(4);
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login_panel.Show();
            Reservation_panel.Hide();
            Administration_panel.Hide();
            Administration_change_password_panel.Hide();
            Staff_password_change_panel.Hide();
            username_textBox.Clear();
            password_textBox.Clear();
            Staff_radioButton.Checked = false;
            Admin_radioButton.Checked = false;
            Error_label.Hide();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        //==========================================================
        // End of ToolStripMenuItem code used for Staff.
        //==========================================================


//***************************************************************************************************************************//






        //Select Format of Date from Staff_datetimePicker and set into Staff_check_joining_textBox on Add Staff tab on Administartion Panel.
        private void Staff_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {

            string joining_date = Staff_dateTimePicker.Value.ToString("yyyy-MM-dd");
            Staff_joining_textBox.Text = joining_date;

        }//End

        //==========================================================
        // 
        // File: Client.cs
        // This code used for Saving the Staff Data into add_staff_table on Add Staff tab on Administration Panel.
        //==========================================================

        private void Staff_save_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Staff_id_textBox.Text != "" && Staff_name_textBox.Text != "" && Staff_privileges_textBox.Text != "" && Staff_password_textBox.Text != "" && Staff_fname_textBox.Text != "" && Staff_cnic_textBox.Text != "" && Staff_male_radioButton.Checked == false || Staff_female_radioButton.Checked == false && Staff_phone_textBox.Text != "" && Staff_joining_textBox.Text != "" && Staff_address_textBox.Text != "" && Staff_salary_textBox.Text != "")
                {
                    List<string> ad = remoteObject.get_Staff_name();
                    string exist_name = ad.ElementAt(0);
                    string name = Staff_name_textBox.Text;
                    if (name != exist_name)
                    {


                        string Staff_id = Staff_id_textBox.Text;
                        string Staff_name = Staff_name_textBox.Text;
                        string Staff_password = Staff_password_textBox.Text;
                        string Staff_privileges = Staff_privileges_textBox.Text;
                        string Staff_fname = Staff_fname_textBox.Text;
                        string Staff_cnic = Staff_cnic_textBox.Text;

                        {
                            if (Staff_male_radioButton.Checked)
                            {
                                Staff_gender = "Male";
                            }
                            if (Staff_female_radioButton.Checked)
                            {
                                Staff_gender = "Female";
                            }

                        }

                        string Staff_phone = Staff_phone_textBox.Text;
                        string Staff_joining_date = Staff_joining_textBox.Text;
                        string Staff_address = Staff_address_textBox.Text;
                        string Staff_salary = Staff_salary_textBox.Text;
                        string result = remoteObject.Add_Staff(Staff_id, Staff_name, Staff_password, Staff_privileges, Staff_fname, Staff_cnic, Staff_gender, Staff_phone, Staff_joining_date, Staff_address, Staff_salary);
                        MessageBox.Show(result);

                        {
                            string report = remoteObject.Staff_to_login(Staff_id, Staff_name, Staff_password, Staff_privileges);
                            MessageBox.Show(report);
                        }


                        int s_id = remoteObject.Staff_id();///////////////////////Through  remoteObject  get id_NO for Add Staff//////////////////////// 
                        Staff_id_textBox.Text = s_id.ToString();///////////////Set id_NO in textbox///////////////////////////////////////////////////
                        Staff_name_textBox.Clear();
                        Staff_fname_textBox.Clear();
                        Staff_joining_textBox.Clear();
                        Staff_password_textBox.Clear();
                        Staff_phone_textBox.Clear();
                        Staff_salary_textBox.Clear();
                        Staff_address_textBox.Clear();
                        Staff_cnic_textBox.Clear();
                        Staff_male_radioButton.Checked = false;
                        Staff_female_radioButton.Checked = false;
                    }
                    else
                    {
                        MessageBox.Show("This Name Already exist Change Name");
                    }
                }
                else
                {
                    MessageBox.Show("First enter the data to All textboxes");
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }//End of Save Button on Add Reservation tab on Administration Panel.


  


        //Reset Button clear All the textBoxes uesd on Add Staff tab on Administration Panel.
        private void Staff_reset_button_Click(object sender, EventArgs e)
        {
            try
            {
                Staff_name_textBox.Clear();
                Staff_fname_textBox.Clear();
                Staff_joining_textBox.Clear();
                Staff_password_textBox.Clear();
                Staff_phone_textBox.Clear();
                Staff_salary_textBox.Clear();
                Staff_address_textBox.Clear();
                Staff_cnic_textBox.Clear();
                Staff_male_radioButton.Checked = false;
                Staff_female_radioButton.Checked = false;
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }//End of Reset Button on Add Staff tab on Administration Panel.

        //==========================================================
        // End 
        //==========================================================


//***************************************************************************************************************************//



        //==========================================================
        // 
        // File: Client.cs
        // This code used for Searching   Staff Data from  add_staff_table and show on DataGridView on Search tab on Administration Panel.
        //==========================================================

        //Staff Search ComboBox used to show Staff_search_textBox  on Search tab on Administration Panel.
        private void Staff_Search_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            Staff_search_textBox.Show();
        }//End

        //Search Button on Search tab on Administration Panel.
        private void staff_sreach_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Staff_Search_comboBox.Text != "")
                {
                    if (Staff_search_textBox.Text != "")
                    {
                        if (Staff_Search_comboBox.SelectedItem.ToString() == "Seach by Id")
                        {

                            String Search_id_number = Staff_search_textBox.Text;
                            DataSet ds = remoteObject.Staff_Search_by_id(Search_id_number);
                            Staff_search_dataGridView.DataSource = ds.Tables[0].DefaultView;
                        }


                        if (Staff_Search_comboBox.SelectedItem.ToString() == "Seach by Name")
                        {
                            String name = Staff_search_textBox.Text;
                            DataSet ds = remoteObject.Staff_Search_by_Name(name);
                            Staff_search_dataGridView.DataSource = ds.Tables[0].DefaultView;

                        }


                        if (Staff_Search_comboBox.SelectedItem.ToString() == "Seach by Cnic")
                        {

                            String Staff_cnic = Staff_search_textBox.Text;
                            DataSet sss = remoteObject.Staff_Search_by_cnic(Staff_cnic);
                            Staff_search_dataGridView.DataSource = sss.Tables[0].DefaultView;
                        }
                    }

                    else
                    {
                        MessageBox.Show(" put value to textfield");
                    }
                }

                else
                {
                    MessageBox.Show("first select one option from comobobox ");
                }
            }
            catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
          }//End of Search Button on Search tab on Administration Panel.


        //==========================================================
        // End 
        //==========================================================


//***************************************************************************************************************************//


        //==========================================================
        // 
        // File: Client.cs
        // This code used for Deleting Staff Data from  add_staff_table on Delete tab on Administration Panel.
        //==========================================================



        //Show Data Button When click on it.it show the data onto dataGridView on Delete tab on Administration Panel. 
        private void Staff_delete_show_data_button_Click(object sender, EventArgs e)
        {
            DataSet result = remoteObject.Staff_delete();
            Staff_delete_dataGridView.DataSource = result.Tables[0].DefaultView;

        }//End of Show Data Button on Delete tab on Administration Panel.


        //DataGridView when click on the cell content of dataGridView it set id of that row into textbox on Delete tab on Administartion Panel.
        private void Staff_delete_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = Staff_delete_dataGridView.Rows[e.RowIndex];
                    Staff_Delete_textBox.Text = row.Cells["id"].Value.ToString();
                }
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }//End of DataGrid on Delete tab on Administartion Panel.



        ////Staff Delete one Record Button on Delete tab on Administration Panel.
        private void Staff_Delete_one_Record_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Staff_Delete_textBox.Text != "")
                {
                    string Delete_id = Staff_Delete_textBox.Text;
                    string result = remoteObject.Staff_Delete_by_id(Delete_id);
                    MessageBox.Show(result);
                    string login_result = remoteObject.Login_Staff_Delete_by_id(Delete_id);
                    MessageBox.Show(login_result);
                    DataSet ds = remoteObject.Staff_delete();
                    Staff_delete_dataGridView.DataSource = ds.Tables[0].DefaultView;
                    
                }

                else
                {
                    MessageBox.Show("first click data on girdview then u can delete data");
                }
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

            
        }//// End of Delete one Record Button on Delete tab on Administration Panel

        
        //Staff Delete Whole Record Button on Delete tab on Administration Panel.
        private void Staff_Delete_whole_record_button_Click(object sender, EventArgs e)
        {
            try
            {
                string result = remoteObject.Staff_All_Delete();
                MessageBox.Show(result);
                string privilege = Staff_privileges_textBox.Text;
                string Login_result = remoteObject.Login_Staff_All_Delete(privilege);
                MessageBox.Show(Login_result);
                int s_id = remoteObject.Staff_id();///////////////////////Through  remoteObject  get id_NO for Add Staff//////////////////////// 
                Staff_id_textBox.Text = s_id.ToString();///////////////Set id_NO in textbox///////////////////////////////////////////////////
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }//End of Staff Delete Whole Record Button on Delete tab on Administration Panel.



        //==========================================================
        // End 
        //==========================================================


//***************************************************************************************************************************// 


        //==========================================================
        // 
        // File: Client.cs
        //This code used for Showing Reports.
        //Which are four Daily Report,Weekly Report ,Monthly and Full Report.
        //Through  method Calling  from remoteobject.
        //==========================================================



        //Daily Report Button on View Report tab on Administration Panel.
        private void Staff_Daily_report_button_Click(object sender, EventArgs e)
        {
            
            string todaydate = DateTime.UtcNow.ToString("yyyy-MM-dd");
           
            DataSet res = remoteObject.Staff_Daily_report(todaydate);
            
            Staff_view_report_dataGridView.DataSource = res.Tables[0].DefaultView;
        }

        //Weekly Report Button on View Report tab on Administration Panel.
        private void Staff_Weekly_report_button_Click(object sender, EventArgs e)
        {
            DataSet res = remoteObject.Staff_Wee_report();
            
            Staff_view_report_dataGridView.DataSource = res.Tables[0].DefaultView;
        }

        //Monthly Report Button on View Report tab on Administration Panel.
        private void Staff_Monthly_report_button_Click(object sender, EventArgs e)
        {
            DataSet res = remoteObject.Staff_Monthly_report();
            
            Staff_view_report_dataGridView.DataSource = res.Tables[0].DefaultView;
        }

        //Full Report Button on View Report tab on Administration Panel.
        private void Staff_Full_report_button_Click(object sender, EventArgs e)
        {
            DataSet result = remoteObject.Staff_Full_report();
            Staff_view_report_dataGridView.DataSource = result.Tables[0].DefaultView;
        }



        //Staff_Print_Report_button will print the report which show in Staff_View_report_dataGridView on View Report tab on Administartion_panel.
        private void Staff_Print_Report_button_Click(object sender, EventArgs e)
        {
            try
            {
                Document doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 42, 35);
                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("C://PDFs//Test.pdf", FileMode.Create));
                doc.Open();

                PdfPTable table = new PdfPTable(Staff_view_report_dataGridView.Columns.Count);
                table.DefaultCell.Padding = 3;
                table.WidthPercentage = 100;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.DefaultCell.BorderWidth = 2;


                for (int j = 0; j < Staff_view_report_dataGridView.Columns.Count; j++)
                {
                    
                    DataGridViewColumn column = Staff_view_report_dataGridView.Columns[j];
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(150, 150, 150);
                    table.AddCell(cell);
                }
                table.HeaderRows = 1;

                for (int i = 0; i < Staff_view_report_dataGridView.Rows.Count; i++)
                {
                    for (int k = 0; k < Staff_view_report_dataGridView.Columns.Count; k++)
                    {
                        if (Staff_view_report_dataGridView[k, i].Value != null)
                        {
                            table.AddCell(new Phrase(Staff_view_report_dataGridView[k, i].Value.ToString()));

                        }
                    }
                }

                doc.Add(table);
                table.HeaderRows = 4;

                doc.Close();
                System.Diagnostics.Process.Start(@"C:\\PDFs\\Test.pdf");
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }//End of Staff Print Report Button.

        //==========================================================
        // End 
        //==========================================================


//***************************************************************************************************************************// 

        //==========================================================
        // 
        // File: Client.cs
        // This code used for Updating  Staff Data from  add_staff_table on Delete tab on Administration Panel.
        //By using show Data Button which show Data in DataGridView and click on cell Content of DataGridView then  set Data to all Text Boxes and update through Save Button. 
        //==========================================================


        //Staff_update_joining_dateTimePicker used to set date into Staff_update_joiningdate_textBox on update tab on Administration Panel.
        private void Staff_update_joining_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            string date = Staff_update_joining_dateTimePicker.Value.ToString("yyyy-MM-dd");
            Staff_update_joiningdate_textBox.Text = date;
        }//End of Staff_update_joining_dateTimePicker.


        //Staff_update_show_data button Enable All the textBoxes and Show Data on DataGridView on update tab on Administration Panel.
        private void Staff_update_show_data_gridview_button_Click(object sender, EventArgs e)
        {
            try
            {
                Staff_update_name_textBox.Enabled = true;
                Staff_update_password_textBox.Enabled = true;
                Staff_update_cnic_textBox.Enabled = true;
                Staff_update_fname_textBox.Enabled = true;
                Staff_update_phone_textBox.Enabled = true;
                Staff_update_address_textBox.Enabled = true;
                Staff_update_salary_textBox.Enabled = true;
                DataSet result = remoteObject.Staff_update();
                Staff_update_dataGridView.DataSource = result.Tables[0].DefaultView;
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }//End of Staff_update_show_data button.

        //Staff_update_dataGridView when click on cell content of it. Set data to All the TextBoxes on update tab on Administration Panel. 
        private void Staff_update_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = Staff_update_dataGridView.Rows[e.RowIndex];
                    Staff_update_id_textBox.Text = row.Cells["id"].Value.ToString();
                    Staff_update_name_textBox.Text = row.Cells["User_name"].Value.ToString();
                    Staff_update_password_textBox.Text = row.Cells["password"].Value.ToString();
                    Staff_update_privileges_textBox.Text = row.Cells["privileges"].Value.ToString();
                    Staff_update_cnic_textBox.Text = row.Cells["cnic"].Value.ToString();
                    Staff_update_fname_textBox.Text = row.Cells["father_name"].Value.ToString();
                    if (row.Cells["gender"].Value.ToString() == "Male")
                    {
                        Staff_update_male_radioButton.Checked = true;
                    }
                    if (row.Cells["gender"].Value.ToString() == "Female")
                    {
                        Staff_update_female_radioButton.Checked = true;
                    }
                    string date = row.Cells["joining_date"].Value.ToString();
                    Staff_update_joiningdate_textBox.Text = DateTime.Parse(date).ToString("yyyy-MM-dd");
                    Staff_update_phone_textBox.Text = row.Cells["phone"].Value.ToString();
                    Staff_update_address_textBox.Text = row.Cells["Address"].Value.ToString();
                    Staff_update_salary_textBox.Text = row.Cells["salary"].Value.ToString();

                }
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }//End of Staff_update_dataGridView.

        //Staff_update_save_button when click on it.it Saves the updated data into database on update tab on Adminstration Panel.
        private void Staff_update_save_button_Click(object sender, EventArgs e)
        {

            try
            {
                if (Staff_update_id_textBox.Text != "" && Staff_update_name_textBox.Text != "" && Staff_update_privileges_textBox.Text != "" && Staff_update_password_textBox.Text != "" && Staff_update_fname_textBox.Text != "" && Staff_update_cnic_textBox.Text != "" && Staff_update_male_radioButton.Checked == false || Staff_update_female_radioButton.Checked == false && Staff_update_phone_textBox.Text != "" && Staff_update_joiningdate_textBox.Text != "" && Staff_update_address_textBox.Text != "" && Staff_update_salary_textBox.Text != "")
                {
                    string updated_id = Staff_update_id_textBox.Text;
                    string updated_name = Staff_update_name_textBox.Text;
                    string updated_password = Staff_update_password_textBox.Text;
                    string updated_privileges = Staff_update_privileges_textBox.Text;
                    string updated_cnic = Staff_update_cnic_textBox.Text;
                    string updated_fname = Staff_update_fname_textBox.Text;
                    string updated_joining = Staff_update_joiningdate_textBox.Text;
                    string updated_phone = Staff_update_phone_textBox.Text;
                    string updated_address = Staff_update_address_textBox.Text;
                    string updated_salary = Staff_update_salary_textBox.Text;

                    if (Staff_update_male_radioButton.Checked)
                    {
                        updated_gender = "Male";
                    }
                    if (Staff_update_female_radioButton.Checked)
                    {
                        updated_gender = "Female";
                    }
                    string result = remoteObject.Staff_save_updated(updated_id, updated_name, updated_fname, updated_cnic, updated_password, updated_privileges, updated_phone, updated_joining, updated_address, updated_salary, updated_gender);
                    MessageBox.Show(result);

                    string login_result = remoteObject.Login_Staff_update(updated_id, updated_name, updated_password);
                    MessageBox.Show(login_result);

                    DataSet ds = remoteObject.Staff_update();
                    Staff_update_dataGridView.DataSource = ds.Tables[0].DefaultView;
                }
                else
                {
                    MessageBox.Show("First enter data to All textboxes");
                }
            }catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }// End of Staff_update_save_button.


        //==========================================================
        // End 
        //==========================================================

//***************************************************************************************************************************// 


//********************************************************End of Administration************************************************************************************************//

      }
}
