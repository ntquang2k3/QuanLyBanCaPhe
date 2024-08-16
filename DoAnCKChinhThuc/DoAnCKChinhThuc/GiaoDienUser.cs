using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCKChinhThuc
{
    public partial class GiaoDienUser : Form
    {
        public GiaoDienUser()
        {
            InitializeComponent();
        }
        private string hoTen;
        private string taiKhoan;

        public GiaoDienUser(string taiKhoan, string hoTen)
        {
            InitializeComponent();
            this.hoTen = hoTen;
            this.taiKhoan = taiKhoan;
        }
        private void GiaoDienAdmin_Load(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelTenChucNang_Click(object sender, EventArgs e)
        {

        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            DoiMatKhauDangNhap dmk = new DoiMatKhauDangNhap();
            dmk.Show();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
                DangNhap dangNhap = new DangNhap();
                dangNhap.Show();
            }
        }

        private void GiaoDienUser_Load(object sender, EventArgs e)
        {
            labelChao.Text = "Chào : " + hoTen;
        }

    }
}
