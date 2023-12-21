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
    public partial class frmQLthongtinmaytinh : Form
    {
        public frmQLthongtinmaytinh()
        {
            InitializeComponent();
        }
        ConnectCSDL co = new ConnectCSDL();
        public void LoadData()
        {
            co.KetNoi();
            dgvmaytinh.DataSource = co.GetData("select * from SanPham");
            co.NgatKetNoi();
        }
        private void btnlammoi_Click(object sender, EventArgs e)
        {

            this.txtmamaytinh.Clear();

            this.txttenmaytinh.Clear();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            string sqlthem = "insert into SanPham values ('" + txtmamaytinh.Text + "','" + txttenmaytinh.Text
                + "','" + cbomancc.Text + "')";

            co.ThucThi(sqlthem);
            string sqlTK = "select * from Kho where MaSP='" + txtmamaytinh.Text + "'";
            DataTable dt = co.GetData(sqlTK);
            if (dt.Rows.Count == 0)
            {
                sqlthem = "insert into Kho values ('KHO00',N'Kho dự trữ','" + txtmamaytinh.Text + "',1)";
                co.ThucThi(sqlthem);
            }
            LoadData();
            frmQLthongtinmaytinh_Load(sender, e);
        }

        private void frmQLthongtinmaytinh_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadData();

            cbomancc.DataSource = co.GetData("select * from NhaCungCap");
            cbomancc.ValueMember = "MaNCC";
            cbomancc.DisplayMember = "MaNCC";
        }

        private void btnsua_Click(object sender, EventArgs e)
        {

            string sqlsua = "update SanPham set MaSP='" + txtmamaytinh.Text + "',TenSP='" + txttenmaytinh.Text
               + "',MaNCC='" + cbomancc.SelectedValue +"' where MaSP ='" + txtmamaytinh.Text + "'";
            co.ThucThi(sqlsua);
            LoadData();

        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn có chắc muốn xóa không?", "Trả lời",
           MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK)
            {
                string sqlxoa = "delete from Kho where MaSP = '" + txtmamaytinh.Text + "'";
                co.ThucThi(sqlxoa);
                DataTable dt = co.GetData("select* from SanPham where MaSP ='" + txtmamaytinh.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    sqlxoa = "delete from SanPham where MaSP = '" + txtmamaytinh.Text + "'";
                    co.ThucThi(sqlxoa);
                }
            }
            frmQLthongtinmaytinh_Load(sender, e);

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

        private void dgvmaytinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtmamaytinh.Text = dgvmaytinh.Rows[i].Cells[0].Value.ToString().Trim();
            txttenmaytinh.Text = dgvmaytinh.Rows[i].Cells[1].Value.ToString().Trim();
            cbomancc.Text = dgvmaytinh.Rows[i].Cells[2].Value.ToString().Trim();

        }

        private void txttktheoten_TextChanged(object sender, EventArgs e)
        {
           
         
            
        }

        
        private void btntktheoma_Click(object sender, EventArgs e)
        {
          
        }

        private void txttktheoma_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void txttktheoten_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void txttktheoma_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}
