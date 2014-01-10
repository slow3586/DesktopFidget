namespace DesktopFidget
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MovingDistanceLabel = new System.Windows.Forms.Label();
            this.MovingDistanceTB = new System.Windows.Forms.TrackBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SecondsToNextMovementLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MovingFrequencyLabel = new System.Windows.Forms.Label();
            this.MovingFrequencyTB = new System.Windows.Forms.TrackBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.SizeLevelLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SizeLevelTB = new System.Windows.Forms.TrackBar();
            this.FollowTheMouseCB = new System.Windows.Forms.CheckBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MovingDistanceTB)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MovingFrequencyTB)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SizeLevelTB)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.MovingDistanceLabel);
            this.groupBox1.Controls.Add(this.MovingDistanceTB);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(535, 82);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Movement distance";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 59);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(473, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "If not set to 0, Fidget will move around the desktop randomly.  (Distance = rnd (" +
    "-10*value;10*value))";
            // 
            // MovingDistanceLabel
            // 
            this.MovingDistanceLabel.AutoSize = true;
            this.MovingDistanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MovingDistanceLabel.Location = new System.Drawing.Point(501, 20);
            this.MovingDistanceLabel.Name = "MovingDistanceLabel";
            this.MovingDistanceLabel.Size = new System.Drawing.Size(15, 16);
            this.MovingDistanceLabel.TabIndex = 1;
            this.MovingDistanceLabel.Text = "0";
            // 
            // MovingDistanceTB
            // 
            this.MovingDistanceTB.Location = new System.Drawing.Point(7, 20);
            this.MovingDistanceTB.Maximum = 100;
            this.MovingDistanceTB.Name = "MovingDistanceTB";
            this.MovingDistanceTB.Size = new System.Drawing.Size(465, 45);
            this.MovingDistanceTB.TabIndex = 0;
            this.MovingDistanceTB.ValueChanged += new System.EventHandler(this.MovingDistanceTB_ValueChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(12, 213);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(97, 19);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Click through";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SecondsToNextMovementLabel);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.MovingFrequencyLabel);
            this.groupBox2.Controls.Add(this.MovingFrequencyTB);
            this.groupBox2.Location = new System.Drawing.Point(12, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(535, 82);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Movement frequency";
            // 
            // SecondsToNextMovementLabel
            // 
            this.SecondsToNextMovementLabel.AutoSize = true;
            this.SecondsToNextMovementLabel.Location = new System.Drawing.Point(492, 59);
            this.SecondsToNextMovementLabel.Name = "SecondsToNextMovementLabel";
            this.SecondsToNextMovementLabel.Size = new System.Drawing.Size(13, 13);
            this.SecondsToNextMovementLabel.TabIndex = 3;
            this.SecondsToNextMovementLabel.Text = "()";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 59);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(447, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Defines how often Fidget will move. (Seconds before next move = rnd (50-value);2*" +
    "(50-value))";
            // 
            // MovingFrequencyLabel
            // 
            this.MovingFrequencyLabel.AutoSize = true;
            this.MovingFrequencyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MovingFrequencyLabel.Location = new System.Drawing.Point(501, 20);
            this.MovingFrequencyLabel.Name = "MovingFrequencyLabel";
            this.MovingFrequencyLabel.Size = new System.Drawing.Size(15, 16);
            this.MovingFrequencyLabel.TabIndex = 1;
            this.MovingFrequencyLabel.Text = "0";
            // 
            // MovingFrequencyTB
            // 
            this.MovingFrequencyTB.Location = new System.Drawing.Point(6, 19);
            this.MovingFrequencyTB.Maximum = 50;
            this.MovingFrequencyTB.Name = "MovingFrequencyTB";
            this.MovingFrequencyTB.Size = new System.Drawing.Size(465, 45);
            this.MovingFrequencyTB.TabIndex = 0;
            this.MovingFrequencyTB.ValueChanged += new System.EventHandler(this.MovingFrequencyTB_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.SizeLevelLabel);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.SizeLevelTB);
            this.groupBox4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.groupBox4.Location = new System.Drawing.Point(12, 238);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(535, 82);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Size";
            // 
            // SizeLevelLabel
            // 
            this.SizeLevelLabel.AutoSize = true;
            this.SizeLevelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SizeLevelLabel.Location = new System.Drawing.Point(490, 19);
            this.SizeLevelLabel.Name = "SizeLevelLabel";
            this.SizeLevelLabel.Size = new System.Drawing.Size(15, 16);
            this.SizeLevelLabel.TabIndex = 3;
            this.SizeLevelLabel.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 59);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(272, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Scales the image up/down. Strange things may happen.";
            // 
            // SizeLevelTB
            // 
            this.SizeLevelTB.Location = new System.Drawing.Point(7, 19);
            this.SizeLevelTB.Maximum = 5;
            this.SizeLevelTB.Minimum = 1;
            this.SizeLevelTB.Name = "SizeLevelTB";
            this.SizeLevelTB.Size = new System.Drawing.Size(465, 45);
            this.SizeLevelTB.TabIndex = 0;
            this.SizeLevelTB.Value = 1;
            this.SizeLevelTB.ValueChanged += new System.EventHandler(this.SizeLevelTB_ValueChanged);
            // 
            // FollowTheMouseCB
            // 
            this.FollowTheMouseCB.AutoSize = true;
            this.FollowTheMouseCB.Cursor = System.Windows.Forms.Cursors.Default;
            this.FollowTheMouseCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FollowTheMouseCB.Location = new System.Drawing.Point(12, 188);
            this.FollowTheMouseCB.Name = "FollowTheMouseCB";
            this.FollowTheMouseCB.Size = new System.Drawing.Size(123, 19);
            this.FollowTheMouseCB.TabIndex = 6;
            this.FollowTheMouseCB.Text = "Follow the mouse";
            this.FollowTheMouseCB.UseVisualStyleBackColor = true;
            this.FollowTheMouseCB.CheckedChanged += new System.EventHandler(this.FollowTheMouseCB_CheckedChanged);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(504, 219);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(37, 13);
            this.VersionLabel.TabIndex = 7;
            this.VersionLabel.Text = "v1312";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(556, 332);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.FollowTheMouseCB);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DFidget Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MovingDistanceTB)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MovingFrequencyTB)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SizeLevelTB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label MovingDistanceLabel;
        private System.Windows.Forms.TrackBar MovingDistanceTB;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label MovingFrequencyLabel;
        private System.Windows.Forms.TrackBar MovingFrequencyTB;
        private System.Windows.Forms.Label SecondsToNextMovementLabel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar SizeLevelTB;
        private System.Windows.Forms.Label SizeLevelLabel;
        private System.Windows.Forms.CheckBox FollowTheMouseCB;
        private System.Windows.Forms.Label VersionLabel;
    }
}