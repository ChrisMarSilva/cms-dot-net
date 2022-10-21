using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_ADO_Net_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            DataTable myTable = new DataTable("Peoples");

            myTable.Columns.Add("ID", typeof(long));
            myTable.Columns.Add("FirstName", typeof(string));
            myTable.Columns.Add("LastName", typeof(string));
            myTable.Columns.Add("Birthday", typeof(DateTime));

            myTable.PrimaryKey = new DataColumn[] { myTable.Columns["ID"] };


            this.lblTableName.Text = myTable.TableName;

            this.lblTableColumns.Text = String.Empty;

            foreach (DataColumn myColumn in myTable.Columns)
            {
                this.lblTableColumns.Text = this.lblTableColumns.Text + myColumn.ColumnName.ToString() + " - " + myColumn.DataType.ToString() + "\n";
            }

            foreach (DataColumn myColumnPrimaryKey in myTable.Columns)
            {
                if(myTable.PrimaryKey.Contains(myColumnPrimaryKey) == true)
                {
                    this.lblTablePrimaryKey.Text = myColumnPrimaryKey.ColumnName.ToString();
                }
            }

        }
    }
}
