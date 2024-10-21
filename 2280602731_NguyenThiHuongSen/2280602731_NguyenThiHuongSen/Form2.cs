using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2280602731_NguyenThiHuongSen
{
    public partial class frmTruyVet : Form
    {
        private CovidModel db;
        public frmTruyVet()
        {
            db = new CovidModel();
            InitializeComponent();
        }

        private void frmTruyVet_Load(object sender, EventArgs e)
        {
            var listBN = db.BenhNhans.ToList();
            cmbTruyVetBN.Items.Clear();
            foreach (var bn in listBN)
            {
                cmbTruyVetBN.Items.Add($"{bn.MaBN}: {bn.TenBN}");
            }
        }

        private void cmbTruyVetBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBN = cmbTruyVetBN.SelectedItem.ToString();
            string maBN = selectedBN.Split(':')[0].Trim();

            // Truy vết bệnh nhân đã bị lây nhiễm từ bệnh nhân này
            var truyVetList = db.BenhNhans
                .Where(bn => bn.BNTXG == maBN)
                .Select(bn => new
                {
                    bn.MaBN,
                    bn.TenBN,
                    bn.BNTXG // Chỉ lấy mã bệnh nhân lây nhiễm
                }).ToList();

            // Tạo danh sách kết quả với thông điệp nguyên nhân
            var resultList = truyVetList.Select(bn => new
            {
                bn.MaBN,
                bn.TenBN,

                NguyenNhan = $"do đã tiếp xúc gần với bệnh nhân {bn.BNTXG}"
            }).ToList();

            // Hiển thị kết quả trong DataGridView
            dgvTruyVet.DataSource = resultList;
            dgvTruyVet.Columns[0].HeaderText = "Mã BN";
            dgvTruyVet.Columns[1].HeaderText = "Tên BN";
            dgvTruyVet.Columns[1].HeaderText = "Tên BN";
            dgvTruyVet.Columns[2].HeaderText = "Nguyên Nhân";
        }
        
    }
}

