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
    public partial class FormQLNhanVien : Form
    {
        public FormQLNhanVien()
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            string ngayvaolam = maskedTxtNgVaoLam.Text;
            //ngaysinh = String.Format("{0:yyyy-MM-dd}",maskedTxtNgSinh.Text);
            DateTime paresedDate = DateTime.ParseExact(ngayvaolam, "dd/MM/yyyy", null);
            ngayvaolam = paresedDate.ToString("yyyy/MM/dd");
            int admin = 0;
            if (checkBoxAdmin.Checked)
                admin = 1;

            int gioitinh = 0;

            if (rdoNu.Checked)
                gioitinh = 1;

            string cauTruyVanThemNV = "insert into NHANVIEN values(N'"+txtMaNV.Text+"',N'"+txtTenNV.Text+"','"+gioitinh+"',N'"+txtChucVu.Text+"','"+ngayvaolam+"',N'"+txtDC+"',N'"+txtSDT.Text+"','"+admin+"',N'"+txtMatKhau.Text+"')";
            int kq = 0;
            if (tonTaiMaNV(txtMaNV.Text))
            {
                MessageBox.Show("Da ton tai ma nhan vien nen khong the them moi");
            }
            else
            {
                kq = db.getNonQuery(cauTruyVanThemNV);
            }
            if (kq == 1)
            {
                MessageBox.Show("Thuc hien them thanh cong");
                HienThiDSNV();
            }
            else
                MessageBox.Show("Them khong thanh cong ");
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            string deleteString = "";
            deleteString = "delete from NHANVIEN where MANV = '" + txtMaNV.Text + "'";
            int kq = 0;
            if (!tonTaiMaNV(txtMaNV.Text))
            {
                MessageBox.Show("Khong ton tai nhan vien co ma vua nhap nen khong the xoa");
            }
            else
            {
                kq = (int)db.getNonQuery(deleteString);
            }
            //Kiem tra ket noi truoc khi dong
            if (kq == 1)
            {
                MessageBox.Show("Thuc hien xoa thanh cong");
                HienThiDSNV();
            }
            else
            {
                MessageBox.Show("Xoa khong thanh cong ");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

                DBConnect db = new DBConnect();
                string ngayvaolam = maskedTxtNgVaoLam.Text;
                //ngaysinh = String.Format("{0:yyyy-MM-dd}",maskedTxtNgSinh.Text);
                DateTime paresedDate = DateTime.ParseExact(ngayvaolam, "dd/MM/yyyy", null);
                ngayvaolam = paresedDate.ToString("yyyy/MM/dd");
                int admin = 0;
                if (checkBoxAdmin.Checked)
                    admin = 1;

                int gioitinh = 0;

                if (rdoNu.Checked)
                    gioitinh = 1;
                string updateString = "update NHANVIEN set TENNV= N'"+txtTenNV.Text+"',CHUCVU =N'"+txtChucVu.Text+"',GIOITINH = '"+gioitinh+"' ,NGAYVAOLAM = '"+ngayvaolam+"',DIACHI= N'"+txtDC.Text+"',SDT = '"+txtSDT.Text+"',PHANQUYEN='"+admin+"',MATKHAU='"+txtMatKhau.Text+"' where MANV = N'"+txtMaNV.Text+"'";                
                //Goi ExecuteNonQuery de gui command
                int kq = db.getNonQuery(updateString);
                if (kq == 1)
                {
                    MessageBox.Show("Thuc hien sua thanh cong");
                    HienThiDSNV();
                }
                else
                    MessageBox.Show("Sua khong thanh cong do khong ton tai ma nhan vien hoac ten nhan vien");
        }

        private void FormQLNhanVien_Load(object sender, EventArgs e)
        {
            HienThiDSNV();
        }

        void lkDataWithTextBox_dgvSelectedRowChange()
        {
            //txtMaKhoa.DataBindings.Clear();
            //txtTenKhoa.DataBindings.Clear();
            //Lien ket du lieu tren text box voi truong du lieu tuong ung trong datatale
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                txtMaNV.Text = (string)selectedRow.Cells[0].Value;
                txtTenNV.Text = (string)selectedRow.Cells[1].Value;
                if (selectedRow.Cells[2].Value == null || selectedRow.Cells[2].Value.ToString() == "{}")
                {
                    rdoNam.Checked = true;
                    rdoNu.Checked = false;
                }
                else
                {
                    if ((bool)selectedRow.Cells[2].Value == true)
                    {
                        rdoNu.Checked = true;
                    }
                    else
                    {
                        rdoNam.Checked = true;
                    }
                }
                txtChucVu.Text = (string)selectedRow.Cells[3].Value;
                if (selectedRow.Cells[4].Value == null)
                {
                    maskedTxtNgVaoLam.Text = "";
                }
                else
                {
                    DateTime dateTime = new DateTime();
                    dateTime = (DateTime)selectedRow.Cells[4].Value;
                    string formattedDate = dateTime.ToString("dd-MM-yyyy");
                    maskedTxtNgVaoLam.Text = formattedDate;
                }
                
                if (selectedRow.Cells[5].Value == null || selectedRow.Cells[5].Value.ToString() == "{}")
                {
                    txtDC.Text = "";
                }
                else
                {
                    string cellValue = selectedRow.Cells[5].Value.ToString();
                    txtDC.Text = cellValue;     
                }
                txtSDT.Text = (string)selectedRow.Cells[6].Value;

                if (selectedRow.Cells[7].Value == null || selectedRow.Cells[7].Value.ToString() == "{}")
                {
                    checkBoxAdmin.Checked = false;
                }
                else
                {
                    checkBoxAdmin.Checked = (bool)selectedRow.Cells[7].Value;
                }
                txtMatKhau.Text = (string)selectedRow.Cells[8].Value;             
            }
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            lkDataWithTextBox_dgvSelectedRowChange();
        }

    }
}
