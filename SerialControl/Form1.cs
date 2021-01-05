using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialControl
{
    public partial class Form1 : Form
    {
        //device1
        const byte DeviceOpen1 = 0x01;
        const byte DeviceClose1 = 0x81;
        //device2
        const byte DeviceOpen2 = 0x02;
        const byte DeviceClose2 = 0x82;
        //device3
        const byte DeviceOpen3 = 0x03;
        const byte DeviceClose3 = 0x83;
        //serialPort Write Buffer
        byte[] SerialPortDataBuffer = new byte[1];
        public Form1()
        {
            InitializeComponent();//窗口构造
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SearchAndAddSerialToComboBox(serialPort1, comboBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Close();
                }
                catch { }
                button2.Text = "打开串口";
            }
            else
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text; //端口号
                    serialPort1.Open();//打开端口
                    button2.Text = "关闭串口";
                }
                catch 
                {
                    MessageBox.Show("串口打开失败", "错误");
                }
            }
        }
        private void SearchAndAddSerialToComboBox(SerialPort MyPort, ComboBox MyBox)//扫描
        {
            string[] MyString = new string[20];                         //将可用端口号添加到ComboBox
            string Buffer;                                              //缓存
            MyBox.Items.Clear();                                        //清空ComboBox内容
            //int count = 0;
            for (int i = 1; i < 20; i++)                                //循环这里只扫描1-19
            {
                try                                                     //核心原理是依靠try和catch完成遍历
                {
                    Buffer = "COM" + i.ToString();
                    MyPort.PortName = Buffer;
                    MyPort.Open();                                      //如果失败，后面的代码不会执行
                    //MyString[count] = Buffer;                          
                    MyBox.Items.Add(Buffer);                            //打开成功，添加至下俩列表
                    MyPort.Close();                                     //关闭
                    //count++;
                }
                catch//出错了什么也不做继续循环
                {
                }
                MyBox.Text = MyString[0];
            }
        }


        private void WriteByteToSerialPort(byte data)                   //单字节写入串口
        {
            byte[] Buffer = new byte[2] { 0x00, data };                       //定义数组
            if (serialPort1.IsOpen)                                     //传输数据的前提是端口已打开
            {
                try
                {
                    serialPort1.Write(Buffer, 0, 2);                    //写数据
                }
                catch
                {
                    MessageBox.Show("串口数据发送出错，请检查.", "错误");//错误处理
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchAndAddSerialToComboBox(serialPort1, comboBox1);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DeviceOpen1);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DeviceClose1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DeviceOpen2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DeviceClose2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DeviceOpen3);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DeviceClose3);
        }
    }
}
