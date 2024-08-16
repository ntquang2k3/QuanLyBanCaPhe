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
    public partial class DoiMatKhauDangNhap : Form
    {
        public DoiMatKhauDangNhap()
        {
            InitializeComponent();
        }
        private string taiKhoan;
        private string hoTen;
        public DoiMatKhauDangNhap(string taiKhoan, string hoTen)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoan;
            this.hoTen = hoTen;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                    if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtMatKhauCu.Text) || string.IsNullOrEmpty(txtMatKhauMoi.Text))
                    {
                        throw new Exception("Vui lòng nhập đầy đủ dữ liệu");                       
                    }
                    else
                        if (txtMatKhauMoi.Text.Length < 5)
                        {
                            throw new Exception("Mật khẩu mới phải trên từ 5 đến 18 kí tự");
                        }
                    string cautruyvan = "Select * from NHANVIEN where MaNV = '" + taiKhoan + "'";

                    DBConnect db = new DBConnect();
                    DataTable dt = db.getDataTable(cautruyvan);

                    if (txtMatKhauCu.Text == dt.Rows[0]["MatKhau"].ToString())
                    {
                        dt.Rows[0]["MatKhau"] = txtMatKhauMoi.Text;
                        db.updateDataTable(dt, cautruyvan);
                        MessageBox.Show("Cập nhật thành công");
                    }
                    else
                    {
                        MessageBox.Show("Không thành công /n Mật khẩu cũ không đúng");
                    }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DoiMatKhauDangNhap_Load(object sender, EventArgs e)
        {
            txtHoTen.Text = hoTen;
            txtHoTen.Enabled = false;
            txtMatKhauCu.Focus();
        }

        private void txtMatKhauMoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Kích hoạt sự kiện Click của button
                btnCapNhat.PerformClick();
            }
        }
    }
}
