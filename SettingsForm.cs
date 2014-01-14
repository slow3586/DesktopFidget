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

        private bool CheckstateChangedByProgram1 = false;
        private bool CheckstateChangedByProgram2 = false;
        public SettingsForm()
        {
            InitializeComponent();
            MovingDistanceTB.Value = Var.MovementDistance;
            MovingFrequencyTB.Value = Var.MovementFrequency;
            SizeLevelTB.Value = Var.SizeLevel;

            if (Var.FollowTheMouse)
            {
                if (!Var.IniFileWasLoaded) { CheckstateChangedByProgram2 = true; }
                FollowTheMouseCB.CheckState = CheckState.Checked;
            }

            if (MovingDistanceTB.Value == 0)
            {
                groupBox2.Visible = false;
            }
            else { groupBox2.Visible = true; };
            if (Var.ClickThroughWindow == true)
            {
                if (!Var.IniFileWasLoaded) { CheckstateChangedByProgram1 = true; }
                checkBox1.CheckState = CheckState.Checked;
            }
            Var.IniFileWasLoaded = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckstateChangedByProgram1 == false)
            {
                if (!Var.IniFileWasLoaded) { Var.ClickThroughWindow = !Var.ClickThroughWindow; }
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
            else
                CheckstateChangedByProgram1 = false;
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
            if (CheckstateChangedByProgram2 == false)
            {
                if (!Var.IniFileWasLoaded) { Var.FollowTheMouse = !Var.FollowTheMouse; }
            }
            else
                CheckstateChangedByProgram2 = false;
            groupBox1.Visible = Var.FollowTheMouse ? false : true;
            //groupBox2.Visible = Variables.FollowTheMouse ? false : true;
            if (Var.FollowTheMouse) { MovingDistanceTB.Value = 0; MovingFrequencyTB.Value = 0; }
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            IniFile.OpenSettingsFile(false,true);
        }
    }
}
