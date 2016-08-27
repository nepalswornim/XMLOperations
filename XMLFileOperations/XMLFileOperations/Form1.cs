using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMLFileOperations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Application.StartupPath + "\\Data.xml");
            dataGridView1.DataSource = ds.Tables["email"];
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
        }
        int rowid = 0;

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            rowid = dataGridView1.CurrentRow.Index;
            btnSave.Text = "Update";
            btnDelete.Enabled = true;
            txtName.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells["address"].Value.ToString();
            string active = dataGridView1.CurrentRow.Cells["active"].Value.ToString();
            if ( Convert.ToBoolean( active)==true)
            {
                checkBox1.Checked = true; 
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            LoadGrid();
        }

        private void Clear()
        {
            txtName.Clear();
            txtEmail.Clear();
            checkBox1.Checked = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text!="Update")
            {

                DataSet ds = new DataSet();
                ds.ReadXml(Application.StartupPath + "\\Data.xml");
                DataRow dr = ds.Tables["email"].NewRow();
                dr[0] = txtName.Text;
                dr[1] = txtEmail.Text;
                dr[2] = checkBox1.Checked ? "true" : "false";
                ds.Tables["email"].Rows.Add(dr);
                ds.WriteXml(Application.StartupPath + "\\Data.xml");
                LoadGrid();
                Clear();
                
            }
            else
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Application.StartupPath + "\\Data.xml");
                ds.Tables["email"].Rows[rowid]["Name"] = txtName.Text;
                ds.Tables["email"].Rows[rowid]["address"] = txtEmail.Text;
                ds.Tables["email"].Rows[rowid]["active"] = checkBox1.Checked ? "True" : "False";
                ds.AcceptChanges();
                ds.WriteXml(Application.StartupPath + "\\Data.xml");
                LoadGrid();
               
                 



            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Application.StartupPath + "\\Data.xml");
            ds.Tables["email"].Rows[rowid].Delete();
            ds.AcceptChanges();
            ds.WriteXml(Application.StartupPath + "\\Data.xml");
            LoadGrid();
        }
    }
}
