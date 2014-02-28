namespace DesktopFidget
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MovingDistanceLabel = new System.Windows.Forms.Label();
            this.MovingDistanceTB = new System.Windows.Forms.TrackBar();
            this.ClickThroughCB = new System.Windows.Forms.CheckBox();
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
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.TurnTowardsCenterCB = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.AnimationsFrequencyLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.AnimationsFrequencyTB = new System.Windows.Forms.TrackBar();
            this.ShadowCB = new System.Windows.Forms.CheckBox();
            this.ExportFilesButton = new System.Windows.Forms.Button();
            this.ModsLabel = new System.Windows.Forms.Label();
            this.DialogsCB = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.DialogsFrequencyL = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.DialogsFrequencyTB = new System.Windows.Forms.TrackBar();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MovingDistanceTB)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MovingFrequencyTB)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SizeLevelTB)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimationsFrequencyTB)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DialogsFrequencyTB)).BeginInit();
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
            // ClickThroughCB
            // 
            this.ClickThroughCB.AutoSize = true;
            this.ClickThroughCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClickThroughCB.Location = new System.Drawing.Point(12, 213);
            this.ClickThroughCB.Name = "ClickThroughCB";
            this.ClickThroughCB.Size = new System.Drawing.Size(97, 19);
            this.ClickThroughCB.TabIndex = 2;
            this.ClickThroughCB.Text = "Click through";
            this.ClickThroughCB.UseVisualStyleBackColor = true;
            this.ClickThroughCB.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
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
            this.groupBox4.Location = new System.Drawing.Point(12, 313);
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
            this.label7.Size = new System.Drawing.Size(137, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Scales the image up/down.";
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
            this.VersionLabel.Location = new System.Drawing.Point(515, 219);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(13, 13);
            this.VersionLabel.TabIndex = 7;
            this.VersionLabel.Text = "v";
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.Location = new System.Drawing.Point(472, 188);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.SaveSettingsButton.TabIndex = 8;
            this.SaveSettingsButton.Text = "Save INI";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            this.SaveSettingsButton.Click += new System.EventHandler(this.SaveSettingsButton_Click);
            // 
            // TurnTowardsCenterCB
            // 
            this.TurnTowardsCenterCB.AutoSize = true;
            this.TurnTowardsCenterCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TurnTowardsCenterCB.Location = new System.Drawing.Point(12, 238);
            this.TurnTowardsCenterCB.Name = "TurnTowardsCenterCB";
            this.TurnTowardsCenterCB.Size = new System.Drawing.Size(335, 19);
            this.TurnTowardsCenterCB.TabIndex = 9;
            this.TurnTowardsCenterCB.Text = "Turn towards center of screen (after random movements)";
            this.TurnTowardsCenterCB.UseVisualStyleBackColor = true;
            this.TurnTowardsCenterCB.CheckedChanged += new System.EventHandler(this.TurnTowardsCenterCB_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.AnimationsFrequencyLabel);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.AnimationsFrequencyTB);
            this.groupBox3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.groupBox3.Location = new System.Drawing.Point(12, 401);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(535, 82);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Animations frequency";
            // 
            // AnimationsFrequencyLabel
            // 
            this.AnimationsFrequencyLabel.AutoSize = true;
            this.AnimationsFrequencyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AnimationsFrequencyLabel.Location = new System.Drawing.Point(490, 19);
            this.AnimationsFrequencyLabel.Name = "AnimationsFrequencyLabel";
            this.AnimationsFrequencyLabel.Size = new System.Drawing.Size(15, 16);
            this.AnimationsFrequencyLabel.TabIndex = 3;
            this.AnimationsFrequencyLabel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 59);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(282, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Defines time between head animations. Less = more often.";
            // 
            // AnimationsFrequencyTB
            // 
            this.AnimationsFrequencyTB.LargeChange = 25;
            this.AnimationsFrequencyTB.Location = new System.Drawing.Point(7, 19);
            this.AnimationsFrequencyTB.Maximum = 255;
            this.AnimationsFrequencyTB.Minimum = 50;
            this.AnimationsFrequencyTB.Name = "AnimationsFrequencyTB";
            this.AnimationsFrequencyTB.Size = new System.Drawing.Size(465, 45);
            this.AnimationsFrequencyTB.SmallChange = 5;
            this.AnimationsFrequencyTB.TabIndex = 0;
            this.AnimationsFrequencyTB.TickFrequency = 5;
            this.AnimationsFrequencyTB.Value = 50;
            this.AnimationsFrequencyTB.ValueChanged += new System.EventHandler(this.AnimationsFrequencyTB_ValueChanged);
            // 
            // ShadowCB
            // 
            this.ShadowCB.AutoSize = true;
            this.ShadowCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ShadowCB.Location = new System.Drawing.Point(12, 263);
            this.ShadowCB.Name = "ShadowCB";
            this.ShadowCB.Size = new System.Drawing.Size(71, 19);
            this.ShadowCB.TabIndex = 10;
            this.ShadowCB.Text = "Shadow";
            this.ShadowCB.UseVisualStyleBackColor = true;
            this.ShadowCB.CheckedChanged += new System.EventHandler(this.ShadowCB_CheckedChanged);
            // 
            // ExportFilesButton
            // 
            this.ExportFilesButton.Location = new System.Drawing.Point(472, 238);
            this.ExportFilesButton.Name = "ExportFilesButton";
            this.ExportFilesButton.Size = new System.Drawing.Size(75, 23);
            this.ExportFilesButton.TabIndex = 11;
            this.ExportFilesButton.Text = "Exp. Dialogs";
            this.ExportFilesButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ExportFilesButton.UseVisualStyleBackColor = true;
            this.ExportFilesButton.Click += new System.EventHandler(this.ExportFilesButton_Click);
            // 
            // ModsLabel
            // 
            this.ModsLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ModsLabel.AutoSize = true;
            this.ModsLabel.Location = new System.Drawing.Point(393, 327);
            this.ModsLabel.Name = "ModsLabel";
            this.ModsLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ModsLabel.Size = new System.Drawing.Size(10, 13);
            this.ModsLabel.TabIndex = 12;
            this.ModsLabel.Text = "-";
            this.ModsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // DialogsCB
            // 
            this.DialogsCB.AutoSize = true;
            this.DialogsCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DialogsCB.Location = new System.Drawing.Point(12, 288);
            this.DialogsCB.Name = "DialogsCB";
            this.DialogsCB.Size = new System.Drawing.Size(68, 19);
            this.DialogsCB.TabIndex = 13;
            this.DialogsCB.Text = "Dialogs";
            this.DialogsCB.UseVisualStyleBackColor = true;
            this.DialogsCB.CheckedChanged += new System.EventHandler(this.DialogsCB_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.DialogsFrequencyL);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.DialogsFrequencyTB);
            this.groupBox5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.groupBox5.Location = new System.Drawing.Point(12, 489);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(535, 82);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Dialogs frequency";
            // 
            // DialogsFrequencyL
            // 
            this.DialogsFrequencyL.AutoSize = true;
            this.DialogsFrequencyL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DialogsFrequencyL.Location = new System.Drawing.Point(490, 19);
            this.DialogsFrequencyL.Name = "DialogsFrequencyL";
            this.DialogsFrequencyL.Size = new System.Drawing.Size(15, 16);
            this.DialogsFrequencyL.TabIndex = 3;
            this.DialogsFrequencyL.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 59);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(442, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Defines time between dialogs. Default is 60-75 seconds. Click dialogs will be lef" +
    "t unchanged.";
            // 
            // DialogsFrequencyTB
            // 
            this.DialogsFrequencyTB.LargeChange = 25;
            this.DialogsFrequencyTB.Location = new System.Drawing.Point(7, 19);
            this.DialogsFrequencyTB.Maximum = 230;
            this.DialogsFrequencyTB.Minimum = 25;
            this.DialogsFrequencyTB.Name = "DialogsFrequencyTB";
            this.DialogsFrequencyTB.Size = new System.Drawing.Size(465, 45);
            this.DialogsFrequencyTB.SmallChange = 5;
            this.DialogsFrequencyTB.TabIndex = 0;
            this.DialogsFrequencyTB.TickFrequency = 5;
            this.DialogsFrequencyTB.Value = 50;
            this.DialogsFrequencyTB.ValueChanged += new System.EventHandler(this.DialogsFrequencyTB_ValueChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(555, 583);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.DialogsCB);
            this.Controls.Add(this.ModsLabel);
            this.Controls.Add(this.ExportFilesButton);
            this.Controls.Add(this.ShadowCB);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.TurnTowardsCenterCB);
            this.Controls.Add(this.SaveSettingsButton);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.FollowTheMouseCB);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ClickThroughCB);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
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
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimationsFrequencyTB)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DialogsFrequencyTB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label MovingDistanceLabel;
        private System.Windows.Forms.TrackBar MovingDistanceTB;
        private System.Windows.Forms.CheckBox ClickThroughCB;
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
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.CheckBox TurnTowardsCenterCB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label AnimationsFrequencyLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar AnimationsFrequencyTB;
        private System.Windows.Forms.CheckBox ShadowCB;
        private System.Windows.Forms.Button ExportFilesButton;
        private System.Windows.Forms.Label ModsLabel;
        private System.Windows.Forms.CheckBox DialogsCB;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label DialogsFrequencyL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar DialogsFrequencyTB;
    }
}