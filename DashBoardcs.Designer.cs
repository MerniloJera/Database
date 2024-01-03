namespace HealthCare
{
    partial class DashBoardcs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashBoardcs));
            this.BtnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pbMuntinlupa = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnPInfo = new System.Windows.Forms.Button();
            this.BtnCheckup = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMuntinlupa)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnAdd
            // 
            this.BtnAdd.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.Location = new System.Drawing.Point(110, 221);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(160, 85);
            this.BtnAdd.TabIndex = 0;
            this.BtnAdd.Text = "ADD PATIENT";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pbMuntinlupa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(110, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 100);
            this.panel1.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(133, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Saint Bernard So. Leyte";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(471, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(78, 77);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pbMuntinlupa
            // 
            this.pbMuntinlupa.Image = ((System.Drawing.Image)(resources.GetObject("pbMuntinlupa.Image")));
            this.pbMuntinlupa.Location = new System.Drawing.Point(11, 12);
            this.pbMuntinlupa.Name = "pbMuntinlupa";
            this.pbMuntinlupa.Size = new System.Drawing.Size(78, 77);
            this.pbMuntinlupa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMuntinlupa.TabIndex = 1;
            this.pbMuntinlupa.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(110, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "TAMBIS 2 HEALTH CENTER";
            // 
            // BtnPInfo
            // 
            this.BtnPInfo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPInfo.Location = new System.Drawing.Point(313, 221);
            this.BtnPInfo.Name = "BtnPInfo";
            this.BtnPInfo.Size = new System.Drawing.Size(160, 85);
            this.BtnPInfo.TabIndex = 13;
            this.BtnPInfo.Text = "PATIENT INFORMATION";
            this.BtnPInfo.UseVisualStyleBackColor = true;
            this.BtnPInfo.Click += new System.EventHandler(this.BtnPInfo_Click);
            // 
            // BtnCheckup
            // 
            this.BtnCheckup.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCheckup.Location = new System.Drawing.Point(512, 221);
            this.BtnCheckup.Name = "BtnCheckup";
            this.BtnCheckup.Size = new System.Drawing.Size(160, 85);
            this.BtnCheckup.TabIndex = 14;
            this.BtnCheckup.Text = "CHECK-UP RECORDS";
            this.BtnCheckup.UseVisualStyleBackColor = true;
            this.BtnCheckup.Click += new System.EventHandler(this.BtnCheckup_Click);
            // 
            // DashBoardcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnCheckup);
            this.Controls.Add(this.BtnPInfo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnAdd);
            this.Name = "DashBoardcs";
            this.Text = "DashBoardcs";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMuntinlupa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pbMuntinlupa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnPInfo;
        private System.Windows.Forms.Button BtnCheckup;
    }
}