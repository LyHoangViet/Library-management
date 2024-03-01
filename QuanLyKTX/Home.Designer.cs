namespace QuanLyKTX
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btCsvc = new System.Windows.Forms.Button();
            this.btNhanVien = new System.Windows.Forms.Button();
            this.btDangXuat = new System.Windows.Forms.Button();
            this.btChiPhi = new System.Windows.Forms.Button();
            this.btPhong = new System.Windows.Forms.Button();
            this.btCapNhat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Pic = new Guna.UI2.WinForms.Guna2PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox1.Controls.Add(this.btCsvc);
            this.groupBox1.Controls.Add(this.btNhanVien);
            this.groupBox1.Controls.Add(this.btDangXuat);
            this.groupBox1.Controls.Add(this.btChiPhi);
            this.groupBox1.Controls.Add(this.btPhong);
            this.groupBox1.Controls.Add(this.btCapNhat);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(-2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 794);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chức năng";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btCsvc
            // 
            this.btCsvc.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btCsvc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCsvc.Location = new System.Drawing.Point(34, 437);
            this.btCsvc.Name = "btCsvc";
            this.btCsvc.Size = new System.Drawing.Size(110, 41);
            this.btCsvc.TabIndex = 5;
            this.btCsvc.Text = "CSVC";
            this.btCsvc.UseVisualStyleBackColor = false;
            this.btCsvc.Click += new System.EventHandler(this.btCsvc_Click);
            // 
            // btNhanVien
            // 
            this.btNhanVien.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btNhanVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btNhanVien.Location = new System.Drawing.Point(34, 258);
            this.btNhanVien.Name = "btNhanVien";
            this.btNhanVien.Size = new System.Drawing.Size(110, 41);
            this.btNhanVien.TabIndex = 4;
            this.btNhanVien.Text = "Nhân Viên";
            this.btNhanVien.UseVisualStyleBackColor = false;
            this.btNhanVien.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // btDangXuat
            // 
            this.btDangXuat.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btDangXuat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDangXuat.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btDangXuat.Location = new System.Drawing.Point(34, 531);
            this.btDangXuat.Name = "btDangXuat";
            this.btDangXuat.Size = new System.Drawing.Size(110, 41);
            this.btDangXuat.TabIndex = 1;
            this.btDangXuat.Text = "Đăng Xuất";
            this.btDangXuat.UseVisualStyleBackColor = false;
            this.btDangXuat.Click += new System.EventHandler(this.btDangXuat_Click);
            // 
            // btChiPhi
            // 
            this.btChiPhi.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btChiPhi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btChiPhi.Location = new System.Drawing.Point(34, 347);
            this.btChiPhi.Name = "btChiPhi";
            this.btChiPhi.Size = new System.Drawing.Size(110, 41);
            this.btChiPhi.TabIndex = 0;
            this.btChiPhi.Text = "Thanh Toán";
            this.btChiPhi.UseVisualStyleBackColor = false;
            this.btChiPhi.Click += new System.EventHandler(this.btChiPhi_Click);
            // 
            // btPhong
            // 
            this.btPhong.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btPhong.Location = new System.Drawing.Point(34, 81);
            this.btPhong.Name = "btPhong";
            this.btPhong.Size = new System.Drawing.Size(110, 41);
            this.btPhong.TabIndex = 0;
            this.btPhong.Text = "Phòng";
            this.btPhong.UseVisualStyleBackColor = false;
            this.btPhong.Click += new System.EventHandler(this.btPhong_Click);
            // 
            // btCapNhat
            // 
            this.btCapNhat.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btCapNhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCapNhat.Location = new System.Drawing.Point(34, 170);
            this.btCapNhat.Name = "btCapNhat";
            this.btCapNhat.Size = new System.Drawing.Size(110, 41);
            this.btCapNhat.TabIndex = 0;
            this.btCapNhat.Text = "Sinh Viên";
            this.btCapNhat.UseVisualStyleBackColor = false;
            this.btCapNhat.Click += new System.EventHandler(this.btTimKiem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(165, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(820, 51);
            this.label1.TabIndex = 3;
            this.label1.Text = "CHƯƠNG TRÌNH QUẢN LÝ KÝ TÚC XÁ";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(186, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1191, 122);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // Pic
            // 
            this.Pic.FillColor = System.Drawing.Color.Silver;
            this.Pic.Image = ((System.Drawing.Image)(resources.GetObject("Pic.Image")));
            this.Pic.ImageRotate = 0F;
            this.Pic.Location = new System.Drawing.Point(186, 129);
            this.Pic.Name = "Pic";
            this.Pic.Size = new System.Drawing.Size(1191, 666);
            this.Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic.TabIndex = 2;
            this.Pic.TabStop = false;
            this.Pic.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1379, 794);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Pic);
            this.Controls.Add(this.groupBox1);
            this.IsMdiContainer = true;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btChiPhi;
        private System.Windows.Forms.Button btPhong;
        private System.Windows.Forms.Button btCapNhat;
        private Guna.UI2.WinForms.Guna2PictureBox Pic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btDangXuat;
        private System.Windows.Forms.Button btNhanVien;
        private System.Windows.Forms.Button btCsvc;
    }
}

