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



namespace MySqlCRUD
{
    public partial class Form1 : Form
    {
        string connectionString = @"Server=localhost;Database=userdb;Uid=root;Pwd=Kushya123*;";
        int ID = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GridFill();
        }



        private void button3_Click(object sender, EventArgs e)
        {

        }
        void GridFill()
        {

            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("userViewAll", mysqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dtbluser_info = new DataTable();
                sqlDa.Fill(dtbluser_info);
                dgvuser_info.DataSource = dtbluser_info;
                dgvuser_info.Columns[0].Visible = false;



            }
        }







        private void btnSave_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
        {
            mysqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("userAddorEdit",mysqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_ID", ID);
                mySqlCmd.Parameters.AddWithValue("_Name", txtName.Text.Trim());
                mySqlCmd.Parameters.AddWithValue("_PhoneNumber", txtPhoneNumber.Text.Trim());
                mySqlCmd.Parameters.AddWithValue("_Gender", txtGender.Text.Trim());
                mySqlCmd.Parameters.AddWithValue("_LanguagesKnown", txtLanguagesKnown.Text.Trim());
                mySqlCmd.Parameters.AddWithValue("_Hobbies", txtHobbies.Text.Trim());
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Submited Successfully");
                GridFill();
            }

        }

        private void dgvuser_info_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvuser_info_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvuser_info.CurrentRow.Index != -1)
            {
                txtName.Text = dgvuser_info.CurrentRow.Cells[1].Value.ToString();
                txtPhoneNumber.Text = dgvuser_info.CurrentRow.Cells[2].Value.ToString();
                txtGender.Text = dgvuser_info.CurrentRow.Cells[3].Value.ToString();
                txtLanguagesKnown.Text = dgvuser_info.CurrentRow.Cells[4].Value.ToString();
                txtHobbies.Text = dgvuser_info.CurrentRow.Cells[5].Value.ToString();
                ID = Convert.ToInt32(dgvuser_info.CurrentRow.Cells[0].Value.ToString());
                btnSave.Text = "Update";


            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("userSearchByvalue", mysqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("_SearchValue", txtSearch.Text);
                DataTable dtbluser_info = new DataTable();
                sqlDa.Fill(dtbluser_info);
                dgvuser_info.DataSource = dtbluser_info;
                dgvuser_info.Columns[0].Visible = false;



            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("userDeleteByID", mysqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_ID", ID);
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully");
                GridFill();
            }
        }
    }
}
