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
    public partial class FormQLBan : Form
    {
        public FormQLBan()
        {
            InitializeComponent();
        }
        string TaoMaBan()
        {
            string maBan = "B";

            //Tìm trong bảng BAN có mã như mẫu

            string cauTV = "select top 1 * from BAN order by (MaBan) desc";
            DBConnect db = new DBConnect();
            DataTable dt = db.getDataTable(cauTV);
            string pn;
            pn = dt.Rows[0]["MaBan"].ToString();

            if (dt.Rows.Count > 0) // Đã có bàn
            {
                //Cắt 3 kí tự cuối chuyển sang số rồi cộng thêm 1               
                int stt = int.Parse(pn.Substring(1, 3)) + 1;

                //Bổ sung vào thêm cho đầy đủ ký tự
                if (stt < 10)
                {
                    maBan += "00" + stt;
                }
                else if (stt < 100)
                {
                    maBan += "0" + stt;
                }
                else
                {
                    maBan += stt;
                }
            }
            else //Ngược lại thì ngày chưa có cái mã phiếu nhập nào
            {
                maBan += "001";
            }

            return maBan;
        }

        string TaoMaKhuVuc()
        {
            string maKhu = "KV";

            //Tìm trong bảng BAN có mã như mẫu
            string cauTV = "select top 1 * from KHUVUC order by (MaKV) desc";
            DBConnect db = new DBConnect();
            DataTable dt = db.getDataTable(cauTV);
            string pn;
            pn = dt.Rows[0]["MaKV"].ToString();

            if (dt.Rows.Count > 0) // Đã có bàn
            {
                //Cắt 3 kí tự cuối chuyển sang số rồi cộng thêm 1

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

        bool tonTaiMaBan(string maBan)
        {
            DBConnect db = new DBConnect();
            string cautruyvan = "select count(*) from BAN where MaBan = '" + maBan + "'";
            int soLuong = (int)db.getExcuteScalar(cautruyvan);
            if (soLuong == 0)
            {
                return false;
            }
            else return true;
        }

        void KhoiTaoComboboxTrangThai()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TrangThai");
            DataRow dr = dt.NewRow();
            dr["TrangThai"] = "Trống";
            dt.Rows.Add(dr);
            DataRow dr2 = dt.NewRow();
            dr2["TrangThai"] = "Đang dùng";
            dt.Rows.Add(dr2);

            cbbTrangThai.DataSource = dt;
            cbbTrangThai.DisplayMember = "TrangThai";
            cbbTrangThai.ValueMember = "TrangThai";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnThem.Tag == "Đã Click")
                {
                    // Xóa DataBindings trước khi liên kết mới
                    txtMaBan.DataBindings.Clear();
                    txtTenBan.DataBindings.Clear();            
                    comboBox1.DataBindings.Clear();

                    if (string.IsNullOrEmpty(txtTenBan.Text) || cbbTrangThai.SelectedIndex == -1 || comboBox1.SelectedIndex == -1)
                    {
                        throw new Exception("Vui lòng nhập đầy đủ dữ liệu");
                    }                   
                    
                    // Tạo một DataRow mới và thêm dữ liệu vào
                    DataTable dt = (DataTable)dataGridView1.DataSource;
                    DataRow newRow = dt.NewRow();
                    newRow["MaBan"] = txtMaBan.Text;
                    newRow["TenBan"] = txtTenBan.Text;
                    newRow["TrangThai"] = cbbTrangThai.SelectedValue.ToString();
                    newRow["MaKV"] = comboBox1.SelectedValue;

                    // Thêm DataRow vào DataTable
                    dt.Rows.Add(newRow);

                    // Gán DataTable làm DataSource cho DataGridView
                    dataGridView1.DataSource = dt;
                    btnThem.Text = "Thêm";
                    loadGridBan();
                    btnThem.Tag = "Chưa Click";
                    btnHuyThem.Visible = false;
                    txtMaBan.Enabled = true;
                }
                else if (btnThem.Tag == "Chưa Click")
                {
                    // Xóa DataBindings trước khi liên kết mới
                    txtMaBan.DataBindings.Clear();
                    txtTenBan.DataBindings.Clear();
                    comboBox1.DataBindings.Clear();
                    cbbTrangThai.DataBindings.Clear();

                    txtMaBan.Enabled = false;

                    txtMaBan.Text = TaoMaBan();
                    txtTenBan.Text = "";
                    txtTenBan.Focus();
                    cbbTrangThai.SelectedIndex = -1;
                    comboBox1.SelectedIndex = -1;
                    btnThem.Text = "Hoàn tất thêm";
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
            loadGridBan();
            btnThem.Text = "Thêm";
            btnThem.Tag = "Chưa Click";
            btnHuyThem.Visible = false;
            txtMaBan.Enabled = true;
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

                    string maBan = txtMaBan.Text;

                    //Kiểm tra khóa ngoại trong bảng HOADON
                    string ctv = "select count(*) from HOADON where MaBan = '" + maBan + "'";

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

        private void btnLuuDuLieu_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn LƯU cơ sở dữ liệu này ?", "Xác nhận LƯU", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DBConnect db = new DBConnect();
                string cauTruyVan = "Select * from BAN";
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

        void HienThiDSBan()
        {
            DBConnect db = new DBConnect();
            string cauTruyVan = "select * from BAN";
            //Thuc thi
            DataTable dt = db.getDataTable(cauTruyVan);

            //Do du lieu len datagridview
            dataGridView1.DataSource = dt;
        }

        void HienThiDSKV()
        {
            DBConnect db = new DBConnect();
            string cauTruyVan = "select * from KHUVUC";
            //Thuc thi
            DataTable dt = db.getDataTable(cauTruyVan);

            //Do du lieu len datagridview
            dataGridViewKV.DataSource = dt;
        }

        void Load_ComboBox()
        {
            DBConnect db = new DBConnect();
            string cauTruyVan1 = "select * from KHUVUC";
            //Thuc thi
            DataTable dt1 = db.getDataTable(cauTruyVan1);

            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "TenKV";
            comboBox1.ValueMember = "MaKV";

            KhoiTaoComboboxTrangThai();
        }      

        private void FormQLBan_Load(object sender, EventArgs e)
        {
            HienThiDSBan();
            HienThiDSKV();
            Load_ComboBox();
            loadGridBan();
            loadGridKV();
            btnHuyThem.Visible = false;
            btnHuyThemKV.Visible = false;
        }

        void DatabingdingBan(DataTable dt)
        {
            // Xóa DataBindings trước khi liên kết mới
            txtMaBan.DataBindings.Clear();
            txtTenBan.DataBindings.Clear();
            //txtTrangThai.DataBindings.Clear();
            comboBox1.DataBindings.Clear();
            //Liên kết dữ liệu với text box
            txtMaBan.DataBindings.Add("Text", dt, "MaBan");
            txtTenBan.DataBindings.Add("Text", dt, "TenBan");
            //txtTrangThai.DataBindings.Add("Text", dt, "TrangThai");
            //Liên kết dữ liệu với radio button
            // Liên kết dữ liệu cho ComboBox
            comboBox1.DataBindings.Add("SelectedValue", dt, "MaKV");
            cbbTrangThai.DataBindings.Add("SelectedValue", dt, "TrangThai");// Liên kết dữ liệu từ DataTable dt với ComboBox
        }

        void loadGridBan()
        {
            DatabingdingBan((DataTable)dataGridView1.DataSource);
        }

        void DatabingdingKV(DataTable dt)
        {
            // Xóa DataBindings trước khi liên kết mới
            txtMaKhuVuc.DataBindings.Clear();
            txtTenKhuVuc.DataBindings.Clear();
            //Liên kết dữ liệu với text box
            txtMaKhuVuc.DataBindings.Add("Text", dt, "MaKV");
            txtTenKhuVuc.DataBindings.Add("Text", dt, "TenKV");
        }

        void loadGridKV()
        {
            DatabingdingKV((DataTable)dataGridViewKV.DataSource);
        }

        private void btnThemKhuVuc_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnThemKhuVuc.Tag == "Đã Click")
                {
                    // Xóa DataBindings trước khi liên kết mới
                    txtMaKhuVuc.DataBindings.Clear();
                    txtTenKhuVuc.DataBindings.Clear();

                    if (string.IsNullOrEmpty(txtTenKhuVuc.Text))
                    {
                        throw new Exception("Vui lòng nhập đầy đủ dữ liệu");
                    }

                    // Tạo một DataRow mới và thêm dữ liệu vào
                    DataTable dt = (DataTable)dataGridViewKV.DataSource;
                    DataRow newRow = dt.NewRow();
                    newRow["MaKV"] = txtMaKhuVuc.Text;
                    newRow["TenKV"] = txtTenKhuVuc.Text;

                    // Thêm DataRow vào DataTable
                    dt.Rows.Add(newRow);

                    // Gán DataTable làm DataSource cho DataGridView
                    dataGridViewKV.DataSource = dt;
                    btnThemKhuVuc.Text = "Thêm";
                    loadGridKV();
                    btnThemKhuVuc.Tag = "Chưa Click";
                    btnHuyThemKV.Visible = false;
                    txtMaKhuVuc.Enabled = true;
                }
                else if (btnThem.Tag == "Chưa Click")
                {
                    // Xóa DataBindings trước khi liên kết mới
                    txtMaKhuVuc.DataBindings.Clear();
                    txtTenKhuVuc.DataBindings.Clear();

                    txtMaKhuVuc.Enabled = false;

                    txtMaKhuVuc.Text = TaoMaKhuVuc();
                    txtTenKhuVuc.Text = "";
                    txtTenKhuVuc.Focus();
                    btnThemKhuVuc.Text = "Hoàn tất thêm";
                    btnThemKhuVuc.Tag = "Đã Click";
                    btnHuyThemKV.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaKhuVuc_Click(object sender, EventArgs e)
        {
            if (dataGridViewKV.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {                 
                    // Lấy dòng đang chọn
                    DataGridViewRow selectedRow = dataGridViewKV.SelectedRows[0];

                    // Lấy DataTable từ DataSource của DataGridView (giả sử DataSource là DataTable)
                    DataTable dataTable = (DataTable)dataGridViewKV.DataSource;

                    // Xác định index của dòng đang chọn trong DataTable
                    int rowIndex = dataGridViewKV.SelectedRows[0].Index;

                    string makv = txtMaKhuVuc.Text;

                    //Kiểm tra khóa ngoại trong bảng BAN
                    string ctv = "select count(*) from BAN where MAKV = '"+makv+"'";

                    DBConnect db = new DBConnect();
                    int soLuong = (int)db.getExcuteScalar(ctv);
                    if (soLuong != 0)
                    {
                        MessageBox.Show("Tồn tại khóa ngoại với BÀN nên không thể xóa");
                        return;
                    }
                    // Kiểm tra index có hợp lệ không
                    if (rowIndex >= 0 && rowIndex < dataGridViewKV.Rows.Count)
                    {
                        // Xóa dòng từ DataSource
                        dataGridViewKV.Rows.RemoveAt(rowIndex);

                        // Gán lại DataSource mới cho DataGridView
                        dataGridViewKV.DataSource = dataTable;
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!");
            }
        }

        private void btnLuuDuLieuKhuVuc_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn LƯU cơ sở dữ liệu này ?", "Xác nhận LƯU", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DBConnect db = new DBConnect();
                string cauTruyVan = "Select * from KHUVUC";
                DataTable dt = (DataTable)dataGridViewKV.DataSource;
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

        private void btnHuyThemKV_Click(object sender, EventArgs e)
        {
            loadGridKV();
            btnThemKhuVuc.Text = "Thêm";
            btnThemKhuVuc.Tag = "Chưa Click";
            btnHuyThemKV.Visible = false;
            txtMaKhuVuc.Enabled = true;
        }

        private void txtTenKhuVuc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Kích hoạt sự kiện Click của button
                btnThemKhuVuc.PerformClick();
            }
        }
    }
}
