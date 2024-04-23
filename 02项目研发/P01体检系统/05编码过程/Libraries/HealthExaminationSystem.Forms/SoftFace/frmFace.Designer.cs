namespace Sw.Hospital.HealthExaminationSystem.SoftFace
{
    partial class frmFace
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
            this.components = new System.ComponentModel.Container();
            this.irVideoSource = new AForge.Controls.VideoSourcePlayer();
            this.rgbVideoSource = new AForge.Controls.VideoSourcePlayer();
            this.label1 = new System.Windows.Forms.Label();
            this.txtThreshold = new System.Windows.Forms.TextBox();
            this.chooseMultiImgBtn = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.TextBox();
            this.imageList = new System.Windows.Forms.ListView();
            this.imageLists = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // irVideoSource
            // 
            this.irVideoSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.irVideoSource.BackColor = System.Drawing.SystemColors.Control;
            this.irVideoSource.Location = new System.Drawing.Point(365, 3);
            this.irVideoSource.Name = "irVideoSource";
            this.irVideoSource.Size = new System.Drawing.Size(140, 107);
            this.irVideoSource.TabIndex = 39;
            this.irVideoSource.Text = "videoSource";
            this.irVideoSource.VideoSource = null;
            this.irVideoSource.Visible = false;
            this.irVideoSource.PlayingFinished += new AForge.Video.PlayingFinishedEventHandler(this.videoSource_PlayingFinished);
            this.irVideoSource.Paint += new System.Windows.Forms.PaintEventHandler(this.irVideoSource_Paint);
            // 
            // rgbVideoSource
            // 
            this.rgbVideoSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rgbVideoSource.Location = new System.Drawing.Point(12, 3);
            this.rgbVideoSource.Name = "rgbVideoSource";
            this.rgbVideoSource.Size = new System.Drawing.Size(494, 362);
            this.rgbVideoSource.TabIndex = 40;
            this.rgbVideoSource.Text = "videoSource";
            this.rgbVideoSource.VideoSource = null;
            this.rgbVideoSource.PlayingFinished += new AForge.Video.PlayingFinishedEventHandler(this.videoSource_PlayingFinished);
            this.rgbVideoSource.Paint += new System.Windows.Forms.PaintEventHandler(this.videoSource_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(318, 393);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 44;
            this.label1.Text = "阈值：";
            // 
            // txtThreshold
            // 
            this.txtThreshold.BackColor = System.Drawing.SystemColors.Window;
            this.txtThreshold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtThreshold.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtThreshold.Location = new System.Drawing.Point(365, 387);
            this.txtThreshold.Name = "txtThreshold";
            this.txtThreshold.Size = new System.Drawing.Size(60, 25);
            this.txtThreshold.TabIndex = 43;
            this.txtThreshold.Text = "0.8";
            // 
            // chooseMultiImgBtn
            // 
            this.chooseMultiImgBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chooseMultiImgBtn.Location = new System.Drawing.Point(223, 387);
            this.chooseMultiImgBtn.Name = "chooseMultiImgBtn";
            this.chooseMultiImgBtn.Size = new System.Drawing.Size(72, 26);
            this.chooseMultiImgBtn.TabIndex = 45;
            this.chooseMultiImgBtn.Text = "注册人脸";
            this.chooseMultiImgBtn.UseVisualStyleBackColor = true;
            this.chooseMultiImgBtn.Click += new System.EventHandler(this.chooseMultiImgBtn_Click);
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.Color.White;
            this.logBox.Location = new System.Drawing.Point(2, 466);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(556, 10);
            this.logBox.TabIndex = 46;
            this.logBox.WordWrap = false;
            // 
            // imageList
            // 
            this.imageList.HideSelection = false;
            this.imageList.Location = new System.Drawing.Point(451, 379);
            this.imageList.Name = "imageList";
            this.imageList.Size = new System.Drawing.Size(55, 33);
            this.imageList.TabIndex = 47;
            this.imageList.UseCompatibleStateImageBehavior = false;
            // 
            // imageLists
            // 
            this.imageLists.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageLists.ImageSize = new System.Drawing.Size(80, 80);
            this.imageLists.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 371);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 48;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // frmFace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 426);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.imageList);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.chooseMultiImgBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtThreshold);
            this.Controls.Add(this.irVideoSource);
            this.Controls.Add(this.rgbVideoSource);
            this.Name = "frmFace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "人脸识别";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFace_FormClosed);
            this.Load += new System.EventHandler(this.frmFace_Load);
            this.Shown += new System.EventHandler(this.frmFace_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AForge.Controls.VideoSourcePlayer irVideoSource;
        private AForge.Controls.VideoSourcePlayer rgbVideoSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtThreshold;
        private System.Windows.Forms.Button chooseMultiImgBtn;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.ListView imageList;
        private System.Windows.Forms.ImageList imageLists;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}