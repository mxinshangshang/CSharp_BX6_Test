using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BX6_Test
{
    public partial class ShowResult : Form
    {
        public ShowResult(string result)
        {
            InitializeComponent();
            if (result.Contains("FAIL"))
            {
                //label1.ForeColor = Color.Red;
                this.BackColor = Color.Red;
                label1.Text = result;
            }
            else if (result.Contains("PASS"))
            {
                //label1.ForeColor = Color.Green;
                this.BackColor = Color.Green;
                label1.Text = result;
            }
            label2.Text = "请断开 JTHS 和其他断路器，再拔下所有接插件";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
