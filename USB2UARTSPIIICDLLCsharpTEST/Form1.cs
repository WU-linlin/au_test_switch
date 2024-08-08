using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;

using System.Runtime.InteropServices; // 用 DllImport 需用此 命名空间
using System.Reflection; // 使用 Assembly 类需用此 命名空间
using System.Reflection.Emit;
using System.Threading; // 使用 ILGenerator 需用此 命名空间

namespace USB2UARTSPIIICDLLCsharpTEST
{
    public partial class Form1 : Form
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //关于dll中函数的说明请查阅USB2UARTSPIIICDLL.h文件

        [DllImport("USB2UARTSPIIICDLL.dll")]
        public static extern int OpenUsb(uint UsbIndex);

        [DllImport("USB2UARTSPIIICDLL.dll")]
        public static extern int CloseUsb(uint UsbIndex);

        [DllImport("USB2UARTSPIIICDLL.dll")]
        public static extern int ConfigIICParam(uint rate, uint clkSLevel, uint UsbIndex);


        [DllImport("USB2UARTSPIIICDLL.dll")]
        public static extern int IICSendData(byte strartBit, byte stopBit, byte[] sendBuf, uint slen, uint UsbIndex);

        [DllImport("USB2UARTSPIIICDLL.dll")]
        public static extern int IICRcvData(byte stopBit, byte[] rcvBuf, uint rlen, uint UsbIndex);



        //需要用到dll中的其它函数，需要在这里添加声明

        //////////////////////////////////////////////////////////////////////////////////////////////////////

        public Form1()
        {
            InitializeComponent();
            

        }

        private void sendcmd(byte[] data, uint usb_index)
        {
            //IIC主机向从机发送数据
            data[0] = (byte)(data[0] << 1); //地址需要移位
            IICSendData(1, 1, data, (uint)data.Length, usb_index);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*原指令
            byte[] temp1 = new byte[] { 0x40, 0x04, 0x3F, 0xff};//0x40为TX MCU地址
            byte[] temp2 = new byte[] { 0x40, 0x04, 0x40, 0xff};//0x40为TX MCU地址
            byte[] temp3 = new byte[] { 0x40, 0x04, 0x41, 0xff};//0x40为TX MCU地址
            byte[] temp4 = new byte[] { 0x40, 0x04, 0x42, 0x00};//0x40为TX MCU地址
            byte[] temp5 = new byte[] { 0x40, 0x04, 0x43, 0x00};//0x40为TX MCU地址
            byte[] temp6 = new byte[] { 0x40, 0x04, 0x44, 0x00};//0x40为TX MCU地址
            byte[] temp7 = new byte[] { 0x40, 0x04, 0x3d, 0x01};//0x40为TX MCU地址
            */

            //白->灰白：
            byte[] temp1 = new byte[] { 0x40, 0x04, 0x3F, 0x80 };//0x40为TX MCU地址
            byte[] temp2 = new byte[] { 0x40, 0x04, 0x40, 0x80 };//0x40为TX MCU地址
            byte[] temp3 = new byte[] { 0x40, 0x04, 0x41, 0x80 };//0x40为TX MCU地址
            byte[] temp4 = new byte[] { 0x40, 0x04, 0x42, 0x80 };//0x40为TX MCU地址
            byte[] temp5 = new byte[] { 0x40, 0x04, 0x43, 0x80 };//0x40为TX MCU地址
            byte[] temp6 = new byte[] { 0x40, 0x04, 0x44, 0x80 };//0x40为TX MCU地址
            byte[] temp7 = new byte[] { 0x40, 0x04, 0x45, 0x00 };//0x40为TX MCU地址
            byte[] temp8 = new byte[] { 0x40, 0x04, 0x46, 0x00 };//0x40为TX MCU地址
            byte[] temp9 = new byte[] { 0x40, 0x04, 0x47, 0x00 };//0x40为TX MCU地址
            byte[] temp10 = new byte[] { 0x40, 0x04, 0x3d, 0x01 };//0x40为TX MCU地址

            uint usb_index = 0;
            //打开索引为0的USB转SPI/IIC转接板的USB
            if (OpenUsb(0) >= 0)
            {
                usb_index = 0;
            }
            else if (OpenUsb(1) >= 0)
            {
                usb_index = 1;
            }
            else if (OpenUsb(2) >= 0)
            {
                usb_index = 2;
            }
            else if (OpenUsb(3) >= 0)
            {
                usb_index = 3;
            }
            else if (OpenUsb(4) >= 0)
            {
                usb_index = 4;
            }
            else
            {
                MessageBox.Show("打开USB0~4错误，请检查设备");
                this.Close();
                //Application.Exit();
                return;
            }

            //设置索引为0的USB转SPI/IIC转接板IIC主模式 速率100K
            ConfigIICParam(8, 1000, 0); //6-100k 7-200k 8-400k


            sendcmd(temp1, usb_index);
            sendcmd(temp2, usb_index);
            sendcmd(temp3, usb_index);
            sendcmd(temp4, usb_index);
            sendcmd(temp5, usb_index);
            sendcmd(temp6, usb_index);
            sendcmd(temp7, usb_index);
            sendcmd(temp8, usb_index);
            sendcmd(temp9, usb_index);
            sendcmd(temp10, usb_index);

            //temp[0] = (0x40 << 1) + 1;  //读回数据
            //IICSendData(1,0, temp, 1, 0);
            //IICRcvData( 1, temp, 2, 0);

            CloseUsb(0);
            //this.Close();
            //Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //暗：
            byte[] temp1 = new byte[] { 0x40, 0x04, 0x3F, 0x00 };//0x40为TX MCU地址
            byte[] temp2 = new byte[] { 0x40, 0x04, 0x40, 0x00 };//0x40为TX MCU地址
            byte[] temp3 = new byte[] { 0x40, 0x04, 0x41, 0x00 };//0x40为TX MCU地址
            byte[] temp4 = new byte[] { 0x40, 0x04, 0x42, 0x00 };//0x40为TX MCU地址
            byte[] temp5 = new byte[] { 0x40, 0x04, 0x43, 0x00 };//0x40为TX MCU地址
            byte[] temp6 = new byte[] { 0x40, 0x04, 0x44, 0x00 };//0x40为TX MCU地址
            byte[] temp7 = new byte[] { 0x40, 0x04, 0x45, 0x00 };//0x40为TX MCU地址
            byte[] temp8 = new byte[] { 0x40, 0x04, 0x46, 0x00 };//0x40为TX MCU地址
            byte[] temp9 = new byte[] { 0x40, 0x04, 0x47, 0x00 };//0x40为TX MCU地址
            byte[] temp10 = new byte[] { 0x40, 0x04, 0x3d, 0x01 };//0x40为TX MCU地址

            uint usb_index = 0;
            //打开索引为0的USB转SPI/IIC转接板的USB
            if (OpenUsb(0) >= 0)
            {
                usb_index = 0;
            }
            else if (OpenUsb(1) >= 0)
            {
                usb_index = 1;
            }
            else if (OpenUsb(2) >= 0)
            {
                usb_index = 2;
            }
            else if (OpenUsb(3) >= 0)
            {
                usb_index = 3;
            }
            else if (OpenUsb(4) >= 0)
            {
                usb_index = 4;
            }
            else
            {
                MessageBox.Show("打开USB0~4错误，请检查设备");
                this.Close();
                //Application.Exit();
                return;
            }

            //设置索引为0的USB转SPI/IIC转接板IIC主模式 速率100K
            ConfigIICParam(8, 1000, 0); //6-100k 7-200k 8-400k


            sendcmd(temp1, usb_index);
            sendcmd(temp2, usb_index);
            sendcmd(temp3, usb_index);
            sendcmd(temp4, usb_index);
            sendcmd(temp5, usb_index);
            sendcmd(temp6, usb_index);
            sendcmd(temp7, usb_index);
            sendcmd(temp8, usb_index);
            sendcmd(temp9, usb_index);
            sendcmd(temp10, usb_index);

            //temp[0] = (0x40 << 1) + 1;  //读回数据
            //IICSendData(1,0, temp, 1, 0);
            //IICRcvData( 1, temp, 2, 0);

            CloseUsb(0);
            //this.Close();
            //Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //星：
            byte[] temp1 = new byte[] { 0x40, 0x04, 0x3F, 0xFF };//0x40为TX MCU地址
            byte[] temp2 = new byte[] { 0x40, 0x04, 0x40, 0xFF };//0x40为TX MCU地址
            byte[] temp3 = new byte[] { 0x40, 0x04, 0x41, 0xFF };//0x40为TX MCU地址
            byte[] temp4 = new byte[] { 0x40, 0x04, 0x42, 0x00 };//0x40为TX MCU地址
            byte[] temp5 = new byte[] { 0x40, 0x04, 0x43, 0x00 };//0x40为TX MCU地址
            byte[] temp6 = new byte[] { 0x40, 0x04, 0x44, 0x00 };//0x40为TX MCU地址
            byte[] temp7 = new byte[] { 0x40, 0x04, 0x45, 0x0A };//0x40为TX MCU地址
            byte[] temp8 = new byte[] { 0x40, 0x04, 0x46, 0x0A };//0x40为TX MCU地址
            byte[] temp9 = new byte[] { 0x40, 0x04, 0x47, 0x0A };//0x40为TX MCU地址
            byte[] temp10 = new byte[] { 0x40, 0x04, 0x3d, 0x01 };//0x40为TX MCU地址

            uint usb_index = 0;
            //打开索引为0的USB转SPI/IIC转接板的USB
            if (OpenUsb(0) >= 0)
            {
                usb_index = 0;
            }
            else if (OpenUsb(1) >= 0)
            {
                usb_index = 1;
            }
            else if (OpenUsb(2) >= 0)
            {
                usb_index = 2;
            }
            else if (OpenUsb(3) >= 0)
            {
                usb_index = 3;
            }
            else if (OpenUsb(4) >= 0)
            {
                usb_index = 4;
            }
            else
            {
                MessageBox.Show("打开USB0~4错误，请检查设备");
                this.Close();
                //Application.Exit();
                return;
            }

            //设置索引为0的USB转SPI/IIC转接板IIC主模式 速率100K
            ConfigIICParam(8, 1000, 0); //6-100k 7-200k 8-400k


            sendcmd(temp1, usb_index);
            sendcmd(temp2, usb_index);
            sendcmd(temp3, usb_index);
            sendcmd(temp4, usb_index);
            sendcmd(temp5, usb_index);
            sendcmd(temp6, usb_index);
            sendcmd(temp7, usb_index);
            sendcmd(temp8, usb_index);
            sendcmd(temp9, usb_index);
            sendcmd(temp10, usb_index);

            //temp[0] = (0x40 << 1) + 1;  //读回数据
            //IICSendData(1,0, temp, 1, 0);
            //IICRcvData( 1, temp, 2, 0);

            CloseUsb(0);
            //this.Close();
            //Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //竖条纹：
            byte[] temp1 = new byte[] { 0x40, 0x04, 0x3D, 0x02 };//0x40为TX MCU地址


            uint usb_index = 0;
            //打开索引为0的USB转SPI/IIC转接板的USB
            if (OpenUsb(0) >= 0)
            {
                usb_index = 0;
            }
            else if (OpenUsb(1) >= 0)
            {
                usb_index = 1;
            }
            else if (OpenUsb(2) >= 0)
            {
                usb_index = 2;
            }
            else if (OpenUsb(3) >= 0)
            {
                usb_index = 3;
            }
            else if (OpenUsb(4) >= 0)
            {
                usb_index = 4;
            }
            else
            {
                MessageBox.Show("打开USB0~4错误，请检查设备");
                this.Close();
                //Application.Exit();
                return;
            }

            //设置索引为0的USB转SPI/IIC转接板IIC主模式 速率100K
            ConfigIICParam(8, 1000, 0); //6-100k 7-200k 8-400k


            sendcmd(temp1, usb_index);

            //temp[0] = (0x40 << 1) + 1;  //读回数据
            //IICSendData(1,0, temp, 1, 0);
            //IICRcvData( 1, temp, 2, 0);

            CloseUsb(0);
            //this.Close();
            //Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //红：
            byte[] temp1 = new byte[] { 0x40, 0x04, 0x3F, 0xFF };//0x40为TX MCU地址
            byte[] temp2 = new byte[] { 0x40, 0x04, 0x40, 0x00 };//0x40为TX MCU地址
            byte[] temp3 = new byte[] { 0x40, 0x04, 0x41, 0x00 };//0x40为TX MCU地址
            byte[] temp4 = new byte[] { 0x40, 0x04, 0x42, 0xFF };//0x40为TX MCU地址
            byte[] temp5 = new byte[] { 0x40, 0x04, 0x43, 0x00 };//0x40为TX MCU地址
            byte[] temp6 = new byte[] { 0x40, 0x04, 0x44, 0x00 };//0x40为TX MCU地址
            byte[] temp7 = new byte[] { 0x40, 0x04, 0x45, 0x00 };//0x40为TX MCU地址
            byte[] temp8 = new byte[] { 0x40, 0x04, 0x46, 0x00 };//0x40为TX MCU地址
            byte[] temp9 = new byte[] { 0x40, 0x04, 0x47, 0x00 };//0x40为TX MCU地址
            byte[] temp10 = new byte[] { 0x40, 0x04, 0x3d, 0x01 };//0x40为TX MCU地址

            uint usb_index = 0;
            //打开索引为0的USB转SPI/IIC转接板的USB
            if (OpenUsb(0) >= 0)
            {
                usb_index = 0;
            }
            else if (OpenUsb(1) >= 0)
            {
                usb_index = 1;
            }
            else if (OpenUsb(2) >= 0)
            {
                usb_index = 2;
            }
            else if (OpenUsb(3) >= 0)
            {
                usb_index = 3;
            }
            else if (OpenUsb(4) >= 0)
            {
                usb_index = 4;
            }
            else
            {
                MessageBox.Show("打开USB0~4错误，请检查设备");
                this.Close();
                //Application.Exit();
                return;
            }

            //设置索引为0的USB转SPI/IIC转接板IIC主模式 速率100K
            ConfigIICParam(8, 1000, 0); //6-100k 7-200k 8-400k


            sendcmd(temp1, usb_index);
            sendcmd(temp2, usb_index);
            sendcmd(temp3, usb_index);
            sendcmd(temp4, usb_index);
            sendcmd(temp5, usb_index);
            sendcmd(temp6, usb_index);
            sendcmd(temp7, usb_index);
            sendcmd(temp8, usb_index);
            sendcmd(temp9, usb_index);
            sendcmd(temp10, usb_index);

            //temp[0] = (0x40 << 1) + 1;  //读回数据
            //IICSendData(1,0, temp, 1, 0);
            //IICRcvData( 1, temp, 2, 0);

            CloseUsb(0);
            //this.Close();
            //Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //绿：
            byte[] temp1 = new byte[] { 0x40, 0x04, 0x3F, 0x00 };//0x40为TX MCU地址
            byte[] temp2 = new byte[] { 0x40, 0x04, 0x40, 0xFF };//0x40为TX MCU地址
            byte[] temp3 = new byte[] { 0x40, 0x04, 0x41, 0x00 };//0x40为TX MCU地址
            byte[] temp4 = new byte[] { 0x40, 0x04, 0x42, 0x00 };//0x40为TX MCU地址
            byte[] temp5 = new byte[] { 0x40, 0x04, 0x43, 0xFF };//0x40为TX MCU地址
            byte[] temp6 = new byte[] { 0x40, 0x04, 0x44, 0x00 };//0x40为TX MCU地址
            byte[] temp7 = new byte[] { 0x40, 0x04, 0x45, 0x00 };//0x40为TX MCU地址
            byte[] temp8 = new byte[] { 0x40, 0x04, 0x46, 0x00 };//0x40为TX MCU地址
            byte[] temp9 = new byte[] { 0x40, 0x04, 0x47, 0x00 };//0x40为TX MCU地址
            byte[] temp10 = new byte[] { 0x40, 0x04, 0x3d, 0x01 };//0x40为TX MCU地址

            uint usb_index = 0;
            //打开索引为0的USB转SPI/IIC转接板的USB
            if (OpenUsb(0) >= 0)
            {
                usb_index = 0;
            }
            else if (OpenUsb(1) >= 0)
            {
                usb_index = 1;
            }
            else if (OpenUsb(2) >= 0)
            {
                usb_index = 2;
            }
            else if (OpenUsb(3) >= 0)
            {
                usb_index = 3;
            }
            else if (OpenUsb(4) >= 0)
            {
                usb_index = 4;
            }
            else
            {
                MessageBox.Show("打开USB0~4错误，请检查设备");
                this.Close();
                //Application.Exit();
                return;
            }

            //设置索引为0的USB转SPI/IIC转接板IIC主模式 速率100K
            ConfigIICParam(8, 1000, 0); //6-100k 7-200k 8-400k


            sendcmd(temp1, usb_index);
            sendcmd(temp2, usb_index);
            sendcmd(temp3, usb_index);
            sendcmd(temp4, usb_index);
            sendcmd(temp5, usb_index);
            sendcmd(temp6, usb_index);
            sendcmd(temp7, usb_index);
            sendcmd(temp8, usb_index);
            sendcmd(temp9, usb_index);
            sendcmd(temp10, usb_index);

            //temp[0] = (0x40 << 1) + 1;  //读回数据
            //IICSendData(1,0, temp, 1, 0);
            //IICRcvData( 1, temp, 2, 0);

            CloseUsb(0);
            //this.Close();
            //Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //蓝：
            byte[] temp1 = new byte[] { 0x40, 0x04, 0x3F, 0x00 };//0x40为TX MCU地址
            byte[] temp2 = new byte[] { 0x40, 0x04, 0x40, 0x00 };//0x40为TX MCU地址
            byte[] temp3 = new byte[] { 0x40, 0x04, 0x41, 0xFF };//0x40为TX MCU地址
            byte[] temp4 = new byte[] { 0x40, 0x04, 0x42, 0x00 };//0x40为TX MCU地址
            byte[] temp5 = new byte[] { 0x40, 0x04, 0x43, 0x00 };//0x40为TX MCU地址
            byte[] temp6 = new byte[] { 0x40, 0x04, 0x44, 0xFF };//0x40为TX MCU地址
            byte[] temp7 = new byte[] { 0x40, 0x04, 0x45, 0x00 };//0x40为TX MCU地址
            byte[] temp8 = new byte[] { 0x40, 0x04, 0x46, 0x00 };//0x40为TX MCU地址
            byte[] temp9 = new byte[] { 0x40, 0x04, 0x47, 0x00 };//0x40为TX MCU地址
            byte[] temp10 = new byte[] { 0x40, 0x04, 0x3d, 0x01 };//0x40为TX MCU地址

            uint usb_index = 0;
            //打开索引为0的USB转SPI/IIC转接板的USB
            if (OpenUsb(0) >= 0)
            {
                usb_index = 0;
            }
            else if (OpenUsb(1) >= 0)
            {
                usb_index = 1;
            }
            else if (OpenUsb(2) >= 0)
            {
                usb_index = 2;
            }
            else if (OpenUsb(3) >= 0)
            {
                usb_index = 3;
            }
            else if (OpenUsb(4) >= 0)
            {
                usb_index = 4;
            }
            else
            {
                MessageBox.Show("打开USB0~4错误，请检查设备");
                this.Close();
                //Application.Exit();
                return;
            }

            //设置索引为0的USB转SPI/IIC转接板IIC主模式 速率100K
            ConfigIICParam(8, 1000, 0); //6-100k 7-200k 8-400k


            sendcmd(temp1, usb_index);
            sendcmd(temp2, usb_index);
            sendcmd(temp3, usb_index);
            sendcmd(temp4, usb_index);
            sendcmd(temp5, usb_index);
            sendcmd(temp6, usb_index);
            sendcmd(temp7, usb_index);
            sendcmd(temp8, usb_index);
            sendcmd(temp9, usb_index);
            sendcmd(temp10, usb_index);

            //temp[0] = (0x40 << 1) + 1;  //读回数据
            //IICSendData(1,0, temp, 1, 0);
            //IICRcvData( 1, temp, 2, 0);

            CloseUsb(0);
            //this.Close();
            //Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //白：
            byte[] temp1 = new byte[] { 0x40, 0x04, 0x3F, 0xFF };//0x40为TX MCU地址
            byte[] temp2 = new byte[] { 0x40, 0x04, 0x40, 0xFF };//0x40为TX MCU地址
            byte[] temp3 = new byte[] { 0x40, 0x04, 0x41, 0xFF };//0x40为TX MCU地址
            byte[] temp4 = new byte[] { 0x40, 0x04, 0x42, 0xFF };//0x40为TX MCU地址
            byte[] temp5 = new byte[] { 0x40, 0x04, 0x43, 0xFF };//0x40为TX MCU地址
            byte[] temp6 = new byte[] { 0x40, 0x04, 0x44, 0xFF };//0x40为TX MCU地址
            byte[] temp7 = new byte[] { 0x40, 0x04, 0x45, 0x00 };//0x40为TX MCU地址
            byte[] temp8 = new byte[] { 0x40, 0x04, 0x46, 0x00 };//0x40为TX MCU地址
            byte[] temp9 = new byte[] { 0x40, 0x04, 0x47, 0x00 };//0x40为TX MCU地址
            byte[] temp10 = new byte[] { 0x40, 0x04, 0x3d, 0x01 };//0x40为TX MCU地址

            uint usb_index = 0;
            //打开索引为0的USB转SPI/IIC转接板的USB
            if (OpenUsb(0) >= 0)
            {
                usb_index = 0;
            }
            else if (OpenUsb(1) >= 0)
            {
                usb_index = 1;
            }
            else if (OpenUsb(2) >= 0)
            {
                usb_index = 2;
            }
            else if (OpenUsb(3) >= 0)
            {
                usb_index = 3;
            }
            else if (OpenUsb(4) >= 0)
            {
                usb_index = 4;
            }
            else
            {
                MessageBox.Show("打开USB0~4错误，请检查设备");
                this.Close();
                //Application.Exit();
                return;
            }

            //设置索引为0的USB转SPI/IIC转接板IIC主模式 速率100K
            ConfigIICParam(8, 1000, 0); //6-100k 7-200k 8-400k


            sendcmd(temp1, usb_index);
            sendcmd(temp2, usb_index);
            sendcmd(temp3, usb_index);
            sendcmd(temp4, usb_index);
            sendcmd(temp5, usb_index);
            sendcmd(temp6, usb_index);
            sendcmd(temp7, usb_index);
            sendcmd(temp8, usb_index);
            sendcmd(temp9, usb_index);
            sendcmd(temp10, usb_index);

            //temp[0] = (0x40 << 1) + 1;  //读回数据
            //IICSendData(1,0, temp, 1, 0);
            //IICRcvData( 1, temp, 2, 0);

            CloseUsb(0);
            //this.Close();
            //Application.Exit();
        }
        
        private void buttonCombined_Click(object sender, EventArgs e)
            {
                System.Threading.Thread.Sleep(2000); // 延时2秒
                button1.PerformClick();
                System.Threading.Thread.Sleep(2000); // 延时2秒
                button2.PerformClick();
                System.Threading.Thread.Sleep(2000); // 延时2秒
                button3.PerformClick();
                System.Threading.Thread.Sleep(2000); // 延时2秒
                button4.PerformClick();
                System.Threading.Thread.Sleep(2000); // 延时2秒
                button5.PerformClick();
                System.Threading.Thread.Sleep(2000); // 延时2秒
                button6.PerformClick();
                System.Threading.Thread.Sleep(2000); // 延时2秒
                button7.PerformClick();
                System.Threading.Thread.Sleep(2000); // 延时2秒
                button8.PerformClick();

            }


      }
    
}


