using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            this.richTextBox2.Clear();
            this.tabControl1.SelectedTab = this.tabPage1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.AppendText("Teste Enviar Msg QM" + Environment.NewLine);
            this.richTextBox1.ScrollToCaret();

            //DialogResult res = MessageBox.Show("Are you sure you want to Delete", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            //if (res == DialogResult.OK)
            //{
            //    MessageBox.Show("You have clicked Ok Button");
            //    //Some task…  
            //}
            //if (res == DialogResult.Cancel)
            //{
            //    MessageBox.Show("You have clicked Cancel Button");
            //    //Some task…  
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.richTextBox2.AppendText("Teste Receber Msg QM" + Environment.NewLine);
            this.richTextBox2.ScrollToCaret();
        }
    }
}
