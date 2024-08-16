using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DoAnCKChinhThuc
{
    class DBConnect
    {
        //Thuoc tinh
        SqlConnection conn;
        string chuoiketnoi = "Data Source = DESKTOP-1USN3F4\\SQLEXPRESS01; Initial Catalog = DoAnQuanLyBanCoffee; Integrated Security = True";
        //Phuong thuc khoi tao
        public DBConnect()
        {
            conn = new SqlConnection(chuoiketnoi);
        }
        public DBConnect(string chuoikn)
        {
            conn = new SqlConnection(chuoikn);
        }
        //Phuong thuc xu ly
        public void Open()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        public void Close()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        public int getNonQuery(string chuoitruyvan)
        {
            Open();
            SqlCommand cmd = new SqlCommand(chuoitruyvan, conn);
            //THuc thi
            int kq = cmd.ExecuteNonQuery();
            Close();
            return kq;
        }
        public object getExcuteScalar(string chuoitruyvan)
        {
            Open();
            SqlCommand cmd = new SqlCommand(chuoitruyvan, conn);
            //THuc thi
            object kq = cmd.ExecuteScalar();
            Close();
            return kq;
        }
        public SqlDataReader getDataReader(string chuoitruyvan)
        {
            Open();
            SqlCommand cmd = new SqlCommand(chuoitruyvan, conn);
            //THuc thi
            SqlDataReader rd = cmd.ExecuteReader();
            Close();
            return rd;
        }
        public DataTable getDataTable(string chuoiTruyVan)
        {
            SqlDataAdapter da = new SqlDataAdapter(chuoiTruyVan, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
        public int updateDataTable(DataTable dtnew, string cautruyvan)
        {
            SqlDataAdapter da = new SqlDataAdapter(cautruyvan, conn); //Du lieu trong sql
            SqlCommandBuilder cb = new SqlCommandBuilder(da); //Cap nhat trong csdl

            int kq = da.Update(dtnew); //luu du lieu moi vao sql || Cap nhat trong dataset
            return kq;
        }
    }

}
