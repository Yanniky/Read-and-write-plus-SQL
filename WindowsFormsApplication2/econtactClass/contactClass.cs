using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.econtactClasses
{
    class contactClass
    {
        //getter setter properties
        //acts as data carrier in the properties
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;



        // select data from database
        public DataTable Select() // method to select from database
        {
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {

                // slq queries
                string sql = "SELECT * FROM econtact";
                //creating cmd uaing sql and conn

                SqlCommand cmd = new SqlCommand(sql, conn);
                // sql  DataAdapter
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt); // database object
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();

            }
            return dt; //return value from database
        }
        // insert in database  
        public bool Insert(contactClass c)
        {
            // creating a default return type if value is false
            bool IsSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "INSERT INTO econtact (FirstName, LastName, ContactNumber, Address, Gender) VALUES(@FirstName, @LastName, @ContactNumber, @Address, @Gender)";
                // create sql command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                // create parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNumber", c.ContactNumber);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //open connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // if the query is successfull then the value of rows will be greater then zero.
                if (rows > 0)
                {
                    IsSuccess = true;

                }
                else
                {
                    IsSuccess = false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }

            return IsSuccess;
        }
        // Method to update data in database from our application
        public bool Update(contactClass c)
        {
            // create a default type and set it default value to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //SQL to update data in our database
                string sql = "UPDATE econtact SET  FirstName=@FirstName, LastName=@LastName, ContactNumber=@ContactNumber, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";
                // create sql command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //  create parameters to add values
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNumber", c.ContactNumber);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // if the query runs successfully then the value of rows will be greater than zero
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        public bool Delete(contactClass c)
        {
            // create default return value and set it value to false
            bool isSuccess = false;
            // create sql connection
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                // sql to delete data from table
                string sql = "DELETE FROM econtact WHERE ContactID=@ContactID";
                // create sql command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // if query run successfully then the value of rows is greater than zero
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        
    }

}
