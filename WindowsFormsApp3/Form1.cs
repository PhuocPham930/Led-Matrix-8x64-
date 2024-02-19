using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            string[] baudrate = { "1200", "2400", "4800", "9600", "115200" };
            cbbPort.Items.AddRange(ports);
            cbbBaudrate.Items.AddRange(baudrate);
            btnClose.Enabled = false;
            cbbSpeed.SelectedIndex = 2;
            cbbMode.SelectedIndex = 0;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if(cbbPort.Text=="")
            {
                MessageBox.Show("Chưa chọn COM Port","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else if(cbbBaudrate.Text=="")
            {
                MessageBox.Show("Chưa chọn Baudrate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if(serialPort1.IsOpen)
                    {
                        MessageBox.Show("Port đang kết nối", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        serialPort1.Open();
                        txtStatus.BackColor = Color.Lime;
                        txtStatus.Text = "Conecting...";
                        cbbPort.Enabled = false;
                        btnClose.Enabled = true;
                        btnOpen.Enabled = false;
                        cbbBaudrate.Enabled = false;
                        btnScan.Enabled = false;
                        MessageBox.Show("Port: " + serialPort1.PortName + " " + serialPort1.BaudRate);
                    }
                }
                catch
                {
                    MessageBox.Show("Không tìm được COM Port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    txtStatus.BackColor = Color.Red;
                    txtStatus.Text = "Disconected";
                    cbbPort.Enabled = true;
                    btnClose.Enabled = false;
                    btnOpen.Enabled = true;
                    cbbBaudrate.Enabled = true;
                    btnScan.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Chưa kết nối với Port nào cả!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Không tìm được COM Port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
                serialPort1.Close();
        }

        private void cbbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = cbbPort.Text;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            string time = dateTime.ToString("HH:mm:ss");
            string date = dateTime.ToString("dd/MM/yyyy");
            string chuoi = "";
            if(cbbMode.SelectedIndex == 0)       chuoi = txtData.Text + '0';
            else if(cbbMode.SelectedIndex == 1)  chuoi = txtData.Text + '1';
            else                                 chuoi = txtData.Text + '2';

            if (cbbSpeed.SelectedIndex == 0)     chuoi += '1';
            else if(cbbSpeed.SelectedIndex == 1) chuoi += '2';
            else if (cbbSpeed.SelectedIndex == 2)chuoi += '3';
            else if (cbbSpeed.SelectedIndex == 3)chuoi += '4';
            else                                 chuoi += '5';

            if (serialPort1.IsOpen == true)
            {
                if(cbbMode.SelectedIndex != 2)
                {
                    lsbSendData.Items.Add(date + "  " + time + "\t" +
                                          txtData.Text + "\t" +
                                          "Hiệu ứng: " + cbbMode.SelectedItem + '\t' +
                                          "Tốc độ chạy chữ: " + cbbSpeed.SelectedItem);
                }
                else
                {
                    lsbSendData.Items.Add(date + "  " + time + "\t" +
                      txtData.Text + "\t" +
                      "Hiệu ứng: " + cbbMode.SelectedItem);
                } 
                serialPort1.Write(chuoi);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtData.Clear();
        }

        private void txtStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbMode.SelectedIndex == 2)
                cbbSpeed.Enabled = false;
            else
                cbbSpeed.Enabled = true;
        }

        private void lsbSendData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void cbbBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.BaudRate = Convert.ToInt32(cbbBaudrate.Text);
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            cbbPort.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cbbPort.Items.AddRange(ports);
        }
    }
}
