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
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }
        private string hoTen;
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            string cauTruyVan = "select * from NHANVIEN where MaNV = '"+txtTenDangNhap.Text+"' and MatKhau = '"+txtMatKhau.Text+"' ";
            DataTable dt = db.getDataTable(cauTruyVan);
            if (dt.Rows.Count > 0) // Có đăng nhập được
            {
                hoTen = dt.Rows[0]["TenNV"].ToString();
                if (dt.Rows[0]["PhanQuyen"].ToString() == "Admin")
                {
                    GiaoDienAdmin giaodien = new GiaoDienAdmin(txtTenDangNhap.Text,hoTen);
                    giaodien.Show();
                    this.Hide();
                }
                else
                {
                    GiaoDienUser giaodien = new GiaoDienUser(txtTenDangNhap.Text,hoTen);
                    giaodien.Show();
                    this.Hide();
                }
            }
            else //Đăng nhập không được
            MessageBox.Show("Thông tin đăng nhập không chính xác \n Vui lòng đăng nhập lại", "Sai thông tin đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Ban co muon thoat ?", "Thoat", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
                e.Cancel = true;
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Kích hoạt sự kiện Click của button
                btnDangNhap.PerformClick();
            }
        }
    }
}
