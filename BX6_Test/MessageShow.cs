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
    public partial class MessageShow : Form
    {
        public MessageShow(string w)
        {
            InitializeComponent();
            label1.Text = w;
            //label2.Text = b;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
