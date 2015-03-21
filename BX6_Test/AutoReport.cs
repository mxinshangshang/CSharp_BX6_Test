using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace BX6_Test
{
    public partial class AutoReport : Form
    {
        bool result;
        string PLCCom;

        protected override void WndProc(ref   Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                return;
            }
            base.WndProc(ref m);
        }

        public AutoReport(bool next,string PLCCom)
        {
            InitializeComponent();

            this.result = next;
            this.PLCCom = PLCCom;

            serialPort1.PortName = PLCCom;
            serialPort1.BaudRate = 9600;
            serialPort1.DataBits = 7;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Parity = Parity.Even;
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
            }
            serialPort1.Open();

            if (next == true)
            {
                label1.ForeColor = Color.Green;
                label1.Text = "模拟运行测试  PASS";
            }
            else if(next==false)
            {
                label1.ForeColor = Color.Red;
                label1.Text = "模拟运行测试  FAIL";
            }
        }

        private string GetLRC(string a)
        {
            string original = a;
            //": 01 03 06 14 00 08 "起始数据地址高字节06 起始数据地址低字节14 接点个数高字节00 接点个数低字节08 + LRC校验码
            string[] aa = original.Split(' ');
            byte[] message = new byte[aa.Length - 1];
            byte lrc = 0;
            for (int i = 1; i < aa.Length; i++)
            {
                message[i - 1] = Convert.ToByte(aa[i], 16);
            }
            foreach (byte c in message)
            {
                lrc += c;
            }
            byte hex1 = (byte)-lrc;
            string hex = Convert.ToString(hex1, 16).ToUpper();
            return a.Replace(" ", "") + hex + "\r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = ": 01 05 09 14 00 00";                                          //PowerOff
            string b = GetLRC(a);
            byte[] message1 = System.Text.Encoding.ASCII.GetBytes(b);
            serialPort1.Write(message1, 0, b.Length);
            serialPort1.Close();
            MessageBox.Show("请移除所有PCBA上的插件"+"\n\n"+"将控制柜中所有元器件的插件插回到ECB印板上");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
