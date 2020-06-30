using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;


public class product : MarshalByRefObject
{
    
    // Create connection with Database

  
    
    static string Mysql = "server=localhost;Database=hotel_reservation;Uid=root;Pwd=302420";
    MySqlConnection connection = new MySqlConnection(Mysql);
//*************************************************Start of loign Panel functions**************************************************************************************************************//
    
    //Login function ////////////////////////////////////////////////////////////
   //Take two parameters apply query on them and return a boolean. 
    
    public Boolean Login(string username,string password,string privilege)
    {
          try
        {
            connection.Open();
            
            string strQuery= "Select * From login_table WHERE password='"+password+"' ";
            MySqlCommand cmd = new MySqlCommand(strQuery,connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            
            while(dr.Read())
           {
                if(username==(dr.GetString("user_name")) && privilege==(dr.GetString("privlege")))
                {
                   // adp.Close();
                    connection.Close();
                    return true;
                }
           }
           
        }

        catch (SystemException)
        {

            connection.Close();
            return false;
        }


        connection.Close();
        return false;
    
    }
    //End of Login Function.
//*************************************************End of loign Panel functions**************************************************************************************************************//

//*************************************************Start of Reservation Panel functions**************************************************************************************************************//
    
    //Auto ID set function//////////////////////////////////////////////////////////////////
    // Run a query and return int .
    public int id_set()
    {
        
        try
        {
            int id=0;
            connection.Open();
            string query = "Select * From  reservation_table";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                 id=dr.GetInt32("id_number");
            }
            dr.Close();
            connection.Close();
            return id+1;
        }
        catch(SystemException)
        {
            connection.Close();
            return 0;
        }
        
       
    }//End of Auto ID Set Function.




    

    //Add Reservation function/////////////////////////////////////////////////////////
    //Take All the Data from Add Reservation tab of Reservation Panel and return String function Calling From GUI. 
    public string Add_Reservation(string id,string room_type,string room,string name,string father_name,string cnic,string gender,string phone,string number_of_days,string start_date,string end_date,string address,string rate)
    {
        try
        {
         // DateTime startdate = Convert.ToDateTime(start_date);
           //DateTime enddate = Convert.ToDateTime(end_date);
           //MessageBox.Show(startdate + "   " + enddate);
            connection.Open();
            //string CmdString = "INSERT INTO user(User_id,User_name,User_nic,User_reservation)VALUES(6,'ttt','88888','hu')";
            string strQuery = "INSERT INTO reservation_table(id_number,room_type,room_number,name,father_name,cnic,gender,phone,number_of_days,check_in_date,check_out_date,address,rate) VALUES('" + id + "','" + room_type + "','" + room + "','" + name + "','" + father_name + "','" + cnic + "','" + gender + "','" + phone + "','" + number_of_days + "','"+start_date+"','"+end_date+"','" + address + "','" + rate + "') ";
            //string strQuery = "insert into hotel_reservation.reservation_table(id_number,room_type,room_number,name,father_name,cnic,gender,phone,number_of_days,check_in_date,check_out_date,address,rate) values(1,'njn','njn','njn','njn','njn','njn','njn','njn','2015-12-01','2015-04-01','njn','njn')";
            MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();
            return "Data Sucessfully Store";
        }
        catch(SystemException obj)
        {
            connection.Close();
            return obj.ToString();
        }

        

    }//End of Add Reservation Function.


    //Search through id function//////////////////////////////////////////////////////////////////////
    //Take id_number Parameter from Search tab of Reservation Panel and return DataSet function Calling From GUI.
    public DataSet  Search_by_id(string id_number)
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select * From reservation_table where id_number='"+id_number+"' ";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }
    

        
    
    }//End of Search through id function.

    //Search through name function//////////////////////////////////////////////////////////////////////
    //Take name Parameter from Search tab of Reservation Panel and return DataSet function Calling From GUI.
    public DataSet Search_by_Name(string name)
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select * From reservation_table where name='"+name+"' ";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }


       
    }//End of Search through name function.


    //Search through room function//////////////////////////////////////////////////////////////////////
    //Take Room Number Parameter from Search tab of Reservation Panel and return DataSet function Calling From GUI.
    public DataSet Search_by_Room(string room_number)
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select * From reservation_table where room_number='"+room_number+"' ";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }

        
    }//End of Search through room function.





    //Add_Room function//////////////////////////////////////////////////////////////////////
    //Take id Parameter from Delete tab of Reservation Panel and return String function Calling From GUI.  
    public List<string> Add_Room(string id)
    {
        try
        {
            List<string> List = new List<string>();
            connection.Open();

            string strQuery = "Select * From reservation_table where id_number='"+id+"' ";
            MySqlCommand cmd = new MySqlCommand(strQuery, connection);
            MySqlDataReader adp = cmd.ExecuteReader();

            while (adp.Read())
            {
                List.Add(adp.GetString("room_number"));

            }
            connection.Close();
            return List;



        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }




    }//End of Add_Room function.

    
    //Delete through id function//////////////////////////////////////////////////////////////////////
    //Take Delete_id Parameter from Delete tab of Reservation Panel and return String function Calling From GUI.  
    public string Delete_by_id(string Delete_id)
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE  From reservation_table where id_number='" + Delete_id + "' "; 
           // MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();
            
            return "Data Deleted Sucessfully";
            
            
            
        }
        catch (SystemException obj)
        {
            connection.Close();
            return obj.ToString();
        }

        

        
    }//End of Delete through id function.



    //All Delete function//////////////////////////////////////////////////////////////////////
    //Take no Parameter from Delete tab of Reservation Panel and return String function Calling From GUI.
    public string All_Delete()
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE From reservation_table";
            // MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();

            return "Data Deleted Sucessfully";



        }
        catch (SystemException obj)
        {
            connection.Close();
            return obj.ToString();
        }

        

    }//End of All Delete function.


    //Delete function//////////////////////////////////////////////////////////////////////
    //Take no Parameter from Delete tab of Reservation Panel and return DataSet function Calling From GUI.
    //it return the DataSet After Delete Any Reservation For  verifying Delete Reservation into DataGridView on Gui side.
    public DataSet Delete()
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select *  From reservation_table";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }

    }//End of Delete function.

    //Daily Report function//////////////////////////////////////////////////////////////////////
    //Take one Parameter from  and return DataSet function Calling From GUI.
    public DataSet Daily_report(string date)
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select *  From reservation_table where check_in_date='" + date + "' ";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }
        
    }//End of Daily Report function.



    //Weekly Report function//////////////////////////////////////////////////////////////////////
    //Take no Parameter from  and return DataSet function Calling From GUI.
    public DataSet Wee_report()
    {
        try
        {

            string today = DateTime.UtcNow.ToString("yyyy-MM-dd");

            DateTime week = DateTime.UtcNow.Date;

            string weekly = week.AddDays(-7).ToString("yyyy-MM-dd");
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select * From reservation_table WHERE  check_in_date BETWEEN '" +weekly+ "' AND '" +today+ "' ";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }

    }//End of Weekly Report function.


    //Monthly Report function//////////////////////////////////////////////////////////////////////
    //Take one Parameter from  and return DataSet function Calling From GUI.
    public DataSet Monthly_report()
    {
        try
        {

            string today = DateTime.UtcNow.ToString("yyyy-MM-dd");

            DateTime Month = DateTime.UtcNow.Date;

            string Monthly = Month.AddDays(-30).ToString("yyyy-MM-dd");
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select * From reservation_table WHERE  check_in_date BETWEEN '" + Monthly + "' AND '" + today + "' ";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }

    }//End of Weekly Report function.

    //Full Report function//////////////////////////////////////////////////////////////////////
    //Take one Parameter from  and return DataSet function Calling From GUI.
    public DataSet Full_report()
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select *  From reservation_table";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }

    }//End of Full Report function.

       
    //Update Function////////////////////////////////////////////////////////
    //Take No Parameter and return Dataset to the function calling From GUI side that take dataset and Show the Data into DataGridView. 
    public DataSet update()
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select *  From reservation_table";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException )
        {
            connection.Close();
            return null;
        }

    }//End of Update Function.

    


    //Update Reservation Function////////////////////////////////////////////////////////
    //Take All Parameter from update tab of Reservation Panel return String to the function calling From GUI side.
    public string Update_Reservation(string id, string room_type, string room, string name, string father_name, string cnic, string gender, string phone, string number_of_days, string start_date, string end_date, string address, string rate)
    {
        try
        {
            connection.Open();
            //string CmdString = "INSERT INTO user(User_id,User_name,User_nic,User_reservation)VALUES(6,'ttt','88888','hu')";
            string strQuery = "UPDATE reservation_table SET room_type='" + room_type + "', room_number='" + room + "', name='" + name + "', father_name='" + father_name + "', cnic='" + cnic + "', gender='" + gender + "', phone='" + phone + "', number_of_days='" + number_of_days + "', check_in_date='" + start_date + "', check_out_date='" + end_date + "', address='" + address + "', rate='" + rate+ "' where id_Number='"+id+"' ";
            MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();
            return "Data Sucessfully Update";
        }
        catch (SystemException obj)
        {
            connection.Close();
            return obj.ToString();
        }



    }//End of Update Reservation Function.


//**********************************************************************End of Reservation Panel Functions**************************************************************************//


//**********************************************************************Start of Administration Panel Functions**************************************************************************//
    
    //Auto ID set function for Staff in Administration//////////////////////////////////////////////////////////////////
    // Run a query and return int .
    public int Staff_id()
    {
        try
        {
            int Staff_id = 0;
            connection.Open();
            string query = "Select * From  add_staff_table";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Staff_id = dr.GetInt32("id");
            }
            dr.Close();
            connection.Close();
            return Staff_id + 1;

        }
        catch (SystemException)
        {
            connection.Close();
            return 0;
        }


    }//End of Auto ID set function. 


    //get Staff_name function/////////////////////////////////////////////////////////
    //Run Query and return List<string>.
    public List<string> get_Staff_name()
    {
        try
        {
            List<string> List = new List<string>();
            connection.Open();

            string strQuery = "Select * From add_staff_table";
            MySqlCommand cmd = new MySqlCommand(strQuery, connection);
            MySqlDataReader adp = cmd.ExecuteReader();

            while (adp.Read())
            {
                List.Add(adp.GetString("User_name"));
               
            }
            connection.Close();
            return List;

        }

        catch (SystemException)
        {

            connection.Close();
            return null;
        }

}//End of get Staff_name function. 



    //Add Staff function/////////////////////////////////////////////////////////
    ////Take All the Data from Add Staff tab of Administration Panel and return String function Calling From GUI.
    public string Add_Staff(string staff_id, string staff_name, string staff_password, string staff_privileges, string staff_father_name, string staff_cnic, string staff_gender, string staff_phone, string joining_date, string staff_address, string staff_salary)
    {
        try
        {
            connection.Open();
            //string CmdString = "INSERT INTO user(User_id,User_name,User_nic,User_reservation)VALUES(6,'ttt','88888','hu')";
            string strQuery = "INSERT INTO add_staff_table(id,User_name,password,privileges,father_name,cnic,gender,phone,joining_date,Address,salary) VALUES('" + staff_id + "','" + staff_name + "','" + staff_password + "','" + staff_privileges + "','" + staff_father_name + "','" + staff_cnic + "','" + staff_gender + "','" + staff_phone + "','" + joining_date + "','" + staff_address + "','" + staff_salary + "') ";
            MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();
            return "Data Sucessfully Store";
        }
        catch (SystemException obj)
        {
            connection.Close();
            return obj.ToString();
        }



    }//End of Add Staff function. 


    //Staff to login function/////////////////////////////////////////////////////////
    ////Take Four Parameters  from Add Staff tab of Administration Panel and return String function Calling From GUI.
    public string Staff_to_login(string staff_id, string staff_name, string staff_password, string staff_privileges)
    {
        try
        {
            connection.Open();
            //string CmdString = "INSERT INTO user(User_id,User_name,User_nic,User_reservation)VALUES(6,'ttt','88888','hu')";
            string strQuery = "INSERT INTO login_table(idLogin_table,user_name,password,privlege) VALUES('" + staff_id + "','" + staff_name + "','" + staff_password + "','" + staff_privileges + "') ";
            MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();
            return "Data Sucessfully Store in Login table";
        }
        catch (SystemException obj)
        {
            connection.Close();
            return obj.ToString();
        }



    }//End of Staff to login function.



    //Staff Search through id function//////////////////////////////////////////////////////////////////////
    //Take one Parameter run query and return Dataset to the function Calling From Gui of search tab of Administration Panel.
    public DataSet Staff_Search_by_id(string id_number)
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select * From add_staff_table  where id='" + id_number + "' ";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }




    }//End of Staff Search through id function

    //Staff Search through name function//////////////////////////////////////////////////////////////////////
    //Take one Parameter run query and return Dataset to the function Calling From Gui of search tab of Administration Panel.
    public DataSet Staff_Search_by_Name(string name)
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select * From add_staff_table where User_name='" + name + "' ";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }



    }//End of Staff Search through name function.


    //Staff Search through Cnic function//////////////////////////////////////////////////////////////////////
    //Take one Parameter run query and return Dataset to the function Calling From Gui of search tab of Administration Panel.
    public DataSet Staff_Search_by_cnic(string cnic)
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select * From add_staff_table where cnic='" + cnic + "' ";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }


    }//End of Staff Search Through Cnic Fuction.


    //Delete function///////////////////////////////////////////////////////////////////////////////
    //Used to return a DataSet to function calling from Gui side of delete tab on Administration Panel and that ds used on Gui side for Showing data onto DataGridView.

    public DataSet Staff_delete()
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select *  From add_staff_table";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }

    }//End of Delete Function.

    
    //Staff Delete through id function//////////////////////////////////////////////////////////////////////
    //Take one Parameter run query and return string to the function Calling From Gui of Delete tab of Administration Panel.
    public string Staff_Delete_by_id(string Delete_id)
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE  From add_staff_table where id='" + Delete_id + "' ";
            // MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();

            return "Data Deleted Sucessfully";



        }
        catch (SystemException obj)
        {
            connection.Close();
            return obj.ToString();
        }




    }//End of Staff Delete through id function.

    
    //Staff All Delete Function/////////////////////////////////////////////////////////////////////////////////////////////
    //Take no Parameter run query and return string to the function Calling From Gui of Delete tab of Administration Panel.

    public string Staff_All_Delete()
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE From add_staff_table";
            // MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();

            return "Data Deleted Sucessfully";



        }
        catch (SystemException obj)
        {
            connection.Close();
            return obj.ToString();
        }



    }//End of Staff All Delete function.


    //Login Staff All Delete function////////////////////////////////////////////////////////////////////////////////////
    //Take one Parameter run query and return string to the function Calling From Gui of Delete tab of Administration Panel.

    public string Login_Staff_All_Delete(string privilege)
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE From login_table where privlege='"+privilege+"'";
            // MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();

            return "Data Deleted Sucessfully";



        }
        catch (SystemException obj)
        {
            connection.Close();
            return obj.ToString();
        }



    }//End of Login Staff All Delete function.


    //Staff Delete from Login_table through id function//////////////////////////////////////////////////////////////////////
    //Take one Parameter run query and return string to the function Calling From Gui of Delete tab of Administration Panel.
    public string Login_Staff_Delete_by_id(string Delete_id)
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE  From login_table where idLogin_table='" + Delete_id + "' ";
            // MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();

            return "Data Deleted Sucessfully";



        }
        catch (SystemException)
        {
            connection.Close();
            return "error in deletion";
        }




    }//End of Staff Delete from login table through id fuction.




    


  //Staff Daily Report function///////////////////////////////////////////////////////////////////////////////////
  //Take one Parameter and return dataset to function calling which used to show data in dataGridView on View Report tab of Administration Panel.

     public DataSet Staff_Daily_report(string date)
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select *  From add_staff_table where joining_date='" + date + "' ";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }

    }//End of Staff Daily Report function.


     //Staff Weekly Report function///////////////////////////////////////////////////////////////////////////////////
     //Take one Parameter and return dataset to function calling which used to show data in dataGridView on View Report tab of Administration Panel.
    public DataSet Staff_Wee_report()
    {
        try
        {

            string today = DateTime.UtcNow.ToString("yyyy-MM-dd");

            DateTime week = DateTime.UtcNow.Date;

            string weekly = week.AddDays(-7).ToString("yyyy-MM-dd");
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select * From add_staff_table WHERE  joining_date BETWEEN '" + weekly + "' AND '" + today + "' ";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }

    }//End of Staff Weekly Report function.


    //Staff Monthly Report function///////////////////////////////////////////////////////////////////////////////////
    //Take one Parameter and return dataset to function calling which used to show data in dataGridView on View Report tab of Administration Panel.
    public DataSet Staff_Monthly_report()
    {
        try
        {

            string today = DateTime.UtcNow.ToString("yyyy-MM-dd");

            DateTime Month = DateTime.UtcNow.Date;

            string Monthly = Month.AddDays(-30).ToString("yyyy-MM-dd");
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select * From add_staff_table WHERE  joining_date BETWEEN '" + Monthly + "' AND '" + today + "' ";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }

    }//End of Staff Monthly Report function.



    //Staff Full Report function///////////////////////////////////////////////////////////////////////////////////
    //Take one Parameter and return dataset to function calling which used to show data in dataGridView on View Report tab of Administration Panel.
    public DataSet Staff_Full_report()
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select *  From add_staff_table";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }

    }//End of Staff Full Report function.


    //Staff update function///////////////////////////////////////////////////////////////////////////////////////////////
    //Used to return a dataset to the function calling on update tab of Administration Panel that show the whole data on DataGridView.

    public DataSet Staff_update()
    {
        try
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select *  From add_staff_table";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
        }
        catch (SystemException)
        {
            connection.Close();
            return null;
        }

    }//End of Staff update function.


    //Staff Save update function///////////////////////////////////////////////////////////////////////////////////////////////////
    //Take Parameters from function calling of Gui side and return string. 

    public string Staff_save_updated(string id, string name, string fname, string cnic, string password, string prvilige, string phone, string Joining, string Address, string salary, string gender)
    {
        try
        {
            connection.Open();
            //string CmdString = "INSERT INTO user(User_id,User_name,User_nic,User_reservation)VALUES(6,'ttt','88888','hu')";
            string strQuery = "UPDATE add_staff_table SET User_name='" + name + "', password='" + password + "' , privileges='" + prvilige + "', father_name='" + fname + "', cnic='" + cnic + "', phone='" + phone + "' , joining_date='" + Joining + "' , Address='" + Address + "' , salary='" + salary + "', gender='" + gender + "'  where id='" + id + "'  ";
            MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();
            return "Data Sucessfully Edit";
        }
        catch (SystemException obj)
        {
            connection.Close();
            return obj.ToString();
        }



    }//End of Staff Save update function.


    //login Staff update function/////////////////////////////////////////////////////////////////////////////////////////////////////
    //Take Parameters from function calling of Gui side and return string.

    public string Login_Staff_update(string id,string name, string password)
    {
        try
        {
            connection.Open();
            //string CmdString = "INSERT INTO user(User_id,User_name,User_nic,User_reservation)VALUES(6,'ttt','88888','hu')";
            string strQuery = "UPDATE login_table SET password='" + password + "', user_name='" + name + "'  where idLogin_table='"+id+"' ";
            MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();
            return "Data Sucessfully Edit to login table";
        }
        catch (SystemException obj)
        {
            connection.Close();
            return obj.ToString();
        }



    }//End of login staff update function.

//***********************************************************************End of Administration Panel Functions******************************************************************//

//***********************************************************************Start of Administration Change Password Panel Functions************************************************//

    //Admin change Password function///////////////////////////////////////////////////////
    //Take two Parameters and return a string to gui side from where the function calling on  Administration Change Password Panel.
    public string Admin_change_password(string user_name,string new_password)
    {
        try
        {
            connection.Open();
            //string CmdString = "INSERT INTO user(User_id,User_name,User_nic,User_reservation)VALUES(6,'ttt','88888','hu')";
            string strQuery = "UPDATE login_table SET password='" + new_password + "'  where user_name='" +user_name+ "' ";
            MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();
            return "Password changed Sucessfully";
        }
        catch (SystemException obj)
        {
            connection.Close();
            return obj.ToString();
        }



    }//End of Admin Change Password.

//****************************************************************************End of Administration Change Password Panel*******************************************************//

//*****************************************************************************Start of Staff Change Password Panel************************************************************//

    //Staff change Password function///////////////////////////////////////////////////////
    //Take two Parameters and return a string to gui side from where the function calling on  Staff Change Password Panel.
    public string Staff_change_password(string user_name, string new_password)
    {
        try
        {
            connection.Open();
            //string CmdString = "INSERT INTO user(User_id,User_name,User_nic,User_reservation)VALUES(6,'ttt','88888','hu')";
            string strQuery = "UPDATE add_staff_table SET password='" + new_password + "'  where User_name='" + user_name + "' ";
            MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();
            return "Password changed Sucessfully";
        }
        catch (SystemException obj)
        {
            connection.Close();
            return obj.ToString();
        }



    }//End of Staff Change Password function.


    //Login Change Password function/////////////////////////////////////////////////////////
    ////Take two Parameters and return a string to gui side from where the function calling on  Staff Change Password Panel.
    public string login_change_password(string user_name, string new_password)
    {
        try
        {
            connection.Open();
            //string CmdString = "INSERT INTO user(User_id,User_name,User_nic,User_reservation)VALUES(6,'ttt','88888','hu')";
            string strQuery = "UPDATE login_table SET password='" + new_password + "'  where user_name='" + user_name + "' ";
            MySqlCommand cmd = new MySqlCommand(strQuery, connection);

            cmd.ExecuteNonQuery();
            connection.Close();
            return "Password changed Sucessfully";
        }
        catch (SystemException obj)
        {
            connection.Close();
            return obj.ToString();
        }



    }//End of Login Change Password function.

//*****************************************************************************End of Staff Change Password Panel************************************************************//
    //Room Reserved Function///////////////////////////////////////////////////////////////////
    //Take no Parameters and return a List<string> from where the function calling on Gui side of Add Reservation tab of Reservation Panel. 
    public List<string> Reserved_Room()
    {
        try
        {
            List<string> Reserved_Room = new List<string>();
            connection.Open();

            string strQuery = "Select * From reservation_table ";
            MySqlCommand cmd = new MySqlCommand(strQuery, connection);
            MySqlDataReader adp = cmd.ExecuteReader();

            while (adp.Read())
            {
                Reserved_Room.Add(adp.GetString("room_number"));
            }
            connection.Close();
            return Reserved_Room;

        }

        catch (SystemException)
        {

            connection.Close();
            return null;
        }

    }//End of Room Reserved Function.


}

    



   
   
   

