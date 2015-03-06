using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            Student aStudent = new Student();
            aStudent.Name = nameTextBox.Text;
            aStudent.RegNo = regTextBox.Text;
            aStudent.Address = addressTextBox.Text;

            //Connect to database

            string connectionString = "Server=BITM-401-PC0\\SQLEXPRESS; Database=VersityManagementDB; Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionString);

            //insert query

            string query = "INSERT INTO Students VALUES('" + aStudent.Name + "' , '" + aStudent.RegNo + "' ,'" + aStudent.Address + "')";
            
            
            //execute

            SqlCommand command = new SqlCommand(query, connection);
            //command.CommandText = query;
            //command.Connection = connection;

            connection.Open();

            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            if (rowAffected > 0)
            {
                MessageBox.Show("Student Added");
                nameTextBox.Text = "";
                regTextBox.Text = "";
                addressTextBox.Text = "";
            }

        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            UpdateAll();
        }

        private void UpdateAll()
        {
//clear the items in list view

            studentListView.Items.Clear();


            //connect to database

            string connectionString =
                "Server=BITM-401-PC0\\SQLEXPRESS; Database=VersityManagementDB; Integrated Security=true";

            SqlConnection connection = new SqlConnection(connectionString);

            //write query

            string query = "SELECT * FROM Students";

            //execute query

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            //read data from database

            SqlDataReader dataReader = command.ExecuteReader();

            List<Student> students = new List<Student>();

            while (dataReader.Read())
            {
                Student aStudent = new Student();

                aStudent.ID = int.Parse(dataReader["ID"].ToString());
                aStudent.Name = dataReader["Name"].ToString();
                aStudent.RegNo = dataReader["RegNo"].ToString();
                aStudent.Address = dataReader["Address"].ToString();

                students.Add(aStudent);
            }

            dataReader.Close();
            connection.Close();

            //populate listView

            foreach (Student student in students)
            {
                ListViewItem item = new ListViewItem(student.ID.ToString());
                item.SubItems.Add(student.Name);
                item.SubItems.Add(student.RegNo);
                item.SubItems.Add(student.Address);
                item.Tag = student;
                studentListView.Items.Add(item);
            }
        }

        private void studentListView_DoubleClick(object sender, EventArgs e)
        {
            if (studentListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = studentListView.SelectedItems[0];
                Student selectedStudent = (Student)selectedItem.Tag;

                nameTextBox.Text = selectedStudent.Name;
                regTextBox.Text = selectedStudent.RegNo;
                addressTextBox.Text = selectedStudent.Address;
                idLabel.Text = selectedStudent.ID.ToString();
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            GetAllStudents();
        }

        private void GetAllStudents()
        {
            Student aStudent = new Student();
            aStudent.ID = int.Parse(idLabel.Text);
            aStudent.Name = nameTextBox.Text;
            aStudent.RegNo = regTextBox.Text;
            aStudent.Address = addressTextBox.Text;

            //Connect to database

            string connectionString = "Server=BITM-401-PC0\\SQLEXPRESS; Database=VersityManagementDB; Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionString);

            //insert query

            string query = "UPDATE Students SET Name = '" + aStudent.Name + "' , RegNo = '" + aStudent.RegNo + "' , Address = '" +
                           aStudent.Address + "' WHERE ID = " + aStudent.ID;


            //execute

            SqlCommand command = new SqlCommand(query, connection);
            //command.CommandText = query;
            //command.Connection = connection;

            connection.Open();

            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            if (rowAffected > 0)
            {
                MessageBox.Show("Inserted Successfully");
                UpdateAll();
                nameTextBox.Text = "";
                regTextBox.Text = "";
                addressTextBox.Text = "";
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int tempid = int.Parse(idLabel.Text);
            

            //Connect to database

            string connectionString = "Server=BITM-401-PC0\\SQLEXPRESS; Database=VersityManagementDB; Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionString);

            //insert query

            string query = "DELETE FROM Students WHERE ID = " + tempid;


            //execute

            SqlCommand command = new SqlCommand(query, connection);
            //command.CommandText = query;
            //command.Connection = connection;

            connection.Open();

            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            if (rowAffected > 0)
            {
                MessageBox.Show("Deleted Successfully");
                UpdateAll();
                nameTextBox.Text = "";
                regTextBox.Text = "";
                addressTextBox.Text = "";
            }
        }
    }

    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string RegNo { get; set; }
        public string Address { get; set; }
    }
}
