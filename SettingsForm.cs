using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace DesktopFidget
{

    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            VersionLabel.Text = 'v' + Convert.ToString(Var.ProgramVersion);
            MovingDistanceTB.Value = Var.MovementDistance;
            MovingFrequencyTB.Value = Var.MovementFrequency;
            SizeLevelTB.Value = Var.SizeLevel;
            if (Var.AnimationsFrequency > 255)
                AnimationsFrequencyTB.Value = 255;
            else
                AnimationsFrequencyTB.Value = Var.AnimationsFrequency;

            if (Var.FollowTheMouse)
                FollowTheMouseCB.CheckState = CheckState.Checked;

            if (MovingDistanceTB.Value == 0)
                groupBox2.Visible = false;
            else
                groupBox2.Visible = true;

            if (Var.ClickThroughWindow == true)
                ClickThroughCB.CheckState = CheckState.Checked;

            if (Var.TurnTowardsCenter == true)
                TurnTowardsCenterCB.CheckState = CheckState.Checked;

            if (Var.Shadow == true)
                ShadowCB.CheckState = CheckState.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
                Var.ClickThroughWindow = ClickThroughCB.Checked;
                IntPtr _window = NativeMethods.FindWindowByCaption(IntPtr.Zero, Var.WINDOW_NAME);
                if (Var.ClickThroughWindow)
                {
                    int _initialStyle = NativeMethods.GetWindowLong(_window, -20);
                   NativeMethods.SetWindowLong(_window, -20, _initialStyle | 0x80000 | 0x20);
                }
                else
                {
                    NativeMethods.SetWindowLong(_window, -20, 0x80000);
                }
        }

        private void MovingDistanceTB_ValueChanged(object sender, EventArgs e)
        {
            MovingDistanceLabel.Text = MovingDistanceTB.Value.ToString();
            Var.MovementDistance = MovingDistanceTB.Value;
            if (MovingDistanceTB.Value == 0)
            {
                groupBox2.Visible = false;
            }
            else { groupBox2.Visible = true; };
            FollowTheMouseCB.Visible = Var.MovementDistance>0 ? false : true;
        }

        private void MovingFrequencyTB_ValueChanged(object sender, EventArgs e)
        {
            MovingFrequencyLabel.Text = MovingFrequencyTB.Value.ToString();
            Var.MovementFrequency = MovingFrequencyTB.Value;
            Random _rnd = new Random();
            Var.SecondsToNextMovement = Convert.ToInt32(_rnd.Next(50 - Var.MovementFrequency, (50 - Var.MovementFrequency) * 2));
            SecondsToNextMovementLabel.Text = '(' + Var.SecondsSpentBeforeNextMovement.ToString() + '/' + Var.SecondsToNextMovement.ToString() + ')';
        }

        private void SizeLevelTB_ValueChanged(object sender, EventArgs e)
        {
            SizeLevelLabel.Text = Convert.ToString(SizeLevelTB.Value*25) + '%';
            Var.SizeLevel = SizeLevelTB.Value;
        }

        private void FollowTheMouseCB_CheckedChanged(object sender, EventArgs e)
        {
            Var.FollowTheMouse = FollowTheMouseCB.Checked;
            groupBox1.Visible = Var.FollowTheMouse ? false : true;
            if (Var.FollowTheMouse) { MovingDistanceTB.Value = 0; MovingFrequencyTB.Value = 0; }
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            IniFile.OpenSettingsFile(false,true);
        }

        private void TurnTowardsCenterCB_CheckedChanged(object sender, EventArgs e)
        {
            Var.TurnTowardsCenter = TurnTowardsCenterCB.Checked;   
        }

        private void AnimationsFrequencyTB_ValueChanged(object sender, EventArgs e)
        {
            AnimationsFrequencyLabel.Text = Convert.ToString(AnimationsFrequencyTB.Value) + '%';
            Var.AnimationsFrequency = AnimationsFrequencyTB.Value;
            if (AnimationsFrequencyTB.Value == 255)
            {
                AnimationsFrequencyLabel.Text = "NVR!";
                Var.AnimationsFrequency = 100000;
            }
        }

        private void ShadowCB_CheckedChanged(object sender, EventArgs e)
        {
            Var.Shadow = ShadowCB.Checked;  
        }
    }
}
