using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Management;

namespace AuthorizationManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cpuId = GetCpuId();
            string motherboardSerialNumber = GetMotherboardSerialNumber();
            string hardwareFingerprint = cpuId + motherboardSerialNumber;

            string encryptedFingerprint = EncryptionHelper.Encrypt(hardwareFingerprint);

            //复制到剪贴板
            Clipboard.SetText(encryptedFingerprint);

            DialogResult result = MessageBox.Show("success");

            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        //获取CpuId
        public static string GetCpuId()
        {
            try
            {
                string cpuId = string.Empty;
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                foreach (ManagementObject obj in searcher.Get())
                {
                    cpuId = obj["ProcessorId"].ToString();
                    break;
                }
                return cpuId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("权限不足，无法读取设备信息");
                return string.Empty;
            }
        }


        //获取主板序列号
        public static string GetMotherboardSerialNumber()
        {
            try
            {
                string serialNumber = string.Empty;
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
                foreach (ManagementObject obj in searcher.Get())
                {
                    serialNumber = obj["SerialNumber"].ToString();
                    break;
                }
                return serialNumber;
            }
            catch (Exception ex)
            {
                Console.WriteLine("权限不足，无法读取设备信息");
                return string.Empty;
            }
        }
    }
}
