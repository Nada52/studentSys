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

namespace StudentManagementSystem
{
    public partial class ManageCourseForm : Form
    {
        CourseClass course = new CourseClass();
        public ManageCourseForm()
        {
            InitializeComponent();
        }

        private void ManageCourseForm_Load(object sender, EventArgs e)
        {

            showData();

        }

        // show data of the course
        private void showData()
        {
            // to show course list on datagridview
            dataGridView_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));

        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_courseID.Clear();
            textBox_Cname.Clear();
            textBox_Chour.Clear();
            textBox_description.Clear();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (textBox_Cname.Text == "" || textBox_Chour.Text == "" || textBox_courseID.Text.Equals(""))
            {
                MessageBox.Show("Need Course Data", "Field ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int id = Convert.ToInt32(textBox_courseID.Text);
                string cName = textBox_Cname.Text;
                int chr = Convert.ToInt32(textBox_Chour.Text);
                string desc = textBox_description.Text;

                if (course.updateCourse(id,cName, chr, desc))
                {
                    showData();
                    button_clear.PerformClick();
                    MessageBox.Show("Course updated successfully", "Update Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Course Is not updated", "Update Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_courseID.Text.Equals(""))
            {
                MessageBox.Show("Need Course ID", "Field ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try { 
                    int id = Convert.ToInt32(textBox_courseID.Text);
                    if (course.deleteCourse(id))
                    {
                        showData();
                        button_clear.PerformClick();
                        MessageBox.Show("Course deleted successfully", "Delete Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView_course_Click(object sender, EventArgs e)
        {
            textBox_courseID.Text = dataGridView_course.CurrentRow.Cells[0].Value.ToString();
            textBox_Cname.Text = dataGridView_course.CurrentRow.Cells[1].Value.ToString();
            textBox_Chour.Text = dataGridView_course.CurrentRow.Cells[2].Value.ToString();
            textBox_description.Text = dataGridView_course.CurrentRow.Cells[3].Value.ToString();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            // to search course and show on datagridview
            dataGridView_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`WHERE CONCAT(`CourseName`)LIKE'%"+textBox_search.Text+"%'"));
            textBox_search.Clear();
        }
    }
}
