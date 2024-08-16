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
    public partial class GiaoDienAdmin : Form
    {
        public GiaoDienAdmin()
        {
            InitializeComponent();
        }
        private string hoTen;
        private string taiKhoan;

        public GiaoDienAdmin(string taiKhoan, string hoTen)
        {
            InitializeComponent();
            this.hoTen = hoTen;
            this.taiKhoan = taiKhoan;
        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild!=null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_Body.Controls.Add(childForm);
            panel_Body.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQLNV());
            label1.Text = button2.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQLNV());
            label1.Text = button2.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQLBan());
            label1.Text = button6.Text;
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            DoiMatKhauDangNhap dmk= new DoiMatKhauDangNhap(taiKhoan,hoTen);
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

        private void GiaoDienAdmin_Load(object sender, EventArgs e)
        {
            button1.PerformClick();
            labelChao.Text = "Chào : " + hoTen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
