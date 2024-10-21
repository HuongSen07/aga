namespace _2280602731_NguyenThiHuongSen
{
    partial class frmTruyVet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvTruyVet = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbTruyVetBN = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTruyVet)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTruyVet
            // 
            this.dgvTruyVet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTruyVet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTruyVet.Location = new System.Drawing.Point(12, 204);
            this.dgvTruyVet.Name = "dgvTruyVet";
            this.dgvTruyVet.Size = new System.Drawing.Size(782, 225);
            this.dgvTruyVet.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbTruyVetBN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(84, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(580, 111);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Truy vết theo nguồn lây nhiễm từ";
            // 
            // cmbTruyVetBN
            // 
            this.cmbTruyVetBN.FormattingEnabled = true;
            this.cmbTruyVetBN.Location = new System.Drawing.Point(242, 53);
            this.cmbTruyVetBN.Name = "cmbTruyVetBN";
            this.cmbTruyVetBN.Size = new System.Drawing.Size(293, 21);
            this.cmbTruyVetBN.TabIndex = 1;
            this.cmbTruyVetBN.SelectedIndexChanged += new System.EventHandler(this.cmbTruyVetBN_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(98, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bệnh nhân";
            // 
            // frmTruyVet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvTruyVet);
            this.Name = "frmTruyVet";
            this.Text = "Truy vết";
            this.Load += new System.EventHandler(this.frmTruyVet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTruyVet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTruyVet;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbTruyVetBN;
        private System.Windows.Forms.Label label1;
    }
}