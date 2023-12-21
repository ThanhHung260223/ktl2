using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLcuahangbanmaytinh
{
    public partial class frmhoadonnhap : Form
    {
        public frmhoadonnhap()
        {
            InitializeComponent();
        }
        ConnectCSDL co = new ConnectCSDL();
        public void LoadData()
        {
            co.KetNoi();
            dgvDShdn.DataSource = co.GetData("select * from Hoadonnhap");
            co.NgatKetNoi();
        }

        private void frmhoadonnhap_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadData();
            cbomamaytinh.DataSource = co.GetData("select * from SanPham");
            cbomamaytinh.ValueMember = "MaSP";
            cbomamaytinh.DisplayMember = "MaSP";

            cbomancc.DataSource = co.GetData("select * from NhaCungCap");
            cbomancc.ValueMember = "MaNCC";
            cbomancc.DisplayMember = "MaNCC";

        }

        private void btnlammoi_Click(object sender, EventArgs e)
        {
            this.txtmahoadonnhap.Clear();
            this.txtdiachi.Clear();
            this.txtdongia.Clear();
            this.txtsodienthoai.Clear();
            this.txtsoluong.Clear();
            this.txtTongtien.Clear();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {

            double soluong = double.Parse(txtsoluong.Text);
            double dongia = double.Parse(txtdongia.Text);
            double tongtien = soluong * dongia;
            txtTongtien.Text = tongtien.ToString();
            co.KetNoi();
            string ktra = cbomamaytinh.SelectedValue.ToString();
            string sqlTK = "select * from Kho where MaKho ='" + txtKho.Text + "'";
            DataTable dt = co.GetData(sqlTK);
            DataRow kq = null;
            if(dt != null&& dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    string kt = row[2].ToString();
                    if(kt == ktra)
                    {
                        kq = row;
                    }
                }
            }
            if(kq != null)
            {
                string sqlthem = "insert into HoaDonNhap values ('" + txtmahoadonnhap.Text + "','" + txtmanv.Text +
                "','" + cbomamaytinh.SelectedValue + "','" + txtKho.Text + "','" + cbomancc.SelectedValue + "','" + txtsoluong.Text + "','" + mtbNgaynhap.Text +
                "','" + txtdiachi.Text + "','" + txtsodienthoai.Text + "','" + txtdongia.Text + "','" + txtTongtien.Text + "')";
                co.ThucThi(sqlthem);
                string sqlcapnhat = "update Kho set SoLuong = SoLuong + '" + txtsoluong.Text + "' where MaKho ='" + txtKho.Text + "' and MaSP ='" + cbomamaytinh.SelectedValue + "'";
                co.ThucThi(sqlcapnhat);
            }
            else
            {
                MessageBox.Show("Kho không có sản phẩm đó để cập nhật\nYêu cầu thêm sản phẩm vào kho trước khi cập nhật", "", MessageBoxButtons.OK);
            }
            frmhoadonnhap_Load(sender, e);
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            int soluong = int.Parse(txtsoluong.Text), soluongcu = int.Parse(txtSoLuong1.Text);
            double dongia = double.Parse(txtdongia.Text);
            double tongtien = soluong * dongia;
            txtTongtien.Text = tongtien.ToString();
            co.KetNoi();
            string sqlsua = "update HoaDonNhap set MaHDN='" + txtmahoadonnhap.Text + "',MaNV='" + txtmanv.Text +
                "',MaNCC='" + cbomancc.SelectedValue + "',SoLuong='" + txtsoluong.Text + "',NgayNhap='" + mtbNgaynhap.Text + "',DiaChi='" + txtdiachi.Text +
                "',SDT='" + txtsodienthoai.Text + "',DonGia='" + txtdongia.Text + "',TongTien='" + txtTongtien.Text + "' where MaHDN='" + txtmahoadonnhap.Text + "'";
            co.ThucThi(sqlsua);
            int kq = soluongcu - soluong;
            if (kq > 0)
            {
                string sqlcapnhat = "update Kho set SoLuong = SoLuong + '" + kq.ToString() + "' where MaKho ='" + txtKho.Text + "' and MaSP ='" + cbomamaytinh.SelectedValue + "'";
                co.ThucThi(sqlcapnhat);
            }else if (kq < 0)
            {
                kq = Math.Abs(kq);
                string sqlcapnhat = "update Kho set SoLuong = SoLuong - '" + kq.ToString() + "' where MaKho ='" + txtKho.Text + "' and MaSP ='" + cbomamaytinh.SelectedValue + "'";
                co.ThucThi(sqlcapnhat);
            }
            LoadData();
        }

        private void dgvDShdn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtmahoadonnhap.Text = dgvDShdn.Rows[i].Cells[0].Value.ToString().Trim();
            txtmanv.Text = dgvDShdn.Rows[i].Cells[1].Value.ToString().Trim();
            cbomamaytinh.Text = dgvDShdn.Rows[i].Cells[2].Value.ToString().Trim();
            cbomancc.Text = dgvDShdn.Rows[i].Cells[4].Value.ToString().Trim();
            txtsoluong.Text = dgvDShdn.Rows[i].Cells[5].Value.ToString().Trim();
            txtSoLuong1.Text = dgvDShdn.Rows[i].Cells[5].Value.ToString().Trim();
            mtbNgaynhap.Text = dgvDShdn.Rows[i].Cells[6].Value.ToString().Trim();
            txtdiachi.Text = dgvDShdn.Rows[i].Cells[7].Value.ToString().Trim();
            txtsodienthoai.Text = dgvDShdn.Rows[i].Cells[8].Value.ToString().Trim();
            txtdongia.Text = dgvDShdn.Rows[i].Cells[9].Value.ToString().Trim();
            txtKho.Text = dgvDShdn.Rows[i].Cells[3].Value.ToString().Trim();
            txtTongtien.Text = (double.Parse(txtdongia.Text)*int.Parse(txtsoluong.Text)).ToString();
        }

        private void btnquaylai_Click(object sender, EventArgs e)
        {
            frmMenu tc = new frmMenu();
            tc.Show();
            this.Hide();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn có chắc muốn thoát không?", "Trả lời",
           MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK)
                Application.Exit();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn có chắc muốn xóa không?", "Trả lời",
           MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK)
            {
                string sqlcapnhat = "update Kho set SoLuong = SoLuong - '" + txtsoluong.Text + "' where MaKho ='" + txtKho.Text + "' and MaSP ='" + cbomamaytinh.SelectedValue + "'";
                co.ThucThi(sqlcapnhat);
                string sqlxoa = "delete from HoaDonNhap where MaHDN = '" + txtmahoadonnhap.Text + "'";
                co.ThucThi(sqlxoa);
            }
            frmhoadonnhap_Load(sender, e);
        }
    }
}
