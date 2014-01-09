﻿using System;
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

    public partial class Form2 : Form
    {
        [DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
        [DllImport("user32.dll")]
        static extern bool DrawMenuBar(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        private readonly string WINDOW_NAME = "Desktop Fidget";

        private bool CheckstateChangedByProgram=false;
        public Form2()
        {
            InitializeComponent();
            MovingDistanceTB.Value = Variables.MovementDistance;
           // MovingDistanceLabel.Text = MovingDistanceTB.Value.ToString();
            MovingFrequencyTB.Value = Variables.MovementFrequency;
            //MovingFrequencyLabel.Text = MovingFrequencyTB.Value.ToString();
            AlphaLevelTB.Value = Variables.AlphaLevel;
            SizeLevelTB.Value = Variables.SizeLevel;
            if (Variables.ClickThroughWindow == true)
            {
                CheckstateChangedByProgram = true;
                checkBox1.CheckState = CheckState.Checked;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckstateChangedByProgram == false)
            {
                Variables.ClickThroughWindow = !Variables.ClickThroughWindow;
                IntPtr _window = FindWindowByCaption(IntPtr.Zero, WINDOW_NAME);
                if (Variables.ClickThroughWindow)
                {
                   int _initialStyle = GetWindowLong(_window, -20);
                   SetWindowLong(_window, -20, _initialStyle | 0x80000 | 0x20);
                }
                else
                {
                    SetWindowLong(_window, -20, 0x80000);
                }
            }
            else
                CheckstateChangedByProgram = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void MovingDistanceTB_ValueChanged(object sender, EventArgs e)
        {
            MovingDistanceLabel.Text = MovingDistanceTB.Value.ToString();
            Variables.MovementDistance = MovingDistanceTB.Value;
        }

        private void MovingFrequencyTB_ValueChanged(object sender, EventArgs e)
        {
            MovingFrequencyLabel.Text = MovingFrequencyTB.Value.ToString();
            Variables.MovementFrequency = MovingFrequencyTB.Value;
            Random _rnd = new Random();
            Variables.SecondsToNextMovement = Convert.ToInt32(_rnd.Next(50 - Variables.MovementFrequency, (50 - Variables.MovementFrequency) * 2));
            SecondsToNextMovementLabel.Text = '(' + Variables.SecondsSpentBeforeNextMovement.ToString() + '/' + Variables.SecondsToNextMovement.ToString() + ')';
        }

        private void AlphaLevelTB_ValueChanged(object sender, EventArgs e)
        {
            AlphaLevelLabel.Text = AlphaLevelTB.Value.ToString();
            Variables.AlphaLevel = AlphaLevelTB.Value;
        }

        private void SizeLevelTB_ValueChanged(object sender, EventArgs e)
        {
            SizeLevelLabel.Text = SizeLevelTB.Value.ToString();
            Variables.SizeLevel = SizeLevelTB.Value;
        }
    }
}