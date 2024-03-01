using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKTX
{
    public partial class Home : Form
    {
        
        public Home()
        {
            InitializeComponent();

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            SinhVien formcon1 = new SinhVien();
            formcon1.Show();
        }
        private void btPhong_Click(object sender, EventArgs e)
        {
            Phong phong = new Phong();
            phong.Show();
        }

        private void btChiPhi_Click(object sender, EventArgs e)
        {
            ChiPhi formcon5 = new ChiPhi();
            formcon5.Show();
        }

        private void btDangXuat_Click(object sender, EventArgs e)
        {
            SinhVien sv = new SinhVien();
            Formlogin fm = new Formlogin();
            sv.Hide();
            fm.Show();
            this.Close();
           
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
           NhanVien sv = new NhanVien();
            sv.Show();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btCsvc_Click(object sender, EventArgs e)
        {
            CSVC sv = new CSVC();
            sv.Show();
        }
    }
}
