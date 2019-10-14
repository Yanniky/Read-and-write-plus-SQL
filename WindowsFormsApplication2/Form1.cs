using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.econtactClasses;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass();
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // get the contact id from the app
            c.ContactID = Convert.ToInt32(txtContactID.Text);
            bool success = c.Delete(c);
            if (success==true)
            {
                MessageBox.Show("Contact successfully delete.");
                DataTable dt = c.Select();
                dvgLoader.DataSource = dt;
                this.clear();
            }
            else
            {
                MessageBox.Show(" Failed to delete Contact.");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // get value from input field
            c.FirstName = txtFirstName.Text;
            c.LastName = txtLastName.Text;
            c.ContactNumber = txtContactNumber.Text;
            c.Address = txtAddress.Text;
            c.Gender = cmbGender.SelectedItem.ToString();

            // insert in data

            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("New Contact successfully Inserted");
                clear();
            }
            else
            {
                MessageBox.Show("Desole something went wrong");
            }
            // load data on grid view
            DataTable dt = c.Select();
            dvgLoader.DataSource = dt;
        }

    
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dvgLoader.DataSource = dt;
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void clear()
        {

           txtContactID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtContactNumber.Text = "";
            cmbGender.Text = "";
        }
       
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //once the data has been edited then same 
            // get the data from gridview return it into the textboxes respectively, then save it in the db 
            c.ContactID = int.Parse(txtContactID.Text);
            c.FirstName = txtFirstName.Text;
            c.LastName = txtLastName.Text;
            c.ContactNumber = txtContactNumber.Text;
            c.Address = txtAddress.Text;
            c.Gender = cmbGender.Text;
            // update db from from grid
            bool success = c.Update(c);
            if (success==true)
            {
                MessageBox.Show("contact has been  successfull updated.");
                DataTable dt = c.Select();
                dvgLoader.DataSource = dt;
                
            }
            else
            {
                MessageBox.Show("Mr Mountou,  you're a cunt "); 
            }
        }

        private void dvgLoader_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // get data from grid view and load it into the txtboxes respectively
            //identify the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            txtContactID.Text = dvgLoader.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dvgLoader.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dvgLoader.Rows[rowIndex].Cells[2].Value.ToString();
            txtContactNumber.Text = dvgLoader.Rows[rowIndex].Cells[3].Value.ToString();
            txtAddress.Text = dvgLoader.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dvgLoader.Rows[rowIndex].Cells[5].Value.ToString();

        }

        private void dvgLoader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstring);

            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM econtact WHERE FirstName LIKE '%"+keyword+ "%' OR LastName LIKE '%"+keyword+"%' OR Address LIKE '%"+keyword +"%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dvgLoader.DataSource = dt;
        }
    }
}
