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
    public partial class FormQLNV : Form
    {
        public FormQLNV()
        {
            InitializeComponent();
        }
        bool tonTaiMaNV(string maNV)
        {
            DBConnect db = new DBConnect();
            string cautruyvan = "select count(*) from NHANVIEN where MANV = '" + maNV + "'";
            int soLuong = (int)db.getExcuteScalar(cautruyvan);
            if (soLuong == 0)
            {
                return false;
            }
            else return true;
        }
        string TaoMaNhanVien()
        {
            string maKhu = "NV";

            //Tìm trong bảng BAN có mã như mẫu

            int soDong = dataGridView1.Rows.Count;

            if (soDong > 0) // Đã có bàn
            {
                //Cắt 3 kí tự cuối chuyển sang số rồi cộng thêm 1
                string pn = ((DataTable)dataGridView1.DataSource).Rows[soDong - 1]["MaNV"].ToString();

                int stt = int.Parse(pn.Substring(2, 3)) + 1;

                //Bổ sung vào thêm cho đầy đủ ký tự
                if (stt < 10)
                {
                    maKhu += "00" + stt;
                }
                else if (stt < 100)
                {
                    maKhu += "0" + stt;
                }
                else
                {
                    maKhu += stt;
                }
            }
            else //Ngược lại thì ngày chưa có cái mã phiếu nhập nào
            {
                maKhu += "001";
            }

            return maKhu;
        }
        void KhoiTaoComboboxPhanQuyen()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PhanQuyen");
            DataRow dr = dt.NewRow();
            dr["PhanQuyen"] = "Admin";
            dt.Rows.Add(dr);
            DataRow dr2 = dt.NewRow();
            dr2["PhanQuyen"] = "User";
            dt.Rows.Add(dr2);

            cbbQuyen.DataSource = dt;
            cbbQuyen.DisplayMember = "PhanQuyen";
            cbbQuyen.ValueMember = "PhanQuyen";
        }
        void KhoiTaoComboboxGioiTinh()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("GioiTinh");
            DataRow dr = dt.NewRow();
            dr["GioiTinh"] = "Nam";
            dt.Rows.Add(dr);
            DataRow dr2 = dt.NewRow();
            dr2["GioiTinh"] = "Nữ";
            dt.Rows.Add(dr2);

            cbbGioiTinh.DataSource = dt;
            cbbGioiTinh.DisplayMember = "GioiTinh";
            cbbGioiTinh.ValueMember = "GioiTinh";
        }

        void HienThiDSNV()
        {
            DBConnect db = new DBConnect();
            string cauTruyVan = "select * from NHANVIEN";
            //Thuc thi
            DataTable dt = db.getDataTable(cauTruyVan);

            //Do du lieu len datagridview
            dataGridView1.DataSource = dt;
        }

        void DatabingdingNV(DataTable dt)
        {
            // Xóa DataBindings trước khi liên kết mới
            txtMaNV.DataBindings.Clear();
            txtTenNV.DataBindings.Clear();
            txtMatKhau.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            txtDC.DataBindings.Clear();
            //txtTrangThai.DataBindings.Clear();
            cbbQuyen.DataBindings.Clear();
            cbbGioiTinh.DataBindings.Clear();
            //Liên kết dữ liệu với text box
            txtMaNV.DataBindings.Add("Text", dt, "MaNV");
            txtTenNV.DataBindings.Add("Text", dt, "TenNV");
            txtMatKhau.DataBindings.Add("Text", dt, "MatKhau");
            txtSDT.DataBindings.Add("Text", dt, "SoDienThoai");
            txtDC.DataBindings.Add("Text", dt, "DiaChi");
            // Liên kết dữ liệu cho ComboBox
            cbbQuyen.DataBindings.Add("SelectedValue", dt, "PhanQuyen");
            cbbGioiTinh.DataBindings.Add("SelectedValue", dt, "GioiTinh");// Liên kết dữ liệu từ DataTable dt với ComboBox
        }

        void loadGridNV()
        {
            DatabingdingNV((DataTable)dataGridView1.DataSource);
        }
        private void FormQLNV_Load(object sender, EventArgs e)
        {
            HienThiDSNV();
            KhoiTaoComboboxGioiTinh();
            KhoiTaoComboboxPhanQuyen();
            loadGridNV();
            btnHuyThem.Visible = false;
        }

        private void txtTimkiem_Click(object sender, EventArgs e)
        {
            txtTimkiem.Clear();
        }

        private void txtTimkiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimkiem.Text))
            {
                txtTimkiem.Text = "        Nhập mã nhân viên hoặc SĐT";
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string cauTruyVan;
            if (txtTimkiem.Text.ToString() == "        Nhập mã nhân viên hoặc SĐT")
            {
                cauTruyVan = "select * from NHANVIEN";
            }
            else
            {
                cauTruyVan = "select * from NHANVIEN where MaNV = '" + txtTimkiem.Text + "' or SoDienThoai like N'%" + txtTimkiem.Text + "%'";
            }
            DBConnect db = new DBConnect();
            //Thuc thi
            DataTable dt = db.getDataTable(cauTruyVan);
            //Them khoa chinh
            DataColumn[] key = new DataColumn[1];
            key[0] = dt.Columns["MaNV"];

            dt.PrimaryKey = key;
            //Do du lieu len datagridview
            dataGridView1.DataSource = dt;
            loadGridNV();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnThem.Tag == "Đã Click")
                {
                    // Xóa DataBindings trước khi liên kết mới
                    txtMaNV.DataBindings.Clear();
                    txtTenNV.DataBindings.Clear();
                    txtMatKhau.DataBindings.Clear();
                    txtSDT.DataBindings.Clear();
                    txtDC.DataBindings.Clear();
                    //txtTrangThai.DataBindings.Clear();
                    cbbQuyen.DataBindings.Clear();
                    cbbGioiTinh.DataBindings.Clear();

                    if (string.IsNullOrEmpty(txtTenNV.Text) || string.IsNullOrEmpty(txtDC.Text) || string.IsNullOrEmpty(txtSDT.Text) || string.IsNullOrEmpty(txtMatKhau.Text) || cbbQuyen.SelectedIndex == -1 || cbbGioiTinh.SelectedIndex == -1)
                    {
                        throw new Exception("Vui lòng nhập đầy đủ dữ liệu");
                    }

                    // Tạo một DataRow mới và thêm dữ liệu vào
                    DataTable dt = (DataTable)dataGridView1.DataSource;
                    DataRow newRow = dt.NewRow();
                    newRow["MaNV"] = txtMaNV.Text;
                    newRow["TenNV"] = txtTenNV.Text;
                    newRow["DiaChi"] = txtDC.Text;
                    newRow["SoDienThoai"] = txtSDT.Text;
                    newRow["MatKhau"] = txtMatKhau.Text;
                    newRow["PhanQuyen"] = cbbQuyen.SelectedValue.ToString();
                    newRow["GioiTinh"] = cbbGioiTinh.SelectedValue;

                    // Thêm DataRow vào DataTable
                    dt.Rows.Add(newRow);

                    // Gán DataTable làm DataSource cho DataGridView
                    dataGridView1.DataSource = dt;
                    btnThem.Text = "Thêm";
                    loadGridNV();
                    btnThem.Tag = "Chưa Click";
                    btnHuyThem.Visible = false;
                    txtMaNV.Enabled = true;
                }
                else if (btnThem.Tag == "Chưa Click")
                {
                    // Xóa DataBindings trước khi liên kết mới
                    txtMaNV.DataBindings.Clear();
                    txtTenNV.DataBindings.Clear();
                    txtMatKhau.DataBindings.Clear();
                    txtSDT.DataBindings.Clear();
                    txtDC.DataBindings.Clear();
                    //txtTrangThai.DataBindings.Clear();
                    cbbQuyen.DataBindings.Clear();
                    cbbGioiTinh.DataBindings.Clear();

                    txtMaNV.Enabled = false;

                    txtMaNV.Text = TaoMaNhanVien();
                    txtTenNV.Text = "";
                    txtTenNV.Focus();
                    txtDC.Text = "";
                    txtSDT.Text = "";
                    txtMatKhau.Text = "";
                    cbbGioiTinh.SelectedIndex = -1;
                    cbbQuyen.SelectedIndex = -1;
                    btnThem.Text = "Xong";
                    btnThem.Tag = "Đã Click";
                    btnHuyThem.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHuyThem_Click(object sender, EventArgs e)
        {
            loadGridNV();
            btnThem.Text = "Thêm";
            btnThem.Tag = "Chưa Click";
            btnHuyThem.Visible = false;
            txtMaNV.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Lấy dòng đang chọn
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // Lấy DataTable từ DataSource của DataGridView (giả sử DataSource là DataTable)
                    DataTable dataTable = (DataTable)dataGridView1.DataSource;

                    // Xác định index của dòng đang chọn trong DataTable
                    int rowIndex = dataGridView1.SelectedRows[0].Index;

                    string maNhanVien = txtMaNV.Text;

                    //Kiểm tra khóa ngoại trong bảng HOADON
                    string ctv = "select count(*) from HOADON where MaNV = '" + maNhanVien + "'";

                    DBConnect db = new DBConnect();
                    int soLuong = (int)db.getExcuteScalar(ctv);
                    if (soLuong != 0)
                    {
                        MessageBox.Show("Tồn tại khóa ngoại với HOADON nên không thể xóa");
                        return;
                    }

                    // Kiểm tra index có hợp lệ không
                    if (rowIndex >= 0 && rowIndex < dataGridView1.Rows.Count)
                    {
                        // Xóa dòng từ DataSource
                        dataGridView1.Rows.RemoveAt(rowIndex);

                        // Gán lại DataSource mới cho DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn LƯU cơ sở dữ liệu này ?", "Xác nhận LƯU", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DBConnect db = new DBConnect();
                string cauTruyVan = "Select * from NHANVIEN";
                DataTable dt = (DataTable)dataGridView1.DataSource;
                int kq = db.updateDataTable(dt, cauTruyVan);
                if (kq != 0)
                {
                    MessageBox.Show("Lưu dữ liệu thành công");
                }
                else
                {
                    MessageBox.Show("Lưu dữ liệu không thành công");
                }
            }
        }

    }
}
