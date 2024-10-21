using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2280602731_NguyenThiHuongSen
{
    public partial class frmQLTTBN : Form
    {
        public frmQLTTBN()
        {
            InitializeComponent();

        }



        private void frmQLTTBN_Load(object sender, EventArgs e)
        {


            LoadTinhTrang();
            LoadBenhNhan();
            dgvBN.Columns["BNTXG"].Visible = false;
           
            
        }



        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmQLTTBN_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // Hủy bỏ việc đóng form nếu người dùng chọn "Không"
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvBN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectrow = dgvBN.Rows[e.RowIndex];
                txtMaBN.Text = selectrow.Cells["MaBN"].Value.ToString();
                txtTenBN.Text = selectrow.Cells["TenBN"].Value.ToString();
                cmbTinhTrang.Text = selectrow.Cells["TenTT"].Value.ToString();
                rtbGhiChu.Text = selectrow.Cells["Ghichu"].Value.ToString();
                cmbLayNhiemTu.Text = selectrow.Cells["BNTXG"].Value?.ToString() ?? string.Empty;
            }
        }
        private void LoadTinhTrang()
        {
            
            using (CovidModel db = new CovidModel())
            {
                var listTT = db.TinhTrangs.ToList();
                cmbTinhTrang.Items.Clear();
                foreach (var item in listTT)
                {
                    cmbTinhTrang.Items.Add(item.TenTT);
                }
                cmbTinhTrang.SelectedIndex = 0;
            }
        }

        private void LoadBenhNhan()
        {
            

            using (CovidModel db = new CovidModel())
            {
                var listBN = db.BenhNhans.ToList();
                cmbLayNhiemTu.Items.Clear();

                foreach (var item in listBN)
                {
                    cmbLayNhiemTu.Items.Add(item.MaBN);
                }

                // Tạo danh sách bệnh nhân với tính toán cấp F
                var patientData = new List<object>();

                foreach (var bn in listBN)
                {
                    string fLevel = "F0"; // Giả định mặc định là F0

                    if (!string.IsNullOrEmpty(bn.BNTXG))
                    {
                        int level = 1;
                        string currentBNTXG = bn.BNTXG;

                        while (!string.IsNullOrEmpty(currentBNTXG))
                        {
                            var infectedPatient = listBN.FirstOrDefault(b => b.MaBN == currentBNTXG);
                            if (infectedPatient == null) break;

                            // Nếu bệnh nhân này cũng có nguồn lây nhiễm, tăng cấp F
                            if (!string.IsNullOrEmpty(infectedPatient.BNTXG))
                            {
                                level++;
                                currentBNTXG = infectedPatient.BNTXG;
                            }
                            else
                            {
                                // Nếu không còn nguồn lây nhiễm, thoát vòng lặp
                                break;
                            }
                        }

                        fLevel = "F" + level; // Cập nhật giá trị F
                    }

                    patientData.Add(new
                    {
                        bn.MaBN,
                        bn.TenBN,
                        bn.TinhTrang.TenTT,
                        bn.GhiChu,
                        BNTXG = bn.BNTXG ?? string.Empty, // Cập nhật cột BNTXG
                        F = fLevel // Thêm cấp F vào danh sách
                    });
                }

                dgvBN.DataSource = patientData.ToList();

                dgvBN.Columns[0].HeaderText = "Mã BN";
                dgvBN.Columns[1].HeaderText = "Tên BN";
                dgvBN.Columns[2].HeaderText = "Tình Trạng";
                dgvBN.Columns[3].Visible = false; // Ẩn cột Ghi Chú
                dgvBN.Columns[4].HeaderText = "BNTXG"; // Cột lây nhiễm từ
                dgvBN.Columns[5].HeaderText = "F"; // Cột F
                dgvBN.CellClick += dgvBN_CellClick;
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            

            try
            {
                // Kiểm tra thông tin bắt buộc
                if (string.IsNullOrWhiteSpace(txtMaBN.Text) || string.IsNullOrWhiteSpace(txtTenBN.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin bệnh nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra độ dài mã bệnh nhân
                if (txtMaBN.Text.Length != 6)
                {
                    MessageBox.Show("Mã bệnh nhân phải có 6 kí tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra lây nhiễm từ chính mình
                if (cmbLayNhiemTu.SelectedItem != null && cmbLayNhiemTu.SelectedItem.ToString() == txtMaBN.Text)
                {
                    MessageBox.Show("Bệnh nhân không thể lây nhiễm từ chính mình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (CovidModel db = new CovidModel())
                {
                    // Kiểm tra xem mã bệnh nhân đã tồn tại trong CSDL chưa
                    var existingPatient = db.BenhNhans.SingleOrDefault(bn => bn.MaBN == txtMaBN.Text);

                    if (existingPatient == null)
                    {
                        // Thêm mới bệnh nhân
                        BenhNhan newPatient = new BenhNhan
                        {
                            MaBN = txtMaBN.Text,
                            TenBN = txtTenBN.Text,
                            GhiChu = rtbGhiChu.Text,
                            TinhTrang = db.TinhTrangs.FirstOrDefault(tt => tt.TenTT == cmbTinhTrang.Text),
                            BNTXG = cmbLayNhiemTu.SelectedValue?.ToString() // Lây nhiễm từ
                        };

                        db.BenhNhans.Add(newPatient);
                    }
                    else
                    {
                        // Cập nhật thông tin bệnh nhân
                        existingPatient.TenBN = txtTenBN.Text;
                        existingPatient.GhiChu = rtbGhiChu.Text;
                        existingPatient.TinhTrang = db.TinhTrangs.FirstOrDefault(tt => tt.TenTT == cmbTinhTrang.Text);
                        existingPatient.BNTXG = cmbLayNhiemTu.SelectedValue?.ToString(); // Lây nhiễm từ
                    }

                    // Lưu thay đổi vào CSDL
                    db.SaveChanges();

                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Load lại thông tin ComboBox "Lây nhiễm từ"
                    LoadBenhNhan();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void truyVếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTruyVet frmTruyVet = new frmTruyVet();
            frmTruyVet.ShowDialog();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == (Keys.Control | Keys.T))
            {
                truyVếtToolStripMenuItem_Click(null,null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        
    }
}


